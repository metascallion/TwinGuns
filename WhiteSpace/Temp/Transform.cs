using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Temp
{
    public class Transform
    {
        public Vector2 position;
        public Vector2 rotation;
        public Vector2 size;

        public Transform(Vector2 position = new Vector2(), Vector2 rotation = new Vector2(), Vector2 size = new Vector2())
        {
            this.position = position;
            this.rotation = rotation;
            this.size = size;
        }
    }
}
