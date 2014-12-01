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
    public class SpriteSheet
    {
        public Texture2D Texture { get; set; }
        int rows;
        int cols;

        Vector2 tileSize;

        public SpriteSheet(Texture2D texture, int rows, int cols)
        {
            this.Texture = texture;
            this.rows = rows;
            this.cols = cols;

            calculateTileSize();
        }

        private void calculateTileSize()
        {
            int sizeX = this.Texture.Bounds.Width;
            int sizeY = this.Texture.Bounds.Height;

            tileSize = new Vector2(sizeX / this.rows, sizeY / this.cols);
        }

        private Rectangle getTilesRectangleOnPosition(Vector2 position)
        {
            Rectangle tileRectangle = new Rectangle((int)(position.X * tileSize.X), (int)(position.Y * tileSize.Y), (int)this.tileSize.X, (int)this.tileSize.Y);
            return tileRectangle;
        }

        private Vector2 calculatePositionForFrame(int frame)
        {
            int counter = 0;
            Vector2 temp = Vector2.Zero;

            while (counter < frame)
            {
                if(temp.X < rows - 1)
                {
                    temp.X++;
                }

                else
                {
                    temp.X = 0;
                    temp.Y++;
                }

                counter++;
            }

            return temp;
        }

        public Rectangle getRectangleForFrame(int frame)
        {
            return getTilesRectangleOnPosition(calculatePositionForFrame(frame));
        }

    }

    public class Animation
    {
        public int EndFrame { get; set; }

        public int currentFrame;

        public Animation(int startFrame, int endFrame)
        {
            this.currentFrame = startFrame;
            this.EndFrame = endFrame;
        }
    }

    class Animator<StateType> : Updateable<StateType> where StateType : struct
    {
        Dictionary<string, Animation> animations = new Dictionary<string,Animation>();

        TextureRegion<StateType> textureRegion;

        bool play;
        bool loop;

        public float AnimationSpeed{ get; set; }
        private float animationTimer;

        Animation activeAnimation;

        public void addAnimation(string name, Animation animation)
        {
            this.animations[name] = animation;
        }

        public Animator(float animationSpeed, TextureRegion<StateType> region)
        {
            this.AnimationSpeed = animationSpeed;
            this.textureRegion = region;
        }

        public Animator(float animationSpeed, TextureRegion<StateType> region, StateType activeState)
        {
            this.AnimationSpeed = animationSpeed;
            this.textureRegion = region;
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
            this.textureRegion.VisibleArea = this.textureRegion.sheetToReferTo.getRectangleForFrame(this.activeAnimation.currentFrame - 1);

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
