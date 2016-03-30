using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;
using NodeMonitor.Models;
using NodeMonitor.SamplesReaders;
using NodeMonitor.SamplesStore;

namespace NodeMonitor.Server.SamplesCollector
{
    public class SamplesCollector : MarshalByRefObject, ISamplesCollector
    {
        private readonly ISamplesMemoryStore _samplesStore;
        private readonly SampleReadersUnitOfWork _unitOfWork;
        private Timer _timer;

        /// <summary>
        /// Get All instances that implement ISampleReader from IoC container, 
        /// so if you add new sample, just register it and it will works auto  
        /// </summary>
        public SamplesCollector()
        {
            _unitOfWork = new SampleReadersUnitOfWork
            {
                SampleReaders = StructureMapConfigure.Container.GetAllInstances<ISampleReader>().ToList()
            };

            _samplesStore = StructureMapConfigure.Container.GetInstance<ISamplesMemoryStore>();
        }

        /// <summary>
        /// Get last 30 seconds Samples data from memory cached of selected samples types 
        /// </summary>
        /// <param name="sampleTypeIds"></param>
        /// <returns></returns>
        public List<SampleDataModel> Fetch(params int[] sampleTypeIds)
        {
            var samples = _samplesStore.GetLastThirtySeconds(sampleTypeIds);
            return samples;
        }

        /// <summary>
        /// Instalize .Net Remoting object
        /// </summary>
        internal void Start()
        {
            Console.WriteLine("Monitor Server Started ...");

            IPGlobalProperties globalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] activeListeners = globalProperties.GetActiveTcpListeners();

            if (activeListeners.All(conn => conn.Port != TcpChannelConfiguration.Port))
            {
                var tcpChannel = new TcpChannel(TcpChannelConfiguration.Port);
                ChannelServices.RegisterChannel(tcpChannel, true);

                Type commonInterfaceType = TcpChannelConfiguration.CommonInterfaceType;

                if (commonInterfaceType != null)
                {
                    RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType,
                        TcpChannelConfiguration.ObjectUri, TcpChannelConfiguration.WellKnownObjectMode);
                    Console.WriteLine("SUCCESS: TCP Port: " + TcpChannelConfiguration.Port + " opened successfully");
                }
                else
                {
                    Console.WriteLine("ERROR: Problem happened when try to setup server");
                }

                InitTimer();
                ListenToStop();
            }
            else
            {
                Console.WriteLine("WARNING: Server with the same channel already registered.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Press Enter to Dispose current Timer thread that collect samples data and Exit application
        /// </summary>
        internal void ListenToStop()
        {
            Console.WriteLine("Press Enter to terminate process");
            while (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                // Dispose Timer thread that collect samples data
                _timer.Dispose();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// intialize timer fires every one second to collect samples data and store it in 
        /// memory cache then discard all samples data older than 60 seconds. 
        /// </summary>
        internal void InitTimer()
        {
            TimerCallback tmCallback = state =>
            {
                var samples = _unitOfWork.ReadSampleData();
                _samplesStore.Store(samples);
                _samplesStore.DiscardOldSamples();
            };

            _timer = new Timer(tmCallback, "ServerWorker", 0, 1000);
        }

        /// <summary>
        /// Get All samples data types (Ram, memory .. )
        /// </summary>
        /// <returns></returns>
        public List<SampleType> GetSampleTypes()
        {
            return _unitOfWork.SampleReaders
                              .Select(x => new SampleType(x.SampleTypeId, x.Description))
                              .ToList();
        }
    
    }
}
