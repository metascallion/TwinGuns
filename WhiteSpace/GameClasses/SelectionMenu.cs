using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.GameLoop;
using WhiteSpace.Components;
using WhiteSpace.Temp;
using Microsoft.Xna.Framework;

namespace WhiteSpace.GameClasses
{
    public class SelectionMenu
    {
        ComponentsSector<lobbystate> sector;

        public SelectionMenu()
        {
            this.sector = new ComponentsSector<lobbystate>(lobbystate.selection);
            start();
        }

        public void start()
        {
            buildUI();
            StateMachine<lobbystate>.getInstance().changeState(lobbystate.selection);
        }

        private void buildUI()
        {
            buildNameTextField();
            buildOkButton();
        }

        private void buildNameTextField()
        {
            Transform textTransform = Transform.createTransformWithSizeOnPosition(new Vector2(10,10), new Vector2(100,20));
            Transform transform = Transform.createTransformWithSizeOnPosition(new Vector2(100, 10), new Vector2(200, 30));
            GameObjectFactory.createTextField(this.sector, transform);
            GameObjectFactory.createLabel(this.sector, textTransform, "Name:");
        }

        private void buildOkButton()
        {
            Transform transform = Transform.createTransformWithSizeOnPosition(new Vector2(310, 10), new Vector2(40, 30));
            GameObjectFactory.createButton(this.sector, transform, "Ok", nextState);
        }

        private void nextState(Button sender)
        {
            new StartMenu();
        }

    }
}
