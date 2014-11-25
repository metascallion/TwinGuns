using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace
{

    public class Drawable <T>
    {
        public T activeState;

        public Drawable(T activeState)
        {
            this.activeState = activeState;
            registerInDrawExecuter();
            StateMachine<T>.getInstance().stateChangeMethods += processStateChange;
        }

        private void processStateChange(T active)
        {
            if(!EqualityComparer<T>.Default.Equals(activeState, active))
            {
                unregisterInDrawExecuter();
            }
            
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

    /*
    public class TestDrawer : Drawable
    {
        protected override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
        }
    }
    */

    public class GameObject
    {
        public GameObject()
        {
            registerInUpdateExecuter();
        }

        protected virtual void update(GameTime gameTime)
        {  
        }

        private void registerInUpdateExecuter()
        {
            UpdateExecuter.registerUpdateable(this.update);
        }

        private void unregisterInUpdateExecuter()
        {
            UpdateExecuter.unregisterUpdateable(this.update);
        }
    }

    
    public class Unit : GameObject
    {
        //TestDrawer drawer;

        public Unit()
        {
            //drawer = new TestDrawer();
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }
    }


}
