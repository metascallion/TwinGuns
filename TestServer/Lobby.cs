using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;

namespace TestServer
{
    public class Lobby
    {
        public List<string> names = new List<string>();
        public string name;
        public Server lobbyServer = new Server();

        public Lobby(string name)
        {
            this.name = name;
            lobbyServer.startServer("test", 1111);
            lobbyServer.registerNetworkListenerMethod("Close", OnCloseMessageEnter);
            lobbyServer.registerNetworkListenerMethod("FindPlayers", OnFindPlayersRequest);
            lobbyServer.registerNetworkListenerMethod("Leave", OnLeaveRequest);
            lobbyServer.registerNetworkListenerMethod("Start", OnStartRequest);
            lobbyServer.registerNetworkListenerMethod("StartAccepted", OnStartAccepted);
            lobbyServer.OnConnectionEnter += OnConnectionEnter;
        }

        private void OnConnectionEnter(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("Joined");
            lobbyServer.HailMessage = smsg;
        }

        private void OnFindPlayersRequest(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("FoundPlayers");

            foreach (string s in names)
            {
                smsg.addInformationContent("Name", s);
            }
            lobbyServer.sendMessage(smsg);
        }

        private void OnCloseMessageEnter(ReceiveableNetworkMessage msg)
        {     
            GameContainer.activeGames.Remove(this);
            SendableNetworkMessage smsg = new SendableNetworkMessage("Leave");
            smsg.addInformation("MasterPort", Program.masterServer.config.Port);
            lobbyServer.sendMessage(smsg);
        }

        private void OnLeaveRequest(ReceiveableNetworkMessage msg)
        {
            foreach (Lobby lobby in GameContainer.activeGames)
            {
                if (lobby.names.Contains(msg.getInformation("PlayerName")))
                {
                    lobby.names.Remove(msg.getInformation("PlayerName"));
                    OnFindPlayersRequest(msg);
                    SendableNetworkMessage smsg = new SendableNetworkMessage("Leave");
                    smsg.addInformation("MasterPort", Program.masterServer.config.Port);
                    lobbyServer.sendMessageToSingleRecipient(smsg, msg.SenderConnection);
                }
            }
        }

        private void OnStartRequest(ReceiveableNetworkMessage msg)
        {
            lobbyServer.sendMessage(new SendableNetworkMessage("AcceptStart"));
        }

        private void OnStartAccepted(ReceiveableNetworkMessage msg)
        {
            if (msg.getInformation("Answer") == "Yes")
            {
                Game game = new Game();
                SendableNetworkMessage smsg = new SendableNetworkMessage("StartGame");
                smsg.addInformation("GamePort", game.gameServer.config.Port);
                lobbyServer.sendMessage(smsg);
            }

            else
            {
                lobbyServer.sendMessage(new SendableNetworkMessage("NotAccepted"));
            }
        }
    }
}
