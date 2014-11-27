using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace.GameLoop
{
    public static class DrawExecuter
    {
        public delegate void Draw(SpriteBatch spriteBatch);
        private static event Draw drawMethods;

        public static void registerDrawMethod(Draw drawMethodToRegister)
        {
             drawMethods += drawMethodToRegister;
        }

        public static void unregisterDrawMethod(Draw drawMethodToUnregister)
        {
            drawMethods -= drawMethodToUnregister;
        }

        public static void executeRegisteredDrawMethods(SpriteBatch spriteBatch)
        {
            if (drawMethods != null)
            {
                drawMethods(spriteBatch);
            }
        }
    }
}
