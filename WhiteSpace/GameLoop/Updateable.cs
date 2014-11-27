using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameLoop
{
    public class Updateable<StateType> : StateListener<StateType> where StateType : struct
    {
        public Updateable()
        {
            registerInUpdateExecuter();
        }

        public Updateable(StateType activeState) : base(activeState)
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
