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

namespace WhiteSpace.Components.Physics
{
    public class BoxCollider<StateType> : Updateable<StateType> where StateType : struct
    {
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

        public BoxCollider(Transform transform, Updater<StateType> updaterToRegisterTo) : base(updaterToRegisterTo)
        {
            initializeCollider(transform);
        }

        private void initializeCollider(Transform transform)
        {
            body = BodyFactory.CreateRectangle(CollisionDetection.world, transform.Size.X * pixelToUnit, transform.Size.Y * pixelToUnit, 1);
            body.BodyType = BodyType.Kinematic;
            this.Size = transform.Size * pixelToUnit;
            this.Position = new Vector2(transform.Position.X + transform.Size.X / 2, transform.Position.Y + transform.Size.Y / 2);
            this.transform = transform;
            this.transform.Position = this.Position;
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if (follow)
            {
                this.Position = new Vector2(transform.Position.X + transform.Size.X / 2, transform.Position.Y + transform.Size.Y / 2);
                this.body.Rotation = this.transform.Rotation;
            }

            else
            {
                this.transform.Rotation = this.body.Rotation;
            }
        }
    }
}
