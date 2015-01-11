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

        string lobbyName = "";

        public StartMenu()
        {
            this.sector = new ComponentsSector<lobbystate>(lobbystate.start);
            buildUI();
            StateMachine<lobbystate>.getInstance().loadNextState();
            Client.registerNetworkListenerMethod("Close", OnCloseMessageEnter);
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
            msg.addInformation("PlayerName", Client.name);
            Client.sendMessage(msg);
            Client.registerNetworkListenerMethod("Host", OnHostMessageEnter);
            Client.registerNetworkListenerMethod("FoundPlayers", OnFoundPlayersMessageEnter);
        }

        private void OnHostMessageEnter(ReceiveableNetworkMessage msg)
        {
            activeGames.destroy();
            this.lobbyName = msg.getInformation("GameName");
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.lobby);
            buildCloseButton();
        }

        private void buildCloseButton()
        {
            Transform closeBtnTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 450), new Vector2(100, 30));
            GameObjectFactory.createButton(leaveSector, closeBtnTransform, "Close", sendCloseMessage);
        }

        private void sendCloseMessage(Button sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Close");
            msg.addInformation("GameName", this.lobbyName);
            Client.sendMessage(msg);
        }

        private void OnCloseMessageEnter(ReceiveableNetworkMessage msg)
        {
            if (msg.getInformation("GameName") == this.lobbyName)
            {
                StateMachine<lobbystate>.getInstance().changeState(lobbystate.start);
                leaveSector.destroy();
            }
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
            Client.sendMessage(new SendableNetworkMessage("FindGames"));
            Client.registerNetworkListenerMethod("FoundGames", OnFoundGamesMessageEnter);
            Client.registerNetworkListenerMethod("FoundPlayers", OnFoundPlayersMessageEnter);
        }

        private void OnFoundGamesMessageEnter(ReceiveableNetworkMessage msg)
        {
            for (int i = 0; i < msg.getInformationContent("GameName").Count(); i++)
            {
                Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 100 + 35 * i), new Vector2(150, 30));
                GameObjectFactory.createButton(activeGames, buttonTransform, msg.getInformationContent("GameName")[i], sendJoinRequest);
            }
        }

        ComponentsSector<lobbystate> lobbySector = new ComponentsSector<lobbystate>(lobbystate.lobby);
        private void OnFoundPlayersMessageEnter(ReceiveableNetworkMessage msg)
        {
            if (this.lobbyName == msg.getInformation("GameName"))
            {
                lobbySector.destroy();
                for (int i = 0; i < msg.getInformationContent("Name").Count(); i++)
                {
                    Transform textTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 100 + 35 * i), new Vector2(150, 30));
                    GameObjectFactory.createLabel(lobbySector, textTransform, msg.getInformationContent("Name")[i]);
                }
            }
        }

        private void sendJoinRequest(Button sender)
        {
            Client.registerNetworkListenerMethod("Join", OnJoinMessageEnter);
            SendableNetworkMessage msg = new SendableNetworkMessage("Join");
            msg.addInformation("GameName", sender.textD.text);
            msg.addInformation("PlayerName", Client.name);
            Client.sendMessage(msg);
        }

        private void OnJoinMessageEnter(ReceiveableNetworkMessage msg)
        {
            activeGames.destroy();
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.lobby);
            this.lobbyName = msg.getInformation("GameName");
            buildLeaveButton();
        }

        ComponentsSector<lobbystate> leaveSector = new ComponentsSector<lobbystate>(lobbystate.lobby);
        private void buildLeaveButton()
        {
            Transform leaveBtnTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 450), new Vector2(100, 30));
            GameObjectFactory.createButton(leaveSector, leaveBtnTransform, "Leave", sendLeaveRequest);
        }

        private void sendLeaveRequest(Button sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Leave");
            msg.addInformation("PlayerName", Client.name);
            Client.sendMessage(msg);
            Client.registerNetworkListenerMethod("Leave", OnLeaveMessageEnter);
        }

        private void OnLeaveMessageEnter(ReceiveableNetworkMessage msg)
        {
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.start);
            leaveSector.destroy();
        }

    }
}
