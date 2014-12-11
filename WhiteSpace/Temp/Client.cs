using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace WhiteSpace.Temp
{
    public class NetworkMessage
    {
        public string header;
        public Dictionary<string, string> informations;

        public NetworkMessage(string header)
        {
            this.header = header;
        }

    }

    public static class Client
    {
        public delegate void OnNetworkMessageEnter(NetworkMessage message);

        public static Dictionary<string, List<OnNetworkMessageEnter>> networkMessageListeners = new Dictionary<string, List<OnNetworkMessageEnter>>();

        public static NetClient client;
        public static NetPeerConfiguration config;

        public static void startClient(string appId)
        {
            config = new NetPeerConfiguration(appId);
            client = new NetClient(config);
            client.Start();
        }

        public static void connect(string ip, int port)
        {
            client.Connect(ip, port);
        }

        public static void registerNetworkListenerMethod(string headerToListenTo, OnNetworkMessageEnter method)
        {
            if(!networkMessageListeners.Keys.Contains(headerToListenTo))
            {
                networkMessageListeners[headerToListenTo] = new List<OnNetworkMessageEnter>();
            }
            networkMessageListeners[headerToListenTo].Add(method);
        }

        public static void onNetworkMessageEnter(NetworkMessage message)
        {
            foreach(OnNetworkMessageEnter t in networkMessageListeners[message.header])
            {
                t(message);
            }
        }
    }
}
