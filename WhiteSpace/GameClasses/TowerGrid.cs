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
        bool player;
        bool attackTower;

        private GameRessources ressources;

        public TowerGrid(int rows, int cols, int tileSize, Vector2 position, int offset, bool player, GameRessources ressources) : base(rows, cols, tileSize, position, offset)
        {
            this.player = player;
            this.ressources = ressources;
            Client.registerNetworkListenerMethod("BuildTower", OnBuildTowerMessage);
            Client.registerNetworkListenerMethod("DestroyTower", OnDestroyTowerMessage);
        }

        public override void start()
        {
            base.start();
            this.addComponent<Button>();

            if(this.player == Client.host)
            {
                foreach (Button b in this.getComponents<Button>())
                {
                    b.releaseMethods += sendBuildTowerMessage;
                }

                GameObjectFactory.createButton(this.parent.sector, Transform.createTransformWithSizeOnPosition(new Vector2(10, 400), new Vector2(150, 30)), "Ressource Tower", changeToRessouce);
                GameObjectFactory.createButton(this.parent.sector, Transform.createTransformWithSizeOnPosition(new Vector2(10, 440), new Vector2(150, 30)), "Attack Tower", changeToAttack);
            }

            else
            {
                foreach (Button b in this.getComponents<Button>())
                {
                    b.releaseMethods += sendDestroyTowerMessage;
                }
            }
        }

        void changeToRessouce(Clickable sender)
        {
            this.attackTower = false;
        }

        void changeToAttack(Clickable sender)
        {
            this.attackTower = true;
        }

        void sendBuildTowerMessage(Clickable sender)
        {
            if (ressources.haveEnoughRessources(25))
            {
                sender.parent.removeComponent<Tower>();
                SendableNetworkMessage msg = new SendableNetworkMessage("BuildTower");
                msg.addInformation("x", sender.parent.getComponent<GridTile>().x);
                msg.addInformation("y", sender.parent.getComponent<GridTile>().y);
                msg.addInformation("Player", this.player);
                msg.addInformation("Type", this.attackTower);
                Client.sendMessage(msg);
            }
        }

        void sendDestroyTowerMessage(Clickable sender)
        {
            if (ressources.haveEnoughRessources(25))
            {
                SendableNetworkMessage msg = new SendableNetworkMessage("DestroyTower");
                msg.addInformation("x", sender.parent.getComponent<GridTile>().x);
                msg.addInformation("y", sender.parent.getComponent<GridTile>().y);
                msg.addInformation("Player", this.player);
                Client.sendMessage(msg);
            }
        }

        void OnBuildTowerMessage(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) == this.player)
            {
                if (this.player == Client.host)
                {
                    ressources.ressources = (int)float.Parse(msg.getInformation("Ressources"));
                    this.buildTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")), true, Boolean.Parse(msg.getInformation("Type")));
                }

                else
                {
                    this.buildMirroredTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")), false, Boolean.Parse(msg.getInformation("Type")));
                }
            }
        }

        void OnDestroyTowerMessage(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) == this.player)
            {
                if (player == Client.host)
                {
                    this.destroyMirroredTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")));
                }

                else
                {
                    ressources.ressources = (int)float.Parse(msg.getInformation("Ressources"));
                    this.destroyTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")));
                }
            }
        }

        public void buildTower(int x, int y, bool playerOne, bool towerType)
        {
            GameObject o = this.gameObjects[x, y];
            if (towerType)
            {
                o.getComponent<Button>().setStandartDrawer(new ColoredBox(Color.Red));
                o.getComponent<Button>().setHoverDrawer(new ColoredBox(Color.Red));
                o.getComponent<Button>().setClickedDrawer(new ColoredBox(Color.Red));
                o.addComponent(new Tower(1000, playerOne));
            }

            else
            {
                o.getComponent<Button>().setStandartDrawer(new ColoredBox(Color.Blue));
                o.getComponent<Button>().setHoverDrawer(new ColoredBox(Color.Blue));
                o.getComponent<Button>().setClickedDrawer(new ColoredBox(Color.Blue));
            }
        }


        public void buildMirroredTower(int x, int y, bool playerOne, bool towerType)
        {
            buildTower(this.gameObjects.GetLength(0) - x - 1, y, playerOne, towerType);
        }

        public void destroyTower(int x, int y)
        {
            GameObject o = this.gameObjects[x, y];
            o.getComponent<Button>().resetDrawers();
            o.removeComponent<Tower>();
        }

        public void destroyMirroredTower(int x, int y)
        {
            destroyTower(this.gameObjects.GetLength(0) - x -1, y);
        }

        protected override void update(GameTime gameTime)
        {
            if(KeyboardInput.wasKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                this.player = !this.player;
            }
        }
    }
}
