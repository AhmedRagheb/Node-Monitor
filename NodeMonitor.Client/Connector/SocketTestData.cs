using System.Net.Sockets;
using System.Threading;

namespace NodeMonitor.Client.Connector
{
    public class SocketTestData
    {
        public Socket Socket { get; set; }
        public ManualResetEvent ConnectDone { get; set; }
        public bool Connected { get; set; }
    }
}
