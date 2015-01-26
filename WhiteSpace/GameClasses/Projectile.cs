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
using WhiteSpace.Network;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Components.Animation;

namespace WhiteSpace.GameClasses
{

    public class Ship : Projectile
    {
        public Ship(Transform transform)
            : base(transform)
        {
            this.speed = 1;
        }

        public Ship()
        {

        }

        public override void start()
        {
            base.start();
            transform.lookAt(new Vector2(target.Center.X, this.parent.getComponent<Transform>().Position.Y));
        }

        public override void onDestroy()
        {
            new Sound("Explode", false, 0.65f);
            Transform transform = this.parent.getComponent<Transform>();
            GameObject go = GameObjectFactory.createTemporaryObjectWithTransform(this.parent.sector, new Vector2(transform.Position.X - 25, transform.Position.Y - 25), new Vector2(50, 50), 300);
            go.addComponent(new TextureRegion(ContentLoader.getContent<Texture2D>("Explosion")));
            go.addComponent(Animator.loadAnimator(go.getComponent<TextureRegion>(), "ExplosionAnimator", 10));
            go.getComponent<Animator>().playAnimation("Explosion", false);
            base.onDestroy();
        }

        protected override void onTargetHit(BoxCollider targetCollider)
        {
            if (Client.host)
            {
                this.target.parent.getComponent<Life>().reduceHealth();
                this.target.parent.getComponent<LifeSender>().sendLife();
            }
            this.parent.destroy();
        }

        protected override void update(GameTime gameTime)
        {
            //base.update(gameTime);
            controller.move(collider.transform.transformDirection(direction.right) * speed);
        }

        //protected override void update(GameTime gameTime)
        //{
        //    //base.update(gameTime);

        //}
    }
    public abstract class Projectile : UpdateableComponent
    {
        public Transform target;
        public BoxCollider collider;
        protected Transform transform;
        protected CharacterControler controller;
        public int speed;

        public Projectile()
        {
        }

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
            Sound sound = new Sound("Shot", false, 0.3f);
            this.parent.destroy();
        }
    }
}
