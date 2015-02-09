using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Composite;
using WhiteSpace.Content;

namespace WhiteSpace.Components.Drawables
{
    public class TextDrawer : DrawableComponent
    {
        private SpriteFont font;
        public string text = "";
        public Color TextColor { get; set; }
        public Transform transform;

        public TextDrawer()
        {
        }

        public TextDrawer(string fontName, string text)
        {
            this.text = text;
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.White;
        }

        public TextDrawer(string fontName, string text, int layer) : base(layer)
        {
            this.text = text;
            this.font = ContentLoader.getContent<SpriteFont>(fontName);
            this.TextColor = Color.White;
        }

        public override void start()
        {
            base.start();
            this.transform = this.parent.getComponent<Transform>();
        }
        protected override void draw(SpriteBatch batch)
        {
            batch.DrawString(font, this.text, this.transform.position, this.TextColor);
        }
    }
}
