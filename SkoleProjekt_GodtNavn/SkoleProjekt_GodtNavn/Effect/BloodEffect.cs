using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SkoleProjekt_GodtNavn
{
    public class BloodEffect : GameObject
    {
        private GameObject myPosition;
        private int currentRow;
        
        public BloodEffect(GameObject myPosition,Color bloddColor)
        {
            this.myPosition = myPosition;
            this.Color = bloddColor;
        }

        public override void Initialize()
        {
            base.Initialize();
            Sprite = Gameworld.spriteContainer.spriteSheet["bloodBoom"];
            Transform.Scale = 0.3f;
            LayerDepth = 1f;
            fps = 20f;
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            Animate(gameTime);
            UpdatePosition();            
        }

        public override Rectangle rectangle
        {
            get
            {
                return new Rectangle(

                    (Sprite.Width + 0) / 4 * (currentIndex - (4 * currentRow)),
                    (Sprite.Height + 0) / 4 * currentRow,
                    Sprite.Width / 4,
                    Sprite.Height / 4
                    );
            }
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    0,
                    0,
                    0,
                    0
                    );
            }
        }

        protected override void Animate(GameTime gameTime)
        {
            // Adds time that has passed since last update.
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculates the curent index.
            currentIndex = (int)(timeElapsed * fps);

            if(currentIndex >= 4)
            {
                currentRow = 1;
            }
            if(currentIndex >= 8)
            {
                currentRow = 2;
            }
            if (currentIndex >= 12)
            {
                currentRow = 3;
            }


            //sprite = sprites[currentIndex- (4* currentRow)];

            // Check if we need to restart the animation
            if (currentIndex >= 15)
            {
                // Resets the animation
                Gameworld.Destroy(this);
                timeElapsed = 0;
                currentIndex = 0;
                currentRow = 0;

            }
        }

        private void UpdatePosition()
        {
            Transform.Position = myPosition.Transform.Position + new Vector2(-20,-20);
        }

        public override void Awake()
        {
        }
    }
}
