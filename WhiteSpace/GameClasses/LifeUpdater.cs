using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Network;
using WhiteSpace.Composite;
using WhiteSpace.Components;

namespace WhiteSpace.GameClasses
{
    class LifeUpdater : StandardComponent
    {
        bool player1;

        public LifeUpdater(bool player1)
        {
            Client.registerNetworkListenerMethod("Life", OnLifeUpdate);
            this.player1 = player1;
        }

        void OnLifeUpdate(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) != this.player1 && Client.host || Boolean.Parse(msg.getInformation("Player")) == this.player1 && !Client.host)
            {
                this.parent.getComponent<Life>().Health = int.Parse(msg.getInformation("Health"));
            }
        }
    
    }
}
