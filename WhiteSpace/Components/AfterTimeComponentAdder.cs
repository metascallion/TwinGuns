using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Temp;
using WhiteSpace.Components.Drawables;

namespace WhiteSpace.Components
{
    class AfterTimeComponentAdder : UpdateableComponent
    {
        float timeToWait;
        List<Component> componentsToAdd = new List<Component>();

        public AfterTimeComponentAdder(float timeToWait)
        {
            this.timeToWait = timeToWait;
        }

        public override void start()
        {
            base.start();
            this.parent.addComponent(new TextDrawer("Font", ""));
        }

        public void addToComponentsToAddAfterTime(Component c)
        {
            componentsToAdd.Add(c);
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            timeToWait -= gameTime.ElapsedGameTime.Milliseconds;

            if (timeToWait <= 0)
            {
                this.parent.removeComponent<TextDrawer>();
                foreach (Component c in componentsToAdd)
                {
                    this.parent.addComponent(c);
                }
                this.parent.removeComponent(this);
            }

            else
            {
                int seconds = (int)this.timeToWait / 1000;
                this.parent.getComponent<TextDrawer>().text = seconds.ToString();
            }
        }
    }
}
