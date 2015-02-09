using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;
using WhiteSpace.Composite;
using Microsoft.Xna.Framework;
using WhiteSpace.Network;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Components.Drawables;
using System.Threading;
using WhiteSpace.Components.Physics;
using WhiteSpace.Input;
using WhiteSpace.Content;
using Microsoft.Xna.Framework.Input;

namespace WhiteSpace.GameClasses
{
    public  enum towertype
    {
        none,
        attack,
        ressource,
        shield,
        energy,
        defense
    }

    public enum towerselectionstate
    {
        active,
        inactive
    }

    public class TowerTypeSelector : UpdateableComponent
    {
        List<GameObject> btns = new List<GameObject>();
        Vector2 positionOffset;
        public int x, y;
        public bool player;
        towertype currentTypeToBuild;
        TowerGrid grid;


        public TowerTypeSelector()
        {
        }

        public TowerTypeSelector(TowerGrid grid)
        {
            this.grid = grid;
        }

        public override void start()
        {
            base.start();

            addType(new Vector2(45, 45), Color.Blue, towertype.ressource);
            addType(new Vector2(-45, -45), Color.Red, towertype.attack);
        }

        protected override void update(GameTime gameTime)
        {
            if(MouseInput.wasKeyJustReleased(mousebutton.left))
            {
                grid.reverseBtnActive(null);
            }
        }

        private void addType(Vector2 position, Color color, towertype typeToSet)
        {
            Transform btnTransform = Transform.createTransformWithSizeOnPosition(position, new Vector2(25, 25));
            btnTransform.Center = btnTransform.position;

            GameObject btn = GameObjectFactory.createButton(this.parent.sector, btnTransform);
            btn.getComponent<Button>().setAllDrawers(new ColoredBox(color, 100));
            btn.getComponent<Button>().releaseMethods += (Clickable Sender) => { this.currentTypeToBuild = typeToSet; sendBuildTowerMessage(Sender); grid.reverseBtnActive(Sender); resetPosition(Sender); };

            
            btns.Add(btn);
        }

        public void setPosition(Vector2 position)
        {
            for(int i = 0; i < btns.Count(); i++)
            {
                Transform transform = btns[i].getComponent<Transform>();
                transform.Center = transform.Center + position;
            }

            positionOffset = position;
        }

        public void resetPosition(Clickable sender)
        {
            for (int i = 0; i < btns.Count(); i++)
            {
                Transform transform = btns[i].getComponent<Transform>();
                transform.Center = transform.Center - positionOffset;
            }

            positionOffset = Vector2.Zero;

            StateMachine<towerselectionstate>.getInstance().changeState(towerselectionstate.inactive);
        }

        void sendBuildTowerMessage(Clickable sender)
        {
            if (!sender.parent.hasComponent<Tower>() && !sender.parent.hasComponent<RessourceTower>())
            {
                sender.parent.removeComponent<Tower>();
                SendableNetworkMessage msg = new SendableNetworkMessage("BuildTower");
                msg.addInformation("x", x);
                msg.addInformation("y", y);
                msg.addInformation("Player", this.player);
                msg.addInformation("TowerType", this.currentTypeToBuild);
                Client.sendMessage(msg);
            }
        }
    }


    public class TowerGrid : ShiftedGrid
    {
        bool player;
        private GameRessources ressources;
        GameObject towerTypeSelector;

