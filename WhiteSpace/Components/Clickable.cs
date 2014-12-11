using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WhiteSpace.Components;

namespace WhiteSpace.Components
{
    public class Clickable<StateType> : Updateable<StateType> where StateType : struct
    {
        protected bool clicked = false;
        protected bool hover = false;

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
                else if(!hover)
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
        }

        protected virtual void onRelease()
        {
        }
        protected virtual void onHover()
        {
            hover = true;
        }
        protected virtual void onHoverLeave()
        {
            hover = false;
        }
    }
}
