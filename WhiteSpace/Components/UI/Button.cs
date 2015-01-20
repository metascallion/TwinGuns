using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components.Drawables;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Components
{
    public enum displaystate
    {
        normal,
        hover,
        clicked
    }


    public class Button : Clickable
    {
        public TextDrawer textD;
        private ColoredBox standartDrawer;
        private ColoredBox hoverDrawer;
        private ColoredBox clickedDrawer;

        public bool JustReleased { get; protected set; }

        protected displaystate state;


        public void setStandartDrawer(ColoredBox drawer)
        {
            this.standartDrawer = drawer;
            displayDrawer();
        }

        public void setHoverDrawer(ColoredBox drawer)
        {
            this.hoverDrawer = drawer;
            displayDrawer();
        }

        public void setClickedDrawer(ColoredBox drawer)
        {
            this.clickedDrawer = drawer;
            displayDrawer();
        }

        private void activateStandartDrawer()
        {
            deactivateDrawers();
            this.parent.addComponent(standartDrawer);
        }

        private void activateHoverDrawer()
        {
            deactivateDrawers();
            this.parent.addComponent(hoverDrawer);
        }

        private void activateClickedDrawer()
        {
            deactivateDrawers();
            this.parent.addComponent(clickedDrawer);
        }

        private void deactivateDrawers()
        {
            this.parent.removeComponent<ColoredBox>();
        }

        public Button()
        {
            textD = new TextDrawer("Font", "");
        }

        public override void start()
        {
            base.start();
            standartDrawer = new ColoredBox(Color.Gray);
            hoverDrawer = new ColoredBox(Color.LightGray);
            clickedDrawer = new ColoredBox(Color.Silver);
            activateStandartDrawer();
        }

        public Button(string text)
        {
            textD = new TextDrawer("Font", text);
        }

        public void addText(string text)
        {
            textD.text = text;
        }

        protected override void onHover()
        {
            this.state = displaystate.hover;
            displayDrawer();
            activateHoverDrawer();
            base.onHover();
        }

        protected override void onHoverLeave()
        {
            this.state = displaystate.normal;
            displayDrawer();
            if (hover)
            {
                hover = !hover;
            }
            base.onHoverLeave();
        }

        protected override void onClick()
        {
            base.onClick();
            this.state = displaystate.clicked;
            displayDrawer();
        }

        protected override void onRelease()
        {
            base.onRelease();
            this.state = displaystate.hover;
            displayDrawer();
            JustReleased = true;
        }

        bool initialised = false;
        protected override void update(GameTime gameTime)
        {
            if (!initialised)
            {
                initialised = true;
                activateStandartDrawer();
            }

            this.parent.removeComponent(textD);
            this.parent.addComponent(textD);
            JustReleased = false;
            base.update(gameTime);
        }

        public void displayDrawer()
        {
            if (this.state == displaystate.normal)
            {
                activateStandartDrawer();
            }

            else if (this.state == displaystate.hover)
            {
                activateHoverDrawer();
            }

            else if (this.state == displaystate.clicked)
            {
                activateClickedDrawer();
            }
        }

    }
}
