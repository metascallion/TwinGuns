using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lidgren.Network;
using WhiteSpace.Network;

namespace MasterServer
{

    /*
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
        public SendableNetworkMessage(string header)
            : base(header)
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
  
     */

    class Program
    {
        static void onTestNetworkMessageEnter(ReceiveableNetworkMessage rec)
        {
            Console.WriteLine("Rotation: " + (int)float.Parse(rec.getInformation("rotation")) + " Position X: " + (int)float.Parse(rec.getInformation("x")) + "Position Y: " + (int)float.Parse(rec.getInformation("y")));
        }

        static void Main(string[] args)
        {
            Server.startServer("Test", 1111);
            Server.registerNetworkListenerMethod("Transform",onTestNetworkMessageEnter);
            Console.Read();


            /*
            NetPeerConfiguration config = new NetPeerConfiguration("test");
            config.Port = 1111;

            NetServer server = new NetServer(config);
            server.Start();
            Console.WriteLine("Server started.");

            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.EnableMessageType(NetIncomingMessageType.Data);
            
            while(true)
            {
                NetIncomingMessage msg;

                if((msg = server.ReadMessage()) != null)
                {
                    if(msg.MessageType == NetIncomingMessageType.ConnectionApproval)
                    {
                        Console.WriteLine("A Client is trying to connect.");
                        msg.SenderConnection.Approve();
                        Console.WriteLine("Connection approved.");
                    }

                    else if(msg.MessageType == NetIncomingMessageType.Data)
                    {
                        Console.WriteLine(msg.ReadString());
                    }
                }
             

            }
             */ 
        }
    }
}
