using System;
using System.Runtime.Remoting;

namespace NodeMonitor.Server
{
    public static class TcpChannelConfiguration
    {
        private static int _port = 9999;
        private static string _objectUri = "Monitor";
        private static Type _commonInterfaceType = Type.GetType("NodeMonitor.Server.SamplesCollector.SamplesCollector");
        private static WellKnownObjectMode _wellKnownObjectMode = WellKnownObjectMode.SingleCall;

        public static int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public static Type CommonInterfaceType
        {
            get { return _commonInterfaceType; }
            set { _commonInterfaceType = value; }
        }

        public static WellKnownObjectMode WellKnownObjectMode
        {
            get { return _wellKnownObjectMode; }
            set { _wellKnownObjectMode = value; }
        }

        public static string ObjectUri
        {
            get { return _objectUri; }
            set { _objectUri = value; }
        }
    }
}

