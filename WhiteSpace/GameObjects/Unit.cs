using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Drawables;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using WhiteSpace.Components.Animation;

namespace WhiteSpace.GameObjects
{

    public class TestRotationGameObject <StateType> : GameObject <StateType> where StateType : struct
    {
        public TextureRegion<StateType> unitTexture;
        public float speed = 0.5f;
        Animator<StateType> animator;

        public TestRotationGameObject(StateType type, Transform transform) : base(type, transform)
        {

            unitTexture = new TextureRegion<StateType>(type, transform, ContentLoader.getContent<Texture2D>("RunningBastard"));
            animator = Animator<StateType>.loadAnimator(type, unitTexture, "RunningBastard");
            animator.AnimationSpeed = 10;
        }

        public TestRotationGameObject(Transform transform, Texture2D texture/*, SpriteSheet sheet*/)
            : base(transform)
        {
            unitTexture = new TextureRegion<StateType>(transform, texture);
            animator.AnimationSpeed = 10;
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            testMovement(gameTime);
        }

        public void testMovement(GameTime time)
        {
            //transform.correctRotation();
            float elapsedTime = (float)time.ElapsedGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                animator.playAnimation("Run", true);
            }

            else
            {
                //animator.playAnimation("Idle", false);
            }


            if (Vector2.Distance(new Vector2((float)Mouse.GetState().Position.X, (float)Mouse.GetState().Position.Y), new Vector2(this.Position.X + this.Size.X / 2, this.Position.Y + this.Size.Y / 2)) > 10)
            {
                this.transform.translate(transform.transformDirection(direction.right) * speed * elapsedTime);
                animator.playAnimation("Run", true);
            }
            else
            {
                //animator.playAnimation("Idle", true);
            }
            this.transform.lookAt(new Vector2((float)Mouse.GetState().Position.X, (float)Mouse.GetState().Position.Y));
        }
    }
}
