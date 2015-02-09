using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Components;

namespace WhiteSpace.GameClasses
{
    public class ShiftedGrid : Grid
    {
        public int shiftOffset;

        public ShiftedGrid(int rows, int cols, int tileSize, Vector2 position, int offset, int shiftOffset) : base(rows, cols, tileSize, position, offset)
        {
            this.shiftOffset = shiftOffset;
        }

        public override void start()
        {
            base.start();
            shiftGrid();
        }

        private void shiftGrid()
        {
            for(int y = 0; y < this.gameObjects.GetLength(1); y++)
            {
                for(int x = 0; x < this.gameObjects.GetLength(0); x++)
                {
                    this.gameObjects[x, y].getComponent<Transform>().position.X += this.shiftOffset * y;
                }
            }
        }
    }
}
