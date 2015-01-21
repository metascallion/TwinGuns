using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace WhiteSpace.Network
{
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

        public void addInformationContent(string header, object value)
        {
            if (!this.informations.Keys.Contains(header))
            {
                this.informations[header] = value.ToString();
            }
            else
            {
                this.informations[header] += "+" + value.ToString();
            }
        }
    }
}
