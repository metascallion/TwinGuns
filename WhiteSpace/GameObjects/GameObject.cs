using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;
using WhiteSpace.GameLoop;

namespace WhiteSpace
{
    public class GameObject<StateType> : Updateable<StateType> where StateType : struct
    {
        protected Transform transform;

        public GameObject(StateType activeState, Transform transform)
            : base(activeState)
        {
            this.transform = transform;
        }

        public GameObject(Transform transform)
        {
            this.transform = transform;
        }

        public GameObject()
        {
            this.transform = new Transform();
        }

        public Vector2 Position
        {
            get
            {
                return this.transform.Position;
            }

            set
            {
                this.transform.Position = value;
            }
        }

        public Vector2 Size
        {
            get
            {
                return this.transform.Size;
            }

            set
            {
                this.transform.Size = value;
            }
        }

        public Vector2 Rotation
        {
            get
            {
                return this.transform.Rotation;
            }

            set
            {
                this.transform.Rotation = value;
            }
        }
    }
}
