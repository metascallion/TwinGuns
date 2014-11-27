using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Temp;

namespace WhiteSpace
{
    public class Drawable <StateType> : StateListener<StateType> where StateType : struct
    {
        protected Transform transform;

        public Drawable(StateType activeState, Transform transform) : base (activeState)
        {
            this.transform = transform;
            registerInDrawExecuter();
        }

        public Drawable(Transform transform)
        {
            this.transform = transform;
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
