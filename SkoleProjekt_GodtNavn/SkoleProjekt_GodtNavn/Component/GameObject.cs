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
    public abstract class GameObject
    {
        protected Texture2D sprite;
        protected Texture2D[] sprites;

        public Transform Transform { get; set; }

        protected Vector2 velocity;
        protected Vector2 origin;

        protected float fps;
        protected float speed;
        protected float layerDepth = 0;

        private float animateTimeElasped;
        private int animationCurrentIndex;

        public Vector2 screenSize;

        public virtual void Awake()
        {

        }

        public virtual void Initialize()
        {
            screenSize = GameWorld.ScreenSize;
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Transform.Position, rectangle, Color.White, Transform.Rotation, origin, Transform.Scale, SpriteEffects.None, layerDepth);
        }

        protected void Animate(GameTime gameTime)
        {
            animateTimeElasped += (float)gameTime.ElapsedGameTime.TotalSeconds;

            animationCurrentIndex = (int)(animateTimeElasped * fps);

            if (animationCurrentIndex >= sprites.Length)
            {
                animateTimeElasped = 0;
                animationCurrentIndex = 0;
            }

            sprite = sprites[animationCurrentIndex];
        }

        public virtual Rectangle rectangle
        {
            get
            {
                return new Rectangle(
                    0,
                    0,
                    sprite.Width,
                    sprite.Height
                    );
            }
        }

        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)Transform.Position.X - (int)(origin.X * Transform.Scale),
                    (int)Transform.Position.Y - (int)(origin.Y  * Transform.Scale),
                    (int)(sprite.Width * Transform.Scale),
                    (int)(sprite.Height * Transform.Scale)
                    );
            }
        }

        /// <summary>
        /// Checks if this GameObject has collided with another GameObject
        /// </summary>
        /// <param name="other">The object we colloded with</param>
        public void CheckCollision(GameObject other)
        {
            if (CollisionBox.Intersects(other.CollisionBox))
            {
                OnCollision(other);
            }
        }


        /// <summary>
        /// Is executed whenever a collision occurs
        /// </summary>
        /// <param name="other">The object we collided with</param>
        public abstract void OnCollision(GameObject other);
    }
}
