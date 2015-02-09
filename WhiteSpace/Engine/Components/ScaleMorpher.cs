using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Composite;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Components
{
    class ScaleMorpher : UpdateableComponent
    {
        float speed = 0;
        bool grow = true;
        float maxGrowPercent;
        float growPercent = 0;

        public ScaleMorpher(float maxGrowPercent, float speed)
        {
            this.speed = speed;
            this.maxGrowPercent = maxGrowPercent;
        }


        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (grow)
            {
                growPercent += speed;
                this.parent.getComponent<Transform>().Size *= 1 + speed;
                this.parent.getComponent<Transform>().position = this.parent.getComponent<Transform>().position - new Vector2(speed / 2, speed / 2);

                if(growPercent >= maxGrowPercent)
                {
                    grow = false;
                }
            }

            else
            {
                growPercent -= speed;
                this.parent.getComponent<Transform>().Size *= 1 - speed;
                this.parent.getComponent<Transform>().position = this.parent.getComponent<Transform>().position + new Vector2(speed / 2, speed / 2);

                if(growPercent <= 0)
                {
                    grow = true;
                }
            }

        }
    }
}
