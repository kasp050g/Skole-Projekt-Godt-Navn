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
    public class Golem : Enemy
    {
        public AnimationContainer_Sheet animationContainer_Sheet_Walk = new AnimationContainer_Sheet()
        {
            stopAtEnd = false,

            start_Up = 0,
            end_Up = 6,
            row_Up = 0,

            start_Down = 0,
            end_Down = 6,
            row_Down = 2,

            start_Left = 0,
            end_Left = 6,
            row_Left = 1,

            start_Right = 0,
            end_Right = 6,
            row_Right = 3,
        };
        public AnimationContainer_Sheet animationContainer_Sheet_Attack = new AnimationContainer_Sheet()
        {
            stopAtEnd = true,

            spriteHeightOffset = 0,
            spriteWidthOffset = 0,

            start_Up = 0,
            end_Up = 6,
            row_Up = 0,

            start_Down = 0,
            end_Down = 6,
            row_Down = 2,

            start_Left = 0,
            end_Left = 6,
            row_Left = 1,

            start_Right = 0,
            end_Right = 6,
            row_Right = 3,
        };
        public AnimationContainer_Sheet animationContainer_Sheet_Death = new AnimationContainer_Sheet()
        {
            stopAtEnd = true,

            spriteHeightOffset = 0,
            spriteWidthOffset = 0,

            start_Up = 0,
            end_Up = 6,
            row_Up = 0,

            start_Down = 0,
            end_Down = 6,
            row_Down = 0,

            start_Left = 0,
            end_Left = 6,
            row_Left = 0,

            start_Right = 0,
            end_Right = 6,
            row_Right = 0,
        };

        public Texture2D golem_Walk;
        public Texture2D golem_Attack;
        public Texture2D golem_Death;

        public DoAnimation_sheet doAnimation = new DoAnimation_sheet(5);
        public bool doDeathAnimationOneTime = true;
        public Golem()
        {

           
        }
        public Golem(int _health, Color _color, float _scale)
        {
            Health.maxValue = _health;
            Health.currentValue = _health;
            Color = _color;
            Transform.Scale = _scale;
        }


        public override void Initialize()
        {
            base.Initialize();
            golem_Walk = Gameworld.spriteContainer.spriteSheet["Enemy_Golem_Walk_Sheet"];
            golem_Attack = Gameworld.spriteContainer.spriteSheet["Enemy_Golem_Attack_Sheet"];
            golem_Death = Gameworld.spriteContainer.spriteSheet["Enemy_Golem_Die_Sheet"];

            SpritePositionOffset = new Vector2(0, -70);

            Sprite = golem_Walk;

            doAnimation.SetAnimation(animationContainer_Sheet_Walk, facing);
            LayerDepth = 0.2f;
            speed = 120f;

            bloodColor = Color.CornflowerBlue;
            origin = new Vector2(golem_Walk.Width / 7 / 2, golem_Walk.Height / 5 / 2);
        }

        public override void Update(GameTime gameTime)
        {

            if (IsAlive)
            {
                base.Update(gameTime);
                AnimationState();
                isAttack = doAnimation.Animate(gameTime, facing);

            }
            else if (doDeathAnimationOneTime)
            {
                AnimationState();
                isAttack = doAnimation.Animate(gameTime, facing);
                doDeathAnimationOneTime = doAnimation.Animate(gameTime, facing);
            }
            SetImagePosition();

            if (deleteOnDeath == true && IsAlive == false)
            {
                deleteTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (deleteTimer < 0)
                {
                    Gameworld.Destroy(this);
                }
            }
        }

        public void SetImagePosition()
        {
            switch (facing)
            {
                case Facing.Up:
                    SpritePositionOffset = new Vector2(-20, -70);
                    break;
                case Facing.Down:
                    SpritePositionOffset = new Vector2(-20, -70);
                    break;
                case Facing.Left:
                    SpritePositionOffset = new Vector2(-30, -70);
                    break;
                case Facing.Right:
                    SpritePositionOffset = new Vector2(10, -70);
                    break;
                default:
                    break;
            }
        }

        public void AnimationState()
        {
            if (IsAlive)
            {
                if (isAttack == true)
                {
                    Sprite = golem_Attack;
                    doAnimation.SetAnimation(animationContainer_Sheet_Attack, facing);
                }
                else
                {
                    Sprite = golem_Walk;
                    doAnimation.SetAnimation(animationContainer_Sheet_Walk, facing);
                }
            }
            else
            {
                Sprite = golem_Death;
                doAnimation.SetAnimation(animationContainer_Sheet_Death, facing);
            }

        }

        public override Rectangle rectangle
        {
            get
            {
                return new Rectangle(

                    (Sprite.Width + 0) / 7 * doAnimation.currentIndex,
                    (Sprite.Height + 0) / 4 * doAnimation.currentRow,
                    Sprite.Width / 7,
                    Sprite.Height / 4
                    );
            }
        }
        public void NewOriginPoint()
        {
            int Xposition = (Sprite.Width / 7 / 2) + doAnimation.animationContainer.spriteWidthOffset;
            int Yposition = (Sprite.Height / 4 / 2) + doAnimation.animationContainer.spriteHeightOffset;
            origin = new Vector2(Xposition, Yposition);
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)Transform.Position.X - (int)(origin.X * Transform.Scale) + (int)(15 * Transform.Scale),
                    (int)Transform.Position.Y - (int)(origin.Y * Transform.Scale) + (int)(16.25f * Transform.Scale),
                    (int)(Sprite.Width / 7 * Transform.Scale - (40 * Transform.Scale)),
                    (int)(Sprite.Height / 4 * Transform.Scale - (37.5f * Transform.Scale))
                    );
            }
        }

        public override void Death(Player player)
        {
            base.Death(player);
            Gameworld.audioPlayer.SoundEffect_Play("golem_death", 0.1f);
        }

        public override void Awake()
        {;
        }
    }
}
