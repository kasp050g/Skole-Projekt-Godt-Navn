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
        /// <summary>
        /// Time passed in seconds.
        /// </summary>
        private float deltaTime;

        /// <summary>
        /// The amount of XP the Player is rewarded on a kill.
        /// </summary>
        private int xpAmount = 15;

        /// <summary>
        /// The aggro range of the Enemies.
        /// </summary>
        public float MaxAggroRange { get; set; } = 850;

        /// <summary>
        /// When the Enemy enters this range, it attacks.
        /// </summary>
        public float MeleeRange { get; set; } = 80;
        
        /// <summary>
        /// The range the Enemy can cast a spell from.
        /// </summary>
        public float SpellRange { get; set; } = 700;

        /// <summary>
        /// Defines if the Enemy is melee or ranged.
        /// </summary>
        public bool IsMelee { get; set; } = true;

        /// <summary>
        /// Defines the Enemy's damage.
        /// </summary>
        private int meleeDamage = 10;
        
        /// <summary>
        /// Defines the Enemy's attack speed.
        /// </summary>
        private float meleeAttackSpeed = 2f;

        /// <summary>
        /// Defines if the Enemy is attackable.
        /// </summary>
        protected bool isAttack = false;

        /// <summary>
        /// Defines if the Enemy is a boss.
        /// </summary>
        public bool IsBoss { get; set; } = false;

        /// <summary>
        /// Used for collision, to make the Enemy stop moving on colliding with an Enemy.
        /// </summary>
        private Vector2 enemyBlockMove = new Vector2(0, 0);

        // Death
        public bool deleteOnDeath = false;
        protected float deleteTimer = 3f;

        public Enemy(Stat health, bool isAttackable)
        {
            this.Health = health;

            //this.isAttackable = isAttackable;
        }

        public Enemy()
        {
        }


        public override void LoadContent(ContentManager content)
        {

        }

        /// <summary>
        /// What happens when the Enemy collides with another Object.
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other)
        {
            // If the Enemy collides with another Enemy, make the Enemy stop.
            if (other is Enemy)
            {
                if ((other as Enemy).IsAlive == true)
                {
                    Enemy enemy = (other as Enemy);

                    enemyBlockMove = Vector2.Zero;

                    int distanceX = (int)(enemy.Transform.Position.X - Transform.Position.X);
                    int distanceY = (int)(enemy.Transform.Position.Y - Transform.Position.Y);
                    bool positiveX = distanceX > 0;
                    bool positiveY = distanceY > 0;

                    if (Transform.Position.X != enemy.Transform.Position.X)
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

                    if (Transform.Position.Y != enemy.Transform.Position.Y)
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
            if (IsMelee)
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

        /// <summary>
        ///  Makes the enemy chase the player.
        /// </summary>
        /// <param name="player"></param>
        public void FollowPlayer(GameObject player)
        {
            //Reset velocity everytime.
            velocity = Vector2.Zero;
            // If the gameobject is a type of Player)
            if (player is Player)
            {
                // Calculates the distanc between the Player and the Enemy, and checks if its a positive or negative distance.
                int distanceX = (int)(player.Transform.Position.X - Transform.Position.X);
                int distanceY = (int)(player.Transform.Position.Y - Transform.Position.Y);
                bool positiveX = distanceX > 0;
                bool positiveY = distanceY > 0;
                // Makes the Enemy face the correct direction.
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
                        facing = Facing.Right;
                    }
                    else
                    {
                        facing = Facing.Left;
                    }
                }
                // Makes the enemy follow the Player when the Player enters the Enemy's aggro range.
                if ((Math.Abs(distanceX) < MaxAggroRange && Math.Abs(distanceY) < MaxAggroRange) && MoveCloser(distanceX, distanceY))
                {
                    // If the Enemy's X value isn't like the Player's X value.
                    if (Transform.Position.X != player.Transform.Position.X)
                    {
                        // Check if the distance is positive.
                        if (positiveX)
                        {
                            velocity.X += 1;
                        }
                        else
                        {
                            velocity.X -= 1;
                        }
                    }
                    // If the Enemy's Y value isn't like the Player's Y value.
                    if (Transform.Position.Y != player.Transform.Position.Y)
                    {
                        // Check if the distance is positive.
                        if (positiveY)
                        {
                            velocity.Y += 1;
                        }
                        else
                        {
                            velocity.Y -= 1;
                        }
                    }

                    // Collision check.
                    velocity -= enemyBlockMove;

                    // Normalize the vector, so the Enemy doesn't go faster than it's desired speed.
                    if (velocity != Vector2.Zero)
                    {
                        velocity.Normalize();
                    }
                }
            }
        }

        public bool MoveCloser(float distanceX,float distanceY)
        {
            
            if (IsBoss == true)
            {
                return true;
            }


            return (IsMelee ? (Math.Abs(distanceX) > MeleeRange || Math.Abs(distanceY) > MeleeRange) : (Math.Abs(distanceX) > SpellRange || Math.Abs(distanceY) > SpellRange));
        }

        private void AttackMeleePlayer(GameTime gameTime)
        {
            int distanceX = (int)(Gameworld.Player.Transform.Position.X - Transform.Position.X);
            int distanceY = (int)(Gameworld.Player.Transform.Position.Y - Transform.Position.Y);
            bool positiveX = distanceX > 0;
            bool positiveY = distanceY > 0;

            if (Math.Abs(distanceX) < MeleeRange + 5 && Math.Abs(distanceY) < MeleeRange + 5 && isAttack == false)
            {

                velocity = new Vector2(0, 0);
                if (deltaTime > meleeAttackSpeed)
                {
                    isAttack = true;
                }
                if (deltaTime > (meleeAttackSpeed + 1.0f))
                {
                    if (Math.Abs(distanceX) < MeleeRange + 5 && Math.Abs(distanceY) < MeleeRange + 5)
                        Gameworld.Player.TakeDamage(meleeDamage);
                    deltaTime = 0.0f;
                }
            }
        }
        private void AttackRangePlayer(GameTime gameTime)
        {
            int distanceX = (int)(Gameworld.Player.Transform.Position.X - Transform.Position.X);
            int distanceY = (int)(Gameworld.Player.Transform.Position.Y - Transform.Position.Y);
            bool positiveX = distanceX > 0;
            bool positiveY = distanceY > 0;

            if (Math.Abs(distanceX) < SpellRange + 5 && Math.Abs(distanceY) < SpellRange + 5 && isAttack == false)
            {

                velocity = new Vector2(0, 0);
                if (deltaTime > meleeAttackSpeed)
                {
                    isAttack = true;
                }
                if (deltaTime > (meleeAttackSpeed + 0.5f))
                {
                    int spellDamage = 10;
                    Texture2D image = Gameworld.spriteContainer.soleSprite["fire_"];


                    if (IsBoss == true)
                    {
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Down, new Vector2(-100, 0), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Down, new Vector2(0, 0), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Down, new Vector2(+100, 0), true);

                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Up, new Vector2(-100, 0), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Up, new Vector2(0, 0), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Up, new Vector2(+100, 0), true);


                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Right, new Vector2(0, -200), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Right, new Vector2(0, 0), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Right, new Vector2(0, +200), true);

                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Left, new Vector2(0, -200), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Left, new Vector2(0, 0), true);
                        CastSpell(spellDamage, image, new Vector2(50, 50), Facing.Left, new Vector2(0, +200), true);

                        if (Math.Abs(distanceX) < MeleeRange + 5 && Math.Abs(distanceY) < MeleeRange + 5 )
                        {
                            if (Math.Abs(distanceX) < MeleeRange + 5 && Math.Abs(distanceY) < MeleeRange + 5)
                                Gameworld.Player.TakeDamage(meleeDamage);
                        }
                    }
                    else
                    {
                        CastSpell(spellDamage, image, new Vector2(50, 50), facing, new Vector2(0, 0), false);
                    }

                    deltaTime = 0.0f;
                }
            }
        }

        private void CastSpell(int spellDamage, Texture2D image, Vector2 imageOffSet, Facing _facing, Vector2 spawnPosition, bool isBoss)
        {
            Spell spell = new Spell(_facing, 0, spellDamage, false, isBoss);
            spell.Sprite = image;
            Vector2 positionOffSet = SpellPositionOffset();
            spell.Transform.Position = new Vector2(Transform.Position.X + spawnPosition.X, Transform.Position.Y + spawnPosition.Y) + positionOffSet;
            spell.LayerDepth = 0.7f;
            spell.SpritePositionOffset = imageOffSet;
            spell.Color = Color.LightCoral;
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
                case Facing.Right:
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
                    return Facing.Right;
                case Facing.Right:
                    return Facing.Left;
                default:
                    return Facing.Left;
                    break;
            }
        }
        public virtual void Death(Player player)
        {
            LootDrop lootDrop = new LootDrop(this.Transform.Position);
            Gameworld.Instatiate(lootDrop);

            IsAlive = false;
            player.GetXP(xpAmount);

        }
    }
}
