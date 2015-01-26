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
using Microsoft.Xna.Framework.Graphics;

namespace WhiteSpace
{

    public static class GameObjectFactory
    {
        public static GameObject createUnit(IComponentsSector sector, Transform transform, string textureFile, SpriteEffects effect, int health)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new TextureRegion(ContentLoader.getContent<Texture2D>(textureFile), effect));
            Life life = new Life(health);
            life.destroyOnDead = true;
            temp.addComponent(life);
            return temp;
        }

        public static GameObject createMotherShip(IComponentsSector sector, Transform transform, Transform enemyTransform, string textureFile, SpriteEffects effect, int health, bool player, GameRessources ressources)
        {
            GameObject temp = createUnit(sector, transform, textureFile, effect, health);
            temp.addComponent(new BoxCollider());
            temp.addComponent(new Clickable());
            temp.addComponent(new Hangar(enemyTransform, player, ressources));
            temp.addComponent(new HealthRenderer());
            temp.addComponent(new LifeUpdater(player));
            return temp;
        }

        public static GameObject createDrone(IComponentsSector sector, Transform transform, string textureFile, SpriteEffects effect, int health, Transform target)
        {
            Transform droneTransform = Transform.createTransformWithSizeOnPosition(transform.Position, new Vector2(25, 25));
            GameObject temp = createUnit(sector, droneTransform, textureFile, effect, health);
            temp.removeComponent<TextureRegion>();
            temp.addComponent(new BoxCollider());
            temp.addComponent(new CharacterControler());
            Life life = new Life(health);
            life.destroyOnDead = true;
            temp.addComponent(life);
            temp.addComponent<Clickable>();
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

        public static GameObject createTemporaryObjectWithTransform(IComponentsSector sector, Vector2 position, Vector2 size, int timeToStay)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(new OverTimeDestroyer(timeToStay));
            temp.addComponent(Transform.createTransformWithSizeOnPosition(position, size));
            return temp;
        }

        public static GameObject createButton(IComponentsSector sector, Transform transform)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new Button());
            return temp;
        }
        public static GameObject createButton(IComponentsSector sector, Transform transform, string text)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new Button(text));
            return temp;
        }

        public static GameObject createButton(IComponentsSector sector, Transform transform,  Button.stateChange clickMethod)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new Button());
            temp.getComponent<Button>().clickMethods += clickMethod;
            return temp;
        }

        public static GameObject createButton(IComponentsSector sector, Transform transform, int id, Button.stateChange clickMethod)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new Button());
            temp.getComponent<Button>().id = id;
            temp.getComponent<Button>().clickMethods += clickMethod;
            return temp;
        }

        public static GameObject createButton(IComponentsSector sector, Transform transform, string text, Button.stateChange clickMethod)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new Button(text));
            temp.getComponent<Button>().clickMethods += clickMethod;
            return temp;
        }

        public static GameObject createButton(IComponentsSector sector, Transform transform, string text, int id, Button.stateChange clickMethod)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new Button(text));
            temp.getComponent<Button>().id = id;
            temp.getComponent<Button>().clickMethods += clickMethod;
            return temp;
        }

        public static GameObject createTextField(IComponentsSector sector, Transform transform)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new ColoredBox(Color.Gray));
            temp.addComponent(new EditableText());
            return temp;
        }

        public static GameObject createLabel(IComponentsSector sector, Transform transform, string text)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new TextDrawer("Font", text));
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


