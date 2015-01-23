using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WhiteSpace.Components;
using WhiteSpace.Temp;

namespace WhiteSpace.Components
{
    public class Clickable : UpdateableComponent
    {
        public delegate void stateChange(Clickable sender);

        public event stateChange hoverMethods;
        public event stateChange clickMethods;
        public event stateChange leaveMethods;
        public event stateChange releaseMethods;


        protected bool clicked = false;
        protected bool hover = false;

        private Transform transform;

        public int id;

        public Clickable()
        {
        }

        public override void start()
        {
            base.start();
            this.transform = this.parent.getComponent<Transform>();
        }

        protected override void update(GameTime gameTime)
        {
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
                else if (!hover)
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
        protected bool checkClick()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        protected virtual void onClick()
        {
            if (clickMethods != null)
            {
                clickMethods(this);
            }
        }

        protected virtual void onRelease()
        {
            if (releaseMethods != null)
            {
                releaseMethods(this);
            }
        }
        protected virtual void onHover()
        {
            if (!hover)
            {
                hover = !hover;
                if (hoverMethods != null)
                {
                    hoverMethods(this);
                }
            }

            hover = true;
        }
        protected virtual void onHoverLeave()
        {
            hover = false;

            if (leaveMethods != null)
            {
                leaveMethods(this);
            }
        }
    }
}
