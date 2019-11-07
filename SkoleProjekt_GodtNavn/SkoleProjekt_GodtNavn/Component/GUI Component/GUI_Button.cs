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
    public class GUI_Button : GUI_Component
    {
        #region Fields

        private MouseState currentMouse;
        private MouseState previousMouse;
        private bool isHovering;

        private SpriteFont font;
        private Texture2D sprite;
        private Color hoverColor = Color.Gray;
        private Color defaultColor = Color.White;
        private Color fontColor = Color.Black;

        #endregion



        #region Properties

        public float layerDepth;
        public event EventHandler Click;
        public GUI_OriginPosition Origin { get; set; }
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }
        public float FontScale { get; set; }
        public float ButtonScale { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    sprite.Width,
                    sprite.Height);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public GUI_Button(Texture2D sprite, SpriteFont font)
        {
            this.sprite = sprite;
            this.font = font;
        }
        public GUI_Button(Texture2D sprite, SpriteFont font, Color fontColor, Color defaultColor, Color hoverColor)
        {
            this.sprite = sprite;
            this.font = font;
            this.fontColor = fontColor;
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

                spriteBatch.Draw(sprite, Position, null, colour, 0f, Vector2.Zero, ButtonScale, SpriteEffects.None, layerDepth);
                //spriteBatch.Draw(sprite, Rectangle, colour);
                //spriteBatch.Draw(sprite, Position, Rectangle, colour, 0, new Vector2(0, 0), 1, SpriteEffects.None, layerDepth);

                if (!string.IsNullOrEmpty(Text))
                {
                    var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2) * FontScale;
                    var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2) * FontScale;

                    //spriteBatch.DrawString(font, Text, new Vector2(x, y), fontColor);
                    spriteBatch.DrawString(font, Text, new Vector2(x, y), fontColor, 0f, Vector2.Zero, FontScale, SpriteEffects.None, layerDepth);
                    //spriteBatch.DrawString(font, Text, new Vector2(x, y), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, layerDepth + 0.001f);
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

                    if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        Click?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        #endregion
    }
}
