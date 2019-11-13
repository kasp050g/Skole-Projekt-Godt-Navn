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
        public float speed = 250;
        public Vector2 velocity;
        public bool isAlive = true;
        public Stat health = new Stat();
        public virtual void Move(GameTime gameTime)
        {
            // Calculates deltaTime based on the gameTime.
            float deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move the GameObject based on the result from HandleInput, speed and deltaTime.
            transform.position += ((velocity * speed) * deltatime);
        }

        public void TakeDamage(int damage)
        {
            isAlive = health.LowerValueBool(damage);
        }
    }
}
