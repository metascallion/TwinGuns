using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSpace.Network;

namespace TestServer
{
    public class Lobby
    {
        public List<string> names = new List<string>();
        public string name;

        public Lobby(string name)
        {
            this.name = name;
        }
    }
}