        public TowerGrid(int rows, int cols, int tileSize, Vector2 position, int offset, bool player, GameRessources ressources, int shiftOffset) : base(rows, cols, tileSize, position, offset, shiftOffset)
        {
            this.player = player;
            this.ressources = ressources;
            Client.registerNetworkListenerMethod("BuildTower", OnBuildTowerMessage);
            Client.registerNetworkListenerMethod("DestroyTower", OnDestroyTowerMessage);
            Client.registerNetworkListenerMethod("TowerUpdate", OnTowerUpdateMessage);  
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
                towerTypeSelector = new GameObject(new ComponentsSector<towerselectionstate>(towerselectionstate.active));
                towerTypeSelector.addComponent(new TowerTypeSelector(this));
                towerTypeSelector.getComponent<TowerTypeSelector>().player = this.player;

                foreach (Button b in this.getComponents<Button>())
                {
                    b.releaseMethods += (Clickable sender) => 
                    {
                        towerTypeSelector.getComponent<TowerTypeSelector>().resetPosition(null);
                        towerTypeSelector.getComponent<TowerTypeSelector>().setPosition(b.parent.getComponent<Transform>().Center);
                        towerTypeSelector.getComponent<TowerTypeSelector>().x = b.parent.getComponent<GridTile>().x;
                        towerTypeSelector.getComponent<TowerTypeSelector>().y = b.parent.getComponent<GridTile>().y;
                        StateMachine<towerselectionstate>.getInstance().changeState(towerselectionstate.active);
                        reverseBtnActive(sender);
                    };
                }
            }

            else
            {
                foreach (Button b in this.getComponents<Button>())
                {
                    b.releaseMethods += sendDestroyTowerMessage;
                }
            }
        }

        public void reverseBtnActive(Clickable sender)
        {
            foreach (Button btn in this.getComponents<Button>())
            {
                btn.active = !btn.active;
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


        void OnTowerUpdateMessage(ReceiveableNetworkMessage msg)
        {
            bool player = Boolean.Parse(msg.getInformation("player"));

            if (player == this.player)
            {
                for (int x = 0; x < this.gameObjects.GetLength(0); x++)
                {
                    for (int y = 0; y < this.gameObjects.GetLength(1); y++)
                    {
                        switch (msg.getInformation(x.ToString() + y.ToString()))
                        {
                            case "none":
                                if (player == Client.host)
                                {
                                    gameObjects[x, y].removeComponent<Tower>();
                                    gameObjects[x, y].removeComponent<RessourceTower>();
                                }
                                else
                                {
                                    gameObjects[gameObjects.GetLength(0) - 1 - x, y].removeComponent<Tower>();
                                    gameObjects[gameObjects.GetLength(0) - 1 - x, y].removeComponent<RessourceTower>();
                                }
                                break;
                            case "attack":
                                bool owned = false;

                                if(!Client.host)
                                {
                                    if(player == Client.host)
                                    {
                                        owned = true;
                                    }
                                }
                                else
                                {
                                    if(player == Client.host)
                                    {
                                        owned = true;
                                    }
                                }

                                if (player != Client.host)
                                {
                                    if (!gameObjects[gameObjects.GetLength(0) - 1 - x, y].hasComponent<Tower>())
                                    {
                                        buildMirroredTower(x, y, owned, true);
                                    }
                                }
                                else
                                {
                                    if (!gameObjects[x, y].hasComponent<Tower>())
                                    {
                                        buildTower(x, y, owned, true);
                                    }
                                }
                                break;
                            case "ressource":

                                if (player != Client.host)
                                {
                                    if (!gameObjects[gameObjects.GetLength(0) - 1 - x, y].hasComponent<RessourceTower>())
                                    {
                                        buildMirroredTower(x, y, false, false);
                                    }
                                }
                                else
                                {
                                    if (!gameObjects[x, y].hasComponent<RessourceTower>())
                                    {
                                        buildTower(x, y, true, false);
                                    }
                                }
                                break;
                        }
                    }
                }
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
                o.addComponent(adder);
                adder.expiredFunctions += delegate
               {
                   Transform trans = o.getComponent<Transform>();
                   GameObject tower = GameObjectFactory.createTexture(this.parent.sector, trans.Center - new Vector2(35, 50), new Vector2(65, 62), ContentLoader.getContent<Texture2D>("Attacktower"), SpriteEffects.None, y + 1);
                   o.addComponent(new Tower(850, playerOne, tower));

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
