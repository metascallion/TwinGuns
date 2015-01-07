using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Temp
{

    public delegate void registerInComponentSector(IComponentsSector sector);

    public class GameObject
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

        public T getComponent<T>() where T : Component, new()
        {
            T t = new T();
            foreach (Component component in children)
            {
                if(component.GetType() == typeof(T))
                {
                    component.unregisterInComponentSector();
                    return (T)component;
                }
            }
            throw new MissingMemberException("No " + t.GetType().Name + " attached.");
        }

        public void destroy()
        {
            for(int i = 0; i < this.children.Count; i++)
            {
                children[i].unregisterInComponentSector();
                children[i].onDestroy();
                children[i].parent = null;
                children[i] = null;
            }
            this.children.Clear();
            this.children = null;
            this.sector = null;
        }
    }
}
