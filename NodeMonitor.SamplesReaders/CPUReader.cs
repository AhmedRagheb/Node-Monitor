using System;
using System.Diagnostics;
using System.Linq;
using NodeMonitor.Models;

namespace NodeMonitor.SamplesReaders
{
    public class CpuReader : ISampleReader
    {
        public SampleDataModel ReadSample()
        {
            var cpuUsage = GetCpuUsage();

            var model = new SampleDataModel(SampleTypeId)
            {
                Value = cpuUsage,
                StringValue = string.Format("{0:0.00} %", cpuUsage),
                TimeStamp = DateTime.UtcNow,
                Description = this.Description
            };

            return model;
        }

        public int SampleTypeId
        {
            get { return 1; }
        } 
        
        public string Description 
        {
            get { return "CPU Load"; }
        }

        private double GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };
            
            // CPU Load
            cpuCounter.NextValue(); 
            // first time always 0, should sleep few ms.
            System.Threading.Thread.Sleep(100);

            var cpuUsage = cpuCounter.NextValue();
            return cpuUsage;
        }

    }
}
