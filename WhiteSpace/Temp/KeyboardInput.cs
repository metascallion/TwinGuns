using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace WhiteSpace.Temp
{
    public static class KeyboardInput
    {
        static Dictionary<Keys, bool> keyStates = new Dictionary<Keys, bool>();
        public static Dictionary<Keys, string> keyStringValues = new Dictionary<Keys, string>();
        static StreamReader reader = new StreamReader("Keys.txt");
        static List<Keys> keys = new List<Keys>();


        public static void start()
        {
            string result;
            while( (result = reader.ReadLine()) != "")
            {
                string[] splitResult = result.Split(' ');

                Keys keys = Keys.None;
                Enum.TryParse<Keys>(splitResult[0], out keys);
                keyStringValues[keys] = splitResult[1];
            }
        }

        public static string getKeyString(Keys keys)
        {
            if(keyStringValues.Keys.Contains(keys))
            {
                return keyStringValues[keys];
            }

            return "";
        }

        public static List<Keys> getJustPressedKeys()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if(wasKeyJustPressed(key))
                {
                    keys.Add(key);
                }
            }
            return keys;
        }

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
                keys.Clear();
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
