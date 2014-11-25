using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace
{

    public class Drawable
    {
        public Drawable()
        {
            registerInDrawExecuter();
        }

        protected virtual void draw(SpriteBatch spriteBatch)
        {

        }
        private void registerInDrawExecuter()
        {
            DrawExecuter.registerDrawMethod(this.draw);
        }

        public void unregisterInDrawExecuter()
        {
            DrawExecuter.unregisterDrawMethod(this.draw);
        }
    }


    public class TestDrawer : Drawable
    {
        protected override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }


    public class GameObject
    {
        public GameObject()
        {
            registerInUpdateExecuter();
        }

        protected virtual void update(GameTime gameTime)
        {  
        }

        public void registerInUpdateExecuter()
        {
            UpdateExecuter.registerUpdateable(this.update);
        }

        public void unregisterInUpdateExecuter()
        {
            UpdateExecuter.unregisterUpdateable(this.update);
        }
    }

    /*
    public class Unit : GameObject
    {
        TestDrawer drawer;

        public Unit()
        {
            drawer = new TestDrawer();
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }
    }
 */


}
