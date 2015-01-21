using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using WhiteSpace.Components.Physics;
using WhiteSpace.Components.Drawables;
using Microsoft.Xna.Framework;
using WhiteSpace.Temp;


namespace WhiteSpace.GameClasses
{
    public class Tower : UpdateableComponent
    {
        float kadenz;
        float timeToNextShot;

        public static List<Transform> enemyDronesTransforms = new List<Transform>();
        public static List<Transform> thisDronesTransforms = new List<Transform>();

        bool playerOne;
        public static void unregister(Transform transform)
        {
            if(enemyDronesTransforms.Contains(transform))
            {
                enemyDronesTransforms.Remove(transform);
            }

            else
            {
                thisDronesTransforms.Remove(transform);
            }
        }

        public Tower(float kadenz, bool playerOne)
        {
            this.kadenz = kadenz;
            this.timeToNextShot = kadenz;

            if(playerOne)
            {
                this.playerOne = playerOne;
            }
        }

        protected override void update(GameTime gameTime)
        {
            this.timeToNextShot -= gameTime.ElapsedGameTime.Milliseconds;

            if (thisDronesTransforms.Count > 0)
            {
                if (thisDronesTransforms[0].parent == null)
                    thisDronesTransforms.Remove(thisDronesTransforms[0]);
            }

            if (enemyDronesTransforms.Count > 0)
            {
                if (enemyDronesTransforms[0].parent == null)
                {
                    enemyDronesTransforms.Remove(enemyDronesTransforms[0]);
                }
            }

            if(timeToNextShot <= 0)
            {
                spawnShot();
                timeToNextShot = kadenz;
            }
        }

        private void spawnShot()
        {
            Transform transform = Transform.createTransformWithSizeOnPosition(this.parent.getComponent<Transform>().Position, new Vector2(20, 20));
            
            if (playerOne && enemyDronesTransforms.Count > 0)
            {
                GameObjectFactory.createProjectileWithCustomSpeed(transform, enemyDronesTransforms[0], this.parent.sector, Color.AliceBlue, 20);
            }
            else if(!playerOne && thisDronesTransforms.Count > 0)
            {
                GameObjectFactory.createProjectileWithCustomSpeed(transform, thisDronesTransforms[0], this.parent.sector, Color.Green, 20);
            }
        }

    }
}
