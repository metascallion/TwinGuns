using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using WhiteSpace.Network;
using WhiteSpace.Temp;

namespace WhiteSpace.GameClasses
{
    public class StartMenu
    {
        ComponentsSector<lobbystate> sector;

        GameObject textField;

        public StartMenu()
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

        private void buildHostButton()
        {
            Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(340, 10), new Vector2(150, 30));
            GameObjectFactory.createButton(this.sector, buttonTransform, "Host", sendHostRequest);
        }

        private void sendHostRequest(Button sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Host");
            msg.addInformation("GameName", this.textField.getComponent<EditableText>().textDrawer.text);
            Client.sendMessage(msg);
            Client.registerNetworkListenerMethod("Host", OnHostMessageEnter);
        }

        private void OnHostMessageEnter(ReceiveableNetworkMessage msg)
        {
            //StateMachine<lobbystate>.getInstance().changeState(lobbystate.lobby);
        }

        private void buildFindGamesButton()
        {
            Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(340, 50), new Vector2(150, 30));
            GameObjectFactory.createButton(this.sector, buttonTransform, "Find Games", sendFindGamesRequest);
        }

        ComponentsSector<lobbystate> activeGames = new ComponentsSector<lobbystate>(lobbystate.start);
        private void sendFindGamesRequest(Button sender)
        {
            activeGames.destroy();
            gameId = 0;
            Client.sendMessage(new SendableNetworkMessage("FindGames"));
            Client.registerNetworkListenerMethod("FoundGames", OnFoundGamesMessageEnter);
        }

        int gameId = 0;
        private void OnFoundGamesMessageEnter(ReceiveableNetworkMessage msg)
        {
            Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 100 + 30 * gameId), new Vector2(150, 30));
            GameObjectFactory.createButton(activeGames, buttonTransform, msg.getInformation("GameName"));
            gameId++;
        }

    }
}
