using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace SkoleProjekt_GodtNavn
{
    public class DoAnimation_Array
    {
        public Facing currentFacing;

        float fps = 10;
        float timeElapsed;
        public int currentIndex;

        public AnimationContainer_Array animationContainer;

        private Texture2D[] currentSprites;
        public Texture2D currentSprite;

        private bool runOnetime = false;
        public bool isDead = false;
        //public AnimationContainer_Sheet lastAnimationContainer;

        public DoAnimation_Array(float fps)
        {
            this.fps = fps;
        }

        public void SetAnimation(AnimationContainer_Array animationContainer, Facing facing)
        {
            if (this.animationContainer != animationContainer)
            {
                if (this.animationContainer != null)
                {
                    //lastAnimationContainer = this.animationContainer;
                }

                this.animationContainer = animationContainer;
                switch (facing)
                {
                    case Facing.Up:
                        currentSprites = animationContainer.up.ToArray();
                        break;
                    case Facing.Down:
                        currentSprites = animationContainer.down.ToArray();
                        break;
                    case Facing.Left:
                        currentSprites = animationContainer.left.ToArray();
                        break;
                    case Facing.Rigth:
                        currentSprites = animationContainer.rigth.ToArray();
                        break;
                    default:
                        break;
                }
                runOnetime = animationContainer.stopAtEnd;
                timeElapsed = 0;
                currentIndex = 0;
            }
        }
        public bool Animate(GameTime gameTime, Facing facing)
        {
            FacingRightWay(facing);

            // Adds time that has passed since last update.
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculates the curent index.
            currentIndex = (int)(timeElapsed * fps);

            // Check if we need to restart the animation
            if (currentIndex >= currentSprites.Length - 1 && !isDead)
            {
                // Resets the animation
                timeElapsed = 0;
                currentIndex = 0;

                return false;
            }

            if (!isDead)
            {
                currentSprite = currentSprites[currentIndex];
            }
            else if (currentIndex < currentSprites.Length - 1)
            {
                currentSprite = currentSprites[currentIndex];
            }
            else
            {
                currentSprite = currentSprites[currentSprites.Length - 1];
            }

            if (runOnetime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FacingRightWay(Facing facing)
        {
            if (currentFacing != facing)
            {
                currentFacing = facing;
                switch (facing)
                {
                    case Facing.Up:
                        currentSprites = animationContainer.up.ToArray();
                        break;
                    case Facing.Down:
                        currentSprites = animationContainer.down.ToArray();
                        break;
                    case Facing.Left:
                        currentSprites = animationContainer.left.ToArray();
                        break;
                    case Facing.Rigth:
                        currentSprites = animationContainer.rigth.ToArray();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
