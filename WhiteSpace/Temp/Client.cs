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
        public Dictionary<string, string> informations = new Dictionary<string,string>();

        public NetworkMessage()
        {

        }

        public NetworkMessage(string header)
        {
            this.header = header;
        }

        private static List<string> splitString(string stringToSplit)
        {
            List<string> strings = new List<string>();
            string[] splittedString = stringToSplit.Split(',');
            foreach(string s in splittedString)
            {
                strings.Add(s);
            }

            return strings;
        }

        private static string[] splitKeyValue (string stringToSplit)
        {
            return stringToSplit.Split('=');
        }

        public static NetworkMessage createMessageFromString(string stringToConvert)
        {
            NetworkMessage msg = new NetworkMessage();
           
            List<string> splittedString = splitString(stringToConvert);
            msg.header = splittedString[0];
            splittedString.Remove(splittedString[0]);

            foreach(string s in splittedString)
            {
                msg.informations[splitKeyValue(s)[0]] = splitKeyValue(s)[1];
            }

            return msg;
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

        public static void pollNetworkMessage()
        {
            NetIncomingMessage msg;

            if((msg = client.ReadMessage()) != null)
            {
            }
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
