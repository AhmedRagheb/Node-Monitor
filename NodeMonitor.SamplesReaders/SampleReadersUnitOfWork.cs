using System.Collections.Generic;
using System.Linq;
using NodeMonitor.Models;

namespace NodeMonitor.SamplesReaders
{
    public class SampleReadersUnitOfWork
    {

        public List<ISampleReader> SampleReaders
        {
            get;
            set;
        }

        public List<SampleDataModel> ReadSampleData()
        {
            return SampleReaders.Select(dataReader => dataReader.ReadSample()).ToList();
        }
    }
}
