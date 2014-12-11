using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Components;

namespace WhiteSpace.Drawables
{
    public class ColoredBox <StateType> : Drawable<StateType> where StateType : struct
    {
        private Texture2D texture;
        Color[] colorContainer;
        public static GraphicsDevice device = Game1.graphics.GraphicsDevice;
        public ColoredBox(Transform transform, Color color) : base(transform)
        {
            texture = new Texture2D(device, (int)transform.Size.X, (int)transform.Size.Y);
            colorContainer = new Color[(int)transform.Size.X * (int)transform.Size.Y];
            setColor(color);
        }

        public ColoredBox(Transform transform)
            : base(transform)
        {
            texture = new Texture2D(device, (int)transform.Size.X, (int)transform.Size.Y);
            colorContainer = new Color[(int)transform.Size.X * (int)transform.Size.Y];
        }

        public ColoredBox(Transform transform, StateType stateType)
            : base(stateType, transform)
        {
            texture = new Texture2D(device, (int)transform.Size.X, (int)transform.Size.Y);
            colorContainer = new Color[(int)transform.Size.X * (int)transform.Size.Y];
        }

        public ColoredBox(Transform transform, StateType stateType, Color color)
            : base(stateType, transform)
        {
            texture = new Texture2D(device, (int)transform.Size.X, (int)transform.Size.Y);
            colorContainer = new Color[(int)transform.Size.X * (int)transform.Size.Y];
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
            batch.Draw(texture, new Rectangle((int)this.transform.Position.X, (int)this.transform.Position.Y, (int)this.transform.Size.X, (int)this.transform.Size.Y), Color.White);
        }
    }
}
