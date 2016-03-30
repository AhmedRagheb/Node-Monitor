using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeMonitor.SamplesReaders;
using System;
using NodeMonitor.Models;

namespace NodeMonitor.Test
{
    

    [TestClass]
    public class MemoryReaderTest
    {

        private ISampleReader _memoryReader;

        [TestInitialize]
        public void Instalize()
        {
            StructureMapConfigure.Configure();
            var container = StructureMapConfigure.Container;
            _memoryReader = container.GetInstance<ISampleReader>("Memory");
        }

        [TestMethod]
        public void TestSampleTypeIdHasValueNotNull()
        {
            Assert.IsNotNull(_memoryReader.SampleTypeId);
        }
        
        [TestMethod]
        public void TestDescriptionTextValueNotEmptyOrNull()
        {
            Assert.IsFalse(string.IsNullOrEmpty(_memoryReader.Description));
        }
        
        [TestMethod]
        public void TestMemoryUsageLoadValueNotZero()
        {
            SampleDataModel value = _memoryReader.ReadSample();
            Assert.IsTrue(value.Value > 0);
        }
        
        [TestMethod]
        public void TestMemoryUsageSampleTimeStampIsSaved()
        {
            SampleDataModel value = _memoryReader.ReadSample();
            Assert.IsTrue(value.TimeStamp != DateTime.MinValue && value.TimeStamp != null);
        }
       
    }
}
