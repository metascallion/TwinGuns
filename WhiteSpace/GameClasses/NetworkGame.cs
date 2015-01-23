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

/*Way to handle unreliability example hangar
 * just send Message with Header HangarUpdate and add InformationContent with header DroneCounter
 * if the client receives a hangarupdate which differs from its current state it just equals its state 
 * by adding drones if it has less drones then the update and open the hangar if it has more drones then
 * the hangars in the update if the hangars have still more then zero drones but less then the client
 * then the client adds the amount of drones it needs to add to have the same amount
 */ 

namespace WhiteSpace.GameClasses
{
    public class NetworkGame
    {

        ComponentsSector<gamestate> gameSector = new ComponentsSector<gamestate>(gamestate.game);
        GameObject p1Ship;
        GameObject p2Ship;
        GameObject counter;
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

            counter = GameObjectFactory.createLabel(gameSector, Transform.createTransformWithSizeOnPosition(new Vector2(400, 30), new Vector2(100, 20)), "0");            
            
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
            GameObject g = GameObjectFactory.createButton(gameSector, transform, "Build Drone", sendBuildDroneMessage);


            Transform t1 = Transform.createTransformWithSizeOnPosition(new Vector2(120, 200), new Vector2(50, 30));
            GameObject b1 = GameObjectFactory.createButton(gameSector, t1, "+", 0, addToHangar);

            Transform t2 = Transform.createTransformWithSizeOnPosition(new Vector2(120, 240), new Vector2(50, 30));
            GameObject b2 = GameObjectFactory.createButton(gameSector, t2, "+",  1, addToHangar);

            Transform t3 = Transform.createTransformWithSizeOnPosition(new Vector2(120, 280), new Vector2(50, 30));
            GameObject b3 = GameObjectFactory.createButton(gameSector, t3, "+", 2, addToHangar);

            Transform t4 = Transform.createTransformWithSizeOnPosition(new Vector2(180, 200), new Vector2(50, 30));
            GameObject b4 = GameObjectFactory.createButton(gameSector, t4, "open", 0, openHangar);

            Transform t5 = Transform.createTransformWithSizeOnPosition(new Vector2(180, 240), new Vector2(50, 30));
            GameObject b5 = GameObjectFactory.createButton(gameSector, t5, "open", 1, openHangar);

            Transform t6 = Transform.createTransformWithSizeOnPosition(new Vector2(180, 280), new Vector2(50, 30));
            GameObject b6 = GameObjectFactory.createButton(gameSector, t6, "open", 2, openHangar);
        }

        void addToHangar(Clickable sender)
        {
            p1Ship.getComponent<Hangar>().addDrone(sender.id);
        }

        void openHangar(Clickable sender)
        {
            p1Ship.getComponent<Hangar>().openStock(sender.id);
        }

        public static void sendBuildDroneMessage(Clickable sender)
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

        int count = 0;

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

            counter.getComponent<TextDrawer>().text = count++.ToString();
        }
    }
}
