using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Temp;
using WhiteSpace.Components.Drawables;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Components
{
    public class DroneAmp : UpdateableComponent
    {
        public float time = 0;
        public bool hasDrone = false;

        public DroneAmp()
        {
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            time -= gameTime.ElapsedGameTime.Milliseconds;

            if (hasDrone)
            {
                if (time >= 1000)
                {
                    this.parent.getComponent<ColoredBox>().setColor(Color.Red);
                }

                else if (time >= 0)
                {
                    this.parent.getComponent<ColoredBox>().setColor(Color.Yellow);
                }

                else
                {
                    this.parent.getComponent<ColoredBox>().setColor(Color.Green);
                }
            }

            else
            {
                this.parent.getComponent<ColoredBox>().setColor(Color.Black);
            }
        }
    }
}
