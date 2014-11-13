using System;
using System.Threading;
using BachelorPrototype.GameClasses;
using System.Collections.Generic;
using BachelorPrototype;

namespace Network
{

	public class Client
	{
		public string IpToConnectTo{protected get; set;}
		public int PortToConnectTo{ protected get; set;}
        int lobbyCounter = 0;
        List<TextDrawer> playerDrawers = new List<TextDrawer>();
        public static List<Projectile> projectiles = new List<Projectile>();

        public string name;

		public Client ()
		{
		}

        public virtual void sendMessage(NetworkMessage msg)
        {
        }

		private void setSynchroContext()
		{
			SynchronizationContext.SetSynchronizationContext (new SynchronizationContext ());
		}

		public virtual void registerReceivedDataCallBack()
		{
			this.setSynchroContext ();
		}

		public virtual void connect()
		{
		}

        private void joinLobby(ClickableUI sender)
        {
            sendMessage(new NetworkMessage("join," + sender.textDrawer.Text));
            StateContainer.getInstance().setState(state.lobby);
        }

        private void joinLobby(string lobbyName)
        {
            sendMessage(new NetworkMessage("join," + lobbyName));
            StateContainer.getInstance().setState(state.lobby);
        }

        public void handleMessage(string msg)
        {
            NetworkMessage nwMsg = new NetworkMessage(msg);
            int size = 30;
            switch (nwMsg.type)
            {
                case "enemygridinput":
                    if (nwMsg.data.Contains("a"))
                    {
                        Game1.grid1.destoryTower(nwMsg.data);
                    }

                    else
                    {
                        Game1.grid2.destoryTower(nwMsg.data);
                    }
                    break;
                case "gridinput":
                    
                    if(nwMsg.data.Contains("a"))
                    {
                        Game1.grid1.buildTower(nwMsg.data);
                    }

                    else
                    {
                        Game1.grid2.buildTower(nwMsg.data);
                    }
                    break;
                case "join":
                    joinLobby(nwMsg.data);
                    break;
                case "lobby":
                    ClickableUI drawer = new ClickableUI();
                    drawer.addText(nwMsg.data);
                    drawer.Position = new Microsoft.Xna.Framework.Vector2(300, 200 + lobbyCounter * size);
                    drawer.activeState = state.main;
                    lobbyCounter++;
                    drawer.Size = new Microsoft.Xna.Framework.Vector2(200, 25);
                    drawer.clickEvent += joinLobby;
                    DrawableCollector.drawables.Add(drawer);
                    UpdateExecuter.Updates += drawer.update;
                    break;
                case "name":
                    this.name = nwMsg.data;
                    break;
                case "player":
                    bool contains = false;
                    for (int i = 0; i < playerDrawers.Count; i++)
                    {
                        if (playerDrawers[i].Text == nwMsg.data)
                        {
                            contains = true;
                            playerDrawers[i].activeState = state.lobby;
                        }
                    }
                    if (!contains)
                    {
                        TextDrawer tdrawer = new TextDrawer();
                        tdrawer.Text = nwMsg.data;
                        tdrawer.Position = new Microsoft.Xna.Framework.Vector2(300, 200 + lobbyCounter * size);
                        tdrawer.activeState = state.lobby;
                        lobbyCounter++;
                        tdrawer.Size = new Microsoft.Xna.Framework.Vector2(200, 25);
                        DrawableCollector.drawables.Add(tdrawer);
                        playerDrawers.Add(tdrawer);
                    }

                    break;

                case "left":
                    for (int i = 0; i < playerDrawers.Count; i++)
                    {
                        if (playerDrawers[i].Text == nwMsg.data)
                        {
                            playerDrawers[i].activeState = state.none;
                        }
                    }
                    break;
                case "spawn":
                    if (nwMsg.data == "1")
                    {
                        projectiles.Add(new Projectile(true));
                    }
                    else
                    {
                        projectiles.Add(new Projectile(false));
                    }
                    break;

            }
        }

	}
}

