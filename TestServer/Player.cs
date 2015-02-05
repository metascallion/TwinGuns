using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;
using System.Threading;

namespace TestServer
{

    public enum tower
    {
        none,
        attack,
        ressource
    }

    public class Player
    {
        float ressources;
        bool player1;
        Server gameServer;
        int towerCosts = 20;
        int towerDestroy = 20;
        int attackTowerCosts = 25;
        ThreadStart start;
        Thread ressourceThread;
        public float ressourceGain;
        public float ressourceGainPerTower = 0.3f;
        public tower[,] towers = new tower[4, 3];

        public Player(int ressources, bool player1, Server gameServer)
        {
            this.ressources = ressources;
            this.player1 = player1;
            this.gameServer = gameServer;
            gameServer.registerNetworkListenerMethod("BuildTower", OnBuildTowerRequest);
            gameServer.registerNetworkListenerMethod("DestroyTower", OnDestroyTowerRequest);
            gameServer.registerNetworkListenerMethod("BuildDrone", OnBuildDroneRequest);

            start = new ThreadStart(this.higherRessources);
            ressourceThread = new Thread(start);
            ressourceThread.Start();
            initializeTowers();
        }

        private void initializeTowers()
        {
            for(int x = 0; x < towers.GetLength(0); x++)
            {
                for(int y = 0; y < towers.GetLength(1); y++)
                {
                    towers[x, y] = tower.none;
                }
            }
        }

        private void sendTowerUpdate()
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("TowerUpdate");
            msg.addInformation("player", this.player1);

            for (int x = 0; x < towers.GetLength(0); x++)
            {
                for (int y = 0; y < towers.GetLength(1); y++)
                {
                    msg.addInformation(x.ToString() + y.ToString(), towers[x, y].ToString());
                }
            }

            gameServer.sendMessage(msg);
        }

        public void OnBuildTowerRequest(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) == this.player1)
            {
                if ((Boolean.Parse(msg.getInformation("Type")) && this.ressources >= attackTowerCosts) || (!Boolean.Parse(msg.getInformation("Type")) && this.ressources >= towerCosts))
                {
                    int x = int.Parse(msg.getInformation("x"));
                    int y = int.Parse(msg.getInformation("y"));

                    if (Boolean.Parse(msg.getInformation("Type")) == false)
                    {
                        towers[x, y] = tower.ressource;
                        ressources -= towerCosts;
                    }
                    else
                    {
                        towers[x, y] = tower.attack;
                        ressources -= attackTowerCosts;
                    }
                    SendableNetworkMessage smsg = new SendableNetworkMessage("BuildTower");
                    smsg.addInformation("x", msg.getInformation("x"));
                    smsg.addInformation("y", msg.getInformation("y"));
                    smsg.addInformation("Player", msg.getInformation("Player"));
                    smsg.addInformation("Type", msg.getInformation("Type"));
                    smsg.addInformation("Ressources", ressources);
                    //gameServer.sendMessage(smsg);
                    sendTowerUpdate();
                    if (Boolean.Parse(msg.getInformation("Type")) == false)
                    {
                        ressourceGain += ressourceGainPerTower;
                    }
                }
            }
        }

        public void OnDestroyTowerRequest(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) != this.player1)
            {
                if (Boolean.Parse(msg.getInformation("Type")) == false)
                {
                    ressources -= towerDestroy;
                }
                else
                {
                    ressources -= towerDestroy;
                }
                int x = int.Parse(msg.getInformation("x"));
                int y = int.Parse(msg.getInformation("y"));

                towers[x, y] = tower.none;
                sendTowerUpdate();
                SendableNetworkMessage smsg = new SendableNetworkMessage("DestroyTower");
                smsg.addInformation("x", msg.getInformation("x"));
                smsg.addInformation("y", msg.getInformation("y"));
                smsg.addInformation("Player", msg.getInformation("Player"));
                smsg.addInformation("Ressources", ressources);
                //gameServer.sendMessage(smsg);
            }

            else if(Boolean.Parse(msg.getInformation("Type")) == false)
            {
                ressourceGain -= ressourceGainPerTower;
            }
        }

        public void OnBuildDroneRequest(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) == player1)
            {
                SendableNetworkMessage smsg = new SendableNetworkMessage("BuildDrone");
                smsg.addInformation("Player", msg.getInformation("Player"));
                smsg.addInformation("Index", msg.getInformation("Index"));
                ressources -= 20;
                smsg.addInformation("Ressources", ressources);
                gameServer.sendMessage(smsg);
            }
        }

        public void higherRessources()
        {

            Thread.Sleep(1000);
            Console.WriteLine(this.ressources += ressourceGain);
            sendRessourceUpdate();
            higherRessources();
        }


        public void sendRessourceUpdate()
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("RessourceUpdate");

            msg.addInformation("Ressources", this.ressources);
            msg.addInformation("Player", this.player1);

            gameServer.sendMessage(msg);
        }
    }
}
