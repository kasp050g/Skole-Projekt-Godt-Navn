using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SkoleProjekt_GodtNavn.Component;

namespace SkoleProjekt_GodtNavn
{
    public class Enemy : Character
    {
        public Enemy(Stat health, Stat armor, Stat mana, bool isAttackable) : base(health, armor, mana, isAttackable)
        {
            this.health = health;
            this.armor = armor;
            this.mana = mana;
            this.isAttackable = isAttackable;
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void Update(GameTime gameTime)
        {
            FollowPlayer(GameWorld.Player);
        }

        private void FollowPlayer(GameObject player)
        {
            if(player is Player)
            {
                int distanceX = 0;
                int distanceY = 0;
                bool positiveX = distanceX > 0;
                bool positiveY = distanceY > 0;

                if(Transform.Position.X != player.Transform.Position.X)
                {
                    distanceX = (int)(player.Transform.Position.X - Transform.Position.X);

                    if (positiveX)
                    {
                        velocity.X += 1;
                    }
                    else
                    {
                        velocity.X -= 1;
                    }
                }
                
                if(Transform.Position.Y != player.Transform.Position.Y)
                {
                    distanceY = (int)(player.Transform.Position.Y - Transform.Position.Y);

                    if (positiveY)
                    {
                        velocity.Y += 1;
                    }
                    else
                    {
                        velocity.Y -= 1;
                    }
                }

                if (velocity != Vector2.Zero)
                {
                    velocity.Normalize();
                }
            }
        }
    }
}
