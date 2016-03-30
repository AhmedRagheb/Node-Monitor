using System;
using System.Diagnostics;
using System.Linq;
using NodeMonitor.Models;

namespace NodeMonitor.SamplesReaders
{
    public class MemoryReader : ISampleReader
    {
        public SampleDataModel ReadSample()
        {
            var memoryUsed = GetMemoryUsage();

            var model = new SampleDataModel(SampleTypeId)
            {
                Value = memoryUsed,
                StringValue = string.Format("{0:0.00} Byte(s)", memoryUsed),
                TimeStamp = DateTime.UtcNow,
                Description = this.Description
            };

            return model;
        }

        public int SampleTypeId
        {
            get { return 2; }
        }

        public string Description
        {
            get { return "Memory Usage"; }
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/system.diagnostics.process.workingset64(v=vs.110).aspx
        /// </summary>
        /// <returns></returns>
        private float GetMemoryUsage()
        {
            var currentProcess = Process.GetProcesses();
            long totalBytesOfMemoryUsed = currentProcess.Sum(x => x.WorkingSet64);

            return totalBytesOfMemoryUsed;
        }

    }
}
