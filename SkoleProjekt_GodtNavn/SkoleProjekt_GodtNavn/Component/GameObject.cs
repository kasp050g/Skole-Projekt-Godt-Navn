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
        public Transform transform = new Transform();
        public Vector2 origin;
        public Texture2D sprite;
        public float layerDepth = 0;

        public Vector2 spritePositionOffset = new Vector2(0, 0);

        public Vector2 screenSize;

        #region Animate Value's
        public Texture2D[] sprites;
        protected float fps = 10;
        protected float timeElapsed;
        protected int currentIndex;
        #endregion

        public virtual void Awake()
        {

        }

        public virtual void Initialize()
        {
            screenSize = Gameworld.ScreenSize;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent(ContentManager content);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, transform.position + spritePositionOffset, rectangle, Color.White, transform.rotation, origin, transform.scale, SpriteEffects.None, layerDepth);
        }

        public virtual void Animate(GameTime gameTime)
        {
            // Adds time that has passed since last update.
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculates the curent index.
            currentIndex = (int)(timeElapsed * fps);

            sprite = sprites[currentIndex];

            // Check if we need to restart the animation
            if (currentIndex >= sprites.Length)
            {
                // Resets the animation
                timeElapsed = 0;
                currentIndex = 0;
            }
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
            set { }
        }

        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)transform.position.X - (int)(origin.X * transform.scale),
                    (int)transform.position.Y - (int)(origin.Y  *transform.scale),
                    (int)(sprite.Width * transform.scale),
                    (int)(sprite.Height * transform.scale)
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
