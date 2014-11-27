using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;

namespace WhiteSpace.Drawables
{
    public class TextureRegion<StateType> : Drawable<StateType> where StateType : struct
    {
        Texture2D textureToDraw;
        Rectangle visibleArea;

        public TextureRegion(StateType stateType, Transform transform, Texture2D texture) : base(stateType, transform)
        {
            this.textureToDraw = texture;
        }

        public TextureRegion(Transform transform, Texture2D texture) : base(transform)
        {
            this.textureToDraw = texture;
        }

        protected override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            Rectangle drawRectangle = new Rectangle((int)this.transform.Position.X, (int)this.transform.Position.Y, (int)this.transform.Size.X, (int)this.transform.Size.Y);
            spriteBatch.Draw(this.textureToDraw, drawRectangle, Color.White);  
        }

    }
}
