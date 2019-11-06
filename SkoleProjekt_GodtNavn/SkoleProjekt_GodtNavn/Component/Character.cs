using Microsoft.Xna.Framework;
using SkoleProjekt_GodtNavn.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{

    public abstract class Character : GameObject
    {
        protected string name;

        protected Stat health;
        protected Stat armor;
        protected Stat mana;

        protected bool isAttackable = true;
        protected bool isAlive = true;

        public Character()
        {
        }

        public Character(Stat health, Stat armor, Stat mana, bool isAttackable)
        {
            this.health = health;
            this.armor = armor;
            this.mana = mana;
            this.isAttackable = isAttackable;
        }

        /// <summary>
        /// When the character takes damage.
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            health.LowerValue(damage);
        }

        /// <summary>
        /// When the character recieves healing.
        /// </summary>
        /// <param name="heal"></param>
        public void Heal(float heal)
        {
            health.AddValue(heal);
        }

        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Transform.Position += ((speed * velocity) * deltaTime);
        }
    }
}
