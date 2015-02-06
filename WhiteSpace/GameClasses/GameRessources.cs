using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Network;

namespace WhiteSpace.GameClasses
{
    public class GameRessources
    {
        public int ressources = 100;
        public float ressourceGain = 2;

        public GameRessources()
        {
            Client.registerNetworkListenerMethod("RessourceUpdate", OnRessourceUpdateEnter);
        }

        public void adjustRessources(int amount)
        {
            ressources += amount;
        }

        public bool haveEnoughRessources(int costs)
        {
            if (ressources >= costs)
            {
                return true;
            }
            return false;
        }

        public void OnRessourceUpdateEnter(ReceiveableNetworkMessage msg)
        {
            if(Boolean.Parse(msg.getInformation("Player")) == Client.host)
            {
                this.ressources = (int)float.Parse(msg.getInformation("Ressources"));
                this.ressourceGain = float.Parse(msg.getInformation("RessourceGain"));
            }
        }
    }
}
