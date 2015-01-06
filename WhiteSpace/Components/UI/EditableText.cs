using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components.Drawables;
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

    public class EditableText : Clickable
    {
        bool write = false;
        bool capslock = false;
        public TextDrawer textDrawer;
        ColoredBox backGroundDrawer;

        public EditableText(Transform transform) : base (transform)
        {
            backGroundDrawer = new ColoredBox(Color.Gray);
            textDrawer = new TextDrawer(transform, "Font", "");
        }

        protected override void onClick()
        {
            base.onClick();
            write = true;
            backGroundDrawer.setColor(Color.Silver);
        }

        protected override void onHoverLeave()
        {
            if(checkClick())
            {
                write = false;
                backGroundDrawer.setColor(Color.Gray);
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
                    textDrawer.text += "|";
                }

                ticked = !ticked;
            }
        }


        public bool started = false;
        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);

            if (!started)
            {
                this.parent.addComponent(backGroundDrawer);
                started = true;
            }
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

                pollnput();

                blink(gameTime);
            }

            this.parent.removeComponent(this.textDrawer);
            this.parent.addComponent(this.textDrawer);
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

        private void pollnput()
        {
            if (KeyboardInput.getJustPressedKeys().Count != 0)
            {
                string test = KeyboardInput.getKeyString(KeyboardInput.getJustPressedKeys()[0]);

                CutLastSign();

                if(test != "")
                {
                    changeText(test, ref textDrawer.text);
                }
            }
        }

        private void CutLastSign()
        {
            if (textDrawer.text != "")
            {
                if (textDrawer.text[textDrawer.text.Length - 1] == '|')
                {
                    textDrawer.text = textDrawer.text.Remove(textDrawer.text.Length - 1);
                }
            }
        }

    }
}
