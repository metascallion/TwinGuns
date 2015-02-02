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
            Player p1 = new Player(100, true, this.gameServer);
            p1.ressourceGain = 2.0f;
            Player p2 = new Player(100, false, this.gameServer);
            p2.ressourceGain = 2.0f;
            gameServer.registerNetworkListenerMethod("OpenHangar", OnOpenHangarRequest);
            gameServer.registerNetworkListenerMethod("Life", OnDamageDealed);
        }

        public void OnOpenHangarRequest(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("OpenHangar");
            smsg.addInformation("Index", msg.getInformation("Index"));
            smsg.addInformation("Player", msg.getInformation("Player"));
            gameServer.sendMessage(smsg);
        }

        public void OnDamageDealed(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage(msg.Header);
            smsg.addInformation("Player", msg.getInformation("Player"));
            smsg.addInformation("Health", msg.getInformation("Health"));
            gameServer.sendMessage(smsg);
        }
    }
}
