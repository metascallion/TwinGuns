using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Drawables;
using WhiteSpace.GameLoop;
using System.IO;

namespace WhiteSpace.Components.Animation
{
    public class Animator<StateType> : Updateable<StateType> where StateType : struct
    {
        private Dictionary<string, Animation> animations = new Dictionary<string,Animation>();

        private TextureRegion<StateType> textureRegionToAnimate;
        public SpriteSheet SheetToTakeFramesFrom { get; set; }

        private bool play;
        private bool loop;

        public float AnimationSpeed{ get; set; }
        private float animationTimer;

        Animation activeAnimation;

        public static Animator<StateType> loadAnimator<StateType>(TextureRegion<StateType> regionToAnimate, string animatorName, ComponentsSector<StateType> updaterToRegisterTo) where StateType : struct
        {
            StreamReader reader = new StreamReader(animatorName + ".txt");
            Animator<StateType> animatorToReturn = new Animator<StateType>(regionToAnimate, updaterToRegisterTo);
            string[] spriteSheetData = reader.ReadLine().Split(',');
            animatorToReturn.SheetToTakeFramesFrom = new SpriteSheet(regionToAnimate.Texture, int.Parse(spriteSheetData[0]), int.Parse(spriteSheetData[1]));
            string animation;
            while ((animation = reader.ReadLine()) != null)
            {
                string[] animationData = animation.Split(',');
                animatorToReturn.addAnimation(animationData[0], new Animation(int.Parse(animationData[1]), int.Parse(animationData[2])));
            }
            return animatorToReturn;
        }


        private Animator(TextureRegion<StateType> region, ComponentsSector<StateType> updaterToRegisterTo) : base(updaterToRegisterTo)
        {
            this.textureRegionToAnimate = region;
        }

        public void addAnimation(string name, Animation animation)
        {
            this.animations[name] = animation;
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
            this.textureRegionToAnimate.VisibleArea = this.SheetToTakeFramesFrom.getRectangleForFrame(this.activeAnimation.currentFrame - 1);

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
