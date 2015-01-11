using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.Components;
using WhiteSpace.Components.Drawables;
using WhiteSpace.GameLoop;

namespace WhiteSpace.GameClasses
{
    public class GameUI
    {
        ComponentsSector<gamestate> sector;

        public GameUI()
        {
            sector = new ComponentsSector<gamestate>(gamestate.game);
            start();
        }

        private void start()
        {
            buildUI();
        }

        public void activate()
        {
            StateMachine<gamestate>.getInstance().changeState(gamestate.game);
        }

        private void buildUI()
        {
            buildMotherShipsMock();
        }

        private void buildMotherShipsMock()
        {
            Vector2 shipSize = new Vector2(670, 300);
            Transform shipOneTransform = Transform.createTransformWithSizeOnPosition(new Vector2(), shipSize);
            Transform shipTwoTransform = Transform.createTransformWithSizeOnPosition(new Vector2(130, 0), shipSize);

            GameObjectFactory.createTexturedObject(this.sector, shipOneTransform, "MotherShip");
            GameObjectFactory.createTexturedObject(this.sector, shipTwoTransform, "MotherShip2");
        }
    }
}
