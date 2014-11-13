using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BachelorPrototype.GameClasses
{
    public class UpdateExecuter
    {
        public delegate void update();
        public static event update Updates;

        public static void updateAll()
        {
            if (Updates != null)
            {
                Updates();
            }
        }
    }
}
