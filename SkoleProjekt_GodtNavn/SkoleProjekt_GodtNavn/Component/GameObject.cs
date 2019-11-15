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
        /// <summary>
        /// GameObject's: Position, rotation and scale.
        /// </summary>
        public Transform Transform { get; set; } = new Transform();

        /// <summary>
        /// GameObject's sprite.
        /// </summary>
        public Texture2D Sprite { get; set; }

        /// <summary>
        /// The layer the GameObject is drawed on.
        /// </summary>
        public float LayerDepth { get; set; } = 0;

        /// <summary>
        /// GameObject's color.
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The GameObject's sprite position offset.
        /// </summary>
        public Vector2 SpritePositionOffset { get; set; } = new Vector2(0, 0);

        /// <summary>
        /// The screen size of the game.
        /// </summary>
        protected Vector2 screenSize;

        /// <summary>
        /// GameObject's velocity.
        /// </summary>
        protected Vector2 velocity;

        /// <summary>
        /// GameObject's origin vector.
        /// </summary>
        protected Vector2 origin;


        /// <summary>
        /// What way the GameObject is facing. Options: Up, Down, Left, Right
        /// </summary>
        protected Facing facing = Facing.Down;

        #region Animate Value's
        public Texture2D[] sprites;
        protected float fps = 10;
        protected float timeElapsed;
        protected int currentIndex;
        #endregion

        /// <summary>
        /// The object executes what's listed in awake, when it's spawned. This only gets executed once.
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// Object on initialize.
        /// </summary>
        public virtual void Initialize()
        {
            screenSize = Gameworld.ScreenSize;
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Loads content for a certain object.
        /// </summary>
        /// <param name="content"></param>
        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// Draws an object.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Transform.Position + SpritePositionOffset, rectangle, Color, Transform.Rotation, origin, Transform.Scale, SpriteEffects.None, LayerDepth);
        }

        /// <summary>
        /// Animates an object.
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void Animate(GameTime gameTime)
        {
            // Adds time that has passed since last update.
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculates the curent index.
            currentIndex = (int)(timeElapsed * fps);

            Sprite = sprites[currentIndex];

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
                    Sprite.Width,
                    Sprite.Height
                    );
            }
            set { }
        }

        /// <summary>
        /// Creates a collision box, aka bounding box.
        /// </summary>
        public virtual Rectangle CollisionBox
        {
            get
            {
                // returns a new rectangle based on the position, scale, sprite width and height.
                return new Rectangle(
                    (int)Transform.Position.X - (int)(origin.X * Transform.Scale),
                    (int)Transform.Position.Y - (int)(origin.Y  * Transform.Scale),
                    (int)(Sprite.Width * Transform.Scale),
                    (int)(Sprite.Height * Transform.Scale)
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
