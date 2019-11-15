using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{
    public abstract class Character : GameObject
    {
        /// <summary>
        /// Character speed.
        /// </summary>
        protected float speed = 250;
        
        /// <summary>
        /// Check if the character is alive.
        /// </summary>
        public bool IsAlive { get; set; } = true;

        /// <summary>
        /// Character's health.
        /// </summary>
        public Stat Health { get; set; } = new Stat();

        /// <summary>
        /// Blood color.
        /// </summary>
        protected Color bloodColor = Color.White;
        
        /// <summary>
        /// Defines how the object moves.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Move(GameTime gameTime)
        {
            // Calculates deltaTime based on the gameTime.
            float deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move the GameObject based on the result from HandleInput, speed and deltaTime.
            Transform.Position += ((velocity * speed) * deltatime);
        }

        /// <summary>
        /// Makes the object take a specific damage.
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(int damage)
        {
            // If  the object is alive, then we can proceed.
            if (IsAlive)
            {
                // We use the object we checked IsAlive on, to reduce it's health. Then a blood effect is created and a sound is played.
                IsAlive = Health.LowerValueBool(damage);
                BloodEffect bloodEffect = new BloodEffect(this,bloodColor);
                bloodEffect.Initialize();
                Gameworld.Instatiate(bloodEffect);
                Gameworld.audioPlayer.SoundEffect_Play("SoundEffect_Hit", 0.1f);

                // if the object is the player and the player is not alive, and the attacker is an enemy, then the Player dies.
                if (IsAlive == false && this is Enemy)
                {
                    (this as Enemy).Death(Gameworld.Player);
                }
            }
        }
    }
}
