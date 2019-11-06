using Microsoft.Xna.Framework;
using SkoleProjekt_GodtNavn.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{

    abstract class Character : GameObject
    {
        protected string name;

        protected Stat health;
        protected Stat armor;
        protected Stat mana;

        protected bool isAttackable = true;
        protected bool isAlive = true;

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

        }

        public void Heal(float heal)
        {

        }

        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            transform.Position += ((speed * velocity) * deltaTime);
        }
    }
}
