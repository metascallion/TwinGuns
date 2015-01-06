using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteSpace
{
    public class StateListener<StateType> where StateType : struct
    {
        public List<StateType> activeStates = new List<StateType>();
        bool invalidTriggered = true;
        bool destroyOnStateChange = false;

        public StateListener(StateType activeState)
        {
            this.activeStates.Add(activeState);
            registerInStateMachine();
        }

        public void addActiveState(StateType activeStateToAdd)
        {
            this.activeStates.Add(activeStateToAdd);
        }

        public void destroyOnInvalidState()
        {
            destroyOnStateChange = true;
        }

        public void reload()
        {
            destroyOnStateChange = false;
            registerInStateMachine();
        }

        protected void registerInStateMachine()
        {
            StateMachine<StateType>.getInstance().stateChangeMethods += processStateChange;
        }

        private void processStateChange(StateType activeState)
        {
            bool invalidState = true;
            foreach(StateType state in this.activeStates)
            {
                if (EqualityComparer<StateType>.Default.Equals(state, activeState))
                {
                    invalidState = false;
                }
            }
            if(invalidState)
            {
                processInvalidState();
            }
            else if (invalidTriggered)
            {
                processValidState();
            }
        }

        protected void unregisterInStateMachine()
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
