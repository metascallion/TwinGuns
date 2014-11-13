using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BachelorPrototype.GameClasses
{
    public class ClickableUI : Drawable
    {
        public string Name { get; set; }
        private Drawable drawerToUse;
        public TextDrawer textDrawer;
        public Drawable StandartDrawer { get; set; }
        public Drawable HoverDrawer { get; set; }
        public Drawable ClickedDrawer { get; set;}

        public delegate void onMouseClick(ClickableUI sender);
        public delegate void onMouseHover(ClickableUI sender);
        public delegate void onMouseLeave(ClickableUI sender);

        public event onMouseClick clickEvent;
        public event onMouseHover mouseHoverEvent;
        public event onMouseLeave mouseLeaveEvent;

        public bool hover;
        bool left;
        bool clicked;

        public void addText(string text)
        {
            textDrawer.Text = text;
        }

        public ClickableUI()
        {
            textDrawer = new TextDrawer();

            ColorBoxDrawer standartDrawer = new ColorBoxDrawer(new Vector2(10, 10));
            standartDrawer.setColor(Color.Silver);
            this.StandartDrawer = standartDrawer;
            this.drawerToUse = StandartDrawer;

            ColorBoxDrawer hoverDrawer = new ColorBoxDrawer(new Vector2(10, 10));
            hoverDrawer.setColor(Color.LightGoldenrodYellow);
            this.HoverDrawer = hoverDrawer;

            ColorBoxDrawer clickedDrawer = new ColorBoxDrawer(new Vector2(10, 10));
            clickedDrawer.setColor(Color.PaleGoldenrod);
            this.ClickedDrawer = clickedDrawer;
        }

        public virtual void update()
        {
            if (this.activeState == StateContainer.getInstance().getState())
            {
                handleButtonState();
                this.drawerToUse.Position = this.Position;
                this.drawerToUse.Size = this.Size;
                this.textDrawer.Position = this.Position;
                this.textDrawer.Size = this.Size;
                this.textDrawer.activeState = this.activeState;
            }
        }

        protected bool checkHover()
        {
            bool isHovering = false;

            if(this.getRect().Contains(Mouse.GetState().Position))
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

        private void handleButtonState()
        {

            if (checkHover())
            {
                if (checkClick())
                {
                    clicked = true;
                    this.drawerToUse = ClickedDrawer;
                }

                else if (!checkClick() && clicked)
                {
                    clicked = false;
                    onClick();
                }

                else
                {
                    onHover();
                }
            }

            else
            {
                onHoverLeave();
            }
        }

        public virtual void onClick()
        {
            if (clickEvent != null)
            {
                clickEvent(this);
            }
        }

        public virtual void onHover()
        {
            left = false;

            if (!hover)
            {
                if (mouseHoverEvent != null)
                {
                    mouseHoverEvent(this);
                }
            }

            this.drawerToUse = HoverDrawer;
            hover = true;
        }

        public virtual void onHoverLeave()
        {
            hover = false;

            if (!left)
            {
                if (mouseLeaveEvent != null)
                {
                    mouseLeaveEvent(this);
                }
            }
            this.drawerToUse = StandartDrawer;

            left = true;
        }

        public override void draw(SpriteBatch batch)
        {
            base.draw(batch);
            if (this.activeState == StateContainer.getInstance().getState())
            {
                drawerToUse.draw(batch);
                textDrawer.draw(batch);
            }
        }
    }
}
