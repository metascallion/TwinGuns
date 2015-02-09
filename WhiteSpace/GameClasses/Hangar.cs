using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using WhiteSpace.Network;
using WhiteSpace.Composite;
using WhiteSpace.Components.Drawables;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.Components.Physics;
using WhiteSpace.Input;
using WhiteSpace.Content;

namespace WhiteSpace.GameClasses
{
    public class Hangar : UpdateableComponent
    {
        public GameObject targetShip;
        public Transform targetTransform;
        public Transform transform;

        GameObject[,] droneStocks = new GameObject[3, 3];
        GameObject[,] droneLights = new GameObject[3, 3];

        bool player;

        ComponentsSector<gamestate> sector;

        GameObject ressourceCounter;
        GameRessources ressources;

        public Hangar()
        {
        }

        public Hangar(bool player, GameRessources ressources, GameObject targetShip)
        {
            this.player = player;
            this.ressources = ressources;
            this.targetShip = targetShip;
        }

        public override void start()
        {
            ressourceCounter = GameObjectFactory.createLabel(this.parent.sector, Transform.createTransformWithSizeOnPosition(new Vector2(555, 7), new Vector2(100, 20)), "0", 30);   

            base.start();
            Transform parentTransform = this.parent.getComponent<Transform>();

            if (player == Client.host)
            {
                this.transform = Transform.createTransformWithSizeOnPosition(new Vector2(parentTransform.position.X, 130), new Vector2(180, 250));
            }

            else
            {
                this.transform = Transform.createTransformWithSizeOnPosition(new Vector2(Game1.graphics.PreferredBackBufferWidth - 180, 130), new Vector2(180, 250));
            }

            GameObject clickArea = GameObjectFactory.createClickableArea(this.parent.sector, this.transform, 0);
            clickArea.addComponent<BoxCollider>();

            if (this.player == Client.host)
            {
                clickArea.getComponent<Clickable>().releaseMethods += buildHangarButtons;
                buildHangarButtons(clickArea.getComponent<Clickable>());
            }
            Client.registerNetworkListenerMethod("BuildDrone", OnBuildDroneMessageEnter);
            Client.registerNetworkListenerMethod("OpenHangar", OnOpenHangarMessageEnter);


            if (this.player == Client.host)
            {
                for (int x = 0; x < droneLights.GetLength(0); x++)
                {
                    for (int y = 0; y < droneLights.GetLength(1); y++)
                    {
                        GameObject currentLight = new GameObject(this.parent.sector);
                        Transform trans = Transform.createTransformOnPosition(new Vector2(this.transform.position.X + 85 + 20 * x - y * 35, this.transform.position.Y + 30 + y * 45));
                        trans.Size = new Vector2(10, 10);
                        currentLight.addComponent(trans);
                        currentLight.addComponent(new ColoredBox(Color.BlanchedAlmond, 10));
                        currentLight.addComponent(new DroneAmp());

                        droneLights[x, y] = currentLight;
                    }
                }
            }
        }

        public void buildHangarButtons(Clickable sender)
        {
            sector = new ComponentsSector<gamestate>(gamestate.game);

            Transform background = Transform.createTransformWithSizeOnPosition(new Vector2(200, 170), new Vector2(110, 140));
            GameObject go = new GameObject(sector);
            go.addComponent(background);
            go.addComponent(new ColoredBox(Color.SlateGray));

            Transform t0 = Transform.createTransformWithSizeOnPosition(new Vector2(200, 170), new Vector2(110, 20));
            GameObjectFactory.createLabel(sector, t0, "Drone [20 R]");

            Transform t1 = Transform.createTransformWithSizeOnPosition(new Vector2(200, 200), new Vector2(50, 30));
            GameObject b1 = GameObjectFactory.createButton(sector, t1, "+", 0, sendBuildDroneMessage);

            Transform t2 = Transform.createTransformWithSizeOnPosition(new Vector2(200, 240), new Vector2(50, 30));
            GameObject b2 = GameObjectFactory.createButton(sector, t2, "+", 1, sendBuildDroneMessage);

            Transform t3 = Transform.createTransformWithSizeOnPosition(new Vector2(200, 280), new Vector2(50, 30));
            GameObject b3 = GameObjectFactory.createButton(sector, t3, "+", 2, sendBuildDroneMessage);

            Transform t4 = Transform.createTransformWithSizeOnPosition(new Vector2(260, 200), new Vector2(50, 30));
            GameObject b4 = GameObjectFactory.createButton(sector, t4, "open", 0, sendOpenHangarMessage);

            Transform t5 = Transform.createTransformWithSizeOnPosition(new Vector2(260, 240), new Vector2(50, 30));
            GameObject b5 = GameObjectFactory.createButton(sector, t5, "open", 1, sendOpenHangarMessage);

            Transform t6 = Transform.createTransformWithSizeOnPosition(new Vector2(260, 280), new Vector2(50, 30));
            GameObject b6 = GameObjectFactory.createButton(sector, t6, "open", 2, sendOpenHangarMessage);

            StateMachine<gamestate>.getInstance().changeState(gamestate.game);
            sender.releaseMethods -= buildHangarButtons;
            sender.releaseMethods += deactivateHangarButtons;
        }

