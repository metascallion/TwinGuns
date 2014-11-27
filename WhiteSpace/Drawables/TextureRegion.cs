using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;

namespace WhiteSpace.Drawables
{
    public class TextureRegion<StateType> : Drawable<StateType>
    {
        Texture2D textureToDraw;
        Rectangle visibleArea;

        public TextureRegion(StateType stateType, Transform transform, Texture2D texture) : base(stateType, transform)
        {
            this.textureToDraw = texture;
        }

        protected override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            Rectangle drawRectangle = new Rectangle((int)this.transform.position.X, (int)this.transform.position.Y, (int)this.transform.size.X, (int)this.transform.size.Y);
            spriteBatch.Draw(this.textureToDraw, drawRectangle, Color.White);  
        }

    }
}
