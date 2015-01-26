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
            bool contains = false;
            foreach(Component c in children)
            {
                if(c.GetType() == componentToAdd.GetType())
                {
                    contains = true;
                }
            }

            if (!contains)
            {
                this.children.Add(componentToAdd);
                componentToAdd.parent = this;
                componentToAdd.registerInComponentSector();
                componentToAdd.start();
            }
        }

        public void addComponentIgnoreDuplication(Component componentToAdd)
        {
            this.children.Add(componentToAdd);
            componentToAdd.parent = this;
            componentToAdd.registerInComponentSector();
            componentToAdd.start();
        }

        public void addComponent<T>() where T : Component, new()
        {
            T t = new T();
            this.addComponent(t);
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
            if (this.children != null)
            {
                foreach (Component component in children)
                {
                    if (component.GetType() == typeof(T))
                    {
                        return (T)component;
                    }
                }
                throw new MissingMemberException("No " + t.GetType().Name + " attached.");
            }
            return null;
        }

        public bool hasComponent<T>() where T : Component
        {
            foreach(Component c in children)
            {
                if(c.GetType() == typeof(T))
                {
                    return true;
                }
            }
            return false;
        }

        public void destroy()
        {
            for(int i = 0; i < this.children.Count; i++)
            {
                children[i].unregisterInComponentSector();
                children[i].onDestroy();

            }

            for (int i = 0; i < this.children.Count; i++)
            {
                children[i].parent = null;
                children[i] = null;
            }
            
            this.children.Clear();
            this.children = null;
            this.sector = null;
        }
    }
}
