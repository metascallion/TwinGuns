#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using BachelorPrototype.GameClasses;
using Network;

#endregion

namespace BachelorPrototype
{
    interface Drawable
    {
        void draw();
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static Texture2D r;
        public static Texture2D l;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D flowerTexture;
        Texture2D background;

        public static Texture2D towerTexture1;
        public static Texture2D towerTexture2;

        public static Client client;
        TextField gameNameField;
        ClickableUI p1HealthDrawer;
        ClickableUI p2HealthDrawer;
        int xPos = 0;
        ClickableUI twinGunShootButton;
        public static Grid grid1;
        public static Grid grid2;
        public static Texture2D projectile;
   
        int p1Health = 100;
        int p2Health = 100;

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
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        public void startTestClient()
        {
            client = new ClientAdapter("TestApplication");
            client.IpToConnectTo = "localhost";
            client.PortToConnectTo = 1024;
            client.registerReceivedDataCallBack();
            client.connect();
        }

        public void hostGame(ClickableUI sender)
        {
            NetworkMessage msg = new NetworkMessage("host," + gameNameField.textDrawer.Text);
            client.sendMessage(msg);
        }

        public void findServers(ClickableUI sender)
        {
            NetworkMessage msg = new NetworkMessage("find,default");
            client.sendMessage(msg);
        }

        public void backToMain(ClickableUI sender)
        {
            StateContainer.getInstance().setState(state.main);
            client.sendMessage(new NetworkMessage("leave," + client.name));
        }

        public void sendShoot(ClickableUI sender)
        {
            client.sendMessage(new NetworkMessage("shoot," + client.name));
        }
        protected override void LoadContent()
        {
            r = Content.Load<Texture2D>("P1");
            l = Content.Load<Texture2D>("P2");
            

            startTestClient();
            StateContainer.getInstance().setState(state.main);

            background = Content.Load<Texture2D>("space");
            towerTexture1 = Content.Load<Texture2D>("TowerP1");
            towerTexture2 = Content.Load<Texture2D>("TowerP2");

            projectile = Content.Load<Texture2D>("kugelding");

            TextureRegion region = new TextureRegion();

            region.Position = new Vector2(0, 0);
            region.Size = new Vector2(800, 480);
            region.Texture = Content.Load<Texture2D>("Planet");
            DrawableCollector.drawables.Add(region);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            flowerTexture = Content.Load<Texture2D>("flowers");
            TextDrawer.font = Content.Load<SpriteFont>("MyFont");
            ColorBoxDrawer.gpsDevice = graphics.GraphicsDevice;
            
            ClickableUI findServersButton = new ClickableUI();
            findServersButton.addText("Find Games");
            findServersButton.Position = new Vector2(0, 0);
            findServersButton.Size = new Vector2(200, 50);
            findServersButton.clickEvent += findServers;
            findServersButton.activeState = state.main;
            UpdateExecuter.Updates += findServersButton.update;
            DrawableCollector.drawables.Add(findServersButton);

            ClickableUI hostButton = new ClickableUI();
            hostButton.addText("Start Game");
            hostButton.Position = new Vector2(210, 0);
            hostButton.Size = new Vector2(200, 50);
            hostButton.clickEvent += hostGame;
            hostButton.activeState = state.main;
            UpdateExecuter.Updates += hostButton.update;
            DrawableCollector.drawables.Add(hostButton);

            p1HealthDrawer = new ClickableUI();
            p1HealthDrawer.Position = new Vector2(0, 450);
            p1HealthDrawer.Size = new Vector2(125, 30);
            p1HealthDrawer.textDrawer.Text = "";
            p1HealthDrawer.activeState = state.lobby;
            DrawableCollector.drawables.Add(p1HealthDrawer);
            UpdateExecuter.Updates += p1HealthDrawer.update;

            p2HealthDrawer = new ClickableUI();
            p2HealthDrawer.Position = new Vector2(675, 450);
            p2HealthDrawer.Size = new Vector2(125, 30);
            p2HealthDrawer.textDrawer.Text = "";
            p2HealthDrawer.activeState = state.lobby;
            DrawableCollector.drawables.Add(p2HealthDrawer);
            UpdateExecuter.Updates += p2HealthDrawer.update;

            gameNameField = new TextField();
            gameNameField.addText("Enter Gamename here");
            gameNameField.Position = new Vector2(420, 0);
            gameNameField.Size = new Vector2(200, 25);
            gameNameField.maximumCharacters = 30;
            gameNameField.activeState = state.main;
            UpdateExecuter.Updates += gameNameField.update;
            DrawableCollector.drawables.Add(gameNameField);

            
            ClickableUI backButton = new ClickableUI();
            backButton.clickEvent += backToMain;
            backButton.activeState = state.lobby;
            backButton.Position = new Vector2(0, 0);
            backButton.Size = new Vector2(200, 30);
            backButton.addText("Back");
            UpdateExecuter.Updates += backButton.update;
            DrawableCollector.drawables.Add(backButton);

            ClickableUI shootButton = new ClickableUI();
            shootButton.clickEvent += sendShoot;
            shootButton.activeState = state.lobby;
            shootButton.Position = new Vector2(300, 450);
            shootButton.Size = new Vector2(200, 30);
            shootButton.addText("SHOOT");
            UpdateExecuter.Updates += shootButton.update;
            DrawableCollector.drawables.Add(shootButton);

            twinGunShootButton = new ClickableUI();
            twinGunShootButton.activeState = state.lobby;
            twinGunShootButton.addText("shootTwinGun");
            twinGunShootButton.clickEvent += shootTwinGun;
            twinGunShootButton.Size = new Vector2(200, 30);
 

            UpdateExecuter.Updates += twinGunShootButton.update;
            DrawableCollector.drawables.Add(twinGunShootButton);

            grid1 = new Grid(new Vector2(0, 250), 50, 3, 3,true);

            grid2 = new Grid(new Vector2(630, 250), 50, 3, 3, false);


            // TODO: use this.Content to load your game content here
        }

        public void shootTwinGun(ClickableUI sender)
        {
            grid1.destoryTower("a22");
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
        protected override void Update(GameTime gameTime)
        {
          
            // TODO: Add your update logic here 
            KeyboardState kbs = Keyboard.GetState();
            UpdateExecuter.updateAll();
            base.Update(gameTime);
            p1HealthDrawer.textDrawer.Text = "Player 1: " + p1Health.ToString();
            p2HealthDrawer.textDrawer.Text = "Player 2: " + p2Health.ToString();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            if(client.name == "2")
            {
                xPos = 600;
            }

            twinGunShootButton.Position = new Vector2(xPos, 30);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            for (int i = 0; i < Client.projectiles.Count; i++ )
            {
                Client.projectiles[i].update();

                if (Client.projectiles[i].Position.X < 0)
                {
                    Client.projectiles.Remove(Client.projectiles[i]);
                    p1Health--;
                }
                else if (Client.projectiles[i].Position.X > 700)
                {
                    Client.projectiles.Remove(Client.projectiles[i]);
                    p2Health--;
                }
            }

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            for (int i = 0; i < Client.projectiles.Count; i++)
            {
                Client.projectiles[i].draw(spriteBatch);
            }
            DrawableCollector.draw(spriteBatch);
           
            for (int i = 0; i < Shot.shots.Count; i++ )
            {
                Shot.shots[i].draw(spriteBatch);
                Shot.shots[i].update();
            }

            for (int i = 0; i < Tower.towers.Count; i++)
            {
                Tower.towers[i].update();
            }

                spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
