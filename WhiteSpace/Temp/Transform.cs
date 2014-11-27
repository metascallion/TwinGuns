using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WhiteSpace.Temp
{
    public class Transform
    {
        public Vector2 Position{get; set;}
        public Vector2 Rotation{get; set;}
        public Vector2 Size{get; set;}

        public Transform()
        {
        }

        public Transform(Vector2 position, Vector2 rotation, Vector2 size)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Size = size;
        }

        public static Transform createTransformOnPosition(Vector2 position)
        {
            return new Transform(position, new Vector2(), new Vector2());
        }

        public static Transform createTransformWithSize(Vector2 size)
        {
            return new Transform(new Vector2(), new Vector2(), size);
        }

        public static Transform createRotatedTransform(Vector2 rotation)
        {
            return new Transform(new Vector2(), rotation, new Vector2());
        }

        public static Transform createTransformWithSizeOnPosition(Vector2 position, Vector2 size)
        {
            return new Transform(position, new Vector2(), size);
        }

        public static Transform createRotatedTransformOnPosition(Vector2 position, Vector2 rotation)
        {
            return new Transform(position, rotation, new Vector2());
        }

        public static Transform createRotatedTransformWithSize(Vector2 rotation, Vector2 size)
        {
            return new Transform(new Vector2(), rotation, size);
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
