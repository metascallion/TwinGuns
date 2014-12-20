using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Components;
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace.GameLoop
{

    public class ComponentsSector<StateType> : StateListener<StateType> where StateType : struct
    {

        public delegate void Update(GameTime gameTime);
        public List<Update> updateMethodsToExecute = new List<Update>();

        public delegate void Draw(SpriteBatch spriteBatch);
        public List<Draw> drawMethodsToExecute = new List<Draw>();

        public void addDrawMethod(Draw drawMethodToAdd)
        {
            if (!drawMethodsToExecute.Contains(drawMethodToAdd))
            {
                drawMethodsToExecute.Add(drawMethodToAdd);
            }
        }

        public void addDrawable(Drawable<StateType> drawable)
        {
            drawable.registerInUpdater();
        }

        public void addUpdateMethod(Update updateMethodToAdd)
        {
            if (!updateMethodsToExecute.Contains(updateMethodToAdd))
            {
                updateMethodsToExecute.Add(updateMethodToAdd);
            }
        }

        public void addUpdateable(Updateable<StateType> updateableToAdd)
        {
            updateableToAdd.registerInUpdater();
        }

        public ComponentsSector(StateType activeState) : base(activeState)
        {
            registerInUpdateExecuter();
        }

        protected override void processInvalidState()
        {
            base.processInvalidState();
            unregisterInUpdateExecuter();
        }

        protected override void processValidState()
        {
            base.processValidState();
            registerInUpdateExecuter();
        }

        private void update(GameTime gameTime)
        {
            if (updateMethodsToExecute.Count != 0)
            {
                foreach (Update update in updateMethodsToExecute)
                {
                    update(gameTime);
                }
            }
        }

        private void draw(SpriteBatch spriteBatch)
        {
                 if (drawMethodsToExecute.Count != 0)
                 {
                     foreach (Draw draw in drawMethodsToExecute)
                     {
                         draw(spriteBatch);
                     }
                 }
        }

        private void registerInUpdateExecuter()
        {
            UpdateExecuter.registerUpdateable(this.update);
            DrawExecuter.registerDrawMethod(this.draw);
        }

        private void unregisterInUpdateExecuter()
        {
            UpdateExecuter.unregisterUpdateable(this.update);
            DrawExecuter.unregisterDrawMethod(this.draw);
        }
    }
}
