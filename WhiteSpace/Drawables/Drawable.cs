using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace
{
    public class Drawable <StateType> : StateListener<StateType>
    {
        public Drawable(StateType activeState) : base (activeState)
        {
            registerInDrawExecuter();
        }

        protected override void processInvalidState()
        {
            base.processInvalidState();
            unregisterInDrawExecuter();
        }

        protected override void processValidState()
        {
            base.processValidState();
            registerInDrawExecuter();
        }

        protected virtual void draw(SpriteBatch spriteBatch)
        {

        }
        private void registerInDrawExecuter()
        {
            DrawExecuter.registerDrawMethod(this.draw);
        }

        public void unregisterInDrawExecuter()
        {
            DrawExecuter.unregisterDrawMethod(this.draw);
        }
    }
}
