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

    public class Ship : Projectile
    {
        public Ship(Transform transform)
            : base(transform)
        {
            this.speed = 2;
        }

        public override void onDestroy()
        {
            base.onDestroy();
        }

        protected override void onTargetHit(BoxCollider targetCollider)
        {
            this.parent.destroy();
        }
    }
    public abstract class Projectile : UpdateableComponent
    {
        public Transform target;
        public BoxCollider collider;
        protected Transform transform;
        CharacterControler controller;
        public int speed;

        public Projectile(Transform target)
        {
            this.target = target;
            speed = 5;
        }

        public Projectile(Transform target, int speed)
        {
            this.target = target;
            this.speed = speed;
        }

        public override void start()
        {
            this.collider = this.parent.getComponent<BoxCollider>();
            this.collider.body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
            this.collider.body.IgnoreGravity = true;
            this.collider.trigger = true;
            this.controller = this.parent.getComponent<CharacterControler>();
            this.transform = this.parent.getComponent<Transform>();
            this.collider.collisionMethods += this.onColliderHit;
            controller.registerInComponentSector();
        }

        protected override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            transform.lookAt(target.Center);
            controller.move(collider.transform.transformDirection(direction.right) * speed);

            if(target.parent == null)
            {
                this.parent.destroy();
            }
        }

        private void onColliderHit(BoxCollider collider)
        {
            if (collider.parent.getComponent<Transform>() == this.target)
            {
                onTargetHit(collider);
            }
        }

        public override void onDestroy()
        {
           base.onDestroy();
        }

        protected abstract void onTargetHit(BoxCollider targetCollider);
    }

    public class Shot : Projectile
    {
        public Shot(Transform transform, int speed) : base(transform)
        {
            this.speed = speed;
        }

        protected override void onTargetHit(BoxCollider targetCollider)
        {
            targetCollider.parent.getComponent<Life>().reduceHealth();
            this.parent.destroy();
        }
    }
}
