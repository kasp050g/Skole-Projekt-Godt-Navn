using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SkoleProjekt_GodtNavn
{
    public class Spell : GameObject
    {
        /// <summary>
        /// Sets the facing for the projectile.
        /// </summary>
        private Facing facingCaster = new Facing();

        /// <summary>
        /// Projectile damage.
        /// </summary>
        private int damage = 0;

        /// <summary>
        /// The mana cost from the spell.
        /// </summary>
        private int manaCost;

        /// <summary>
        /// Checks if the owner is a Player.
        /// </summary>
        private bool isPlayer = false;

        /// <summary>
        /// Checks if the owner is a Boss.
        /// </summary>
        private bool isBoss = false;

        /// <summary>
        /// Projectile speed.
        /// </summary>
        private int speed = 500;

        /// <summary>
        /// How long the projectile is alive, after being created.
        /// </summary>
        private float spellLiveTimer = 8f;

        public Spell(Facing facing, int mana, bool isPlayer)
        {
            facingCaster = facing;
            damage = 20;
            manaCost = mana;
            this.isPlayer = isPlayer;

        }

        public Spell(Facing facing, int mana, int damage, bool isPlayer)
        {
            facingCaster = facing;
            this.damage = damage;
            manaCost = mana;
            this.isPlayer = isPlayer;
        }

        public Spell(Facing facing, int mana, int damage, bool isPlayer, bool isBoss)
        {
            facingCaster = facing;
            this.damage = damage;
            manaCost = mana;
            this.isPlayer = isPlayer;
            this.isBoss = isBoss;
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);

            spellLiveTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spellLiveTimer < 0)
            {
                Gameworld.Destroy(this);
            }
        }

        /// <summary>
        /// On projectile collision.
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other)
        {
            // if the gameobject the projectile collided with as a type of Character.
            if (other is Character)
            {
                // Then check if the character is a player, and the projectile doesn't belong to the player.
                if (!isPlayer && other is Player)
                {
                    // Do damage, then self destruct.
                    (other as Player).TakeDamage(damage);
                    Gameworld.Destroy(this);
                }
                // Else if the projectile belongs to an enemy or boss, and it hits the player.
                else if (isPlayer && other is Enemy && (other as Character).IsAlive)
                {
                    // Do damage, then self destruct.
                    (other as Character).TakeDamage(damage);
                    Gameworld.Destroy(this);
                }
            }
        }
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)Transform.Position.X - (int)(0) + 25,
                    (int)Transform.Position.Y - (int)(0) + 25,
                    (int)(50),
                    (int)(50)
                    );
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            CastSpell();
            Transform.Scale = 0.7f;
            origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);

        }

        public virtual void Move(GameTime gameTime)
        {
            // Calculates deltaTime based on the gameTime.
            float deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move the GameObject based on the result from HandleInput, speed and deltaTime.
            Transform.Position += ((velocity * speed) * deltatime);
        }

        /// <summary>
        /// When the spell is being created / cast.
        /// </summary>
        private void CastSpell()
        {
            // if the owner is a player or boss, the projectile will copy the facing of its owner, and move in the same direction.
            if (isPlayer || isBoss)
            {
                if (facingCaster == Facing.Up)
                {
                    velocity.Y -= 1;
                    Transform.Rotation = MathHelper.ToRadians(180f);
                    //spritePositionOffset = new Vector2(50, 50);
                }
                if (facingCaster == Facing.Down)
                {
                    velocity.Y += 1;
                    Transform.Rotation = MathHelper.ToRadians(0);
                    //spritePositionOffset = new Vector2(50, 50);
                }
                if (facingCaster == Facing.Right)
                {
                    velocity.X += 1;

                    Transform.Rotation = MathHelper.ToRadians(-90f);
                    //spritePositionOffset = new Vector2(50, 50);
                }
                if (facingCaster == Facing.Left)
                {
                    velocity.X -= 1;
                    Transform.Rotation = MathHelper.ToRadians(90f);
                    //spritePositionOffset = new Vector2(50, 50);
                }
            }
            // If the owner isn't a player or boss, the projectile will move towards the player's position.
            else
            {
                // Checks the position of the Player compared to the Projectile, then checks if the distance is a positive or negative value.
                int distanceX = (int)(Gameworld.Player.Transform.Position.X - Transform.Position.X);
                int distanceY = (int)(Gameworld.Player.Transform.Position.Y - Transform.Position.Y);
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

                // Check if the X distance is positive.
                if (positiveX)
                {
                    velocity.X = 1;
                }
                else
                {
                    velocity.X = -1;
                }

                // Check if the Y distance is positive.
                if (positiveY)
                {
                    velocity.Y = 1;
                }
                else
                {
                    velocity.Y = -1;
                }

                if (velocity != Vector2.Zero)
                {
                    velocity.Normalize();
                }
            }


        }

        public override void Awake()
        {
        }
    }
}
