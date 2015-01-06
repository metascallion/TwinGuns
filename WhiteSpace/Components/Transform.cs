using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;

namespace WhiteSpace.Components
{
    public enum direction
    {
        forward,
        backward,
        right,
        left,
    }

    public class Transform : StandardComponent
    {
        public Vector2 Position{get; set;}
        public float Rotation{get; set;}
        public Vector2 Size{get; set;}

        public Transform()
        {
        }

        public void lookAt(Vector2 targetLocation)
        {
            Vector2 direction = targetLocation - new Vector2(this.Position.X + this.Size.X / 2, this.Position.Y + this.Size.Y / 2);
            direction.Normalize();
            float angle = (float)Math.Atan2(direction.X, direction.Y);
            float radian = (float)Math.Atan2(Math.Cos(angle), (float)Math.Sin(angle));
            this.Rotation = radian;
        }

        public Transform(Vector2 position, float rotation, Vector2 size)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Size = size;
        }

        public static Transform createTransformOnPosition(Vector2 position)
        {
            return new Transform(position, 0, new Vector2());
        }

        public static Transform createTransformWithSize(Vector2 size)
        {
            return new Transform(Vector2.Zero, 0, size);
        }

        public static Transform createRotatedTransform(float rotation)
        {
            return new Transform(Vector2.Zero, rotation, Vector2.Zero);
        }

        public static Transform createTransformWithSizeOnPosition(Vector2 position, Vector2 size)
        {
            return new Transform(position, 0, size);
        }

        public static Transform createRotatedTransformOnPosition(Vector2 position, float rotation)
        {
            return new Transform(position, rotation, Vector2.Zero);
        }

        public static Transform createRotatedTransformWithSize(float rotation, Vector2 size)
        {
            return new Transform(Vector2.Zero, rotation, size);
        }

        public void correctRotation()
        {
            if(Rotation <= 0)
            {
                Rotation = 0.1f;
            }

            else if(Rotation >= 360)
            {
                Rotation = 0.1f;
            }
        }

        private Vector2 calculateDirectionVector(direction inputDirection)
        {
            switch(inputDirection)
            {
                case direction.forward:
                    return new Vector2((float)Math.Sin(Rotation), -(float)Math.Cos(Rotation));
                case direction.backward:
                     return new Vector2(-(float)Math.Sin(Rotation), (float)Math.Cos(Rotation));
                case direction.right:
                    return new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                case direction.left:
                    return new Vector2(-(float)Math.Cos(Rotation), -(float)Math.Sin(Rotation));
            }
            return Vector2.Zero;
        }

        public Vector2 transformDirection(direction inputDirection)
        {
            Vector2 directionVector = calculateDirectionVector(inputDirection);
            directionVector.Normalize();
            return directionVector;
        }

        public void translate(Vector2 direction)
        {
            this.Position = new Vector2(this.Position.X + direction.X, this.Position.Y + direction.Y);
        }

        public void translate(float x, float y)
        {
            this.Position = new Vector2(this.Position.X + x, this.Position.Y + y);
        }

        public void translateOnXAxis(float value)
        {
            this.Position = new Vector2(this.Position.X + value, this.Position.Y);
        }

        public void translateOnYAxis(float value)
        {
            this.Position = new Vector2(this.Position.X, this.Position.Y + value);
        }

        public void scale(Vector2 scaleFactor)
        {
            this.Size = this.Size * scaleFactor;
        }

        public void scale(float scaleFactor)
        {
            this.Size = this.Size * scaleFactor;
        }

        public void changeSize(Vector2 sizeToSet)
        {
            this.Size = sizeToSet;
        }

        public Rectangle getRect()
        {
            return new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y);
        }
    }
}
