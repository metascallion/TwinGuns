using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;
using System.Threading;

namespace TestServer
{

    public enum towertype
    {
        none,
        attack,
        ressource,
        shield,
        energy,
        defense
    }

    public class Player
    {
        float ressources;
        bool player1;
        Server gameServer;
        ThreadStart start;
        Thread ressourceThread;
        public float ressourceGain;
        public float ressourceGainPerTower = 0.3f;
        public towertype[,] towers = new towertype[5, 7];
        int health;

        public Player(int ressources, int health, bool player1, Server gameServer)
        {
            this.ressources = ressources;
            this.health = health;
            this.player1 = player1;
            this.gameServer = gameServer;
            gameServer.registerNetworkListenerMethod("BuildTower", OnBuildTowerRequest);
            gameServer.registerNetworkListenerMethod("DestroyTower", OnDestroyTowerRequest);
            gameServer.registerNetworkListenerMethod("BuildDrone", OnBuildDroneRequest);
            gameServer.registerNetworkListenerMethod("Life", OnDamageDealed);

            start = new ThreadStart(this.higherRessources);
            ressourceThread = new Thread(start);
            ressourceThread.Start();
            new Thread(new ThreadStart(sendHealthUpdate)).Start();
            initializeTowers();
        }

        private void initializeTowers()
        {
            for(int x = 0; x < towers.GetLength(0); x++)
            {
                for(int y = 0; y < towers.GetLength(1); y++)
                {
                    towers[x, y] = towertype.none;
                }
            }

            new Thread(new ThreadStart(updateTowerAfterTime)).Start();
        }

        private void updateTowerAfterTime()
        {
            Thread.Sleep(3000);
            sendTowerUpdate();
            updateTowerAfterTime();
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
                towertype tower = (towertype)Enum.Parse(typeof(towertype), msg.getInformation("TowerType"));
                if(this.ressources >= getCosts(tower))
                {
                    int x = int.Parse(msg.getInformation("x"));
                    int y = int.Parse(msg.getInformation("y"));
                    this.ressources -= getCosts(tower);
                    towers[x, y] = tower;
                    sendTowerUpdate();
                }
            }
        }

        private int getCosts(towertype type)
        {
            return Game.costs[type];
        }

        public void OnDestroyTowerRequest(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) == this.player1)
            {
                if (Boolean.Parse(msg.getInformation("Type")) == false)
                {
                    ressourceGain -= ressourceGainPerTower;
                }
                int x = int.Parse(msg.getInformation("x"));
                int y = int.Parse(msg.getInformation("y"));

                towers[towers.GetLength(0) - x - 1, y] = towertype.none;
                sendTowerUpdate();
            }

            else
            {
                if (Boolean.Parse(msg.getInformation("Type")) == false)
                {
                    ressources -= Game.ressourceTowerCosts;
                }
                else
                {
                    ressources -= Game.attackTowerCosts;
                }
            }
        }

        public void OnBuildDroneRequest(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) == player1)
            {
                if (ressources >= Game.droneCosts)
                {
                    SendableNetworkMessage smsg = new SendableNetworkMessage("BuildDrone");
                    smsg.addInformation("Player", msg.getInformation("Player"));
                    smsg.addInformation("Index", msg.getInformation("Index"));
                    ressources -= Game.droneCosts;
                    smsg.addInformation("Ressources", ressources);
                    gameServer.sendMessage(smsg);
                }
            }
        }

        public void higherRessources()
        {
            Thread.Sleep(1000);
            ressources += ressourceGain;
            sendRessourceUpdate();
            higherRessources();
        }


        public void sendRessourceUpdate()
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("RessourceUpdate");
            msg.addInformation("Ressources", this.ressources);
            msg.addInformation("RessourceGain", this.ressourceGain);
            msg.addInformation("Player", this.player1);
            gameServer.sendMessage(msg);
        }

        public void OnDamageDealed(ReceiveableNetworkMessage msg)
        {
            if(Boolean.Parse(msg.getInformation("Player")) == this.player1)
            {
                this.health--;
            }
        }

        private void sendHealthUpdate()
        {
            Thread.Sleep(1000);
            SendableNetworkMessage smsg = new SendableNetworkMessage("Life");
            smsg.addInformation("Player", this.player1);
            smsg.addInformation("Health", this.health);
            gameServer.sendMessage(smsg);
            sendHealthUpdate();
        }
    }
}
