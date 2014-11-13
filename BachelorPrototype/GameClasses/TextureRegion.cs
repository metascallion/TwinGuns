using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BachelorPrototype.GameClasses
{
    public class TextureRegion : Drawable
    {
        public Texture2D Texture { get; set; }
        public Vector2 Offset { get; set; }

        public override void draw(SpriteBatch batch)
        {
            base.draw(batch);
            batch.Draw(this.Texture, new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y), new Rectangle((int)Offset.X * (int)Size.X, (int)Offset.Y * (int)Size.Y, (int)Size.X, (int)Size.Y), Color.White);
        }
    }
}
