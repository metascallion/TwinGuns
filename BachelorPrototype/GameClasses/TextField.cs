using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace BachelorPrototype.GameClasses
{
    public class TextField : ClickableUI
    {
        bool canWrite;
        Keys lastPressedKey;
        public int maximumCharacters;

        public TextField()
        {
            canWrite = false;
            lastPressedKey = Keys.Zoom;
            maximumCharacters = 15;

            ColorBoxDrawer standartColorDrawer = new ColorBoxDrawer(Vector2.One);
            standartColorDrawer.setColor(Color.Blue);
            this.StandartDrawer = standartColorDrawer;

            ColorBoxDrawer hoverColorDrawer = new ColorBoxDrawer(Vector2.One);
            hoverColorDrawer.setColor(Color.AliceBlue);
            this.HoverDrawer= hoverColorDrawer;

            ColorBoxDrawer clickedColorDrawer = new ColorBoxDrawer(Vector2.One);
            clickedColorDrawer.setColor(Color.BlueViolet);
            this.ClickedDrawer = clickedColorDrawer;
        }

        public override void update()
        {
            if(this.activeState == StateContainer.getInstance().getState())
            {
                base.update();
                resetLastPressedKey();
                if (checkHover())
                {
                    if (checkClick())
                    {
                        canWrite = true;
                    }
                }
                writeInputIfSelected();
            }
        }

        private string getCutString(string stringToCut)
        {
            string cuttetString = "";
            for (int i = 0; i < stringToCut.Length - 1; i++)
            {
                cuttetString += stringToCut[i];
            }
            return cuttetString;
        }

        public void resetLastPressedKey()
        {
            if (Keyboard.GetState().IsKeyUp(lastPressedKey))
            {
                lastPressedKey = Keys.Zoom;
            }
        }

        public bool checkIfNumber(string stringToCheck)
        {
            foreach (char c in stringToCheck)
            {
                if (char.IsNumber(c))
                {
                    return true;
                }
            }
            return false;
        }

        public string getNumber(string stringToGetNumberFrom)
        {
            string number = "";

            foreach(char c in stringToGetNumberFrom)
            {
                if(char.IsNumber(c))
                {
                    number = c.ToString();
                }
            }
            return number;
        }

        public bool checkIfLetter(string stringToCheck)
        {
            if (stringToCheck.Length > 1)
            {
                return false;
            }
            else
            {
                foreach (char c in stringToCheck)
                {
                    if (char.IsLetter(c))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public string getSpecialSign(Keys pressedKey)
        {
            switch (pressedKey)
            {
                case Keys.OemComma:
                    return ",";
                       
                case Keys.OemPeriod:
                    return ".";
                    
                case Keys.Space:
                    return " ";

                case Keys.Back:
                    string tempText = getCutString(this.textDrawer.Text);
                    this.textDrawer.Text = "";
                    return tempText;
                    
            }

            return "";
        }

        public string getRightString(Keys pressedKey)
        {
            string temp = "";

            if(checkIfNumber(pressedKey.ToString()))
            {
                temp = getNumber(pressedKey.ToString());
            }

            else if (checkIfLetter(pressedKey.ToString()))
            {
                temp = pressedKey.ToString();
            }

            else
            {
                temp = getSpecialSign(pressedKey);
            }

            return this.textDrawer.Text + temp;
        }

        public void writeInputIfSelected()
        {

            if (canWrite)
            {
                KeyboardState kbs = Keyboard.GetState();
                Keys[] pressedKeys = kbs.GetPressedKeys();

                if (pressedKeys != null)
                {
                    if (pressedKeys.Length > 0)
                    {
                        Keys pressedKey = pressedKeys[0];

                        if (pressedKey != lastPressedKey)
                        {
                            if (this.textDrawer.Text.Length < maximumCharacters)
                            {
                                this.textDrawer.Text = getRightString(pressedKey);
                                lastPressedKey = pressedKey;
                            }

                            else
                            {
                                this.textDrawer.Text = getCutString(this.textDrawer.Text);
                            }
                        }
                    }
                }
            }
        }

        public override void onHoverLeave()
        {
            base.onHoverLeave();
            if (checkClick())
            {
                canWrite = false;
            }
        }
    }
}
