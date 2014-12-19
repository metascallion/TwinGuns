using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace
{
    public enum lobbystate
    {
        start,
        host,
        lobby,
        selection
    }

    public enum gamestate
    {
        main,
        game,
        lobby,
        gameover
    }

    public class StateMachine <StateType>
    {
        private static StateMachine<StateType> instance;
        public delegate void stateChangeMethod(StateType activeState);
        public event stateChangeMethod stateChangeMethods;

        private StateMachine()
        {
        }

        public void changeState(StateType stateToSet)
        {
            if (stateChangeMethods != null)
            {
                stateChangeMethods(stateToSet);
            }
        }

        public static StateMachine<StateType> getInstance()
        {
            if(instance == null)
            {
                instance = new StateMachine<StateType>();
            }
            return instance;
        }

    }
}
