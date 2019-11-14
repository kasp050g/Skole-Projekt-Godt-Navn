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
        private Facing facingCaster = new Facing();

        private int damage = 0;

        private int manaCost;

        private bool isPlayer = false;

        private int speed = 500;

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

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Character)
            {
                if (!isPlayer && other is Player)
                {
                    (other as Player).TakeDamage(damage);
                    Gameworld.Destroy(this);
                }
                else if (isPlayer && other is Enemy && (other as Character).isAlive)
                {                    
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
                    (int)transform.position.X - (int)(0) + 0,
                    (int)transform.position.Y - (int)(0) + 0,
                    (int)(100),
                    (int)(100)
                    );
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            CastSpell();
            transform.scale = 0.7f;
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            
        }

        public virtual void Move(GameTime gameTime)
        {
            // Calculates deltaTime based on the gameTime.
            float deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Move the GameObject based on the result from HandleInput, speed and deltaTime.
            transform.position += ((velocity * speed) * deltatime);
        }

        private void CastSpell()
        {
            if (facingCaster == Facing.Up)
            {
                velocity.Y -= 1;
                transform.rotation = MathHelper.ToRadians(180f);
                //spritePositionOffset = new Vector2(50, 50);
            }
            if (facingCaster == Facing.Down)
            {
                velocity.Y += 1;
                transform.rotation = MathHelper.ToRadians(0);
                //spritePositionOffset = new Vector2(50, 50);
            }
            if (facingCaster == Facing.Rigth)
            {
                velocity.X += 1;
                
                transform.rotation = MathHelper.ToRadians(-90f);
                //spritePositionOffset = new Vector2(50, 50);
            }
            if (facingCaster == Facing.Left)
            {
                velocity.X -= 1;
                transform.rotation = MathHelper.ToRadians(90f);
                //spritePositionOffset = new Vector2(50, 50);
            }
        }
    }
}
