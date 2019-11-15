using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SkoleProjekt_GodtNavn
{
    public class Goblin : Enemy
    {
        public AnimationContainer_Sheet animationContainer_Sheet_Walk = new AnimationContainer_Sheet()
        {
            stopAtEnd = false,

            start_Up = 0,
            end_Up = 6,
            row_Up = 2,

            start_Down = 0,
            end_Down = 6,
            row_Down = 0,

            start_Left = 0,
            end_Left = 6,
            row_Left = 3,

            start_Right = 0,
            end_Right = 6,
            row_Right = 1,
        };
        public AnimationContainer_Sheet animationContainer_Sheet_Attack = new AnimationContainer_Sheet()
        {
            stopAtEnd = true,

            spriteHeightOffset = 0,
            spriteWidthOffset = 0,

            start_Up = 6,
            end_Up = 10,
            row_Up = 2,

            start_Down = 6,
            end_Down = 10,
            row_Down = 0,

            start_Left = 6,
            end_Left = 10,
            row_Left = 3,

            start_Right = 6,
            end_Right = 10,
            row_Right = 1,
        };
        public AnimationContainer_Sheet animationContainer_Sheet_Death = new AnimationContainer_Sheet()
        {
            stopAtEnd = true,

            spriteHeightOffset = 0,
            spriteWidthOffset = 0,

            start_Up = 0,
            end_Up = 4,
            row_Up = 4,

            start_Down = 0,
            end_Down = 4,
            row_Down = 4,

            start_Left = 0,
            end_Left = 4,
            row_Left = 4,

            start_Right = 0,
            end_Right = 4,
            row_Right = 4,
        };

        public DoAnimation_sheet doAnimation = new DoAnimation_sheet(5);
        public bool doDeathAnimationOneTime = true;
        public Goblin()
        {


        }
        public Goblin(int _health,Color _color,float _scale)
        {
            Health.maxValue = _health;
            Health.currentValue = _health;
            Color = _color;
            Transform.Scale = _scale;
        }


        public override void Initialize()
        {
            base.Initialize();
            Sprite = Gameworld.spriteContainer.spriteSheet["Enemy_Goblin_Sheet"];
            doAnimation.SetAnimation(animationContainer_Sheet_Walk, facing);
            LayerDepth = 0.2f;
            speed = 120f;
            Transform.Scale = 2f;
            bloodColor = Color.CornflowerBlue;
        }

        public override void Update(GameTime gameTime)
        {

            if (IsAlive)
            {
                base.Update(gameTime);
                AnimationState();
                isAttack = doAnimation.Animate(gameTime, facing);

            }
            else if(doDeathAnimationOneTime)
            {
                AnimationState();
                isAttack = doAnimation.Animate(gameTime, facing);
                doDeathAnimationOneTime = doAnimation.Animate(gameTime, facing);
            }

            if (deleteOnDeath == true && IsAlive == false)
            {
                deleteTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deleteTimer < 0)
                {
                    Gameworld.Destroy(this);
                }
            }
        }

        public void AnimationState()
        {
            if (IsAlive)
            {
                if (isAttack == true)
                {
                    doAnimation.SetAnimation(animationContainer_Sheet_Attack, facing);
                }
                else
                {
                    doAnimation.SetAnimation(animationContainer_Sheet_Walk, facing);
                }
            }
            else
            {
                 doAnimation.SetAnimation(animationContainer_Sheet_Death, facing);
            }

        }

        public override Rectangle rectangle
        {
            get
            {
                return new Rectangle(

                    (Sprite.Width + 20) / 11 * doAnimation.currentIndex,
                    (Sprite.Height + 2) / 5 * doAnimation.currentRow,
                    Sprite.Width / 11,
                    Sprite.Height / 5
                    );
            }
        }
        public void NewOriginPoint()
        {
            int Xposition = (Sprite.Width / 11 / 2) + doAnimation.animationContainer.spriteWidthOffset;
            int Yposition = (Sprite.Height / 5 / 2) + doAnimation.animationContainer.spriteHeightOffset;
            origin = new Vector2(Xposition, Yposition);
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)Transform.Position.X - (int)(origin.X * Transform.Scale) + 30,
                    (int)Transform.Position.Y - (int)(origin.Y * Transform.Scale) + 10,
                    (int)(Sprite.Width / 11 * Transform.Scale - 60),
                    (int)(Sprite.Height / 5 * Transform.Scale - 20)
                    );
            }
        }

        public override void Death(Player player)
        {
            base.Death(player);
            Gameworld.audioPlayer.SoundEffect_Play("SoundEffect_Goblin", 0.1f);
        }

        public override void Awake()
        {
        }
    }
}
