using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NodeMonitor.Server;
using NodeMonitor.Server.SamplesCollector;

namespace NodeMonitor.Client.Connector
{
    public static class Connector
    {
        internal static ISamplesCollector RemoteObject;

        internal static void ConnectToServer()
        {
            var tcpChannel = new TcpChannel();
            ChannelServices.RegisterChannel(tcpChannel, true);

            var url = ConnectorConfiguration.Protocol + "//" + ConnectorConfiguration.Address + ":" +
                      ConnectorConfiguration.Port + "/" +
                      ConnectorConfiguration.ObjectUri;

            Type requiredType = typeof(ISamplesCollector);
            RemoteObject = (ISamplesCollector)Activator.GetObject(requiredType, url);
        }


    }
}
