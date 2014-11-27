using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Drawables;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameObjects
{
    public class Unit<StateType> : GameObject<StateType>
    {
        public TextureRegion<StateType> unitTexture;
       

        public Unit(StateType type, Transform transform, Texture2D texture) : base(type, transform)
        {
            unitTexture = new TextureRegion<StateType>(type, transform, texture);
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            testMovement();
        }

        public void testMovement()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.transform.position.X += 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.transform.position.X -= 1;
            }
        }
    }
}
