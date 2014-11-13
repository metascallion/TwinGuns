using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BachelorPrototype.GameClasses
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Rectangle getRect()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }
    }
}
