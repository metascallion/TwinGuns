using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Components;
using WhiteSpace.Temp;
using WhiteSpace.GameLoop;
using WhiteSpace.Components.Drawables;
using WhiteSpace.Components.Physics;
using Microsoft.Xna.Framework;
using WhiteSpace.GameClasses;

namespace WhiteSpace
{

    public static class GameObjectFactory
    {
        public static GameObject createBasicShip(Transform transform, Transform target, IComponentsSector sector, Color color)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new BoxCollider());
            temp.addComponent(new CharacterControler());
            temp.addComponent(new Ship(target));
            temp.addComponent(new Life(25));
            temp.addComponent(new ColoredBox(color));
            return temp;
        }

        public static GameObject createProjectileWithCustomSpeed(Transform transform, Transform target, IComponentsSector sector, Color color, int speed)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new BoxCollider());
            temp.addComponent(new CharacterControler());
            temp.addComponent(new Shot(target, speed));
            temp.addComponent(new ColoredBox(color));
            return temp;
        }

        public static GameObject createTestObject(IComponentsSector sec)
        {
            GameObject temp = new GameObject(sec);
            temp.addComponent(new Transform());
            temp.addComponent(new ColoredBox(Color.Red));
            return temp;
        }
    }

    public abstract class GameObjectModifier : GameObject
    {
        protected GameObject gameObjectToModify;
        public GameObjectModifier(GameObject gameObjectToModify) : base(gameObjectToModify.sector)
        {
            this.gameObjectToModify = gameObjectToModify;
        }
        protected abstract void addBehavior();
    }

    public class Triggerable : GameObjectModifier
    {
        public Triggerable(GameObject gameObjectToModify) : base(gameObjectToModify)
        {
            addBehavior();
        }
        protected override void addBehavior()
        {
            BoxCollider collider = new BoxCollider();
            collider.trigger = true;
            this.gameObjectToModify.addComponent(collider);
        }
    }

    public class Killable : GameObjectModifier
    {
        private int health;

        public Killable(GameObject gameObjectToModify, int health) : base(gameObjectToModify)
        {
            this.health = health;
            addBehavior();
        }
        protected override void addBehavior()
        {
            Life life = new Life(this.health);
            life.destroyOnDead = true;
            this.gameObjectToModify.addComponent(life);
        }
    }

    public class Shootable : GameObjectModifier
    {
        Transform target;
        public Shootable(GameObject gameObjectToModify, Transform target) : base(gameObjectToModify)
        {
            this.target = target;
            addBehavior();
        }

        protected override void addBehavior()
        {
            this.gameObjectToModify.addComponent(this.target);
        }
    }
}


