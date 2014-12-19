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
using WhiteSpace.Drawables;
using WhiteSpace.Components.Animation;
using WhiteSpace.Network;
using WhiteSpace.Components;
using WhiteSpace.Components.Physics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Emit;


#endregion

namespace WhiteSpace
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
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
            //new GameObject<gamestate>(gamestate.gameover);
            //StateMachine<gamestate>.getInstance().changeState(gamestate.lobby);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// 
        /// 
        /// 
        //EditableText<lobbystate> editor;
        Clickable<gamestate> clickable;

        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.ContentManager = Content;
            StateMachine<gamestate>.getInstance().changeState(gamestate.main);
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.start);

            Updater<gamestate> updater = new Updater<gamestate>(gamestate.main);

           clickable = new Clickable<gamestate>(Transform.createTransformWithSizeOnPosition(new Vector2(0,0), new Vector2(100,100)), updater);

            Button<gamestate> button = new Button<gamestate>(Transform.createTransformWithSizeOnPosition(new Vector2(100,100), new Vector2(100,100)), updater);
            button.addText("hello");

            //editor = new EditableText<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(0,0), new Vector2(500, 25)), lobbystate.start);
            //Button<lobbystate> hostButton = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(550, 0), new Vector2(100, 50)), lobbystate.start);
            //hostButton.activeState = lobbystate.start;
            //hostButton.addText("Host");
            //Button<lobbystate> findGamesButton = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(550, 55), new Vector2(100, 50)), lobbystate.start);
            //findGamesButton.releaseMethods += sendFindGamesRequest;
            //findGamesButton.activeState = lobbystate.start;
            //findGamesButton.addText("Find Games");
            //hostButton.releaseMethods += sendHostMessage;

            //Client.startClient("Test");
            //Client.connect("localhost", 1111);
            //Client.registerNetworkListenerMethod("FoundGame", onFoundGameMessageEnter);
            //Client.registerNetworkListenerMethod("Join", OnJoinMessageEnter);

            //KeyboardInput.start();
            // TODO: use this.Content to load your game content here
        }

        //public void sendHostMessage(Button<lobbystate> button)
        //{
        //    SendableNetworkMessage msg = new SendableNetworkMessage("Host");
        //    msg.addInformation("Name", editor.textD.text);
        //    Client.sendMessage(msg);
        //}

        //public void sendFindGamesRequest(Button<lobbystate> button)
        //{
        //    SendableNetworkMessage msg = new SendableNetworkMessage("FindGames");
        //    Client.sendMessage(msg);
        //}


        //int offset = 0;

        //public void onFoundGameMessageEnter(ReceiveableNetworkMessage msg)
        //{
        //    Button<lobbystate> button = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(100, 55 * offset + 30), new Vector2(100, 50)), lobbystate.start);
        //    button.activeState = lobbystate.start;
        //    button.addText(msg.getInformation("GameName"));
        //    button.releaseMethods += joinLobby;
        //    button.destroyOnInvalidState();
        //    offset++;
        //}

        //public void joinLobby(Button<lobbystate> sender)
        //{
        //    SendableNetworkMessage msg = new SendableNetworkMessage("Join");
        //    msg.addInformation("GameName", sender.textD.text);
        //    Client.sendMessage(msg);
        //}

        //public void OnJoinMessageEnter(ReceiveableNetworkMessage msg)
        //{
        //    StateMachine<lobbystate>.getInstance().changeState(lobbystate.lobby);
        //    Button<lobbystate> button = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(0, 450), new Vector2(100, 30)), lobbystate.lobby);
        //    button.activeState = lobbystate.lobby;
        //    button.releaseMethods += backToStart;
        //    button.addText("Back");
        //}

        //public void backToStart(Button<lobbystate> sender)
        //{
        //    StateMachine<lobbystate>.getInstance().changeState(lobbystate.start);
        //}

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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //t.Rotation += MathHelper.ToRadians(15.0f);
            //tt.Rotation += MathHelper.ToRadians(-10.0f);

            if(KeyboardInput.wasKeyJustPressed(Keys.B))
            {
                //t.Rotation += MathHelper.ToRadians(10);

                //SendableNetworkMessage msg = new SendableNetworkMessage("Transform");
                //msg.addInformation("rotation", MathHelper.ToDegrees(t.Rotation));
                //msg.addInformation("x", t.Position.X);
                //msg.addInformation("y", t.Position.Y);

                //Client.sendMessage(msg);
                clickable.unregisterInUpdater();

            }

            if (KeyboardInput.isKeyDown(Keys.A))
            {
                clickable.registerInUpdater();
            }

   //         Client.pollNetworkMessage();
            UpdateExecuter.executeUpdates(gameTime);

            

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
