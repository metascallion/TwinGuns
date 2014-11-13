using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BachelorPrototype.GameClasses
{
    public enum state
    {
        main,
        lobby,
        none,
        game
    }
    public class StateContainer
    {
        private static StateContainer instance;
        private state programmstate;

        private StateContainer()
        {
        }

        public static StateContainer getInstance()
        {
            if (instance == null)
            {
                instance = new StateContainer();
            }
            return instance;
        }

        public state getState()
        {
            return programmstate;
        }

        public void setState(state stateToSet)
        {
            programmstate = stateToSet;
        }
    }
}
