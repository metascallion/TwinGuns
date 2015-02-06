using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace
{
    public enum lobbystate
    {
        selection,
        start,
        host,
        lobby,
        hud
    }

    public enum gamestate
    {
        main,
        game,
        lobby,
        gameover
    }

    public class StateMachine <StateType> where StateType : struct
    {
        private static StateMachine<StateType> instance;
        public delegate void stateChangeMethod(StateType activeState);
        public event stateChangeMethod stateChangeMethods;
        Stack<StateType> lastStates = new Stack<StateType>();

        bool firstChange = true;
        StateType lastState;

        private StateMachine()
        {
        }

        public void changeState(StateType stateToSet)
        {
            if (!firstChange)
            {
                lastStates.Push(lastState);
            }
            lastState = stateToSet;
            firstChange = false;

            if (stateChangeMethods != null)
            {
                stateChangeMethods(stateToSet);
            }
        }

        public void restoreLastState()
        {
            lastState = lastStates.Pop();
            if (stateChangeMethods != null)
            {
                stateChangeMethods(lastState);
            }
        }

        public void loadNextState()
        {
            int i; 
            Int32.TryParse(lastState.ToString(), out i);
            int result = i + 1;
            StateType resultingState;
            Enum.TryParse<StateType>(result.ToString(), out resultingState);
            changeState(resultingState);
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
