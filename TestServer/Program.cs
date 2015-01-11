using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.startServer("test", 1111);
            Server.registerNetworkListenerMethod("Host", test);
            Server.registerNetworkListenerMethod("FindGames", OnFindGamesRequestEnter);
            Server.registerNetworkListenerMethod("Join", OnJoinRequest);
            Server.registerNetworkListenerMethod("Leave", OnLeaveRequest);
            Server.registerNetworkListenerMethod("Close", OnCloseMessageEnter);
            Console.ReadLine();
        }

        static void test(ReceiveableNetworkMessage msg)
        {
            Lobby l = new Lobby(msg.getInformation("GameName"));
            l.names.Add(msg.getInformation("PlayerName"));
            GameContainer.activeGames.Add(l);
            SendableNetworkMessage sendMsg = new SendableNetworkMessage("Host");
            sendMsg.addInformation("GameName", l.name);
            Server.sendMessageToSingleRecipient(sendMsg, msg.SenderConnection);
            sendPlayers(l, msg);
        }

        static void OnFindGamesRequestEnter(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("FoundGames");
            foreach(Lobby l in GameContainer.activeGames)
            {
                smsg.addInformationContent("GameName", l.name);
            }
            Server.sendMessageToSingleRecipient(smsg, msg.SenderConnection);
        }

        static void OnJoinRequest(ReceiveableNetworkMessage msg)
        {
            foreach (Lobby l in GameContainer.activeGames)
            {
                if (l.name == msg.getInformation("GameName"))
                {
                    SendableNetworkMessage sendMsg = new SendableNetworkMessage("Join");
                    sendMsg.addInformation("GameName", l.name);
                    Server.sendMessageToSingleRecipient(sendMsg, msg.SenderConnection); 
                    l.names.Add(msg.getInformation("PlayerName"));
                    sendPlayers(l, msg);
                }
            }
        }

        static void OnCloseMessageEnter(ReceiveableNetworkMessage msg)
        {
            for (int i = 0; i < GameContainer.activeGames.Count(); i++)
            {
                Lobby l = GameContainer.activeGames[i];
                if (l.name == msg.getInformation("GameName"))
                {
                    GameContainer.activeGames.Remove(l);
                    SendableNetworkMessage smsg = new SendableNetworkMessage("Close");
                    smsg.addInformation("GameName", l.name);
                    Server.sendMessage(smsg);
                }
            }
        }

        static void sendPlayers(Lobby l, ReceiveableNetworkMessage msgg)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("FoundPlayers");
            msg.addInformation("GameName", l.name);

            foreach (string s in l.names)
            {
                msg.addInformationContent("Name", s);
            }
            Server.sendMessage(msg);
        }

        static void OnLeaveRequest(ReceiveableNetworkMessage msg)
        {
            foreach (Lobby lobby in GameContainer.activeGames)
            {
                if(lobby.names.Contains(msg.getInformation("PlayerName")))
                {
                    lobby.names.Remove(msg.getInformation("PlayerName"));
                    sendPlayers(lobby, msg);
                    Server.sendMessageToSingleRecipient(new SendableNetworkMessage("Leave"), msg.SenderConnection);
                }
            }
        }


    }
}
