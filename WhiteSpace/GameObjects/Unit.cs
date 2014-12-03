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
        Animator<StateType> animator;
       

        public TestRotationGameObject(StateType type, Transform transform, SpriteSheet texture) : base(type, transform)
        {
            unitTexture = new TextureRegion<StateType>(type, transform, texture);
            animator = new Animator<StateType>(20f, this.unitTexture);
            animator.addAnimation("Walk", new Animation(1, 30));
            animator.addAnimation("Idle", new Animation(1, 1));
        }

        public TestRotationGameObject(Transform transform, SpriteSheet texture)
            : base(transform)
        {
            unitTexture = new TextureRegion<StateType>(transform, texture);
            animator = new Animator<StateType>(20f, this.unitTexture);
            animator.addAnimation("Walk", new Animation(1, 30));
            animator.addAnimation("Idle", new Animation(1, 1));
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            testMovement(gameTime);
        }

        public void testMovement(GameTime time)
        {
            float elapsedTime = (float)time.ElapsedGameTime.TotalMilliseconds;

            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.transform.translate(new Vector2(transform.Direction.Y, -transform.Direction.X) * elapsedTime * -0.1f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.transform.translate(new Vector2(-transform.Direction.Y, transform.Direction.X) * elapsedTime * -0.1f);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.transform.translate(transform.Direction * elapsedTime * -0.1f);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.transform.translate(transform.Direction * elapsedTime * 0.1f);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.transform.Rotation = transform.Rotation + 0.1f;
            }
        }
    }

    public class Unit<StateType> : GameObject<StateType> where StateType : struct
    {
        public TextureRegion<StateType> unitTexture;
        Animator<StateType> animator;
       

        public Unit(StateType type, Transform transform, SpriteSheet texture) : base(type, transform)
        {
            unitTexture = new TextureRegion<StateType>(type, transform, texture);
            animator = new Animator<StateType>(20f, this.unitTexture);
            animator.addAnimation("Walk", new Animation(1, 30));
            animator.addAnimation("Idle", new Animation(1, 1));
        }

        public Unit(Transform transform, SpriteSheet texture) : base (transform)
        {
            unitTexture = new TextureRegion<StateType>(transform, texture);
            animator = new Animator<StateType>(20f, this.unitTexture);
            animator.addAnimation("Walk", new Animation(1, 30));
            animator.addAnimation("Idle", new Animation(1, 1));
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            testMovement(gameTime);
        }

        public void testMovement(GameTime time)
        {
            float elapsedTime = (float)time.ElapsedGameTime.TotalMilliseconds;

            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.transform.translateOnXAxis(elapsedTime * 0.4f);
                animator.playAnimation("Walk", true);
                this.unitTexture.SpriteEffect = SpriteEffects.None;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.transform.translateOnXAxis(-elapsedTime * 0.4f);
                animator.playAnimation("Walk", true);
                this.unitTexture.SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.transform.translateOnYAxis(elapsedTime * 0.4f);
                animator.playAnimation("Walk", true);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.transform.translateOnYAxis(-elapsedTime * 0.4f);
                animator.playAnimation("Walk", true);
            }

            else
            {
                animator.playAnimation("Idle", true);
            }
        }
    }
}
