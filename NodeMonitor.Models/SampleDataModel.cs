using System;

namespace NodeMonitor.Models
{
    [Serializable]
    public class SampleDataModel
    {
        public SampleDataModel(int sampleTypeId)
        {
            this.SampleTypeId = sampleTypeId;
        }

        public double Value { get; set; }

        public string StringValue { get; set; }

        public int SampleTypeId { get; private set; }

        public DateTime TimeStamp { get; set; }

        // the only use of this prop here to easy display in client side.
        public string Description { get; set; }
    }
}


