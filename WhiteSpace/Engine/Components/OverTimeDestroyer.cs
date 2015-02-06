using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Composite;

namespace WhiteSpace.Components
{
    public class OverTimeDestroyer : UpdateableComponent
    {
        float milliSecondsToDestroy;

        public OverTimeDestroyer()
        {
        }
      
        public OverTimeDestroyer(float milliSecondsToDestroy)
        {
            this.milliSecondsToDestroy = milliSecondsToDestroy;
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.milliSecondsToDestroy -= gameTime.ElapsedGameTime.Milliseconds;

            if (this.milliSecondsToDestroy <= 0)
            {
                this.parent.destroy();
            }
        }
    }
}
