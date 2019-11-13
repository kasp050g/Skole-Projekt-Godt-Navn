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
            this.color = bloddColor;
        }

        public override void Initialize()
        {
            base.Initialize();
            sprite = Gameworld.spriteContainer.spriteSheet["bloodBoom"];
            transform.scale = 0.3f;
            layerDepth = 1f;
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

                    (sprite.Width + 0) / 4 * (currentIndex - (4 * currentRow)),
                    (sprite.Height + 0) / 4 * currentRow,
                    sprite.Width / 4,
                    sprite.Height / 4
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

        public override void Animate(GameTime gameTime)
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
            transform.position = myPosition.transform.position + new Vector2(-20,-20);
        }
    }
}
