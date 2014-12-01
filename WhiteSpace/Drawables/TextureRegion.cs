using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Drawables
{
    public class TextureRegion<StateType> : Drawable<StateType> where StateType : struct
    {
        //Texture2D textureToDraw;
        public Rectangle VisibleArea { get; set; }
        public SpriteSheet sheetToReferTo { get; set; }

        public SpriteEffects SpriteEffect { private get; set; }

        public TextureRegion(StateType stateType, Transform transform, SpriteSheet spriteSheetToUse) : base(stateType, transform)
        {
            this.sheetToReferTo = spriteSheetToUse;
            this.VisibleArea = spriteSheetToUse.getRectangleForFrame(0);
        }

        public TextureRegion(Transform transform, SpriteSheet spriteSheetToUse)
            : base(transform)
        {
            this.sheetToReferTo = spriteSheetToUse;
            this.VisibleArea = spriteSheetToUse.getRectangleForFrame(0);
        }

        protected override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            Rectangle drawRectangle = new Rectangle((int)this.transform.Position.X, (int)this.transform.Position.Y, (int)this.transform.Size.X, (int)this.transform.Size.Y);
            //spriteBatch.Draw(this.sheetToReferTo.Texture, drawRectangle, Color.White);
            spriteBatch.Draw(this.sheetToReferTo.Texture, drawRectangle, VisibleArea, Color.White, 0, Vector2.Zero, this.SpriteEffect, 0);
        }
    }
}
