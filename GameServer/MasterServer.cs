using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;

namespace MasterServer
{
    public class MasterServer
    {
        List<string> games = new System.Collections.Generic.List<string>();

        public void OnNetworkmessageEnter(ReceiveableNetworkMessage msg)
        {
            games.Add(msg.getInformation("Name"));
            SendableNetworkMessage msg2 = new SendableNetworkMessage("Host");
            Server.sendMessage(msg2);
        }

        public void OnFindGamesRequest(ReceiveableNetworkMessage msg)
        {
            sendGames();
        }

        public void OnJoinGameRequestEnter(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage message = new SendableNetworkMessage("Join");
            Server.sendMessage(message);
        }

        public void setName(ReceiveableNetworkMessage msg)
        {
            Server.sendToSingleRecipient(msg.sender, new SendableNetworkMessage("Test"));
        }

        public void sendGames()
        {
            foreach (string s in games)
            {
                SendableNetworkMessage sendMessage = new SendableNetworkMessage("FoundGame");
                sendMessage.addInformation("GameName", s);
                Server.sendMessage(sendMessage);
            }
        }


    }
}
