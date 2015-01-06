using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;
using Lidgren.Network;

namespace MasterServer
{
    public class MasterServer
    {
        List<Game> games = new System.Collections.Generic.List<Game>();

        public void OnNetworkmessageEnter(ReceiveableNetworkMessage msg)
        {
            Game game = new Game(msg.getInformation("Name"));
            game.players.Add(msg.getInformation("PlayerName"));
            games.Add(game);
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

        public void sendGames()
        {
            foreach (Game s in games)
            {
                SendableNetworkMessage sendMessage = new SendableNetworkMessage("FoundGame");
                sendMessage.addInformation("GameName", s.name);
                Server.sendMessage(sendMessage);
            }
        }


    }
}
