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
        TowerGrid grid;
        TowerGrid grid2;
        GameRessources ressources = new GameRessources();

        public NetworkGame()
        {
            buildMotherShips();
            grid = new TowerGrid(4, 3, 30, new Vector2(230, 350), 5, Client.host, ressources);
            GameObject go = new GameObject(gameSector);
            go.addComponent(grid);

            grid2 = new TowerGrid(4, 3, 30, new Vector2(420, 350), 5, !Client.host, ressources);
            GameObject go2 = new GameObject(gameSector);
            go2.addComponent(grid2);
            grid2.addComponent<Button>();

            StateMachine<gamestate>.getInstance().changeState(gamestate.game);
        }

        void buildMotherShips()
        {
            Transform ship1Transform = Transform.createTransformWithSizeOnPosition(new Vector2(), new Vector2(150,350));
            Transform ship2Transform = Transform.createTransformWithSizeOnPosition(new Vector2(650, 0), new Vector2(150, 350));
            p1Ship = GameObjectFactory.createMotherShip(gameSector, ship1Transform, ship2Transform, "Ship", Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 25, Client.host, ressources);
            p2Ship = GameObjectFactory.createMotherShip(gameSector, ship2Transform, ship1Transform, "Ship", Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally, 25, !Client.host, ressources);
        }
    }
}
