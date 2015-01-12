using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using System.Threading;

namespace WhiteSpace.Network
{
    public class Server
    {
        public delegate void OnNetworkMessageEnter(ReceiveableNetworkMessage message);

        public Dictionary<string, List<OnNetworkMessageEnter>> networkMessageListeners = new Dictionary<string, List<OnNetworkMessageEnter>>();
        public event OnNetworkMessageEnter OnConnectionEnter;

        public NetServer server;
        public NetPeerConfiguration config;

        private static int serverCounter = 0;

        public SendableNetworkMessage HailMessage { get; set; }

        public void startServer(string appId, int portToListenOn)
        {
            config = new NetPeerConfiguration(appId);
            config.Port = portToListenOn + serverCounter;
            serverCounter++;
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.EnableMessageType(NetIncomingMessageType.Data);
            config.EnableMessageType(NetIncomingMessageType.StatusChanged);
            server = new NetServer(config);
            server.Start();
            Console.WriteLine("Server started.");
            setSynchronizationContext();
            server.RegisterReceivedCallback(pollNetworkMessage);
            Console.WriteLine("Message Callback registered.");
        }

        public void setSynchronizationContext()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        public void registerNetworkListenerMethod(string headerToListenTo, OnNetworkMessageEnter method)
        {
            if (!networkMessageListeners.Keys.Contains(headerToListenTo))
            {
                networkMessageListeners[headerToListenTo] = new List<OnNetworkMessageEnter>();
            }
            networkMessageListeners[headerToListenTo].Add(method);
        }

        public void pollNetworkMessage(object state)
        {
            NetIncomingMessage msg;

            if ((msg = server.ReadMessage()) != null)
            {
                if (msg.MessageType == NetIncomingMessageType.Data)
                {
                    onNetworkMessageEnter(ReceiveableNetworkMessage.createMessageFromString(msg.ReadString(), msg.SenderConnection));
                }

                else if(msg.MessageType == NetIncomingMessageType.ConnectionApproval)
                {
                    onConnectionEnter(ReceiveableNetworkMessage.createMessageFromString("", msg.SenderConnection));
                    approveConnection(msg.SenderConnection);
                }
            }
        }

        public void onConnectionEnter(ReceiveableNetworkMessage message)
        {
            if (OnConnectionEnter != null)
            {
                OnConnectionEnter(message);
            }
        }

        private void approveConnection(NetConnection senderConnection)
        {
            if (HailMessage != null)
            {
                NetOutgoingMessage msg = server.CreateMessage();
                msg.Write(HailMessage.getStringFromMessage());

                senderConnection.Approve(msg);
            }

            else
            {
                senderConnection.Approve();
            }

            Console.WriteLine("Client connected");
        }

        public void onNetworkMessageEnter(ReceiveableNetworkMessage message)
        {
            foreach (OnNetworkMessageEnter t in networkMessageListeners[message.Header])
            {
                t(message);
            }
        }

        public void sendMessage(SendableNetworkMessage message)
        {
            NetOutgoingMessage msg = server.CreateMessage();
            msg.Write(message.getStringFromMessage());
            server.SendToAll(msg, NetDeliveryMethod.UnreliableSequenced);
        }

        public void sendMessageToSingleRecipient(SendableNetworkMessage message, NetConnection recipientConnection)
        {
            NetOutgoingMessage msg = server.CreateMessage();
            msg.Write(message.getStringFromMessage());
            server.SendMessage(msg, recipientConnection, NetDeliveryMethod.UnreliableSequenced);
        }

    }
}
