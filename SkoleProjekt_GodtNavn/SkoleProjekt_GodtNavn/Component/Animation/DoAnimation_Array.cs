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

        private Texture2D[] currentSprite;
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
                        currentSprite = animationContainer.up.ToArray();
                        break;
                    case Facing.Down:
                        currentSprite = animationContainer.down.ToArray();
                        break;
                    case Facing.Left:
                        currentSprite = animationContainer.left.ToArray();
                        break;
                    case Facing.Rigth:
                        currentSprite = animationContainer.rigth.ToArray();
                        break;
                    default:
                        break;
                }
            }
        }
        public Texture2D Animate(GameTime gameTime, Facing facing)
        {
            FacingRightWay(facing);

            // Adds time that has passed since last update.
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Calculates the curent index.
            currentIndex = (int)(timeElapsed * fps);

            // Check if we need to restart the animation
            if (currentIndex >= currentSprite.Length -1)
            {
                // Resets the animation
                timeElapsed = 0;
                currentIndex = 0;
            }

            return currentSprite[currentIndex];
        }

        public void FacingRightWay(Facing facing)
        {
            if (currentFacing != facing)
            {
                currentFacing = facing;
                switch (facing)
                {
                    case Facing.Up:
                        currentSprite = animationContainer.up.ToArray();
                        break;
                    case Facing.Down:
                        currentSprite = animationContainer.down.ToArray();
                        break;
                    case Facing.Left:
                        currentSprite = animationContainer.left.ToArray();
                        break;
                    case Facing.Rigth:
                        currentSprite = animationContainer.rigth.ToArray();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
