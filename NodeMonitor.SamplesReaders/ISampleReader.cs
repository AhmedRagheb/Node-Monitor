using NodeMonitor.Models;

namespace NodeMonitor.SamplesReaders
{
    public interface ISampleReader
    {
        SampleDataModel ReadSample();

        int SampleTypeId { get; }

        string Description { get; }
    }
}
