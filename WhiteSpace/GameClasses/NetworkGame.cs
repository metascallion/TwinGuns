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
using WhiteSpace.Content;

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
            GameObject back = GameObjectFactory.createTexture(gameSector, new Vector2(0, 0), new Vector2(1280, 720), ContentLoader.getContent<Texture2D>("Background"));
            GameObject g = GameObjectFactory.createTexture(gameSector, new Vector2(-500, -300), new Vector2(2560, 1440), ContentLoader.getContent<Texture2D>("StarsRotating"));
            g.addComponent(new Rotator(0.0005f));
            GameObject gg = GameObjectFactory.createTexture(gameSector, new Vector2(-500, -300), new Vector2(2560, 1440), ContentLoader.getContent<Texture2D>("StarsRotating"), SpriteEffects.FlipVertically);
            gg.addComponent(new Rotator(-0.00025f));
            GameObjectFactory.createTexture(gameSector, new Vector2(952, 452), new Vector2(114, 115), ContentLoader.getContent<Texture2D>("Planett"), SpriteEffects.None, 2);
            GameObjectFactory.createTexture(gameSector, new Vector2(0, 720 - 193), new Vector2(1280, 193), ContentLoader.getContent<Texture2D>("Planet"));
            GameObjectFactory.createTexture(gameSector, new Vector2(470, 0), new Vector2(349, 57), ContentLoader.getContent<Texture2D>("GUIpaddleup"), SpriteEffects.None, 29);
            GameObjectFactory.createTexture(gameSector, new Vector2(0, Game1.graphics.PreferredBackBufferHeight - 51), new Vector2(324, 51), ContentLoader.getContent<Texture2D>("GUIpaddledown"), SpriteEffects.None, 15);
            GameObjectFactory.createTexture(gameSector, new Vector2(Game1.graphics.PreferredBackBufferWidth - 324, Game1.graphics.PreferredBackBufferHeight - 51), new Vector2(324, 51), ContentLoader.getContent<Texture2D>("GUIpaddledown"), SpriteEffects.FlipHorizontally, 15);


            buildMotherShips();

            GameObjectFactory.createTexture(gameSector, Vector2.Zero, new Vector2(155, 326), ContentLoader.getContent<Texture2D>("Mothershippipes"), SpriteEffects.None, 2);
            GameObjectFactory.createTexture(gameSector, new Vector2(Game1.graphics.PreferredBackBufferWidth - 155, 0), new Vector2(155, 326), ContentLoader.getContent<Texture2D>("Mothershippipes"), SpriteEffects.FlipHorizontally, 2);
            
            grid = new TowerGrid(4, 3, 40, new Vector2(400, 590), 5, Client.host, ressources);
            GameObject go = new GameObject(gameSector);
            go.addComponent(grid);

            grid2 = new TowerGrid(4, 3, 40, new Vector2(655, 590), 5, !Client.host, ressources);
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
            GameObjectFactory.createTexture(gameSector, Vector2.Zero, new Vector2(275, 145), ContentLoader.getContent<Texture2D>("Twingun2"), SpriteEffects.None, 3);
            GameObjectFactory.createTexture(gameSector, Vector2.Zero, new Vector2(283, 149), ContentLoader.getContent<Texture2D>("Twingun1"), SpriteEffects.None, 3);

            p2Ship = GameObjectFactory.createMotherShip(gameSector, ship2Transform, ship1Transform, "Mothership", Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally, 25, !Client.host, ressources);
            GameObjectFactory.createTexture(gameSector, new Vector2(Game1.graphics.PreferredBackBufferWidth - 275, 0), new Vector2(275, 145), ContentLoader.getContent<Texture2D>("Twingun2"), SpriteEffects.FlipHorizontally, 3);
            GameObjectFactory.createTexture(gameSector, new Vector2(Game1.graphics.PreferredBackBufferWidth - 283, 0), new Vector2(283, 149), ContentLoader.getContent<Texture2D>("Twingun1"), SpriteEffects.FlipHorizontally, 3);


            p1Ship.addComponent(new Hangar(Client.host, ressources, p2Ship));
            p2Ship.addComponent(new Hangar(!Client.host, ressources, p1Ship));

            p1Ship.getComponent<Hangar>().targetTransform = p2Ship.getComponent<Hangar>().transform;
            p2Ship.getComponent<Hangar>().targetTransform = p1Ship.getComponent<Hangar>().transform;

        }
    }
}
