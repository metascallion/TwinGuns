using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace.Temp
{
    public class NetworkMessage
    {
        string header;
        string information;
    }

    public static class Client
    {
        public delegate void OnNetworkMessageEnter(NetworkMessage message);

        public static Dictionary<string, List<OnNetworkMessageEnter>> networkMessageListeners = new Dictionary<string, List<OnNetworkMessageEnter>>();

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
            foreach(OnNetworkMessageEnter t in networkMessageListeners["test"])
            {
                t(message);
            }
        }
    }
}
