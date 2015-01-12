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

        private void OnBuildButtonClicked(Clickable sender)
        {
            GameObjectFactory.createDrone(this.parent.sector, this.transform, "Ship", this.parent.getComponent<TextureRegion>().SpriteEffect, 10, this.targetTransform);
        }


    }
}
