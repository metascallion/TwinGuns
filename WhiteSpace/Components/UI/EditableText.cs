using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Drawables;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Components
{
    public enum inputtype
    {
        letter, 
        number,
        special
    }

    public class EditableText<StateType> : Clickable<StateType> where StateType : struct
    {
        bool write = false;
        bool capslock = false;
        public TextDrawer<StateType> textD;
        ColoredBox<StateType> backGroundD;

        public EditableText(Transform transform, Updater<StateType> updaterToRegisterTo) : base (transform, updaterToRegisterTo)
        {
            //Drawer<StateType> backGroundDrawer = new Drawer<StateType>(activeState);
            //Drawer<StateType> textDrawer = new Drawer<StateType>(activeState);

            backGroundD = new ColoredBox<StateType>(transform, Color.Silver, updaterToRegisterTo);
            textD = new TextDrawer<StateType>(transform, "Font", "", updaterToRegisterTo);

            //backGroundD.addToDrawable<StateType>(backGroundDrawer);
            //textD.addToDrawable<StateType>(textDrawer);
        }

        protected override void onClick()
        {
            base.onClick();
            write = true;
            backGroundD.setColor(Color.PaleGoldenrod);
        }

        protected override void onHoverLeave()
        {
            if(checkClick())
            {
                write = false;
                backGroundD.setColor(Color.Silver);
                CutLastSign();
            }
        }

        protected override void onHover()
        {
            base.onHover();
        }


        float elapsed = 0;
        float tickSpeed = 200f;
        bool ticked = false;
        private void blink(Microsoft.Xna.Framework.GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;

            if(elapsed >= tickSpeed)
            {
                elapsed = 0;

                if(ticked)
                {
                    CutLastSign();
                }

                else
                {
                    textD.text += "|";
                }

                ticked = !ticked;
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

                blink(gameTime);
                //textDrawer.text
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
                case "/":
                    result += " ";
                    break;
                case "<":
                    if (result != "")
                    {
                       result = result.Remove(result.Length - 1);
                    }
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

                CutLastSign();

                if(test != "")
                {
                    changeText(test, ref textD.text);
                }
            }
        }

        private void CutLastSign()
        {
            if (textD.text != "")
            {
                if (textD.text[textD.text.Length - 1] == '|')
                {
                    textD.text = textD.text.Remove(textD.text.Length - 1);
                }
            }
        }

    }
}
