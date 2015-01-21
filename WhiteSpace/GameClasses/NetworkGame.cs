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

namespace WhiteSpace.GameClasses
{
    public class NetworkGame
    {

        ComponentsSector<gamestate> gameSector = new ComponentsSector<gamestate>(gamestate.game);
        GameObject p1Ship;
        GameObject p2Ship;
        TowerGrid grid;
        TowerGrid grid2;

        public NetworkGame()
        {
            buildMotherShips();
            buildDroneButton();


            grid = new TowerGrid(4, 3, 30, new Vector2(230, 350), 5);
            GameObject go = new GameObject(gameSector);
            go.addComponent(grid);

            foreach(Button b in grid.getComponents<Button>())
            {
                b.releaseMethods += sendBuildTowerMessage;
            }

            grid2 = new TowerGrid(4, 3, 30, new Vector2(420, 350), 5);
            GameObject go2 = new GameObject(gameSector);
            go2.addComponent(grid2);
            grid2.addComponent<Button>();

            foreach (Button b in grid2.getComponents<Button>())
            {
                b.releaseMethods += sendDestroyTowerMessage;
            }
            
            StateMachine<gamestate>.getInstance().changeState(gamestate.game);
            Client.registerNetworkListenerMethod("BuildDrone", OnBuildDroneMessageEnter);
            Client.registerNetworkListenerMethod("BuildTower", OnBuildTowerMessage);
            Client.registerNetworkListenerMethod("DestroyTower", OnDestroyTowerMessage);
        }

        void OnBuildTowerMessage(ReceiveableNetworkMessage msg)
        {
            if(Boolean.Parse(msg.getInformation("Player")) == Client.host)
            {
                grid.buildTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")), true);
                Button b = grid.gameObjects[int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y"))].getComponent<Button>();
            }

            else
            {
                grid2.buildMirroredTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")), false);
            }
        }

        void OnDestroyTowerMessage(ReceiveableNetworkMessage msg)
        {
            if (Boolean.Parse(msg.getInformation("Player")) == Client.host)
            {
                grid2.destroyTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")));
            }

            else
            {
                grid.destroyMirroredTower(int.Parse(msg.getInformation("x")), int.Parse(msg.getInformation("y")));
            }
        }

        void sendBuildTowerMessage(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("BuildTower");
            msg.addInformation("x", sender.parent.getComponent<GridTile>().x);
            msg.addInformation("y", sender.parent.getComponent<GridTile>().y);
            msg.addInformation("Player", Client.host);
            Client.sendMessage(msg);
        }

        void sendDestroyTowerMessage(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("DestroyTower");
            msg.addInformation("x", sender.parent.getComponent<GridTile>().x);
            msg.addInformation("y", sender.parent.getComponent<GridTile>().y);
            msg.addInformation("Player", Client.host);
            Client.sendMessage(msg);
        }

        void buildMotherShips()
        {
            Transform ship1Transform = Transform.createTransformWithSizeOnPosition(new Vector2(), new Vector2(150,350));
            Transform ship2Transform = Transform.createTransformWithSizeOnPosition(new Vector2(650, 0), new Vector2(150, 350));
            p1Ship = GameObjectFactory.createMotherShip(gameSector, ship1Transform, ship2Transform, "Ship", Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 100);
            p2Ship = GameObjectFactory.createMotherShip(gameSector, ship2Transform, ship1Transform, "Ship", Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally, 100);
        }


        /// <summary>
        /// Droneeee
        /// </summary>
        void buildDroneButton()
        {
            Transform transform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 430), new Vector2(100, 30));
            GameObjectFactory.createButton(gameSector, transform, "Build Drone", sendBuildDroneMessage);
        }

        void sendBuildDroneMessage(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("BuildDrone");
            string player = "";
            if(Client.host)
            {
                player = "true";
            }
            else
            {
                player = "false";
            }
            msg.addInformation("Player", player);
            Client.sendMessage(msg);
        }

        void OnBuildDroneMessageEnter(ReceiveableNetworkMessage msg)
        {
            Transform transform = Transform.createTransformWithSize(new Vector2(55, 55));
            Transform target = new Transform();
            SpriteEffects effect;

            if (Boolean.Parse(msg.getInformation("Player")) == Client.host)
            {
                transform.Position = p1Ship.getComponent<Transform>().Position + new Vector2(0, p1Ship.getComponent<Transform>().Size.Y * 0.5f);
                effect = SpriteEffects.None;
                target = p2Ship.getComponent<Transform>();
                GameObject go = GameObjectFactory.createDrone(gameSector, transform, "Ship", effect, 15, target);
                Tower.thisDronesTransforms.Add(go.getComponent<Transform>());
            }

            else
            {
                transform.Position = p2Ship.getComponent<Transform>().Position + new Vector2(p2Ship.getComponent<Transform>().Size.X - 55, p2Ship.getComponent<Transform>().Size.Y * 0.5f);
                effect = SpriteEffects.None;
                target = p1Ship.getComponent<Transform>();
                GameObject go = GameObjectFactory.createDrone(gameSector, transform, "Ship", effect, 15, target);
                Tower.enemyDronesTransforms.Add(go.getComponent<Transform>());
            }

        }
    }
}
