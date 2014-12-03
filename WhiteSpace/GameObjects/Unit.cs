using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Drawables;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameObjects
{

    public class TestRotationGameObject <StateType> : GameObject <StateType> where StateType : struct
    {
        public TextureRegion<StateType> unitTexture;
        public float speed = 0.5f;
        Animator<StateType> animator;
       
        public TestRotationGameObject(StateType type, Transform transform, Texture2D texture, SpriteSheet sheet) : base(type, transform)
        {
            unitTexture = new TextureRegion<StateType>(type, transform, texture);
            animator = new Animator<StateType>(20f, this.unitTexture, sheet);
            animator.addAnimation("Walk", new Animation(1, 30));
            animator.addAnimation("Idle", new Animation(1, 1));
            animator.playAnimation("Idle", false);
            animator.AnimationSpeed = 20;
        }

        public TestRotationGameObject(Transform transform, Texture2D texture, SpriteSheet sheet)
            : base(transform)
        {
            unitTexture = new TextureRegion<StateType>(transform, texture);
            animator = new Animator<StateType>(20f, this.unitTexture, sheet);
            animator.addAnimation("Walk", new Animation(1, 30));
            animator.addAnimation("Idle", new Animation(1, 1));
            animator.playAnimation("Idle", false);
            animator.AnimationSpeed = 20;
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
                animator.playAnimation("Walk", true);
            }

            else
            {
                animator.playAnimation("Idle", false);
            }

            /*
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.Rotation = 0;
                this.transform.translateOnXAxis(elapsedTime * speed);
                unitTexture.SpriteEffect = SpriteEffects.None;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //this.Rotation = 0;
                //this.transform.translateOnXAxis(elapsedTime *- speed);
                this.transform.translate(transform.transformDirection(direction.left));
                unitTexture.SpriteEffect = SpriteEffects.FlipHorizontally;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.unitTexture.SpriteEffect = SpriteEffects.None;
                this.transform.Rotation = -80;
                this.transform.translateOnYAxis(elapsedTime * speed);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.unitTexture.SpriteEffect = SpriteEffects.None;
                this.transform.Rotation = 80;
                this.transform.translateOnYAxis(elapsedTime * -speed);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.transform.Rotation = transform.Rotation + 0.001f * elapsedTime;
            }
            */

            animator.playAnimation("Walk", true);
            if (Vector2.Distance(new Vector2((float)Mouse.GetState().Position.X, (float)Mouse.GetState().Position.Y), this.Position) > 10)
            {
                this.transform.translate(transform.transformDirection(direction.right) * speed * elapsedTime);
            }
            this.transform.lookAt(new Vector2((float)Mouse.GetState().Position.X, (float)Mouse.GetState().Position.Y));
        }
    }
}
