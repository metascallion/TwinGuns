using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Network;


namespace BachelorPrototype.GameClasses
{
    public class Shot : Drawable
    {

        public static List<Shot> shots = new List<Shot>();
        Projectile target;
        TextureRegion region;

        public Shot(Vector2 position, Projectile target)
        {
            this.Position = position;
            this.target = target;
            this.region = new TextureRegion();
            this.region.Texture = Game1.projectile;
            region.Size = new Vector2(16, 16);
            shots.Add(this);
        }


        public void update()
        {
            Vector2 vec = Vector2.One;
            if (target != null)
            {
                vec = new Vector2(target.Position.X + 50, target.Position.Y + 50) - this.Position;
                vec.Normalize();


                this.Position = this.Position + vec * 10;

                if (Vector2.Distance(this.Position, new Vector2(target.Position.X + 50, target.Position.Y + 50)) < 5f)
                {
                    target.dealDmg();
                    shots.Remove(this);
                }
            }

            else
            {
                shots.Remove(this);
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            if (target != null)
            {
                base.draw(batch);
                region.Position = this.Position;
                region.draw(batch);
            }
        }
    }


    public class Tower : Drawable
    {
        public bool ownedByPlayerOne;
        Projectile target;

        public static List<Tower> towers = new List<Tower>();

        float shootRate;
        float nextShot;

        public int x;
        public int y;

        public  Tower(float shootRate)
        {
            this.shootRate = shootRate;
            towers.Add(this);
        }

        
        public static void destoryTowerOnPosition(int x, int y)
        {
            for (int i = 0; i < towers.Count; i++)
            {
                if (towers[i].x == x && towers[i].y == y)
                {
                    towers.Remove(towers[i]);
                }
            }
        }

        public void chooseTarget()
        {
            float closestDistance = 10000;

            Projectile closest = null;

            foreach(Projectile p in Client.projectiles)
            {
                if(Vector2.Distance(this.Position, p.Position) < closestDistance)
                {
                    if (this.ownedByPlayerOne && !p.ownedByPlayerOne || !this.ownedByPlayerOne && p.ownedByPlayerOne)
                    {
                        closestDistance = Vector2.Distance(this.Position, p.Position);
                        closest = p;
                    }
                }
            }

            this.target = closest;
        }


        private void addShot()
        {
            chooseTarget();
            new Shot(this.Position, this.target);
        }

        public void shoot()
        {
            nextShot += 0.01f;

            if (nextShot >= shootRate)
            {
                addShot();
                nextShot = 0;
            }
        }



        public void update()
        {
            shoot();
        }


    }
}
