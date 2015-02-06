using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteSpace.Composite;

namespace WhiteSpace.Components
{
    delegate void onDead();

    class Life : StandardComponent
    {
        public event onDead killMethods;
        public int Health{ get; set; }

        public bool destroyOnDead = false;

        public Life()
        {
            this.Health = 10;
        }

        public Life(int health)
        {
            this.Health = health;
        }

        public void inspectLife()
        {
            if (Health <= 0)
            {
                executeDead();
            }
        }

        public void executeDead()
        {
            if(destroyOnDead)
            {
                this.parent.destroy();
            }

            if(killMethods != null)
            {
                killMethods();
            }
        }

        public void reduceHealth()
        {
            Health--;
            inspectLife();
        }

        public void reduceHealthByAmount(int amount)
        {
            Health -= amount;
            inspectLife();
        }
    }
}
