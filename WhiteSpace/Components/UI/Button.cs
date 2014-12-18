using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Drawables;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Components
{
    public class Button <StateType> : Clickable<StateType> where StateType : struct
    {
        public delegate void stateChange(Button<StateType> sender);

        public event stateChange hoverMethods;
        public event stateChange clickMethods;
        public event stateChange leaveMethods;
        public event stateChange releaseMethods;

        public TextDrawer<StateType> textDrawer;
        public Drawable<StateType> StandartDrawer { get; set; }
        public Drawable<StateType> HoverDrawer { get; set; }
        public Drawable<StateType> ClickedDrawer { get; set; }

        private void activateStandartDrawer()
        {
            StandartDrawer.registerInDrawExecuter();
            HoverDrawer.unregisterInDrawExecuter();
            ClickedDrawer.unregisterInDrawExecuter();
        }

        private void activateHoverDrawer()
        {
            HoverDrawer.registerInDrawExecuter();
            StandartDrawer.unregisterInDrawExecuter();
            ClickedDrawer.unregisterInDrawExecuter();
        }

        private void activateClickedDrawer()
        {
            ClickedDrawer.registerInDrawExecuter();
            HoverDrawer.unregisterInDrawExecuter();
            StandartDrawer.unregisterInDrawExecuter();
        }

        private void deactivateDrawers()
        {
            StandartDrawer.unregisterInDrawExecuter();
            HoverDrawer.unregisterInDrawExecuter();
            ClickedDrawer.unregisterInDrawExecuter();
        }

        public Button(Transform transform) : base(transform)
        {
            ColoredBox<StateType> standartDrawer = new ColoredBox<StateType>(transform);
            standartDrawer.setColor(Color.Silver);
            this.StandartDrawer = standartDrawer;

            ColoredBox<StateType> hoverDrawer = new ColoredBox<StateType>(transform);
            hoverDrawer.setColor(Color.LightGoldenrodYellow);
            this.HoverDrawer = hoverDrawer;

            ColoredBox<StateType> clickedDrawer = new ColoredBox<StateType>(transform);
            clickedDrawer.setColor(Color.PaleGoldenrod);

            this.ClickedDrawer = clickedDrawer;

            activateStandartDrawer();

            textDrawer = new TextDrawer<StateType>(transform, "Font");
        }

        public void addText(string text)
        {
            textDrawer.text = text;
        }

        protected override void onHover()
        {
            if(!hover)
            {
                if(hoverMethods != null)
                {
                    hoverMethods(this);
                }
            }
            activateHoverDrawer();
            base.onHover();
        }

        protected override void onHoverLeave()
        {
            if(hover)
            {
                if(leaveMethods != null)
                {
                    leaveMethods(this);
                }
            }
            activateStandartDrawer();
            base.onHoverLeave();
        }

        protected override void onClick()
        {
            base.onClick();
            if(clickMethods != null)
            {
                clickMethods(this);
            }
            activateClickedDrawer();
        }

        protected override void onRelease()
        {
            base.onRelease();
            if(releaseMethods != null)
            {
                releaseMethods(this);
            }
            activateHoverDrawer();
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            textDrawer.unregisterInDrawExecuter();
            textDrawer.registerInDrawExecuter();
        }

        protected override void processInvalidState()
        {
            base.processInvalidState();
            deactivateDrawers();
        }
    }
}
