using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Network;

namespace BachelorPrototype.GameClasses
{
    public class Projectile : Drawable
    {
        public bool ownedByPlayerOne;

        public int life = 3;
        ColorBoxDrawer drawer;
        public TextureRegion reg1;
        public TextureRegion reg2;

        public Projectile(bool ownedByPlayerOne)
        {
            reg1 = new TextureRegion();
            reg1.Size = new Vector2(100, 100);
            reg1.Texture = Game1.r;

            reg2 = new TextureRegion();
            reg2.Size = new Vector2(100, 100);
            reg2.Texture = Game1.l; 

            this.ownedByPlayerOne = ownedByPlayerOne;

            if (ownedByPlayerOne)
            {
                this.Position = new Vector2(0, 50);
            }

            else
            {
                this.Position = new Vector2(650, 150);
            }
            drawer = new ColorBoxDrawer(new Vector2(20, 20));
            drawer.setColor(Color.Red);
            drawer.activeState = state.lobby;
            this.activeState = state.lobby;
        }

        public void dealDmg()
        {
            life--;
            if(life <= 0)
            {
                Client.projectiles.Remove(this);
            }
        }
        public void update()
        {
            reg1.Position = this.Position;
            reg2.Position = this.Position;

            if (ownedByPlayerOne)
            {
                this.Position = new Vector2(this.Position.X + 3, this.Position.Y);
            }
            else
            {
                this.Position = new Vector2(this.Position.X - 3, this.Position.Y);
            }
            drawer.Position = this.Position;
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            if (this.activeState == StateContainer.getInstance().getState())
            {
                if (ownedByPlayerOne)
                {
                   reg1.draw(batch);
                }

                else
                    reg2.draw(batch);

            }
            //drawer.draw(batch);
        }
    }
}
