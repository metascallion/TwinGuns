using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Components.Drawables;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameClasses
{
    public class HealthRenderer : UpdateableComponent
    {
        TextDrawer drawer;
        Life life;

        public override void start()
        {
            base.start();
            drawer = new TextDrawer("Font", "");
            Transform trans = this.parent.getComponent<Transform>();
            this.parent.addComponent(drawer);
            drawer.transform = Transform.createTransformOnPosition(new Vector2(trans.Position.X + trans.Size.X / 2, trans.Position.Y));
            life = this.parent.getComponent<Life>();
        }
        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            drawer.text = life.Health.ToString();
        }
    }
}
