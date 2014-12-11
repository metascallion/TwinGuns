using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using System.Threading;

namespace WhiteSpace.Network
{
    public static class Server
    {
        public delegate void OnNetworkMessageEnter(ReceiveableNetworkMessage message);

        public static Dictionary<string, List<OnNetworkMessageEnter>> networkMessageListeners = new Dictionary<string, List<OnNetworkMessageEnter>>();

        public static NetServer server;
        public static NetPeerConfiguration config;

        public static void startServer(string appId, int portToListenOn)
        {
            config = new NetPeerConfiguration(appId);
            config.Port = portToListenOn;
            server = new NetServer(config);
            server.Start();
            setSynchronizationContext();
            server.RegisterReceivedCallback(pollNetworkMessage);
        }

        public static void setSynchronizationContext()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        public static void registerNetworkListenerMethod(string headerToListenTo, OnNetworkMessageEnter method)
        {
            if (!networkMessageListeners.Keys.Contains(headerToListenTo))
            {
                networkMessageListeners[headerToListenTo] = new List<OnNetworkMessageEnter>();
            }
            networkMessageListeners[headerToListenTo].Add(method);
        }

        public static void pollNetworkMessage(object state)
        {
            NetIncomingMessage msg;

            if ((msg = server.ReadMessage()) != null)
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
            NetOutgoingMessage msg = server.CreateMessage();
            msg.Write(message.getStringFromMessage());
            server.SendToAll(msg, NetDeliveryMethod.UnreliableSequenced);
        }

    }
}
