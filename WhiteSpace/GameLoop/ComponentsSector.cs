using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Components;
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace.GameLoop
{
    public delegate void Update(GameTime gameTime);
    public delegate void Draw(SpriteBatch spriteBatch);

    public interface IComponentsSector
    {
        void addDrawMethod(Draw drawMethodToAdd);
        void addUpdateMethod(Update updateMethodToAdd);
        void removeDrawMethod(Draw drawMethodToRemove);
        void removeUpdateMethod(Update updateToRemove);
    }

    public class ComponentsSector<StateType> : StateListener<StateType>, IComponentsSector where StateType : struct
    {
       
        public List<Update> updateMethodsToExecute = new List<Update>();
        public List<Draw> drawMethodsToExecute = new List<Draw>();

        public delegate void ProcessState();
        public event ProcessState invalidStateMethodsToExecute;
        public event ProcessState validStateMethodsToExecute;

        public void addDrawMethod(Draw drawMethodToAdd)
        {
            if (!drawMethodsToExecute.Contains(drawMethodToAdd))
            {
                drawMethodsToExecute.Add(drawMethodToAdd);
            }
        }

        public void addUpdateMethod(Update updateMethodToAdd)
        {
            if (!updateMethodsToExecute.Contains(updateMethodToAdd))
            {
                updateMethodsToExecute.Add(updateMethodToAdd);
            }
        }

        public void removeUpdateMethod(Update updateMethodToRemove)
        {
            if(updateMethodsToExecute.Contains(updateMethodToRemove))
            {
                updateMethodsToExecute.Remove(updateMethodToRemove);
            }
        }

        public void removeDrawMethod(Draw drawMethodToRemove)
        {
            if(drawMethodsToExecute.Contains(drawMethodToRemove))
            {
                drawMethodsToExecute.Remove(drawMethodToRemove);
            }
        }

        public ComponentsSector(StateType activeState) : base(activeState)
        {
        }

        protected override void processInvalidState()
        {
            base.processInvalidState();
            unregisterInUpdateExecuter();
            if (invalidStateMethodsToExecute != null)
            {
                invalidStateMethodsToExecute();
            }
        }

        protected override void processValidState()
        {
            base.processValidState();
            registerInUpdateExecuter();
            if (validStateMethodsToExecute != null)
            {
                validStateMethodsToExecute();
            }
        }

        private void update(GameTime gameTime)
        {
            if (updateMethodsToExecute.Count != 0)
            {
                for(int i = 0; i < updateMethodsToExecute.Count; i++)
                {
                    updateMethodsToExecute[i](gameTime);
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

        public void destroy()
        {
            unregisterInUpdateExecuter();
            unregisterInStateMachine();
        }
    }
}
