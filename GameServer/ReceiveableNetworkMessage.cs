using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace WhiteSpace.Network
{
    public class ReceiveableNetworkMessage : NetworkMessage
    {
        public NetConnection sender;

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

        public static ReceiveableNetworkMessage createMessageFromString(string stringToConvert, NetConnection sender)
        {
            ReceiveableNetworkMessage msg = new ReceiveableNetworkMessage();
            msg.sender = sender;
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
}
