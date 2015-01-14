using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;

namespace TestServer
{
    public class Game
    {
        public Server gameServer = new Server();


        public Game()
        {
            gameServer.startServer("test", 1111);
            gameServer.registerNetworkListenerMethod("BuildDrone", OnBuildDroneRequest);
        }

        public void OnBuildDroneRequest(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("BuildDrone");
            smsg.addInformation("Player", msg.getInformation("Player"));
            gameServer.sendMessage(smsg);
        }

    }
}
