using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;
using System.Threading;

namespace TestServer
{
    class Program
    {
        public static Server masterServer;
        static Server testServer;
        static void Main(string[] args)
        {
            new Game();
            Console.ReadLine();
        }

        static void OnHostRequest(ReceiveableNetworkMessage msg)
        {
            Lobby l = new Lobby(msg.getInformation("GameName"));
            l.names.Add(msg.getInformation("PlayerName"));
            GameContainer.activeGames.Add(l);
            SendableNetworkMessage sendMsg = new SendableNetworkMessage("Host");
            sendMsg.addInformation("GameName", l.name);
            sendMsg.addInformation("Port", l.lobbyServer.config.Port);
            masterServer.sendMessageToSingleRecipient(sendMsg, msg.SenderConnection);
        }

        static void OnFindGamesRequestEnter(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("FoundGames");
            foreach(Lobby l in GameContainer.activeGames)
            {
                smsg.addInformationContent("GameName", l.name);
            }
            masterServer.sendMessageToSingleRecipient(smsg, msg.SenderConnection);
        }

        static void OnJoinRequest(ReceiveableNetworkMessage msg)
        {
            foreach (Lobby l in GameContainer.activeGames)
            {
                if (l.name == msg.getInformation("GameName"))
                {
                    SendableNetworkMessage sendMsg = new SendableNetworkMessage("Join");
                    sendMsg.addInformation("GamePort", l.lobbyServer.config.Port);
                    l.names.Add(msg.getInformation("PlayerName"));
                    masterServer.sendMessageToSingleRecipient(sendMsg, msg.SenderConnection); 
                }
            }
        }
    }
}
