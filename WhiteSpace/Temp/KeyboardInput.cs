using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WhiteSpace.Temp
{
    public static class KeyboardInput
    {
        static Dictionary<Keys, bool> keyStates = new Dictionary<Keys, bool>();

        public static void updateKeyStates()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if(isKeyDown(key))
                {
                    keyStates[key] = true;
                }
                else
                {
                    keyStates[key] = false;
                }
            }
        }

        public static bool isKeyDown(Keys key)
        {
            if(Keyboard.GetState().IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool isKeyUp(Keys key)
        {
            if(Keyboard.GetState().IsKeyDown(key))
            {
                return false;
            }
            return true;
        }

        public static bool wasKeyJustPressed(Keys key)
        {
            if (isKeyDown(key))
            {
                if (!keyStates[key])
                {
                    keyStates[key] = true;
                    return true;
                }
            }
            return false;
        }

        public static bool wasKeyJustReleased(Keys key)
        {
            if (isKeyUp(key))
            {
                if (keyStates[key])
                {
                    keyStates[key] = false;
                    return true;
                }
            }
            return false;
        }
    }
}
