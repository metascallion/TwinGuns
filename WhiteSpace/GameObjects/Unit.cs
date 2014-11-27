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
    public class Unit<StateType> : GameObject<StateType> where StateType : struct
    {
        public TextureRegion<StateType> unitTexture;
       

        public Unit(StateType type, Transform transform, Texture2D texture) : base(type, transform)
        {
            unitTexture = new TextureRegion<StateType>(type, transform, texture);
        }

        public Unit(Transform transform, Texture2D texture) : base (transform)
        {
            unitTexture = new TextureRegion<StateType>(transform, texture);
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            testMovement(gameTime);
        }

        public void testMovement(GameTime time)
        {
            float elapsedTime = (float)time.ElapsedGameTime.TotalMilliseconds;

            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.transform.translateOnXAxis(elapsedTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.transform.translateOnXAxis(-elapsedTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.transform.translateOnYAxis(elapsedTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.transform.translateOnYAxis(-elapsedTime);
            }
        }
    }
}
