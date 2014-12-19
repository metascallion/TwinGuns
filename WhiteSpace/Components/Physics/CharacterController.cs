using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace WhiteSpace.Components.Physics
{
    public class CharacterControler<StateType> : Updateable<StateType> where StateType : struct
    {
        BoxCollider<StateType> collider;

        public CharacterControler(BoxCollider<StateType> collider, Updater<StateType> updaterToRegisterTo) : base (updaterToRegisterTo)
        {
            this.collider = collider;
            collider.follow = false;
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
            collider.body.LinearVelocity = new Vector2(direction.X, direction.Y + collider.body.LinearVelocity.Y);
        }

        public void jump(float force)
        {
            collider.body.ApplyLinearImpulse(new Vector2(0, -force));
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            collider.transform.Position = collider.Position;
        }
    }
}
