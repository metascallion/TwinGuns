using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using WhiteSpace.Network;
using WhiteSpace.Composite;
using WhiteSpace.Components.Drawables;

namespace WhiteSpace.GameClasses
{
    public class Lobby
    {
        public Lobby()
        {
            Client.registerNetworkListenerMethod("Joined", OnJoined);
            Client.registerNetworkListenerMethod("StartGame", OnStartGameMessageEnter);
            Client.registerNetworkListenerMethod("FoundPlayers", OnFoundPlayersMessageEnter);
            Client.registerNetworkListenerMethod("Leave", OnLeaveMessageEnter);
        }

        private void OnJoined(ReceiveableNetworkMessage msg)
        {
            sendFindPlayers();
        }

        private void sendFindPlayers()
        {
            Client.sendMessage(new SendableNetworkMessage("FindPlayers"));
        }

        protected ComponentsSector<lobbystate> leaveSector = new ComponentsSector<lobbystate>(lobbystate.lobby);
        protected virtual void OnLeaveMessageEnter(ReceiveableNetworkMessage msg)
        {
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.start);
            leaveSector.destroy();
            Client.shutdown();
            Client.startClient("test");
            Client.connect("localhost", int.Parse(msg.getInformation("MasterPort")));
        }

        private void OnStartGameMessageEnter(ReceiveableNetworkMessage msg)
        {
            Client.shutdown();
            Client.startClient("test");
            Client.connect("localhost", int.Parse(msg.getInformation("GamePort")));
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.hud);
            new NetworkGame();
        }

        ComponentsSector<lobbystate> lobbySector = new ComponentsSector<lobbystate>(lobbystate.lobby);
        private void OnFoundPlayersMessageEnter(ReceiveableNetworkMessage msg)
        {
            lobbySector.destroy();
            for (int i = 0; i < msg.getInformationContent("Name").Count(); i++)
            {
                Transform textTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 100 + 35 * i), new Vector2(150, 30));
                GameObjectFactory.createButton(lobbySector, textTransform, msg.getInformationContent("Name")[i]);
            }
        }
    }
}
