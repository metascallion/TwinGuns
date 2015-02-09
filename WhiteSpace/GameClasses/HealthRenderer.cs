using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Components.Drawables;
using WhiteSpace.Composite;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameClasses
{
    public class HealthRenderer : UpdateableComponent
    {
        TextDrawer drawer;

        public Transform boxTransform;

        Life life;
        int layer = 0;
        public Vector2 position;


        public HealthRenderer(int layer)
        {
            this.layer = layer;
        }

        public HealthRenderer()
        {

        }

        public override void start()
        {
            base.start();
            drawer = new TextDrawer("Font", "", layer);
            Transform trans = this.parent.getComponent<Transform>();
            this.parent.addComponent(drawer);
            position = new Vector2(trans.position.X + trans.Size.X / 2, trans.position.Y);
            drawer.transform = Transform.createTransformOnPosition(position);
            life = this.parent.getComponent<Life>();


        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            drawer.transform.position = position;
            drawer.text = "Life: " + life.Health.ToString();
        }
    }
}
