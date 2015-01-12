using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components.Drawables;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Components
{
    public class Button : Clickable
    {
        public TextDrawer textD;
        ColoredBox standartDrawer;
        ColoredBox hoverDrawer;
        ColoredBox clickedDrawer;

        public bool JustReleased { get; protected set; }

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
            this.parent.removeComponent(standartDrawer);
            this.parent.removeComponent(hoverDrawer);
            this.parent.removeComponent(clickedDrawer);
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
            activateHoverDrawer();
            base.onHover();
        }

        protected override void onHoverLeave()
        {
            activateStandartDrawer();
            if (hover)
            {
                hover = !hover;
            }
            base.onHoverLeave();
        }

        protected override void onClick()
        {
            base.onClick();
            activateClickedDrawer();
        }

        protected override void onRelease()
        {
            base.onRelease();
            activateHoverDrawer();
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
    }
}
