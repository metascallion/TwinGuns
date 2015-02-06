using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace.Network
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
}
