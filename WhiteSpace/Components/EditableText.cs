using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Drawables;
using WhiteSpace.Temp;

namespace WhiteSpace.Components
{
    public enum inputtype
    {
        letter, 
        number,
        special
    }

    public class EditableText <StateType> : Clickable<StateType> where StateType : struct
    {
        bool write = false;
        bool capslock = false;
        TextDrawer<StateType> textDrawer;
        

        public EditableText(Transform transform) : base (transform)
        {
            textDrawer = new TextDrawer<StateType>(transform, "Font");
        }

        protected override void onClick()
        {
            base.onClick();
            write = true;
        }

        protected override void onHoverLeave()
        {
            if(checkClick())
            {
                write = false;
            }
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if(write)
            {
                if (KeyboardInput.wasKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.LeftShift) || KeyboardInput.wasKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.RightShift))
                {
                    capslock = !capslock;
                }

                if (KeyboardInput.wasKeyJustReleased(Microsoft.Xna.Framework.Input.Keys.LeftShift) || KeyboardInput.wasKeyJustReleased(Microsoft.Xna.Framework.Input.Keys.RightShift))
                {
                    capslock = !capslock;
                }

                test();

                textDrawer.text
            }
        }


        private inputtype getInputType(string input)
        {
            if(Char.IsNumber(input, 0))
            {
                return inputtype.number;
            }

            else if(Char.IsLetter(input, 0))
            {
                return inputtype.letter;
            }
            return inputtype.special;
        }

        private void processSpecialSign(string input, ref string result)
        {
            switch(input)
            {
                case "§":
                    result += " ";
                    break;
                case "<":
                    result = result.Remove(result.Length - 1);
                    break;
                case "%":
                    result += "\n";
                    break;
                case "=":
                    capslock = !capslock;
                    break;
                case ".":
                    result += ".";
                    break;
                case ",":
                    result += ",";
                    break;
            }
        }

        private void changeText(string input, ref string result)
        {
            if(getInputType(input) == inputtype.letter)
            {
                if(capslock)
                {
                    result += input;
                }

                else
                {
                    result += Char.ToLower(Char.Parse(input));
                }
            }

            else if(getInputType(input) == inputtype.number)
            {
                result += input;
            }

            else
            {
                processSpecialSign(input, ref result);
            }
        }

        private void test()
        {
            if (KeyboardInput.getJustPressedKeys().Count != 0)
            {
                string test = KeyboardInput.getKeyString(KeyboardInput.getJustPressedKeys()[0]);
                if(test != "")
                {
                    changeText(test, ref textDrawer.text);
                }
            }
        }

    }
}
