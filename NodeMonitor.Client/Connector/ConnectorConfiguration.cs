
namespace NodeMonitor.Client.Connector
{
    public static class ConnectorConfiguration
    {
        private static int _port = 9999;
        private static int _timeout = 10;
        private static string _objectUri = "Monitor";
        private static string _address = "127.0.0.1";
        private static string _protocol = "tcp:";
       

        public static int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public static int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public static string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public static string Protocol
        {
            get { return _protocol; }
            set { _protocol = value; }
        }

        public static string ObjectUri
        {
            get { return _objectUri; }
            set { _objectUri = value; }
        }
    }
}


