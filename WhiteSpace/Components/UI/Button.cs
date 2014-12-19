using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Drawables;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Components
{
    public class Button<StateType> : Clickable<StateType> where StateType : struct
    {
        public delegate void stateChange(Button<StateType> sender);

        public event stateChange hoverMethods;
        public event stateChange clickMethods;
        public event stateChange leaveMethods;
        public event stateChange releaseMethods;

        public TextDrawer<StateType> textD;
        ColoredBox<StateType> standartDrawer;
        ColoredBox<StateType> hoverDrawer;
        ColoredBox<StateType> clickedDrawer;

        private void activateStandartDrawer()
        {
            deactivateDrawers();
            standartDrawer.registerInUpdater();
        }

        private void activateHoverDrawer()
        {
            deactivateDrawers();
            hoverDrawer.registerInUpdater();
        }

        private void activateClickedDrawer()
        {
            deactivateDrawers();
            clickedDrawer.registerInUpdater();
        }

        private void deactivateDrawers()
        {
            standartDrawer.unregisterInUpdater();
            hoverDrawer.unregisterInUpdater();
            clickedDrawer.unregisterInUpdater();
        }

        public Button(Transform transform, Updater<StateType> updaterToRegisterTo)
            : base(transform, updaterToRegisterTo)
        {
            standartDrawer = new ColoredBox<StateType>(transform, Color.Gray, updaterToRegisterTo);
            hoverDrawer = new ColoredBox<StateType>(transform, Color.LightGoldenrodYellow, updaterToRegisterTo);
            clickedDrawer = new ColoredBox<StateType>(transform, Color.PaleGoldenrod, updaterToRegisterTo);
            activateStandartDrawer();
            textD = new TextDrawer<StateType>(transform, "Font", "", updaterToRegisterTo);
        }

        public void addText(string text)
        {
            textD.text = text;
        }

        protected override void onHover()
        {
            activateHoverDrawer();
            if (!hover)
            {
                hover = !hover;
                if (hoverMethods != null)
                {
                    hoverMethods(this);
                }
            }
            base.onHover();
        }

        protected override void onHoverLeave()
        {
            activateStandartDrawer();
            if (hover)
            {
                hover = !hover;
                if (leaveMethods != null)
                {
                    leaveMethods(this);
                }
            }
            base.onHoverLeave();
        }

        protected override void onClick()
        {
            base.onClick();
            activateClickedDrawer();
            if (clickMethods != null)
            {
                clickMethods(this);
            }
        }

        protected override void onRelease()
        {
            base.onRelease();
            activateHoverDrawer();
            if (releaseMethods != null)
            {
                releaseMethods(this);
            }
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            textD.unregisterInUpdater();
            textD.registerInUpdater();
        }
    }
}
