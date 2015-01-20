using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework;
using WhiteSpace.Network;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Components.Drawables;


namespace WhiteSpace.GameClasses
{
    class TowerGrid : Grid
    {
        public TowerGrid(int rows, int cols, int tileSize, Vector2 position, int offset) : base(rows, cols, tileSize, position, offset)
        {

        }

        public override void start()
        {
            base.start();
            this.addComponent<Button>();

            
        }

        public void buildTower(int x, int y)
        {
            GameObject o = this.gameObjects[x, y];
            o.getComponent<Button>().setStandartDrawer(new ColoredBox(Color.Red));
            o.getComponent<Button>().setHoverDrawer(new ColoredBox(Color.Red));
            o.getComponent<Button>().setClickedDrawer(new ColoredBox(Color.Red));
        }


        public void buildMirroredTower(int x, int y)
        {
            buildTower(this.gameObjects.GetLength(0) - x - 1, y);
        }

        public void destroyTower(int x, int y)
        {
            GameObject o = this.gameObjects[x, y];
            o.getComponent<Button>().resetDrawers();
            
        }

        public void destroyMirroredTower(int x, int y)
        {
            destroyTower(this.gameObjects.GetLength(0) - x -1, y);
        }

    }
}
