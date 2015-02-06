using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace.Components.Animation
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; set; }
        private int rows;
        private int cols;
        private Vector2 tileSize;

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
                if (temp.X < rows - 1)
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
}
