using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace.GameClasses
{
    public class GameRessources
    {
        public int ressources = 100;

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
    }
}
