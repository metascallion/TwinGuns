using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Drawables;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Temp
{
    public class Animator<StateType> : Updateable<StateType> where StateType : struct
    {
        private Dictionary<string, Animation> animations = new Dictionary<string,Animation>();

        private TextureRegion<StateType> textureRegionToAnimate;
        private SpriteSheet sheetToTakeFramesFrom;

        private bool play;
        private bool loop;

        public float AnimationSpeed{ get; set; }
        private float animationTimer;

        Animation activeAnimation;

        public void addAnimation(string name, Animation animation)
        {
            this.animations[name] = animation;
        }

        public Animator(float animationSpeed, TextureRegion<StateType> region, SpriteSheet sheetToTakeFramesFrom)
        {
            this.AnimationSpeed = animationSpeed;
            this.textureRegionToAnimate = region;
            this.sheetToTakeFramesFrom = sheetToTakeFramesFrom;
        }

        public Animator(float animationSpeed, TextureRegion<StateType> region, SpriteSheet sheetToTakeFramesFrom, StateType activeState)
        {
            this.AnimationSpeed = animationSpeed;
            this.textureRegionToAnimate = region;
            this.sheetToTakeFramesFrom = sheetToTakeFramesFrom;
        }

        public void playAnimation(string animationName, bool loop)
        {
            this.activeAnimation = animations[animationName];
            this.play = true;
            this.loop = loop;
        }

        public void stopAnimation()
        {
            this.play = false;
        }

        private void animate(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            animationTimer += elapsedTime;
            if(animationTimer >= AnimationSpeed)
            {
                animationTimer = 0;
                setNextFrame();
            }
        }

        private void setNextFrame()
        {
            this.textureRegionToAnimate.VisibleArea = this.sheetToTakeFramesFrom.getRectangleForFrame(this.activeAnimation.currentFrame - 1);

            if (this.activeAnimation.currentFrame < this.activeAnimation.EndFrame)
            {
                this.activeAnimation.currentFrame++;
            }

            else
            {
                this.activeAnimation.currentFrame = 1;
                if (!this.loop)
                {
                    stopAnimation();
                }
            }
        }

        protected override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if(this.play)
            {
                animate(gameTime);
            }
        }
    }
}
