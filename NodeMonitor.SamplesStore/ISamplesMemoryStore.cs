using System.Collections.Concurrent;
using System.Collections.Generic;
using NodeMonitor.Models;

namespace NodeMonitor.SamplesStore
{
    public interface ISamplesMemoryStore
    {
        List<SampleDataModel> GetLastThirtySeconds(params int[] sampleTypeIds);
        void Store(List<SampleDataModel> samples);
        void DiscardOldSamples();
        ConcurrentDictionary<int, List<SampleDataModel>> GetStoredSamplesCache();
    }
}