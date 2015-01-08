using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameClasses
{
    public class StartMenu
    {
        ComponentsSector<lobbystate> sector;

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
            GameObjectFactory.createTextField(this.sector, transform);
            GameObjectFactory.createLabel(this.sector, nameTransform, "Game Name");
        }

        private void buildHostButton()
        {
            Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(340, 10), new Vector2(150, 30));
            GameObjectFactory.createButton(this.sector, buttonTransform, "Host", sendHostRequest);
        }

        private void sendHostRequest(Button sender)
        {
            throw new NotImplementedException();
        }

        private void buildFindGamesButton()
        {
            Transform buttonTransform = Transform.createTransformWithSizeOnPosition(new Vector2(340, 50), new Vector2(150, 30));
            GameObjectFactory.createButton(this.sector, buttonTransform, "Find Games", sendFindGamesRequest);
        }

        private void sendFindGamesRequest(Button sender)
        {
            throw new NotImplementedException();
        }

    }
}
