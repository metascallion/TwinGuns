using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;

namespace WhiteSpace.Components.Drawables
{
    public class TextDrawer : DrawableComponent
    {
        private SpriteFont font;
        public string text = "";
        public Color TextColor { get; set; }
        Transform transform;

        public TextDrawer(Transform transform, string fontName, string text)
        {
            this.transform = transform;
            this.text = text;
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.White;
        }

        protected override void draw(SpriteBatch batch)
        {
            batch.DrawString(font, this.text, this.transform.Position, this.TextColor);
        }
    }
}
