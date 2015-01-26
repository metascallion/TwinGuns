using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Temp;
using WhiteSpace.Components.Drawables;

namespace WhiteSpace.Components
{
    delegate void timeExpired();
    class AfterTimeComponentAdder : UpdateableComponent
    {
        float timeToWait;
        List<Component> componentsToAdd = new List<Component>();
        public event timeExpired expiredFunctions;
        TextDrawer drawer;

        public AfterTimeComponentAdder(float timeToWait)
        {
            this.timeToWait = timeToWait;
        }

        public override void start()
        {
            base.start();
            drawer = new TextDrawer("Font", "");
            this.parent.addComponentIgnoreDuplication(drawer);
        }

        public void addToComponentsToAddAfterTime(Component c)
        {
            componentsToAdd.Add(c);
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.parent.removeComponent(drawer);
            this.parent.addComponentIgnoreDuplication(drawer);
            timeToWait -= gameTime.ElapsedGameTime.Milliseconds;

            if (timeToWait <= 0)
            {
                foreach (Component c in componentsToAdd)
                {
                    this.parent.addComponent(c);
                }

                if(expiredFunctions != null)
                {
                    expiredFunctions();
                }
                this.parent.removeComponent(drawer);
                this.parent.removeComponent(this);
            }

            else
            {
                int seconds = (int)this.timeToWait / 1000 + 1;
                drawer.text = seconds.ToString();
            }
        }
    }
}
