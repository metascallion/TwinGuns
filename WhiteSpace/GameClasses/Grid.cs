using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameClasses
{
    public class Grid : StandardComponent
    {
        GameObject[,] gameObjects;
        int tileSize;
        Vector2 position;
        int offset;

        public Grid(int rows, int cols, int tileSize, Vector2 position, int offset)
        {
            gameObjects = new GameObject[rows, cols];
            this.tileSize = tileSize;
            this.position = position;
            this.offset = offset;
        }

        public override void start()
        {
            base.start();
            buildGrid();
        }

        private void buildGrid()
        {
            for(int x = 0; x < gameObjects.GetLength(0); x++)
            {
                for(int y = 0; y < gameObjects.GetLength(1); y++)
                {
                    Transform transform = Transform.createTransformWithSizeOnPosition(new Vector2(position.X + (tileSize + offset) * x, position.Y + (tileSize + offset) * y), new Vector2(tileSize, tileSize));
                    gameObjects[x, y] = new GameObject(this.parent.sector);
                    gameObjects[x, y].addComponent(transform);
                }
            }
        }

        public void addComponent<T>() where T : Component, new()
        {
            foreach(GameObject g in gameObjects)
            {
                g.addComponent<T>();
            }
        }
    }
}
