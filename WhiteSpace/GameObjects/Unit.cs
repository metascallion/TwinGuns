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
using WhiteSpace.Network;
using WhiteSpace.Components;
using WhiteSpace.Components.Physics;

namespace WhiteSpace.Temp { }

namespace WhiteSpace.GameObjects
{

    public class TestRotationGameObject <StateType> : GameObject <StateType> where StateType : struct
    {
        public TextureRegion<StateType> unitTexture;
        public float speed = 0.5f;
        Animator<StateType> animator;
        CharacterControler<StateType> controller;

        public TestRotationGameObject(StateType type, Transform transform) : base(type, transform)
        {

            unitTexture = new TextureRegion<StateType>(type, transform, ContentLoader.getContent<Texture2D>("RunningBastard"));
            animator = Animator<StateType>.loadAnimator(type, unitTexture, "RunningBastard");
            animator.AnimationSpeed = 10;
            BoxCollider<StateType> collider = new BoxCollider<StateType>(transform, type);
            controller = new CharacterControler<StateType>(collider);
            controller.useGravity(true);

        }

        public TestRotationGameObject(Transform transform, Texture2D texture)
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

        bool jumped = false;

        public void testMovement(GameTime time)
        {
            float elapsedTime = (float)time.ElapsedGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                animator.playAnimation("Run", true);
            }

            else
            {
                //animator.playAnimation("Idle", false);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                controller.move(new Vector2(5.0f,0));
                unitTexture.SpriteEffect = SpriteEffects.None;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                controller.move(new Vector2(-5.0f, 0));
                unitTexture.SpriteEffect = SpriteEffects.FlipHorizontally;
            }

            else
            {
                animator.stopAnimation();
                controller.move(Vector2.Zero);
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
               
            //    if (!jumped)
            //        controller.jump(15.0f);
            //    jumped = true;
            //}

            if (KeyboardInput.wasKeyJustPressed(Keys.Space))
            {
                controller.jump(15.0f);
            }

            else if (KeyboardInput.wasKeyJustReleased(Keys.Space))
            {
                controller.jump(15.0f);
            }

            else
                jumped = false;
        }
    }
}
