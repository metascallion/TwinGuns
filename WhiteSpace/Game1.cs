#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using WhiteSpace.GameObjects;
using WhiteSpace.GameLoop;
using WhiteSpace.Temp;
using WhiteSpace.Drawables;
using WhiteSpace.Components.Animation;
using WhiteSpace.Network;

#endregion

namespace WhiteSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
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
            //new GameObject<gamestate>(gamestate.gameover);
            //StateMachine<gamestate>.getInstance().changeState(gamestate.lobby);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        protected override void LoadContent()
        {

            //Client.registerNetworkListenerMethod("test", test1);
            //Client.registerNetworkListenerMethod("test", test2);
            //Client.onNetworkMessageEnter(new NetworkMessage("test"));

            Client.startClient("test");
            Client.connect("localhost", 1111);
            Client.registerNetworkListenerMethod("hello", test1);
            Client.registerNetworkListenerMethod("go", test2);
            //string testMessage = "test,x=0,y=10,z=300";

            //ReceiveableNetworkMessage msg = ReceiveableNetworkMessage.createMessageFromString(testMessage);

            //int x = int.Parse(msg.getInformation("y"));

            //Client.onNetworkMessageEnter(msg);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.ContentManager = Content;
            //Unit<gamestate> unit = new Unit<gamestate>(gamestate.game, Temp.Transform.createTransformWithSizeOnPosition(new Vector2(200,200), new Vector2(100, 100)), Content.Load<Texture2D>("Knight"));
            //new Unit<gamestate>(Temp.Transform.createTransformWithSize(new Vector2(50, 50)), Content.Load<Texture2D>("Knight"));
            //new Clickable<gamestate>(Transform.createTransformWithSize(new Vector2(100, 100)));

            /*
            sheet = new SpriteSheet(ContentLoader.getContent<Texture2D>("smurf"), 4, 4);
            ContentLoader.getContent<Texture2D>("smurf");
                
            ContentLoader.getContent<Texture2D>("smurf");
                    
            ContentLoader.getContent<Texture2D>("smurf");
                   
            ContentLoader.getContent<Texture2D>("smurf");
             * */


            //TextureRegion<gamestate> region = new TextureRegion<gamestate>(Transform.createTransformWithSize(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight)), Content.Load<Texture2D>("mushroom"));
            //AnimatorLoader.loadAnimator<gamestate>(gamestate.main, region, "TestAnimator");
            //Unit<gamestate> u = new Unit<gamestate>(Temp.Transform.createTransformWithSize(new Vector2(150, 150)), sheet);

            //TextureRegion<gamestate> region = new TextureRegion<gamestate>(Transform.createTransformWithSize(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight)), Content.Load<Texture2D>("mushroom"));
            //TextureRegion<gamestate> region2 = new TextureRegion<gamestate>(Transform.createTransformWithSizeOnPosition(new Vector2(25,25), new Vector2(350,300)), Content.Load<Texture2D>("smurf"));
            TestRotationGameObject<gamestate> test = new TestRotationGameObject<gamestate>(gamestate.main, Temp.Transform.createTransformWithSize(new Vector2(200, 200)));
            //test.Position = new Vector2(100, 100);

            StateMachine<gamestate>.getInstance().changeState(gamestate.main);
            // TODO: use this.Content to load your game content here
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

        void test1(NetworkMessage msg)
        {

        }

        void test2(NetworkMessage msg)
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //new TestRotationGameObject<gamestate>(Temp.Transform.createTransformWithSize(new Vector2(50, 50)), sheet.Texture, sheet);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

            }

            //else
            //{
            //    StateMachine<gamestate>.getInstance().changeState(gamestate.game);
            //}
            // TODO: Add your update logic here
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawExecuter.executeRegisteredDrawMethods(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
