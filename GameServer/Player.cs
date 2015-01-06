using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace MasterServer
{
    public class Player
    {
        public NetConnection connection;
        public string name;

        public Player(NetConnection connection, string name)
        {
            this.connection = connection;
            this.name = name;
        }
    }
}
