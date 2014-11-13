using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BachelorPrototype.GameClasses
{
    public class TextDrawer : Drawable
    {
        public static SpriteFont font;
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public TextDrawer()
        {
            this.TextColor = Color.Black;
            this.Text = "";
        }

        public override void draw(SpriteBatch batch)
        {
            if (this.activeState == StateContainer.getInstance().getState())
            {
                batch.DrawString(font, this.Text, this.Position, this.TextColor);
            }
        }
    }
}
