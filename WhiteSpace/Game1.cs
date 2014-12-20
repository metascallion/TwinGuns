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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// 
        /// 
        /// 
        EditableText<lobbystate> editor;
        EditableText<lobbystate> nameEditor;
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.ContentManager = this.Content;

            ComponentsSector<lobbystate> sector = new ComponentsSector<lobbystate>(lobbystate.start);
            editor = new EditableText<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(0, 0), new Vector2(400, 25)), sector);
            Button<lobbystate> button = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(405, 0), new Vector2(100, 50)), sector);
            button.addText("Host");
            button.releaseMethods += sendHostRequest;
            Button<lobbystate> button2 = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(405, 55), new Vector2(100, 50)), sector);
            button2.addText("Find Games");
            button2.releaseMethods += sendFindGamesRequest;



            ComponentsSector<lobbystate> selectionSector = new ComponentsSector<lobbystate>(lobbystate.selection);
            nameEditor = new EditableText<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(0, 0), new Vector2(400, 25)), selectionSector);
            Button<lobbystate> buttonName = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(405, 0), new Vector2(100, 50)), selectionSector);
            buttonName.addText("Choose Name");
            buttonName.releaseMethods += chooseName;

            StateMachine<lobbystate>.getInstance().changeState(lobbystate.selection);

            StateMachine<gamestate>.getInstance().changeState(gamestate.main);

            Client.startClient("Test");
            Client.connect("localhost", 1111);  
            Client.registerNetworkListenerMethod("Host", OnHostAccepted);
            Client.registerNetworkListenerMethod("FoundGame", OnFindGame);
        }

        void chooseName(Button<lobbystate> sender)
        {
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.start);
        }

        void sendFindGamesRequest(Button<lobbystate> sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("FindGames");
            Client.sendMessage(msg);
            findGamesButtons = new ComponentsSector<lobbystate>(lobbystate.start);
            offset = 0;
        }

        int offset = 0;
        ComponentsSector<lobbystate> findGamesButtons = new ComponentsSector<lobbystate>(lobbystate.start);
        void OnFindGame(ReceiveableNetworkMessage msg)
        {
            findGamesButtons.destroyOnInvalidState();
            Button<lobbystate> btn = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(100, 32 * offset + 50), new Vector2(150, 30)), findGamesButtons);
            offset++;
            btn.addText(msg.getInformation("GameName"));
        }

        void sendHostRequest(Button<lobbystate> sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Host");
            msg.addInformation("Name", editor.textDrawer.text);
            Client.sendMessage(msg);
        }

        void OnHostAccepted(ReceiveableNetworkMessage msg)
        {
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.host);
            Button<lobbystate> btn = new Button<lobbystate>(Transform.createTransformWithSizeOnPosition(new Vector2(0, 450), new Vector2(100, 30)), new ComponentsSector<lobbystate>(lobbystate.host));
            btn.addText("Back");
            btn.releaseMethods += back;
        }

        void back(Button<lobbystate> sender)
        {
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.start);
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
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
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
