using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WhiteSpace.GameLoop;

namespace WhiteSpace.Temp
{
    public abstract class Component
    {
        public GameObject parent;

        public Component()
        {
        }

        public abstract void registerInComponentSector();
        public abstract void unregisterInComponentSector();
        public abstract void start();
    }

    public class StandardComponent : Component
    {
        public override void registerInComponentSector()
        {
        }

        public override void unregisterInComponentSector()
        {
        }

        public override void start()
        {
        }
    }

    public abstract class DrawableComponent : Component
    {
        protected abstract void draw(SpriteBatch spriteBatch);
        public override sealed void registerInComponentSector()
        {
            this.parent.sector.addDrawMethod(this.draw);
        }
        public override sealed void unregisterInComponentSector()
        {
            this.parent.sector.removeDrawMethod(this.draw);
        }

        public override void start()
        {
        }
    }

    public abstract class UpdateableComponent : Component
    {
        protected abstract void update(GameTime gameTime);
        public override sealed void registerInComponentSector()
        {
            this.parent.sector.addUpdateMethod(this.update);
        }

        public override sealed void unregisterInComponentSector()
        {
            this.parent.sector.removeUpdateMethod(this.update);
        }

        public override void start()
        {
        }
    }

    public class Bitch : UpdateableComponent
    {
        int a;

        public Bitch()
        {
        }

        public Bitch(int a)
        {
            this.a = a;
        }

        protected override void update(GameTime gameTime)
        {
        }
    }

    public class Hoe : UpdateableComponent
    {
        public Hoe()
        {
        }

        protected override void update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }

}
