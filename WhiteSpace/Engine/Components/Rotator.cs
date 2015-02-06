using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Composite;

namespace WhiteSpace.Components
{
    public class Rotator : UpdateableComponent
    {
        private float speed;

        public Rotator(float speed)
        {
            this.speed = speed;
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.parent.getComponent<Transform>().Rotation = this.parent.getComponent<Transform>().Rotation + speed;
        }
    }
}
