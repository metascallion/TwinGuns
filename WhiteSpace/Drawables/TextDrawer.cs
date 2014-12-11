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
        public string Text { get; set; }
        public Color TextColor { get; set; }

        public TextDrawer(StateType activeState, Transform transform, string fontName, string text) : base(activeState, transform)
        {
            this.Text = text;
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.Black;
        }

        public TextDrawer(StateType activeState, Transform transform, string fontName) : base(activeState, transform)
        {
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.Black;
            this.Text = "";
        }

        public TextDrawer(Transform transform, string fontName) : base (transform)
        {
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.Black;
            this.Text = "";
        }

        public TextDrawer(Transform transform, string fontName, string text) : base(transform)
        {
            this.Text = text;
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.Black;
        }

        protected override void draw(SpriteBatch batch)
        {
            batch.DrawString(font, this.Text, this.transform.Position, this.TextColor);
        }
    }
}
