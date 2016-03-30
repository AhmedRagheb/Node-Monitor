using NodeMonitor.SamplesReaders;
using NodeMonitor.SamplesStore;
using StructureMap;

namespace NodeMonitor.Test
{
    internal static class  StructureMapConfigure
    {
        public static Container Container;
        internal static void Configure()
        {
            Container = new Container();

            Container.Configure(x => x.For<SampleReadersUnitOfWork>().Singleton());
            Container.Configure(x => x.For<ISampleReader>().Use<CpuReader>().Named("CPU"));
            Container.Configure(x => x.For<ISampleReader>().Use<MemoryReader>().Named("Memory"));
            Container.Configure(x => x.For<ISamplesMemoryStore>().Use<SamplesMemoryStore>().Singleton());
        }
    }
}
