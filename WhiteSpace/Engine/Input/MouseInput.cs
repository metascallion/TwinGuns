using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WhiteSpace.Input
{
    public enum mousebutton
    {
        left,
        right
    }

    public static class MouseInput
    {
        private static Dictionary<mousebutton, bool> keyStates = new Dictionary<mousebutton, bool>();

        public static void start()
        {
            keyStates[mousebutton.left] = false;
            keyStates[mousebutton.right] = false;
        }

        public static void updateKeyStates()
        {
            foreach (mousebutton key in Enum.GetValues(typeof(mousebutton)))
            {
                if (isKeyDown(key))
                {
                    keyStates[key] = true;
                }
                else
                {
                    keyStates[key] = false;
                }
            }
        }

        public static bool isKeyDown(mousebutton key)
        {
            if (key == mousebutton.left)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }

            else if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }

        public static bool isKeyUp(mousebutton key)
        {
            if(key == mousebutton.left)
            {
                if(Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    return true;
                }
            }

            else if(Mouse.GetState().RightButton == ButtonState.Released)
            {
                return true;
            }

            return false;
        }

        public static bool wasKeyJustPressed(mousebutton key)
        {
            if (isKeyDown(key))
            {
                if (!keyStates[key])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool wasKeyJustReleased(mousebutton key)
        {
            if (isKeyUp(key))
            {
                if (keyStates[key])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
