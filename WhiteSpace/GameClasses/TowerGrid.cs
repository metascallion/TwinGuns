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
using System.Threading;
using WhiteSpace.Components.Physics;

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

            foreach (Button b in this.getComponents<Button>())
            {
                b.setStandartDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("gridnormal")));
                b.setHoverDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("gridblue")));
                b.setClickedDrawer(new TextureRegion(ContentLoader.getContent<Texture2D>("gridred")));

                b.hoverMethods += delegate(Clickable sender) { new Sound("Shot", false, 0.5f); };
            }

            if(this.player == Client.host)
            {
                foreach (Button b in this.getComponents<Button>())
                {
                    b.releaseMethods += sendBuildTowerMessage;
                }

                GameObjectFactory.createButton(this.parent.sector, Transform.createTransformWithSizeOnPosition(new Vector2(150, 600), new Vector2(150, 30)), "Ressource Tower", changeToRessouce);
                GameObjectFactory.createButton(this.parent.sector, Transform.createTransformWithSizeOnPosition(new Vector2(150, 640), new Vector2(150, 30)), "Attack Tower", changeToAttack);
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
            if (!sender.parent.hasComponent<Tower>() && !sender.parent.hasComponent<RessourceTower>())
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
            if (ressources.haveEnoughRessources(25) && !sender.parent.hasComponent<AfterTimeComponentAdder>() && (sender.parent.hasComponent<Tower>() || sender.parent.hasComponent<RessourceTower>()))
            {
                SendableNetworkMessage msg = new SendableNetworkMessage("DestroyTower");
                msg.addInformation("x", sender.parent.getComponent<GridTile>().x);
                msg.addInformation("y", sender.parent.getComponent<GridTile>().y);
                msg.addInformation("Player", this.player);
                bool type = false;
                if(sender.parent.hasComponent<Tower>())
                {
                    type = true;
                }
                msg.addInformation("Type", type);
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
                    GameObject go = new GameObject(this.parent.sector);
                    go.addComponent(Transform.createTransformWithSizeOnPosition(new Vector2(800, 0), new Vector2(25, 25)));
                    go.addComponent(new ColoredBox(Color.White));
                    go.addComponent(new TwingunShot(this.gameObjects[3 - int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y"))].getComponent<Transform>()));
                }

                else
                {
                    ressources.ressources = (int)float.Parse(msg.getInformation("Ressources"));
                    GameObject go = new GameObject(this.parent.sector);
                    go.addComponent(Transform.createTransformWithSizeOnPosition(new Vector2(0, 0), new Vector2(25, 25)));
                    go.addComponent(new ColoredBox(Color.White));
                    go.addComponent(new TwingunShot(this.gameObjects[int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y"))].getComponent<Transform>()));
                }
            }
        }

        public void buildTower(int x, int y, bool playerOne, bool towerType)
        {
            GameObject o = this.gameObjects[x, y];

            if (towerType)
            {
                AfterTimeComponentAdder adder = new AfterTimeComponentAdder(3000);
                adder.addToComponentsToAddAfterTime(new Tower(850, playerOne));
                o.addComponent(adder);
                adder.expiredFunctions += delegate
               {
                   Transform trans = o.getComponent<Transform>();
                   GameObject tower = GameObjectFactory.createTexture(this.parent.sector, trans.Center - new Vector2(35, 50), new Vector2(65, 62), ContentLoader.getContent<Texture2D>("Attacktower"), SpriteEffects.None, y + 1);
                   o.addComponent(new Tower(tower));

               };
            }

            else
            {
                SpriteEffects effect = SpriteEffects.None;
                if (!playerOne)
                {
                    effect = SpriteEffects.FlipHorizontally;
                }

                Transform trans = o.getComponent<Transform>();
                GameObject tower = GameObjectFactory.createTexture(this.parent.sector, trans.Center - new Vector2(25, 75), new Vector2(50, 85), ContentLoader.getContent<Texture2D>("Energietower"), effect, y + 1);
                o.addComponent(new RessourceTower(tower));
            }
        }


        public void buildMirroredTower(int x, int y, bool playerOne, bool towerType)
        {
            buildTower(this.gameObjects.GetLength(0) - x - 1, y, playerOne, towerType);
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
