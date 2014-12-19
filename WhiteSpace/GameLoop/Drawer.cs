using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Components;

namespace WhiteSpace.GameLoop
{
    //public class Drawer <StateType> : StateListener<StateType> where StateType : struct
    //{
    //    public delegate void Draw(SpriteBatch spriteBatch);

    //    public List<Draw> drawMethodsToExecute = new List<Draw>();


    //    public void addDrawMethod(Draw drawMethodToAdd)
    //    {
    //        if(!drawMethodsToExecute.Contains(drawMethodToAdd))
    //        {
    //            drawMethodsToExecute.Add(drawMethodToAdd);
    //        }
    //    }

    //    public void addDrawable(Drawable drawable)
    //    {
    //        drawable.addToDrawable<StateType>(this);
    //    }

    //    public Drawer(StateType activeState) : base (activeState)
    //    {
    //        registerInDrawExecuter();
    //    }

    //    protected override void processInvalidState()
    //    {
    //        base.processInvalidState();
    //        unregisterInDrawExecuter();
    //    }

    //    protected override void processValidState()
    //    {
    //        base.processValidState();
    //        registerInDrawExecuter();
    //    }

    //    private void draw(SpriteBatch spriteBatch)
    //    {
    //        if (drawMethodsToExecute.Count != 0)
    //        {
    //            foreach (Draw draw in drawMethodsToExecute)
    //            {
    //                draw(spriteBatch);
    //            }
    //        }
    //    }

    //    private void registerInDrawExecuter()
    //    {
    //        DrawExecuter.registerDrawMethod(this.draw);
    //    }

    //    private void unregisterInDrawExecuter()
    //    {
    //        DrawExecuter.unregisterDrawMethod(this.draw);
    //    }
    //}
}
