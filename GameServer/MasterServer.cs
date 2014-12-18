using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;
using System.Collections.Generic;

namespace MasterServer
{
    public class MasterServer
    {
        List<string> games = new System.Collections.Generic.List<string>();

        public void OnNetworkmessageEnter(ReceiveableNetworkMessage msg)
        {
            if(msg.getInformation("requesttype") == "host")
            {
                games.Add(msg.getInformation("name"));
            }
        }

        public void sendGames()
        {
            foreach (string s in games)
            {
                SendableNetworkMessage sendMessage = new SendableNetworkMessage("games");
                sendMessage.addInformation("name", s);
                Server.sendMessage(sendMessage);
            }
        }


    }
}
