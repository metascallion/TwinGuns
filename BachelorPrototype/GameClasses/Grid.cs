using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BachelorPrototype.GameClasses
{
    public class Grid
    {
        ClickableUI[,] towerfields;
        int tileSize;
        public Microsoft.Xna.Framework.Vector2 startPos;
        public bool ownedByPlayerOne = false;

        public Grid(Microsoft.Xna.Framework.Vector2 startPos, int tileSize, int rows, int cols, bool ownedByPlayerOne)
        {
            this.towerfields = new ClickableUI[cols, rows];
            this.startPos = startPos;
            this.tileSize = tileSize;
            this.ownedByPlayerOne = ownedByPlayerOne;
            generateGrid(rows, cols);
        }

        private void sendTilePressed(ClickableUI sender)
        {
            if (this.ownedByPlayerOne && Game1.client.name == "1" || !this.ownedByPlayerOne && Game1.client.name == "2")
                Game1.client.sendMessage(new Network.NetworkMessage("gridinput," + sender.Name));
            else
                Game1.client.sendMessage(new Network.NetworkMessage("enemygridinput," + sender.Name));
        }

        public void destoryTower(string msg)
        {
            if (msg != null && msg != "")
            {
                string t1 = msg[1].ToString();
                string t2 = msg[2].ToString();

                int x = System.Convert.ToInt32(t1);
                int y = System.Convert.ToInt32(t2);


                towerfields[x, y] = new ClickableUI();
                towerfields[x, y].clickEvent += sendTilePressed;
                towerfields[x, y].Size = new Microsoft.Xna.Framework.Vector2(tileSize, tileSize);
                towerfields[x, y].Position = new Microsoft.Xna.Framework.Vector2(startPos.X + (tileSize + 10) * x, startPos.Y + (tileSize + 10) * y);
                towerfields[x, y].activeState = state.lobby;
                UpdateExecuter.Updates += towerfields[x, y].update;
                DrawableCollector.drawables.Add(towerfields[x, y]);

                Tower.destoryTowerOnPosition(x, y);
            }
        }

        public void buildTower(string msg)
        {
            if (msg != null && msg != "")
            {
                string t1 = msg[1].ToString();
                string t2 = msg[2].ToString();

                int x = System.Convert.ToInt32(t1);
                int y = System.Convert.ToInt32(t2);

                TextureRegion region = new TextureRegion();
                if (msg[0] == 'a')
                {
                    region.Texture = Game1.towerTexture1;
                }

                else
                {
                    region.Texture = Game1.towerTexture2;
                }

                towerfields[x, y].StandartDrawer = region;
                towerfields[x, y].HoverDrawer = towerfields[x, y].StandartDrawer;
                towerfields[x, y].ClickedDrawer = towerfields[x, y].StandartDrawer;
                Tower t = new Tower(0.5f);
                t.Position = new Microsoft.Xna.Framework.Vector2(x * tileSize + startPos.X, y * tileSize + startPos.Y);
                t.x = x;
                t.y = y;
                if (msg[0].ToString() == "a")
                {
                    t.ownedByPlayerOne = true;
                }
                else
                {
                    t.ownedByPlayerOne = false;
                }
            }
        }

        private void buildTile(int x, int y)
        {
            ClickableUI buttonToBuild = new ClickableUI();
            buttonToBuild.activeState = state.lobby;
            buttonToBuild.clickEvent += sendTilePressed;
            buttonToBuild.Size = new Microsoft.Xna.Framework.Vector2(this.tileSize, this.tileSize);
            buttonToBuild.Position = new Microsoft.Xna.Framework.Vector2(startPos.X + x * (tileSize + 10), startPos.Y + y * (tileSize + 10));
            UpdateExecuter.Updates += buttonToBuild.update;
            DrawableCollector.drawables.Add(buttonToBuild);
            if(this.ownedByPlayerOne)
            {
                buttonToBuild.Name = "a" + x + y;
            }

            else
            {
                buttonToBuild.Name = "b" + x + y;
            }

            towerfields[x, y] = buttonToBuild;
        }

        private void generateGrid(int rows, int cols)
        {
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    buildTile(x, y);
                }
            }
        }
    }
}
