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

        public NetworkGame()
        {
            buildMotherShips();
            buildDroneButton();

            Grid grid = new Grid(5, 5, 45, new Vector2(200, 200), 10);
            GameObject go = new GameObject(gameSector);
            go.addComponent(grid);
            grid.addComponent<Button>();
            StateMachine<gamestate>.getInstance().changeState(gamestate.game);
            Client.registerNetworkListenerMethod("BuildDrone", OnBuildDroneMessageEnter);
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
            Transform transform = Transform.createTransformWithSize(new Vector2(25, 25));
            Transform target = new Transform();
            SpriteEffects effect;

            if (Boolean.Parse(msg.getInformation("Player")) == Client.host)
            {
                transform.Position = p1Ship.getComponent<Transform>().Position + new Vector2(0, p1Ship.getComponent<Transform>().Size.Y * 0.5f);
                effect = SpriteEffects.None;
                target = p2Ship.getComponent<Transform>();
            }

            else
            {
                transform.Position = p2Ship.getComponent<Transform>().Position + new Vector2(p2Ship.getComponent<Transform>().Size.X, p2Ship.getComponent<Transform>().Size.Y * 0.5f);
                effect = SpriteEffects.None;
                target = p1Ship.getComponent<Transform>();
            }

            GameObjectFactory.createDrone(gameSector, transform, "Ship", effect, 5, target);
        }
    }
}
