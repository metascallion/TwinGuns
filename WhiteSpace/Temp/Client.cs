using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace WhiteSpace.Temp
{
    public class NetworkMessage
    {
        public string Header;
        protected Dictionary<string, string> informations = new Dictionary<string, string>();

        public NetworkMessage()
        {
        }

        public NetworkMessage(string header)
        {
            this.Header = header;
        }
    }

    public class SendableNetworkMessage : NetworkMessage
    {
        public SendableNetworkMessage(string header) : base(header)
        {
        }

        public string getStringFromMessage()
        {
            string messageContent = this.Header;

            foreach (string s in informations.Keys)
            {
                messageContent += ";" + s + "=" + this.informations[s];
            }

            return messageContent;
        }

        public void addInformation(string key, object value)
        {
            this.informations[key] = value.ToString();
        }
    }

    public class ReceiveableNetworkMessage : NetworkMessage
    {
        private static string[] splitKeyValue(string stringToSplit)
        {
            return stringToSplit.Split('=');
        }

        private static List<string> splitMessageString(string messageString)
        {
            List<string> strings = new List<string>();
            string[] splittedString = messageString.Split(';');
            foreach (string s in splittedString)
            {
                strings.Add(s);
            }

            return strings;
        }

        public static ReceiveableNetworkMessage createMessageFromString(string stringToConvert)
        {
            ReceiveableNetworkMessage msg = new ReceiveableNetworkMessage();

            List<string> splittedString = splitMessageString(stringToConvert);
            msg.Header = splittedString[0];
            splittedString.Remove(splittedString[0]);

            foreach (string s in splittedString)
            {
                msg.informations[splitKeyValue(s)[0]] = splitKeyValue(s)[1];
            }

            return msg;
        }

        public string getInformation(string key)
        {
            return this.informations[key];
        }
    }



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
            if(!networkMessageListeners.Keys.Contains(headerToListenTo))
            {
                networkMessageListeners[headerToListenTo] = new List<OnNetworkMessageEnter>();
            }
            networkMessageListeners[headerToListenTo].Add(method);
        }

        public static void pollNetworkMessage()
        {
            NetIncomingMessage msg;

            if((msg = client.ReadMessage()) != null)
            {
                if (msg.MessageType == NetIncomingMessageType.Data)
                {
                    onNetworkMessageEnter(ReceiveableNetworkMessage.createMessageFromString(msg.ReadString()));
                }
            }
        }

        public static void onNetworkMessageEnter(ReceiveableNetworkMessage message)
        {
            foreach(OnNetworkMessageEnter t in networkMessageListeners[message.Header])
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
