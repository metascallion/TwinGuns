using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;

namespace WhiteSpace
{
    public class GameObject<StateType> : Updater<StateType> where StateType : struct
    {
        protected Transform transform;

        public GameObject(StateType activeState, Transform transform)
            : base(activeState)
        {
            this.transform = transform;
        }

        public GameObject(Transform transform, StateType activeState) : base(activeState)
        {
            this.transform = transform;
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

        public float Rotation
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
