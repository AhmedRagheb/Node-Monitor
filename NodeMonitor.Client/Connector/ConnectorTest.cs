using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NodeMonitor.Client.Connector
{
    internal static class ConnectorTest
    {
        internal static bool IsChannelOpen()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ConnectorConfiguration.Address), ConnectorConfiguration.Port);
            SocketTestData data;
            try
            {
                using (Socket client = new Socket(endpoint.AddressFamily,
                          SocketType.Stream, ProtocolType.Tcp))
                {

                    data = new SocketTestData() { Socket = client, ConnectDone = new ManualResetEvent(false) };
                    IAsyncResult ar = client.BeginConnect
                              (endpoint, new AsyncCallback(TestConnectionCallback), data);

                    // wait for connection success as signaled from callback, or timeout
                    data.ConnectDone.WaitOne(ConnectorConfiguration.Timeout);
                    return data.Connected;
                }
            }

            catch (SocketException sockEx)
            {
                return false;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        private static void TestConnectionCallback(IAsyncResult ar)
        {
            SocketTestData data = (SocketTestData)ar.AsyncState;
            data.Connected = data.Socket.Connected;
            //data.Socket.EndConnect(ar);
            data.ConnectDone.Set(); // Signal completion
        }
    }
}
