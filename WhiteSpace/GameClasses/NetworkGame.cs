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
            GameObjectFactory.createTexture(gameSector, Vector2.Zero, new Vector2(1280, 720), ContentLoader.getContent<Texture2D>("Background"));
            GameObjectFactory.createTexture(gameSector, Vector2.Zero, new Vector2(1280, 720), ContentLoader.getContent<Texture2D>("Stars"));

            GameObjectFactory.createTexture(gameSector, new Vector2(0, 720 - 193), new Vector2(1280, 193), ContentLoader.getContent<Texture2D>("Planet"));


            buildMotherShips();
            grid = new TowerGrid(4, 3, 40, new Vector2(400, 575), 5, Client.host, ressources);
            GameObject go = new GameObject(gameSector);
            go.addComponent(grid);

            grid2 = new TowerGrid(4, 3, 40, new Vector2(655, 575), 5, !Client.host, ressources);
            GameObject go2 = new GameObject(gameSector);
            go2.addComponent(grid2);
            grid2.addComponent<Button>();

            StateMachine<gamestate>.getInstance().changeState(gamestate.game);
        }

        void buildMotherShips()
        {
            Transform ship1Transform = Transform.createTransformWithSizeOnPosition(new Vector2(), new Vector2(523, 683));
            Transform ship2Transform = Transform.createTransformWithSizeOnPosition(new Vector2(Game1.graphics.PreferredBackBufferWidth - 523, 0), new Vector2(523, 683));



            p1Ship = GameObjectFactory.createMotherShip(gameSector, ship1Transform, ship2Transform, "Mothership", Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 25, Client.host, ressources);
            GameObjectFactory.createTexture(gameSector, Vector2.Zero, new Vector2(275, 145), ContentLoader.getContent<Texture2D>("Twingun2"));
            GameObjectFactory.createTexture(gameSector, Vector2.Zero, new Vector2(283, 149), ContentLoader.getContent<Texture2D>("Twingun1"));

            p2Ship = GameObjectFactory.createMotherShip(gameSector, ship2Transform, ship1Transform, "Mothership", Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally, 25, !Client.host, ressources);
            GameObjectFactory.createTexture(gameSector, new Vector2(Game1.graphics.PreferredBackBufferWidth - 275, 0), new Vector2(275, 145), ContentLoader.getContent<Texture2D>("Twingun2"), SpriteEffects.FlipHorizontally);
            GameObjectFactory.createTexture(gameSector, new Vector2(Game1.graphics.PreferredBackBufferWidth - 283, 0), new Vector2(283, 149), ContentLoader.getContent<Texture2D>("Twingun1"), SpriteEffects.FlipHorizontally);


            p1Ship.getComponent<Hangar>().targetTransform = p2Ship.getComponent<Hangar>().transform;
            p2Ship.getComponent<Hangar>().targetTransform = p1Ship.getComponent<Hangar>().transform;
        }
    }
}
