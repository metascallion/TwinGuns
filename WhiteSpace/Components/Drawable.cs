using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Components
{
    public class Drawable<StateType> where StateType : struct
    {
        protected Transform transform;
        ComponentsSector<StateType> updater;

        public Drawable(Transform transform, ComponentsSector<StateType> updaterToRegisterTo)
        {
            this.transform = transform;
            this.updater = updaterToRegisterTo;
            registerInUpdater();
        }

        public void registerInUpdater()
        {
            updater.addDrawMethod(this.draw);
        }

        public void unregisterInUpdater()
        {
            updater.drawMethodsToExecute.Remove(this.draw);
        }

        protected virtual void draw(SpriteBatch spriteBatch)
        {
        }
    }
}
