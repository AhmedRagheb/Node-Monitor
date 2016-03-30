using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NodeMonitor.Models;

namespace NodeMonitor.SamplesStore
{
    public class SamplesMemoryStore : ISamplesMemoryStore
    {
        private readonly ConcurrentDictionary<int, List<SampleDataModel>> _samplesCache = new ConcurrentDictionary<int, List<SampleDataModel>>();

        public ConcurrentDictionary<int, List<SampleDataModel>> GetStoredSamplesCache()
        {
            return _samplesCache;
        }

        /// <summary>
        /// Get last 30 seconds samples data from memory of selected samples types. 
        /// Default is all types retrieved
        /// </summary>
        /// <param name="sampleTypeIds">id of sample type (ram, memory)</param>
        /// <returns></returns>
        public List<SampleDataModel> GetLastThirtySeconds(params int[] sampleTypeIds)
        {
            var result = new List<SampleDataModel>();
            if (sampleTypeIds != null)
            {
                if (sampleTypeIds.Count() > 0)
                {
                    foreach (var sampleTypeId in sampleTypeIds)
                    {
                        List<SampleDataModel> list;
                        _samplesCache.TryGetValue(sampleTypeId, out list);

                        if (list != null)
                            result.AddRange(list);
                    }
                }
                else
                {
                    result = _samplesCache.SelectMany(x => x.Value).ToList();
                }
            }

            var now = DateTime.UtcNow;
            var lastThirySecondsList = result.Where(x => (now - x.TimeStamp).TotalSeconds <= 30).ToList();

            return lastThirySecondsList;
        }

        /// <summary>
        /// // add new samples in memory with its sample type id
        /// </summary>
        /// <param name="samples">new samples</param>
        public void Store(List<SampleDataModel> samples)
        {
            var samplesGroups = samples
                .GroupBy(x => x.SampleTypeId,
                    (key, g) => new
                    {
                        SampleTypeId = key,
                        data = g
                    })
                .Select(x => new
                {
                    x.SampleTypeId,
                    x.data
                });

            foreach (var samplesGroup in samplesGroups)
            {
                var samplesData = samplesGroup.data.ToList();
                var sampleTypeId = samplesGroup.SampleTypeId;
                bool isTypeInStore = _samplesCache.ContainsKey(sampleTypeId);

                if (isTypeInStore)
                {
                    List<SampleDataModel> list;
                    _samplesCache.TryGetValue(sampleTypeId, out list);
                    if (list != null)
                    {
                        list.AddRange(samplesData);
                    }
                    else
                    {
                        _samplesCache.TryAdd(samplesGroup.SampleTypeId, samplesData);
                    }
                }
                else
                {
                    _samplesCache.TryAdd(samplesGroup.SampleTypeId, samplesData);
                }
            }
        }

        /// <summary>
        /// discard any samples older than 60 seconds from memory
        /// </summary>
        public void DiscardOldSamples()
        {
            var now = DateTime.UtcNow;
            foreach (var samplesGroup in _samplesCache)
            {
                var cachedSamplePerType = samplesGroup.Value;
                if (cachedSamplePerType.Count > 0)
                {
                    var oldSamples = cachedSamplePerType.Where(x => (now - x.TimeStamp).TotalSeconds >= 60).ToList();
                    foreach (var oldSample in oldSamples)
                    {
                        cachedSamplePerType.Remove(oldSample);
                    }
                }
            }


        }

    }
}
