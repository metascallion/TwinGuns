using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Collision;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameLoop
{
    public static class CollisionDetection
    {
        public static World world = new World(new Vector2(0, 4.8f));

        public static void update(GameTime gameTime)
        {
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
