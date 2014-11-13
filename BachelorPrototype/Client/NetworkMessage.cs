using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    public class NetworkMessage
    {
        public string type;
        public string data;

        public NetworkMessage(string stringToBuildMessageFrom)
        {
            type = splitTypeAndData(stringToBuildMessageFrom)[0];
            data = splitTypeAndData(stringToBuildMessageFrom)[1];
        }

        private string[] splitTypeAndData(string split)
        {
            string[] temp = split.Split(',');

            return temp;
        }
    }
}
