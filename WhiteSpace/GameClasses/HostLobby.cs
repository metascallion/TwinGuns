using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;
using WhiteSpace.Network;
using WhiteSpace.Temp;
using WhiteSpace.Components.Drawables;

namespace WhiteSpace.GameClasses
{
    public class HostLobby : Lobby
    {
        string lobbyName;

        public HostLobby(string lobbyName)
        {
            this.lobbyName = lobbyName;
            Client.host = true;
            buildHostMenu();

        }
        private void buildHostMenu()
        {
            Transform closeBtnTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10, 450), new Vector2(100, 30));
            GameObjectFactory.createButton(leaveSector, closeBtnTransform, "Close", sendCloseMessage);

            Transform startBtnTransform = Transform.createTransformWithSizeOnPosition(new Vector2(690, 450), new Vector2(100, 30));
            GameObjectFactory.createButton(leaveSector, startBtnTransform, "Start", sendStartMessage);
        }

        private void sendCloseMessage(Clickable sender)
        {
            SendableNetworkMessage msg = new SendableNetworkMessage("Close");
            msg.addInformation("GameName", this.lobbyName);
            Client.sendMessage(msg);
            Client.unregisterNetworkListenerMethod("NotAccepted", OnNotAccepted);
            Client.host = false;
        }

        GameObject waitText;
        private void sendStartMessage(Clickable sender)
        {
            waitText = GameObjectFactory.createLabel(leaveSector, Transform.createTransformWithSizeOnPosition(new Vector2(300, 250), new Vector2(200, 30)), "Waiting for the other Player");
            waitText.getComponent<TextDrawer>().TextColor = Color.White;
            SendableNetworkMessage msg = new SendableNetworkMessage("Start");
            Client.sendMessage(msg);
            Client.registerNetworkListenerMethod("NotAccepted", OnNotAccepted);
        }

        private void OnNotAccepted(ReceiveableNetworkMessage msg)
        {
            waitText.getComponent<TextDrawer>().text = "Declined start";
            waitText.getComponent<TextDrawer>().TextColor = Color.Red;
            waitText.addComponent(new OverTimeDestroyer(1000f));
        }
    }
}
