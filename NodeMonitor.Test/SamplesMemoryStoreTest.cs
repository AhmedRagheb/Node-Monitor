using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodeMonitor.Models;
using NodeMonitor.SamplesStore;

namespace NodeMonitor.Test
{
    [TestClass]
    public class SamplesMemoryStoreTest
    {
        private ISamplesMemoryStore _store;

        [TestInitialize]
        public void Instalize()
        {
            StructureMapConfigure.Configure();
            var container = StructureMapConfigure.Container;
            _store = container.GetInstance<ISamplesMemoryStore>();
        }

        private List<SampleDataModel> LoadMockSampleData()
        {
            var mockList = new List<SampleDataModel>();
            var cpu1 = new SampleDataModel(1)
                           {
                               StringValue = "15.23 %",
                               Value = 15.23,
                               TimeStamp = DateTime.UtcNow.AddMinutes(-1)
                           };

            var cpu2 = new SampleDataModel(1)
                           {
                               StringValue = "35.98 %",
                               Value = 35.98,
                               TimeStamp = DateTime.UtcNow.AddSeconds(-20)
                           };

            var memeory1 = new SampleDataModel(2)
            {
                StringValue = "555998.235 Byte(s)",
                Value = 555998.235,
                TimeStamp = DateTime.UtcNow.AddSeconds(-10)
            };

            var memeory2 = new SampleDataModel(2)
            {
                StringValue = "23598.2489 Byte(s)",
                Value = 23598.2489,
                TimeStamp = DateTime.UtcNow
            };

            mockList.Add(cpu1);
            mockList.Add(cpu2);
            mockList.Add(memeory1);
            mockList.Add(memeory2);

            return mockList;
        }

        // Test Store Samples Data In Memory
        [TestMethod]
        public void TestStoreSamplesDataInMemory()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);

            Assert.AreEqual(_store.GetStoredSamplesCache().Count, 2);
        }

        // Test Get All Last Thirty Seconds Samples Data From Memory
        [TestMethod]
        public void TestGetAllLastThirtySecondsSamplesDataFromMemory()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);

            var samples = _store.GetLastThirtySeconds();
            Assert.AreEqual(samples.Count, 3);
        }

        // Test Get Last Thirty Seconds Samples Data From Memory Using Diffirent Samples Types
        [TestMethod]
        public void TestGetLastThirtySecondsSamplesDataFromMemoryUsingDiffirentSamplesTypes()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);

            var samples = _store.GetLastThirtySeconds();
            Assert.AreEqual(samples.Count, 3);
        }

        //Test Get Cpu Last Thirty Seconds Samples Data From Memory
        [TestMethod]
        public void TestGetCpuLastThirtySecondsSamplesDataFromMemory()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);

            var samples = _store.GetLastThirtySeconds(1);
            Assert.AreEqual(samples.Count, 1);
        }

        //Test Get Cpu Value Last Thirty Seconds Samples Data From Memory
        [TestMethod]
        public void TestGetCpuValueLastThirtySecondsSamplesDataFromMemory()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);

            var sample = _store.GetLastThirtySeconds(1).First();
            Assert.AreEqual(sample.Value, 35.98);
        }

        //Test Get Memory Last Thirty Seconds Samples Data From Memory
        [TestMethod]
        public void TestGetMemoryLastThirtySecondsSamplesDataFromMemory()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);

            var samples = _store.GetLastThirtySeconds(2);
            Assert.AreEqual(samples.Count, 2);
        }


        // Test Get Memory Value Last Thirty Seconds Samples Data From Memory
        [TestMethod]
        public void TestGetMemoryValueLastThirtySecondsSamplesDataFromMemory()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);

            var sample = _store.GetLastThirtySeconds(2).Last();
            Assert.AreEqual(sample.Value, 23598.2489);
        }

        // Test Discard Data From Memory
        [TestMethod]
        public void TestDiscardDataFromMemory()
        {
            var mockList = LoadMockSampleData();
            _store.Store(mockList);
            _store.DiscardOldSamples();
            var samples = _store.GetStoredSamplesCache().SelectMany(x => x.Value);

            Assert.AreEqual(samples.Count(), 3);
        }

    }
}
