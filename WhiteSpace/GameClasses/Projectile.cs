using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using WhiteSpace.Components.Physics;
using WhiteSpace.Components.Drawables;
using Microsoft.Xna.Framework;
using WhiteSpace.Composite;
using WhiteSpace.Network;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Components.Animation;
using WhiteSpace.Content;

namespace WhiteSpace.GameClasses
{
    public class TwingunShot : UpdateableComponent
    {
        Transform target;
        public TwingunShot(Transform transform)
        {
            this.target = transform;
        }

        protected override void update(GameTime gameTime)
        {
            Transform transform = this.parent.getComponent<Transform>();
            transform.lookAt(target.Center);
            transform.translate(transform.transformDirection(direction.right) * 5);

            if (Vector2.Distance(target.Position, this.parent.getComponent<Transform>().Position) < 12)
            {
                onTargetHit();
            }
        }

        protected void onTargetHit()
        {
            new Sound("Explode", false, 0.5f);
            Transform transform = target.parent.getComponent<Transform>();
            GameObject go = GameObjectFactory.createTemporaryObjectWithTransform(this.parent.sector, new Vector2(transform.Position.X - 25, transform.Position.Y - 25), new Vector2(50, 50), 300);
            go.addComponent(new TextureRegion(ContentLoader.getContent<Texture2D>("Explosion")));
            go.addComponent(Animator.loadAnimator(go.getComponent<TextureRegion>(), "ExplosionAnimator", 10));
            go.getComponent<Animator>().playAnimation("Explosion", false);
            this.parent.destroy();
            target.parent.removeComponent<Tower>();
            target.parent.removeComponent<RessourceTower>();
        }
    }


    public class Ship : Projectile
    {
        GameObject targetShip;
        public Ship(Transform transform, GameObject targetShip)
            : base(transform)
        {
            this.speed = 1.5f;
            this.targetShip = targetShip;
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
            GameObject go = GameObjectFactory.createTemporaryObjectWithTransform(this.parent.sector, new Vector2(transform.Center.X - 25, transform.Center.Y - 25), new Vector2(50, 50), 300);
            go.addComponent(new TextureRegion(ContentLoader.getContent<Texture2D>("Explosion")));
            go.addComponent(Animator.loadAnimator(go.getComponent<TextureRegion>(), "ExplosionAnimator", 10));
            go.getComponent<Animator>().playAnimation("Explosion", false);
            base.onDestroy();
        }

        protected override void onTargetHit(BoxCollider targetCollider)
        {
            if (Client.host)
            {
                this.targetShip.getComponent<Life>().reduceHealth();
                this.targetShip.getComponent<LifeSender>().sendLife();
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
        public float speed;

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

        bool hit = false;
        private void onColliderHit(BoxCollider collider)
        {
            if (!hit)
            {
                if (collider.parent.getComponent<Transform>() == this.target)
                {
                    hit = true;
                    onTargetHit(collider);
                }
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
