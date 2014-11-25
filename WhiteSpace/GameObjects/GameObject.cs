using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WhiteSpace
{
    public class GameObject<StateType> : StateListener<StateType>
    {

        public GameObject(StateType activeState)
            : base(activeState)
        {
            registerInUpdateExecuter();
        }

        protected override void processInvalidState()
        {
            base.processInvalidState();
            unregisterInUpdateExecuter();
        }

        protected override void processValidState()
        {
            base.processValidState();
            registerInUpdateExecuter();
        }

        protected virtual void update(GameTime gameTime)
        {
        }

        private void registerInUpdateExecuter()
        {
            UpdateExecuter.registerUpdateable(this.update);
        }

        private void unregisterInUpdateExecuter()
        {
            UpdateExecuter.unregisterUpdateable(this.update);
        }
    }
}
