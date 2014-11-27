using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace
{
    public class StateListener<StateType> where StateType : struct
    {
        StateType activeState;
        bool invalidTriggered = false;

        public StateListener()
        {
            Enum.TryParse<StateType>("0", out this.activeState);
            registerInStateMachine();
        }

        public StateListener(StateType activeState)
        {
            this.activeState = activeState;
            registerInStateMachine();
        }

        private void registerInStateMachine()
        {
            StateMachine<StateType>.getInstance().stateChangeMethods += processStateChange;
        }

        private void processStateChange(StateType activeState)
        {
            if (!EqualityComparer<StateType>.Default.Equals(this.activeState, activeState))
            {
                processInvalidState();
            }
            else if (invalidTriggered)
            {
                processValidState();
            }
        }

        private void unregisterInStateMachine()
        {
            StateMachine<StateType>.getInstance().stateChangeMethods -= processStateChange;
        }

        protected virtual void processInvalidState()
        {
            invalidTriggered = true;
        }

        protected virtual void processValidState()
        {
            invalidTriggered = false;
        }
    }
}
