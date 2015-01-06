using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer
{
    public class Game
    {
        public string name;
        public List<string> players = new List<string>();

        public Game(string name)
        {
            this.name = name;
        }
    }
}
