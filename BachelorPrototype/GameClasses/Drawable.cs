using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace BachelorPrototype.GameClasses
{
    public class Drawable : Transform
    {
        public state activeState;

        public virtual void draw(SpriteBatch batch)
        {
        }
    }
}
