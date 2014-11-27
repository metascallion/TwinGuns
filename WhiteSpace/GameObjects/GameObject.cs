using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;

namespace WhiteSpace
{
    public class GameObject<StateType> : StateListener<StateType> where StateType : struct
    {
        protected Transform transform;

        public GameObject(StateType activeState, Transform transform)
            : base(activeState)
        {
            registerInUpdateExecuter();
            this.transform = transform;
        }

        public GameObject(Transform transform)
        {
            registerInUpdateExecuter();
            this.transform = transform;
        }

        public GameObject()
        {
            registerInUpdateExecuter();
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

        protected override void processInvalidState()
        {
            base.processInvalidState();
            unregisterInUpdateExecuter();
        }

        protected override void processValidState()
        {
            base.processValidState();
            registerInUpdateExecuter();
        }

        protected virtual void update(GameTime gameTime)
        {
        }

        private void registerInUpdateExecuter()
        {
            UpdateExecuter.registerUpdateable(this.update);
        }

        private void unregisterInUpdateExecuter()
        {
            UpdateExecuter.unregisterUpdateable(this.update);
        }
    }
}
