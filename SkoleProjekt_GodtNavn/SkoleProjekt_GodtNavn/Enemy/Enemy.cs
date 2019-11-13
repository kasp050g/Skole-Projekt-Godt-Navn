using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SkoleProjekt_GodtNavn
{
    public abstract class Enemy : Character
    {
        private float deltaTime;

        private int xpAmount = 10;

        private float maxAggroRange = 850;

        public float meleeRange = 90;

        bool isMelee = true;
        public int meleeDamage = 10;
        public float meleeAttackSpeed = 2f;

        public bool isAttack = false;

        public Enemy(Stat health, bool isAttackable)
        {
            this.health = health;

            //this.isAttackable = isAttackable;
        }

        public Enemy()
        {
        }


        public override void LoadContent(ContentManager content)
        {

        }

        public override void OnCollision(GameObject other)
        {
        }

        public override void Update(GameTime gameTime)
        {

            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            FollowPlayer(Gameworld.Player);
            if (isMelee)
            {
                AttackPlayer(gameTime);
            }
            Move(gameTime);
            //Death(Gameworld.Player);
        }

        public void FollowPlayer(GameObject player)
        {
            velocity = Vector2.Zero;
            if (player is Player)
            {
                int distanceX = (int)(player.transform.position.X - transform.position.X);
                int distanceY = (int)(player.transform.position.Y - transform.position.Y);
                bool positiveX = distanceX > 0;
                bool positiveY = distanceY > 0;

                if (Math.Abs(distanceY) > Math.Abs(distanceX))
                {
                    if (distanceY > 0)
                    {
                        facing = Facing.Up;
                    }
                    else
                    {
                        facing = Facing.Down;
                    }
                }
                else
                {
                    if (distanceX > 0)
                    {
                        facing = Facing.Left;
                    }
                    else
                    {
                        facing = Facing.Rigth;
                    }
                }

                if ((Math.Abs(distanceX) < maxAggroRange && Math.Abs(distanceY) < maxAggroRange) && (Math.Abs(distanceX) > meleeRange || Math.Abs(distanceY) > meleeRange))
                {
                    if (transform.position.X != player.transform.position.X)
                    {
                        if (positiveX)
                        {
                            velocity.X += 1;
                        }
                        else
                        {
                            velocity.X -= 1;
                        }
                    }

                    if (transform.position.Y != player.transform.position.Y)
                    {
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

        private void AttackPlayer(GameTime gameTime)
        {
            int distanceX = (int)(Gameworld.Player.transform.position.X - transform.position.X);
            int distanceY = (int)(Gameworld.Player.transform.position.Y - transform.position.Y);
            bool positiveX = distanceX > 0;
            bool positiveY = distanceY > 0;

            if (Math.Abs(distanceX) < meleeRange + 5 && Math.Abs(distanceY) < meleeRange + 5 && isAttack == false)
            {

                velocity = new Vector2(0, 0);
                if (deltaTime > meleeAttackSpeed)
                {
                    isAttack = true;
                }
                if (deltaTime > (meleeAttackSpeed + 1.0f))
                {
                    if (Math.Abs(distanceX) < meleeRange + 5 && Math.Abs(distanceY) < meleeRange + 5)
                        Gameworld.Player.TakeDamage(meleeDamage);
                    deltaTime = 0.0f;
                }
            }
        }

        public void Death(Player player)
        {
            LootDrop lootDrop = new LootDrop(this.transform.position);
            Gameworld.Instatiate(lootDrop);

            isAlive = false;
            player.GetXP(xpAmount);

        }
    }
}
