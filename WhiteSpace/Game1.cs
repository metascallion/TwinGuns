#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using WhiteSpace.GameLoop;
using WhiteSpace.Temp;
using WhiteSpace.Components.Drawables;
using WhiteSpace.Components.Animation;
using WhiteSpace.Network;
using WhiteSpace.Components;
using WhiteSpace.Components.Physics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Emit;
using WhiteSpace.GameClasses;


#endregion

namespace WhiteSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            IsMouseVisible = true;
            KeyboardInput.updateKeyStates();
            KeyboardInput.start();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// 
        /// 
        /// 


        Transform transe = new Transform();
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.ContentManager = this.Content;
            StateMachine<gamestate>.getInstance().changeState(gamestate.main);

            Client.startClient("Test");
            Client.connect("localhost", 1111);  

            ComponentsSector<gamestate> collisionSector = new ComponentsSector<gamestate>(gamestate.main);


            transe.Position = new Vector2(800, 0);
            transe.Size = new Vector2(100, 200);
            GameObject collider = new GameObject(collisionSector);
            collider.addComponent(transe);
            collider.addComponent(new BoxCollider());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        ///

        ComponentsSector<lobbystate> best = new ComponentsSector<lobbystate>(lobbystate.selection);

        public static List<Transform> ships = new List<Transform>();

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            StateMachine<lobbystate>.getInstance().changeState(lobbystate.selection);

            if (KeyboardInput.wasKeyJustPressed(Keys.Space))
            {
                Transform projTrans = Transform.createTransformWithSizeOnPosition(new Vector2(0, 100), new Vector2(50, 50));
                GameObject temp = GameObjectFactory.createBasicShip(projTrans, transe, best, Color.Blue);
                temp.getComponent<BoxCollider>().trigger = true;
                ships.Add(temp.getComponent<Transform>());
            }

                
            if (ships.Count > 0)
            {
                Transform projTrans = Transform.createTransformWithSizeOnPosition(new Vector2(0, 400), new Vector2(10, 10));
                GameObject temp = GameObjectFactory.createProjectileWithCustomSpeed(projTrans, ships[0], best, Color.Red, 25);
            }

            UpdateExecuter.executeUpdates(gameTime);
            Client.pollNetworkMessage();

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawExecuter.executeRegisteredDrawMethods(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
