using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SkoleProjekt_GodtNavn
{
    public abstract class Enemy : Character
    {
        private float deltaTime;

        public int xpAmount = 10;

        private float maxAggroRange = 850;

        public float meleeRange = 90;

        public bool isMelee = true;
        public int meleeDamage = 10;
        public float meleeAttackSpeed = 2f;

        public bool isAttack = false;

        Vector2 enemyBlockMove = new Vector2(0, 0);

        // Death
        public bool deleteOnDeath = false;
        public float deleteTimer = 3f;

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
            if (other is Enemy)
            {
                if ((other as Enemy).isAlive == true)
                {
                    Enemy enemy = (other as Enemy);

                    enemyBlockMove = Vector2.Zero;

                    int distanceX = (int)(enemy.transform.position.X - transform.position.X);
                    int distanceY = (int)(enemy.transform.position.Y - transform.position.Y);
                    bool positiveX = distanceX > 0;
                    bool positiveY = distanceY > 0;

                    if (transform.position.X != enemy.transform.position.X)
                    {
                        if (positiveX)
                        {
                            enemyBlockMove.X += 1;
                        }
                        else
                        {
                            enemyBlockMove.X -= 1;
                        }
                    }

                    if (transform.position.Y != enemy.transform.position.Y)
                    {
                        if (positiveY)
                        {
                            enemyBlockMove.Y += 1;
                        }
                        else
                        {
                            enemyBlockMove.Y -= 1;
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

            deltaTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            FollowPlayer(Gameworld.Player);
            if (isMelee)
            {
                AttackMeleePlayer(gameTime);
            }
            else
            {
                AttackRangePlayer(gameTime);
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
                        facing = Facing.Down;
                    }
                    else
                    {
                        facing = Facing.Up;
                    }
                }
                else
                {
                    if (distanceX > 0)
                    {
                        facing = Facing.Rigth;
                    }
                    else
                    {
                        facing = Facing.Left;
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

                    velocity -= enemyBlockMove;

                    if (velocity != Vector2.Zero)
                    {
                        velocity.Normalize();
                    }
                }
            }
        }

        private void AttackMeleePlayer(GameTime gameTime)
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
        private void AttackRangePlayer(GameTime gameTime)
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
                    int spellDamage = 10;
                    Texture2D image = Gameworld.spriteContainer.soleSprite["fire_"];
                    CastSpell(spellDamage, image, new Vector2(50, 50));
                    deltaTime = 0.0f;
                }
            }
        }

        void CastSpell(int spellDamage, Texture2D image, Vector2 imageOffSet)
        {
            Spell spell = new Spell(facing, 0, spellDamage, false);
            spell.sprite = image;
            Vector2 positionOffSet = SpellPositionOffset();
            spell.transform.position = new Vector2(transform.position.X, transform.position.Y) + positionOffSet;
            spell.layerDepth = 0.7f;
            spell.spritePositionOffset = imageOffSet;
            spell.color = Color.LightCoral;
            Gameworld.Instatiate(spell);
        }
        public Vector2 SpellPositionOffset()
        {
            switch (facing)
            {
                case Facing.Up:
                    return new Vector2(0, -50);
                case Facing.Down:
                    return new Vector2(0, 50);
                case Facing.Left:
                    return new Vector2(-50, 0);
                case Facing.Rigth:
                    return new Vector2(50, 0);
                default:
                    return new Vector2(0, 0);
            }
        }
        public Facing SetSpellFacing()
        {
            switch (facing)
            {
                case Facing.Up:
                    return Facing.Down;
                case Facing.Down:
                    return Facing.Up;
                case Facing.Left:
                    return Facing.Rigth;
                case Facing.Rigth:
                    return Facing.Left;
                default:
                    return Facing.Left;
                    break;
            }
        }
        public virtual void Death(Player player)
        {
            LootDrop lootDrop = new LootDrop(this.transform.position);
            Gameworld.Instatiate(lootDrop);

            isAlive = false;
            player.GetXP(xpAmount);

        }
    }
}
