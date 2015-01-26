using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WhiteSpace.Components.Drawables;
using WhiteSpace.GameLoop;
using System.IO;
using WhiteSpace.Temp;

namespace WhiteSpace.Components.Animation
{
    public class Animator : UpdateableComponent
    {
        private Dictionary<string, Animation> animations = new Dictionary<string,Animation>();

        private TextureRegion textureRegionToAnimate;
        public SpriteSheet SheetToTakeFramesFrom { get; set; }

        private bool play;
        private bool loop;

        public float AnimationSpeed{ get; set; }
        private float animationTimer;

        Animation activeAnimation;

        public Animator()
        {

        }

        public static Animator loadAnimator(TextureRegion regionToAnimate, string animatorName, int animationInterval)
        {
            StreamReader reader = new StreamReader(animatorName + ".txt");
            Animator animatorToReturn = new Animator(regionToAnimate);
            animatorToReturn.AnimationSpeed = animationInterval;
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


        private Animator(TextureRegion region)
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
            if(this.play)
            {
                animate(gameTime);
            }
        }
    }
}
