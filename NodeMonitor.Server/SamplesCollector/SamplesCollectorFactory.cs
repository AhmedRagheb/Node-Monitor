

namespace NodeMonitor.Server.SamplesCollector
{
    public static class SamplesCollectorFactory
    {
        static readonly object SyncObject = new object();
        private static SamplesCollector _samplesCollector;

        public static SamplesCollector Create()
        {
            return SamplesCollector;
        }

        /// <summary>
        /// SamplesCollector Singleton Object
        /// </summary>
        public static SamplesCollector SamplesCollector
        {
            get
            {
                if (_samplesCollector == null)
                {
                    lock (SyncObject)
                    {
                        // check for null again (double-checked locking pattern) 
                        // to prevent created multiple objects when serving multiple clients
                        if (_samplesCollector == null)
                        {
                            _samplesCollector = new SamplesCollector();
                        }
                    }
                }
                return _samplesCollector;
            }
        }
    }
}
