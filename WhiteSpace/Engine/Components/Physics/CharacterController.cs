using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using WhiteSpace.Composite;

namespace WhiteSpace.Components.Physics
{
    public class CharacterControler : UpdateableComponent
    {
        BoxCollider collider;

        public CharacterControler()
        {
        }

        public override void start()
        {
            this.collider = parent.getComponent<BoxCollider>();
            this.collider.follow = false;
        }

        public void useGravity(bool active)
        {
            if (active)
            {
                collider.body.BodyType = BodyType.Dynamic;
            }
        }

        public void move(Vector2 direction)
        {
            collider.body.LinearVelocity = new Vector2(direction.X, direction.Y);
        }

        public void jump(float force)
        {
            collider.body.ApplyLinearImpulse(new Vector2(0, -force));
        }

        protected override void update(GameTime gameTime)
        {
            collider.transform.Position = collider.Position;
        }
    }
}
