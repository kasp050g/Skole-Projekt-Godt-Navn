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
    public class GUI_EquipButton : GUI_Component
    {
        #region Fields

        private MouseState currentMouse;
        private MouseState previousMouse;
        private bool isHovering;

        private Texture2D sprite_NoEquip;
        private Texture2D sprite_GotEquip;

        private Color hoverColor = Color.LightGray;
        private Color defaultColor = Color.White;

        #endregion



        #region Properties

        public float layerDepth;
        public event EventHandler Click;
        public GUI_OriginPosition Origin { get; set; }
        public bool Clicked { get; private set; }
        public bool IsEquip { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(sprite_NoEquip.Width * Scale.X),
                    (int)(sprite_NoEquip.Height * Scale.Y));
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public GUI_EquipButton(Texture2D sprite, Texture2D itemSprite)
        {
            this.sprite_NoEquip = sprite;
            this.sprite_GotEquip = itemSprite;
        }

        public GUI_EquipButton(Texture2D sprite,Texture2D itemSprite, Color defaultColor, Color hoverColor)
        {
            this.sprite_NoEquip = sprite;
            this.sprite_GotEquip = itemSprite;
            this.defaultColor = defaultColor;
            this.hoverColor = hoverColor;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ShowGUI == true)
            {
                var colour = defaultColor;

                if (isHovering)
                {
                    colour = hoverColor;
                }

                if (IsEquip == true)
                {
                    spriteBatch.Draw(sprite_GotEquip, Position, null, colour, 0f, Vector2.Zero, Scale, SpriteEffects.None, layerDepth);
                }
                else
                {
                    spriteBatch.Draw(sprite_NoEquip, Position, null, colour, 0f, Vector2.Zero, Scale, SpriteEffects.None, layerDepth);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ShowGUI == true)
            {
                previousMouse = currentMouse;
                currentMouse = Mouse.GetState();

                var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

                isHovering = false;

                if (mouseRectangle.Intersects(Rectangle))
                {
                    isHovering = true;

                    if (currentMouse.RightButton == ButtonState.Released && previousMouse.RightButton == ButtonState.Pressed)
                    {
                        Click?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        #endregion
    }

}
