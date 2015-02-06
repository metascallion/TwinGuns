using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Composite;
using WhiteSpace.Components;
using WhiteSpace.Network;

namespace WhiteSpace.GameClasses
{
    public class LifeSender : StandardComponent
    {
        Life life;
        bool player1;

        public LifeSender()
        {
        }

        public LifeSender(bool player1)
        {
            this.player1 = player1;
        }

        public override void start()
        {
            base.start();
            this.life = this.parent.getComponent<Life>();
        }

        public void sendLife()
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Life");
            msg.addInformation("Player", this.player1);
            msg.addInformation("Health", life.Health);
            Client.sendMessage(msg);
        }
    }
}
