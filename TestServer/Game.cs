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
        Registration registration;
        public Game()
        {
            gameServer.startServer("test", 1111);
            gameServer.registerNetworkListenerMethod("BuildDrone", OnBuildDroneRequest);
            gameServer.registerNetworkListenerMethod("BuildTower", OnBuildTowerRequest);
            gameServer.registerNetworkListenerMethod("DestroyTower", OnDestroyTowerRequest);
            registration = new Registration(gameServer);
        }

        public void OnBuildDroneRequest(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("BuildDrone");
            smsg.addInformation("Player", msg.getInformation("Player"));
            Drone drone = new Drone(gameServer);
            registration.enqueueClass(drone);
            gameServer.sendMessage(smsg);
        }

        public void OnBuildTowerRequest(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("BuildTower");
            smsg.addInformation("x", msg.getInformation("x"));
            smsg.addInformation("y", msg.getInformation("y"));
            smsg.addInformation("Player", msg.getInformation("Player"));

            gameServer.sendMessage(smsg);
        }

        public void OnDestroyTowerRequest(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("DestroyTower");
            smsg.addInformation("x", msg.getInformation("x"));
            smsg.addInformation("y", msg.getInformation("y"));
            smsg.addInformation("Player", msg.getInformation("Player"));

            gameServer.sendMessage(smsg);
        }

    }
}
