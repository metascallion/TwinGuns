using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WhiteSpace.Temp
{
    public class Clickable<StateType> : Updateable<StateType> where StateType : struct
    {
        public delegate void stateChange();

        public event stateChange hoverMethods;
        public event stateChange clickMethods;
        public event stateChange leaveMethods;
        public event stateChange releaseMethods;

        private bool clicked = false;
        private bool hover = false;

        private Transform transform;

        public Clickable(Transform transform)
        {
            this.transform = transform;
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            checkState();
        }

        private void checkState()
        {
            if (checkHover())
            {
                if (checkClick() && !clicked)
                {
                    clicked = true;
                    onClick();
                }
                else if (!checkClick() && clicked)
                {
                    clicked = false;
                    onRelease();
                }
                else
                {
                    onHover();
                }
            }
            else
            {
                if (hover)
                {
                    onHoverLeave();
                }
            }
        }

        private bool checkHover()
        {
            bool isHovering = false;
            if (this.transform.getRect().Contains(Mouse.GetState().Position))
            {
                isHovering = true;
            }
            return isHovering;
        }
        private bool checkClick()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        protected virtual void onClick()
        {
            if (releaseMethods != null)
            {
                releaseMethods();
            }
        }

        protected virtual void onRelease()
        {
            if (clickMethods != null)
            {
                clickMethods();
            }
        }
        protected virtual void onHover()
        {
            if (!hover)
            {
                if (hoverMethods != null)
                {
                    hoverMethods();
                }
            }
            hover = true;
        }
        protected virtual void onHoverLeave()
        {
            hover = false;

            if (leaveMethods != null)
            {
                leaveMethods();
            } 
        }
    }
}
