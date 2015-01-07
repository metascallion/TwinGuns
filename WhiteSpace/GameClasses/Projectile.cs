using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using WhiteSpace.Components.Physics;
using WhiteSpace.Components.Drawables;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;

namespace WhiteSpace.GameClasses
{
    public class Projectile : UpdateableComponent
    {
        public Transform target;
        public BoxCollider collider;
        Transform transform;
        CharacterControler controller;
        public int speed = 5;

        public Projectile(Transform target)
        {
            this.target = target;
        }

        public override void start()
        {
            this.collider = this.parent.getComponent<BoxCollider>();
            this.collider.body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
            this.collider.body.IgnoreGravity = true;
            this.controller = this.parent.getComponent<CharacterControler>();
            this.transform = this.parent.getComponent<Transform>();
            this.collider.collisionMethods += this.test;
            controller.registerInComponentSector();
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            transform.lookAt(target.Position);
            controller.move(collider.transform.transformDirection(direction.right) * speed);
        }

        private void test(BoxCollider collider)
        {
            GameObject colliderGameObject = collider.parent;
            if( colliderGameObject == this.target.parent)
            {
                this.parent.destroy();
                collider.collisionMethods -= this.test;
                Game1.ships.Remove(this.parent.getComponent<Transform>());
            }
        }
    }
}
