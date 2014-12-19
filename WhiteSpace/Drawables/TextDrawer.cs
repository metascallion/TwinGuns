using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Drawables
{
    public class TextDrawer<StateType> : Drawable<StateType> where StateType : struct
    {
        private SpriteFont font;
        public string text = "";
        public Color TextColor { get; set; }

        public TextDrawer(Transform transform, string fontName, string text, Updater<StateType> updaterToRegisterTo)
            : base(transform, updaterToRegisterTo)
        {
            this.text = text;
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.Black;
        }

        protected override void draw(SpriteBatch batch)
        {
            batch.DrawString(font, this.text, this.transform.Position, this.TextColor);
        }
    }

}
