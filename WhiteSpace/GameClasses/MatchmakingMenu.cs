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
    public class MatchmakingMenu
    {
        ComponentsSector<lobbystate> sector;

        GameObject textField;

        string lobbyName = "";

        public MatchmakingMenu()
        {
            this.sector = new ComponentsSector<lobbystate>(lobbystate.start);
            buildUI();
            StateMachine<lobbystate>.getInstance().loadNextState();
        }

        public void buildUI()
        {
            buildGameNameTextField();
            buildHostButton();
            buildFindGamesButton();
        }

        private void buildGameNameTextField()
        {
            Transform nameTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 10), new Vector2(100, 20));
            Transform transform = Transform.createTransformWithSizeOnPosition(new Vector2(130, 10), new Vector2(200, 30));
            textField = GameObjectFactory.createTextField(this.sector, transform);
            GameObjectFactory.createLabel(this.sector, nameTransform, "Game Name");
        }

        ComponentsSector<lobbystate> leaveSector = new ComponentsSector<lobbystate>(lobbystate.lobby);
        private void buildHostButton()
        {
            Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(340, 10), new Vector2(150, 30));
            GameObjectFactory.createButton(this.sector, buttonTransform, "Host", sendHostRequest);
        }

        private void sendHostRequest(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Host");
            msg.addInformation("GameName", this.textField.getComponent<EditableText>().textDrawer.text);
            msg.addInformation("PlayerName", Client.name);
            Client.sendMessage(msg);
            Client.registerNetworkListenerMethod("Host", OnHostMessageEnter);
        }

        private void OnHostMessageEnter(ReceiveableNetworkMessage msg)
        {
            activeGames.destroy();
            this.lobbyName = msg.getInformation("GameName");
            Client.shutdown();
            Client.startClient("test");
            Client.connect("localhost", int.Parse(msg.getInformation("Port")));
            new HostLobby(this.textField.getComponent<EditableText>().textDrawer.text);
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.lobby);
        }

        private void buildFindGamesButton()
        {
            Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(340, 50), new Vector2(150, 30));
            GameObjectFactory.createButton(this.sector, buttonTransform, "Find Games", sendFindGamesRequest);
        }

        ComponentsSector<lobbystate> activeGames = new ComponentsSector<lobbystate>(lobbystate.start);
        private void sendFindGamesRequest(Clickable sender)
        {
            activeGames.destroy();
            Client.sendMessage(new SendableNetworkMessage("FindGames"));
            Client.registerNetworkListenerMethod("FoundGames", OnFoundGamesMessageEnter);
        }

        private void OnFoundGamesMessageEnter(ReceiveableNetworkMessage msg)
        {
            for (int i = 0; i < msg.getInformationContent("GameName").Count(); i++)
            {
                Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 100 + 35 * i), new Vector2(150, 30));
                GameObjectFactory.createButton(activeGames, buttonTransform, msg.getInformationContent("GameName")[i], sendJoinRequest);
            }
        }

        private void sendJoinRequest(Clickable sender)
        {
            Client.registerNetworkListenerMethod("Join", OnJoinMessageEnter);
            SendableNetworkMessage msg = new SendableNetworkMessage("Join");
            msg.addInformation("GameName", sender.parent.getComponent<Button>().textD.text);
            msg.addInformation("PlayerName", Client.name);
            Client.sendMessage(msg);
        }

        private void OnJoinMessageEnter(ReceiveableNetworkMessage msg)
        {
            activeGames.destroy();
            Client.shutdown();
            Client.startClient("test");
            Client.connect("localhost", int.Parse(msg.getInformation("GamePort")));
            new PlayerLobby();
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.lobby);
        }
    }
}
