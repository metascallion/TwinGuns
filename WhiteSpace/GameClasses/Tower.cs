using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.GameLoop;
using WhiteSpace.Components.Physics;
using WhiteSpace.Components.Drawables;
using Microsoft.Xna.Framework;
using WhiteSpace.Composite;
using WhiteSpace.Network;


namespace WhiteSpace.GameClasses
{
    public class RessourceTower : StandardComponent
    {
        GameObject linkedTower;

        public RessourceTower()
        {

        }

        public RessourceTower(GameObject linkedTower)
        {
            this.linkedTower = linkedTower;
        }

        public override void onDestroy()
        {
            base.onDestroy();
            linkedTower.destroy();
        }
    }

    public class Tower : UpdateableComponent
    {
        float kadenz;
        float timeToNextShot;
        GameObject linkedTower;

        public static List<Transform> enemyDronesTransforms = new List<Transform>();
        public static List<Transform> thisDronesTransforms = new List<Transform>();

        bool firstTargetFound = false;

        Transform target;

        bool playerOne;

        public Tower()
        {

        }

        public Tower(float kadenz, bool playerOne, GameObject linkedTower)
        {
            this.kadenz = kadenz;
            this.timeToNextShot = kadenz;
            this.linkedTower = linkedTower;
            if (playerOne)
            {
                this.playerOne = playerOne;
            }
        }

        public override void onDestroy()
        {
            base.onDestroy();
            linkedTower.destroy();
        }

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

        Transform getClosestTarget()
        {
            if (playerOne && enemyDronesTransforms.Count > 0)
            {
                return getClosestTarget(enemyDronesTransforms);
            }
            else if (!playerOne && thisDronesTransforms.Count > 0)
            {
                return getClosestTarget(thisDronesTransforms);
            }

            return null;
        }

       Transform getClosestTarget(List<Transform> listToSearchIn)
        {
           Transform closestTransform = null;
           float closestDistance = float.MaxValue;

           foreach(Transform transform in listToSearchIn)
           {
               if (transform.parent != null)
               {
                   float distance = Math.Abs(transform.parent.getComponent<Ship>().target.Position.X - transform.Position.X);
                   
                   if (distance < closestDistance)
                   {
                       closestTransform = transform;
                       closestDistance = distance;
                   }
               }
           }

           return closestTransform;
        }

        private void spawnShot()
        {
            Transform transform = Transform.createTransformWithSizeOnPosition(this.parent.getComponent<Transform>().Position, new Vector2(7, 7));

            if (!firstTargetFound)
            {
                target = getClosestTarget();
            }
            else if(target.parent == null)
            {
                target = null;
                firstTargetFound = false;
                target = getClosestTarget();
            }

            if (target != null)
            {
                Color color;
                if(playerOne)
                {
                    color = Color.Green;
                }

                else
                {
                    color = Color.Red;
                }
                firstTargetFound = true;
                GameObjectFactory.createProjectileWithCustomSpeed(transform, target, this.parent.sector, color, 20);
            }
        }

    }
}
