using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BachelorPrototype.GameClasses
{
    public class ColorBoxDrawer : Drawable
    {
        private Texture2D texture;
        Color[] colorContainer;
        public static GraphicsDevice gpsDevice;

        public ColorBoxDrawer(Vector2 size)
        {
            texture = new Texture2D(gpsDevice, (int)size.X, (int)size.Y);
            colorContainer = new Color[(int)size.X * (int)size.Y];
            this.Size = size;
        }

        public void setColor(Color color)
        {
            for (int i = 0; i < colorContainer.Length; i++)
            {
                colorContainer[i] = color;
            }
            texture.SetData<Color>(colorContainer);
        }

        public override void draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y), Color.White);
        }
    }
}
