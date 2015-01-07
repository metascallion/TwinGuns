using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Temp
{
    public delegate void registerInComponentSector(IComponentsSector sector);

    public sealed class GameObject
    {
        private List<Component> children = new List<Component>();
        public IComponentsSector sector;

        public GameObject(IComponentsSector sectorToRegisterTo)
        {
            this.sector = sectorToRegisterTo;
        }

        public void addComponent(Component componentToAdd)
        {
            this.children.Add(componentToAdd);
            componentToAdd.parent = this;
            componentToAdd.registerInComponentSector();
            componentToAdd.start();
        }

        public void removeComponent(Component componentToRemove)
        {
            if (children.Contains(componentToRemove))
            {
                componentToRemove.unregisterInComponentSector();
                children.Remove(componentToRemove);
            }
        }

        public void removeComponent<T>() where T : Component
        {
            for (int i = 0; i < this.children.Count; i++)
            {
                if (children[i] is T)
                {
                    children[i].unregisterInComponentSector();
                    children.Remove(children[i]);
                    return;
                }
            }
        }

        public T getComponent<T>() where T : Component
        {
            foreach (Component component in children)
            {
                if (component is T)
                {
                    component.unregisterInComponentSector();
                    return (T)component;
                }
            }
            throw new ArgumentOutOfRangeException("No Component of this type attached.");
        }

        public void destroy()
        {
            foreach (Component component in children)
            {
                component.unregisterInComponentSector();
                component.onDestroy();
            }
        }
    }
}
