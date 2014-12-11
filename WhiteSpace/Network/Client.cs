using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace WhiteSpace.Network
{
    public static class Client
    {
        public delegate void OnNetworkMessageEnter(ReceiveableNetworkMessage message);
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
            if (!networkMessageListeners.Keys.Contains(headerToListenTo))
            {
                networkMessageListeners[headerToListenTo] = new List<OnNetworkMessageEnter>();
            }
            networkMessageListeners[headerToListenTo].Add(method);
        }

        public static void pollNetworkMessage()
        {
            NetIncomingMessage msg;

            if ((msg = client.ReadMessage()) != null)
            {
                if (msg.MessageType == NetIncomingMessageType.Data)
                {
                    onNetworkMessageEnter(ReceiveableNetworkMessage.createMessageFromString(msg.ReadString()));
                }
            }
        }

        public static void onNetworkMessageEnter(ReceiveableNetworkMessage message)
        {
            foreach (OnNetworkMessageEnter t in networkMessageListeners[message.Header])
            {
                t(message);
            }
        }

        public static void sendMessage(SendableNetworkMessage message)
        {
            NetOutgoingMessage msg = client.CreateMessage();
            msg.Write(message.getStringFromMessage());
            client.SendMessage(msg, NetDeliveryMethod.UnreliableSequenced);
        }
    }
}