        public void reactivateHangarButtons(Clickable sender)
        {
            sector.reload();
            sender.releaseMethods += deactivateHangarButtons;
            sender.releaseMethods -= reactivateHangarButtons;
        }

        public void deactivateHangarButtons(Clickable sender)
        {
            sector.deactivate();
            sender.releaseMethods += reactivateHangarButtons;
            sender.releaseMethods -= deactivateHangarButtons;
        }

        protected override void update(GameTime gameTime)
        {
            if(KeyboardInput.wasKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.B) && this.player == Client.host)
            {
                sendBuildDroneMessage(new Clickable());
            }

            if(KeyboardInput.wasKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                this.player = !this.player;
                Client.host = true;
                this.parent.addComponent(new LifeSender(this.player));
            }

            if(KeyboardInput.wasKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.U))
            {
                sendOpenHangarMessage(new Clickable());
            }

            ressourceCounter.getComponent<TextDrawer>().text = "R: " + ressources.ressources.ToString() + " / Increase: " + Math.Round(ressources.ressourceGain, 1); 
        }

        public void sendBuildDroneMessage(Clickable sender)
        {
            if (ressources.haveEnoughRessources(15) && !isStockFull(sender.id))
            {
                SendableNetworkMessage msg = new SendableNetworkMessage("BuildDrone");
                msg.addInformation("Player", player);
                msg.addInformation("Index", sender.id);
                Client.sendMessage(msg);
            }
        }

        public void sendOpenHangarMessage(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("OpenHangar");
            msg.addInformation("Index", sender.id);
            msg.addInformation("Player", player);
            Client.sendMessage(msg);
        }

        void OnBuildDroneMessageEnter(ReceiveableNetworkMessage msg)
        {
            Transform transform = Transform.createTransformWithSize(new Vector2(55, 55));
            Transform target = new Transform();

            if (Boolean.Parse(msg.getInformation("Player")) == this.player)
            {
                if (this.player == Client.host)
                {
                    ressources.ressources = (int)float.Parse(msg.getInformation("Ressources"));
                }
                addDrone(int.Parse(msg.getInformation("Index")));
            }
        }

        void OnOpenHangarMessageEnter(ReceiveableNetworkMessage msg)
        {
            if(Boolean.Parse(msg.getInformation("Player")) == this.player)
            {
                openStock(int.Parse(msg.getInformation("Index")));
            }
        }

        public bool isStockFull(int stock)
        {
            for(int i = 0; i < 2; i++)
            {
                if(droneStocks[i, stock] == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void addDrone(int stock)
        {
            for(int i = 2; i >= 0; i--)
            {
                if(droneStocks[i, stock] == null)
                {
                    SpriteEffects effect = SpriteEffects.FlipHorizontally; 

                    if(this.player == Client.host)
                    {
                        effect = SpriteEffects.None;
                    }

                    Transform transform;
                    if (player == Client.host)
                    {
                        transform = Transform.createTransformOnPosition(new Vector2(this.transform.position.X + 40 * i - stock * 35, this.transform.position.Y + 60 + stock * 45));
                        droneLights[i, stock].getComponent<DroneAmp>().time = 3000;
                        droneLights[i, stock].getComponent<DroneAmp>().hasDrone = true;
                    }
                    else
                    {
                        transform = Transform.createTransformOnPosition(new Vector2(this.transform.position.X + (this.transform.Size.X -62) - 40 * i + stock * 35, this.transform.position.Y + 60 + stock * 45));
                    }

                    droneStocks[i, stock] = GameObjectFactory.createDrone(this.parent.sector, transform, "Drone", effect, 6, targetTransform);
                    AfterTimeComponentAdder adder = new AfterTimeComponentAdder(3000);
                    adder.addToComponentsToAddAfterTime(new TextureRegion(ContentLoader.getContent<Texture2D>("Drone"), effect));
                    droneStocks[i, stock].addComponent(adder);
                    break;
                }
            }
        }

        public void openStock(int stock)
        {
            for (int i = 0; i <= 2; i++)
            {
                if (droneStocks[i, stock] != null && !droneStocks[i, stock].hasComponent<AfterTimeComponentAdder>())
                {
                    if (this.player == Client.host)
                    {
                        Tower.thisDronesTransforms.Add(droneStocks[i, stock].getComponent<Transform>());
                        droneLights[i, stock].getComponent<DroneAmp>().hasDrone = false;
                    }
                    else
                    {
                        Tower.enemyDronesTransforms.Add(droneStocks[i, stock].getComponent<Transform>());
                    }
                    droneStocks[i, stock].addComponent(new Ship(targetTransform, this.targetShip));
                    droneStocks[i, stock] = null;
                }
            }
        }

    }
}
