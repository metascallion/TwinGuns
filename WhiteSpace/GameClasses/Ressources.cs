using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace.GameClasses
{
    public static class GameRessources
    {
        public static int ressources = 100;

        public static void adjustRessources(int amount)
        {
            ressources += amount;
        }

        private static void reduceRessources(int amount)
        {
            ressources -= amount;
        }

        public static bool haveEnoughRessources(int costs)
        {
            if(ressources >= costs)
            {
                reduceRessources(costs);
                return true;
            }
            return false;
        }
    }
}
