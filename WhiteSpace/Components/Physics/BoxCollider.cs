using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Contacts;
using WhiteSpace.Temp;

namespace WhiteSpace.Components.Physics
{
    public static class ColliderContainer
    {

        public static Dictionary<Body, BoxCollider> colliders = new Dictionary<Body, BoxCollider>();

    }

    public class BoxCollider : UpdateableComponent
    {
        public delegate void onCollision(BoxCollider collider);
        public event onCollision collisionMethods;
        public bool follow = true;
        private const float unitToPixel = 100.0f;
        private const float pixelToUnit = 1 / unitToPixel;
        public Transform transform;
        public Body body;
        public Vector2 Position
        {
            get { return new Vector2(body.Position.X - this.Size.X / 2, body.Position.Y - this.Size.Y / 2) * unitToPixel; }
            set { body.Position = value * pixelToUnit; }
        }
        private Vector2 size;
        public Vector2 Size
        {
            get { return size * unitToPixel; }
            set { size = value * pixelToUnit; }
        }
        public BoxCollider()
        {
        }

        public override void start()
        {
            this.transform = this.parent.getComponent<Transform>();
            body = BodyFactory.CreateRectangle(CollisionDetection.world, transform.Size.X * pixelToUnit, transform.Size.Y * pixelToUnit, 1);
            body.BodyType = BodyType.Kinematic;
            this.Size = transform.Size * pixelToUnit;
            this.Position = new Vector2(transform.Position.X + transform.Size.X / 2, transform.Position.Y + transform.Size.Y / 2);
            this.transform.Position = this.Position;
            ColliderContainer.colliders.Add(this.body, this);
            this.body.OnCollision += onCollisionEnter;
        }

        //protected override void update(GameTime gameTime)
        //{
        //    if (follow)
        //    {

        //    }
        //    this.transform.Position = this.Position;
        //    this.body.Rotation = this.transform.Rotation;
        //}

        protected override void update(GameTime gameTime)
        {
            if (follow)
            {
                this.transform.Position = this.Position;
            }
            this.body.Rotation = this.transform.Rotation;
        }

        

        protected bool onCollisionEnter(Fixture fix1, Fixture fix2, Contact contact)
        {
            onCollisionEnter(fix2);
            return true;
        }

        private void onCollisionEnter(Fixture fix)
        {
            if (collisionMethods != null)
            {
                collisionMethods(ColliderContainer.colliders[fix.Body]);
            }
        }
    }
}
    
