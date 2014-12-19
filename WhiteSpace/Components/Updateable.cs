using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Components
{
    public class Updateable<StateType> where StateType : struct
    {
        Updater<StateType> updater;

        public Updateable(Updater<StateType> updaterToRegisterTo)
        {
            this.updater = updaterToRegisterTo;
            registerInUpdater();
        }

        public void registerInUpdater()
        {
            updater.addUpdateMethod(this.update);
        }

        public void unregisterInUpdater()
        {
            updater.updateMethodsToExecute.Remove(this.update);
        }

        protected virtual void update(GameTime gameTime)
        {
        }
    }
}
