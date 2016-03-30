
using System;
using System.Diagnostics;
using System.Linq;
using NodeMonitor.Server.SamplesCollector;

namespace NodeMonitor.Server
{
    internal class Program
    {
        
        private static void Main(string[] args)
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
                return;  

            StructureMapConfigure.Configure();  // Configure Structure Map ..
            var sampleCollector = SamplesCollectorFactory.Create();
            sampleCollector.Start(); // Server Start ..
        }


        


    }

}