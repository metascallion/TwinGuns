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
        }

    }
}
