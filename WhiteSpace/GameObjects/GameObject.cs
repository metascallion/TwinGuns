using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;

namespace WhiteSpace
{
    public class GameObject<StateType> : StateListener<StateType>
    {
        protected Transform transform;

        public GameObject(StateType activeState, Transform transform)
            : base(activeState)
        {
            registerInUpdateExecuter();
            this.transform = transform;
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
