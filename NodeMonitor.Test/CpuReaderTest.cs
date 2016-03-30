using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeMonitor.SamplesReaders;
using System;
using NodeMonitor.Models;

namespace NodeMonitor.Test
{
    

    [TestClass]
    public class CpuReaderTest
    {

        private ISampleReader _cpuReader;

        [TestInitialize]
        public void Instalize()
        {
            StructureMapConfigure.Configure();
            var container = StructureMapConfigure.Container;
            _cpuReader = container.GetInstance<ISampleReader>("CPU");
        }

        [TestMethod]
        public void TestSampleTypeIdHasValueNotNull()
        {
            Assert.IsNotNull(_cpuReader.SampleTypeId);
        }
        
        [TestMethod]
        public void TestDescriptionTextValueNotEmptyOrNull()
        {
            Assert.IsFalse(string.IsNullOrEmpty(_cpuReader.Description));
        }
        
        [TestMethod]
        public void TestCpuUsageLoadValueNotZero()
        {
            SampleDataModel value = _cpuReader.ReadSample();
            Assert.IsTrue(value.Value > 0);
        }
        
        [TestMethod]
        public void TestCpuUsageSampleTimeStampIsSaved()
        {
            SampleDataModel value = _cpuReader.ReadSample();
            Assert.IsTrue(value.TimeStamp != DateTime.MinValue && value.TimeStamp != null);
        }
       

    }
}
