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
    class PlayerLobby : Lobby
    {
        public PlayerLobby()
        {
            buildLeaveButton();
            Client.registerNetworkListenerMethod("AcceptStart", OnAcceptMessageEnter);
        }

        private void buildLeaveButton()
        {
            Transform leaveBtnTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 450), new Vector2(100, 30));
            GameObjectFactory.createButton(leaveSector, leaveBtnTransform, "Leave", sendLeaveRequest);
        }

        private void sendLeaveRequest(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Leave");
            msg.addInformation("PlayerName", Client.name);
            Client.sendMessage(msg);
        }

        protected override void OnLeaveMessageEnter(ReceiveableNetworkMessage msg)
        {
            base.OnLeaveMessageEnter(msg);
            Client.unregisterNetworkListenerMethod("AcceptStart", this.OnAcceptMessageEnter);
        }

        ComponentsSector<lobbystate> acceptSector = new ComponentsSector<lobbystate>(lobbystate.lobby);
        private void OnAcceptMessageEnter(ReceiveableNetworkMessage msg)
        {
            Transform textTransform = Transform.createTransformWithSizeOnPosition(new Vector2(350, 230), new Vector2(250, 30));
            GameObjectFactory.createLabel(acceptSector, textTransform, "Ready?");
            Transform yesBtnTransform = Transform.createTransformWithSizeOnPosition(new Vector2(340, 260), new Vector2(50, 30));
            GameObjectFactory.createButton(acceptSector, yesBtnTransform, "Yes", sendAcceptMessage);
            Transform noBtnTransform = Transform.createTransformWithSizeOnPosition(new Vector2(400, 260), new Vector2(50, 30));
            GameObjectFactory.createButton(acceptSector, noBtnTransform, "No", sendAcceptMessage);
        }

        private void sendAcceptMessage(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("StartAccepted");
            msg.addInformation("Answer", sender.parent.getComponent<Button>().textD.text);
            Client.sendMessage(msg);
            acceptSector.destroy();
        }
    }
}
