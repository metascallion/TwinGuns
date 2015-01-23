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
    public class Hangar : StandardComponent
    {
        Transform targetTransform;
        Transform transform;

        GameObject[,] droneStocks = new GameObject[3, 3];

        public Hangar()
        {
        }

        public Hangar(Transform target)
        {
            this.targetTransform = target;
        }

        public override void start()
        {
            base.start();
            this.transform = this.parent.getComponent<Transform>();
            this.parent.getComponent<Clickable>().releaseMethods += OnBuildButtonClicked;
        }

        public void addDrone(int stock)
        {
            for(int i = 0; i <= 2; i++)
            {
                if(droneStocks[i, stock] == null)
                {
                    Transform transform = Transform.createTransformOnPosition(new Vector2(this.transform.Position.X + 40 * i, this.transform.Center.Y + 30 + stock * 40));
                    droneStocks[i, stock] = GameObjectFactory.createDrone(this.parent.sector, transform, "Ship", Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 15, targetTransform);
                    break;
                }
            }
        }

        public void openStock(int stock)
        {
            for (int i = 0; i <= 2; i++)
            {
                if (droneStocks[i, stock] != null)
                {
                    droneStocks[i, stock].addComponent(new Ship(targetTransform));
                    droneStocks[i, stock] = null;
                }
            }
        }

        private void OnBuildButtonClicked(Clickable sender)
        {
            GameObject go = GameObjectFactory.createDrone(this.parent.sector, this.transform, "Ship", this.parent.getComponent<TextureRegion>().SpriteEffect, 3, this.targetTransform);
            go.getComponent<Clickable>().releaseMethods += OnDroneClick;
        }

        private void OnDroneClick(Clickable sender)
        {
            sender.parent.getComponent<Life>().reduceHealth();
        }
    }
}
