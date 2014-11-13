using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace BachelorPrototype.GameClasses
{
    public static class DrawableCollector
    {
        public static  List<Drawable> drawables = new List<Drawable>();

        public static  void draw(SpriteBatch batch)
        {
            for (int i = 0; i < drawables.Count; i++ )
            {

                drawables[i].draw(batch);
            
            }
        }
    }
}
