using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{
    public class DoAnimation_sheet
    {
        public int currentEnd;
        public int currentRow;
        public int currentStart;

        public Facing currentFacing;

        float fps = 10;
        float timerElapsed;
        public int currentIndex;
        private bool runOnetime = false;

        public AnimationContainer_Sheet animationContainer;
        public AnimationContainer_Sheet lastAnimationContainer;

        public DoAnimation_sheet(float fps)
        {
            this.fps = fps;
        }

        public void SetAnimation(AnimationContainer_Sheet animationContainer_Sheet, Facing facing)
        {
            if (this.animationContainer != animationContainer_Sheet)
            {
                if (this.animationContainer != null)
                {
                    //lastAnimationContainer = this.animationContainer;
                }
                this.animationContainer = animationContainer_Sheet;
                runOnetime = animationContainer.stopAtEnd;
                switch (facing)
                {
                    case Facing.Up:
                        timerElapsed = animationContainer_Sheet.start_Up / fps;
                        currentStart = animationContainer_Sheet.start_Up;
                        currentRow = animationContainer_Sheet.row_Up;
                        currentEnd = animationContainer_Sheet.end_Up;
                        break;
                    case Facing.Down:
                        timerElapsed = animationContainer_Sheet.start_Down / fps;
                        currentStart = animationContainer_Sheet.start_Down;
                        currentRow = animationContainer_Sheet.row_Down;
                        currentEnd = animationContainer_Sheet.end_Down;
                        break;
                    case Facing.Left:
                        timerElapsed = animationContainer_Sheet.start_Left / fps;
                        currentStart = animationContainer_Sheet.start_Left;
                        currentRow = animationContainer_Sheet.row_Left;
                        currentEnd = animationContainer_Sheet.end_Left;
                        break;
                    case Facing.Rigth:
                        timerElapsed = animationContainer_Sheet.start_Right / fps;
                        currentStart = animationContainer_Sheet.start_Right;
                        currentRow = animationContainer_Sheet.row_Right;
                        currentEnd = animationContainer_Sheet.end_Right;
                        break;
                    default:
                        break;
                }
            }
        }
        public bool Animate(GameTime gameTime, Facing facing)
        {
            FacingRightWay(facing);

            timerElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentIndex = (int)(timerElapsed * fps);

            if (currentIndex >= currentEnd)
            {
                if (runOnetime)
                {
                    currentIndex = currentEnd;
                    return false;
                }
                timerElapsed = currentStart / fps;
                currentIndex = currentStart;

                return false;
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
                        timerElapsed = animationContainer.start_Up / fps;
                        currentStart = animationContainer.start_Up;
                        currentRow = animationContainer.row_Up;
                        currentEnd = animationContainer.end_Up;
                        break;
                    case Facing.Down:
                        timerElapsed = animationContainer.start_Down / fps;
                        currentStart = animationContainer.start_Down;
                        currentRow = animationContainer.row_Down;
                        currentEnd = animationContainer.end_Down;
                        break;
                    case Facing.Left:
                        timerElapsed = animationContainer.start_Left / fps;
                        currentStart = animationContainer.start_Left;
                        currentRow = animationContainer.row_Left;
                        currentEnd = animationContainer.end_Left;
                        break;
                    case Facing.Rigth:
                        timerElapsed = animationContainer.start_Right / fps;
                        currentStart = animationContainer.start_Right;
                        currentRow = animationContainer.row_Right;
                        currentEnd = animationContainer.end_Right;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
