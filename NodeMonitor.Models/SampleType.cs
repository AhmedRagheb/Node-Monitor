using System;

namespace NodeMonitor.Models
{
    [Serializable]
    public class SampleType
    {
        public SampleType(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
