using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;
using System.IO;

namespace TestServer
{
    public class Game
    {
        public Server gameServer = new Server();

        public static int ressources;
        public static int health;
        public static float ressourceGain;
        public static float ressourceGainPerTower;
        public static int attackTowerCosts;
        public static int ressourceTowerCosts;
        public static int droneCosts;
        public static int towerDestroyCosts;


        public static Dictionary<towertype, int> costs = new Dictionary<towertype, int>();

        public Game()
        {
            gameServer.startServer("test", 1111);

            StreamReader balanceParser = new StreamReader("Balance.txt");
            ressources = int.Parse(balanceParser.ReadLine().Split(' ')[1]);
            health = int.Parse(balanceParser.ReadLine().Split(' ')[1]);
            ressourceGain = float.Parse(balanceParser.ReadLine().Split(' ')[1]);
            ressourceGainPerTower = float.Parse(balanceParser.ReadLine().Split(' ')[1]);
            costs[towertype.attack] = int.Parse(balanceParser.ReadLine().Split(' ')[1]);
            costs[towertype.ressource] = int.Parse(balanceParser.ReadLine().Split(' ')[1]);
            droneCosts = int.Parse(balanceParser.ReadLine().Split(' ')[1]);
            towerDestroyCosts = int.Parse(balanceParser.ReadLine().Split(' ')[1]);

            Player p1 = new Player(ressources, health, true, this.gameServer);
            p1.ressourceGain = ressourceGain;
            Player p2 = new Player(ressources, health, false, this.gameServer);
            p2.ressourceGain = ressourceGain;
            gameServer.registerNetworkListenerMethod("OpenHangar", OnOpenHangarRequest);
        }

        public void OnOpenHangarRequest(ReceiveableNetworkMessage msg)
        {
            SendableNetworkMessage smsg = new SendableNetworkMessage("OpenHangar");
            smsg.addInformation("Index", msg.getInformation("Index"));
            smsg.addInformation("Player", msg.getInformation("Player"));
            gameServer.sendMessage(smsg);
        }
    }
}
