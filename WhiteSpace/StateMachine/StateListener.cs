using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace
{
    public class StateListener<StateType> where StateType : struct
    {
        public StateType activeState;
        bool invalidTriggered = false;
        bool destroyOnStateChange = false;

        public StateListener(StateType activeState)
        {
            this.activeState = activeState;
            registerInStateMachine();
        }

        public void destroyOnInvalidState()
        {
            destroyOnStateChange = true;
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
            if (destroyOnStateChange)
            {
                unregisterInStateMachine();
            }
        }

        protected virtual void processValidState()
        {
            invalidTriggered = false;
        }
    }
}
