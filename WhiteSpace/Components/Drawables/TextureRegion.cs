using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;

namespace WhiteSpace.Components.Drawables
{
    public class TextureRegion : DrawableComponent
    {
        public Rectangle VisibleArea { get; set; }
        public Texture2D Texture { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        Transform transform;

        public TextureRegion()
        {
        }

        public TextureRegion(int layer) : base(layer)
        {

        }

        public TextureRegion(Texture2D texture)
        {
            this.Texture = texture;
            this.VisibleArea = texture.Bounds;
        }

        public TextureRegion(Texture2D texture, int layer) : base (layer)
        {
            this.Texture = texture;
            this.VisibleArea = texture.Bounds;
        }

        public TextureRegion(Texture2D texture, SpriteEffects effect)
        {
            this.Texture = texture;
            this.VisibleArea = texture.Bounds;
            this.SpriteEffect = effect;
        }

        public TextureRegion(Texture2D texture, SpriteEffects effect, int layer) : base (layer)
        {
            this.Texture = texture;
            this.VisibleArea = texture.Bounds;
            this.SpriteEffect = effect;
        }

        public override void start()
        {
            base.start();
            this.transform = this.parent.getComponent<Transform>();
        }

        public void dontDrawBeforeAnimation()
        {
            this.VisibleArea = Rectangle.Empty;
        }

        protected override void draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(this.VisibleArea.Width / 2, this.VisibleArea.Height / 2);
            Vector2 position = this.transform.Position;
            Vector2 size = this.transform.Size;
            Rectangle drawRectangle = new Rectangle((int)(position.X + size.X / 2), (int)(position.Y + size.Y / 2), (int)size.X, (int)size.Y);
            if (this.transform.Rotation < -1.7f || this.transform.Rotation > 1.7f)
            {
                spriteBatch.Draw(this.Texture, drawRectangle, this.VisibleArea, Color.White, this.transform.Rotation, origin, SpriteEffects.FlipVertically, 0);
            }

            else
            {
                spriteBatch.Draw(this.Texture, drawRectangle, this.VisibleArea, Color.White, this.transform.Rotation, origin, this.SpriteEffect, 0);
            }
        }
    }
}
