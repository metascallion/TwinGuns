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
    interface IVisitor
    {
        void visit(IElement element);
    }

    interface IElement
    {
        void accept(IVisitor visitor);
    }



    public class Shopper : IElement
    {
        public int CurrentPrice = 0;

        void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }
    }

    public class RabattVisitor : IVisitor
    {
        int rabattPercentage = 0;

        public RabattVisitor(int RabattPercentage)
        {
            this.rabattPercentage = RabattPercentage;
        }

        public void visit(IElement element)
        {
            Shopper shopper = element as Shopper;
            shopper.CurrentPrice *= shopper.CurrentPrice / 100 * rabattPercentage;            
        }
    }

    public class CouponVisitor : IVisitor
    {
        int CouponValue = 0;

        public CouponVisitor(int CouponValue)
        {
            this.CouponValue = CouponValue;
        }

        public void visit(IElement element)
        {
            Shopper shopper = element as Shopper;
            shopper.CurrentPrice -= this.CouponValue;        
        }
    }


    public static class GameObjectFactory
    {
        public static GameObject createBasicProjectile(Transform transform, Transform target, IComponentsSector sector, Color color)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new BoxCollider());
            temp.addComponent(new CharacterControler());
            temp.addComponent(new Projectile(target));
            temp.addComponent(new ColoredBox(color));
            return temp;
        }

        public static GameObject createProjectileWithCustomSpeed(Transform transform, Transform target, IComponentsSector sector, Color color, int speed)
        {
            GameObject temp = new GameObject(sector);
            temp.addComponent(transform);
            temp.addComponent(new BoxCollider());
            temp.addComponent(new CharacterControler());
            temp.addComponent(new Projectile(target));
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
}
