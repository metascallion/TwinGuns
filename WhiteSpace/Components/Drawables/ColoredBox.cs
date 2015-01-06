using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Components;
using WhiteSpace.Temp;

namespace WhiteSpace.Components.Drawables
{
    public class ColoredBox : DrawableComponent
    {
        private Texture2D texture;
        Color[] colorContainer;
        public static GraphicsDevice device = Game1.graphics.GraphicsDevice;
        Transform transform;
        Color color;

        public ColoredBox(Color color)
        {
            this.color = color;
        }

        public override void start()
        {
            this.transform = this.parent.getComponent<Transform>();
            colorContainer = new Color[(int)transform.Size.X * (int)transform.Size.Y];
            texture = new Texture2D(device, (int)transform.Size.X, (int)transform.Size.Y);
            setColor(color);
        }

        public void setColor(Color color)
        {
            for (int i = 0; i < colorContainer.Length; i++)
            {
                colorContainer[i] = color;
            }
            texture.SetData<Color>(colorContainer);
        }

        protected override void draw(SpriteBatch batch)
        {
            Vector2 origin = new Vector2(this.texture.Width / 2, this.texture.Height / 2);
            batch.Draw(texture, new Rectangle((int)(this.transform.Position.X + transform.Size.X / 2), (int)(this.transform.Position.Y + transform.Size.Y / 2), (int)this.transform.Size.X, (int)this.transform.Size.Y), null, Color.White, transform.Rotation, origin, SpriteEffects.None, 0);
        }
    }
}
