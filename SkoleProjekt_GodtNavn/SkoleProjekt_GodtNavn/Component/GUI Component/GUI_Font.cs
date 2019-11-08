using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkoleProjekt_GodtNavn
{
    class GUI_Font : GUI_Component
    {
        #region Fields

        private SpriteFont font;
        private Color fontColor = Color.Black;

        #endregion



        #region Properties

        public float layerDepth;
        public Vector2 Position { get; set; }
        public Vector2 FontScale { get; set; }

        public string Text { get; set; }

        #endregion

        #region Methods

        public GUI_Font(SpriteFont font)
        {
            this.font = font;
        }
        public GUI_Font(SpriteFont font, Color fontColor)
        {
            this.font = font;
            this.fontColor = fontColor;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ShowGUI == true)
            {
                //spriteBatch.DrawString(font, Text, Position, fontColor);
                spriteBatch.DrawString(font, Text, Position, fontColor, 0f, Vector2.Zero, FontScale, SpriteEffects.None, layerDepth);
                //spriteBatch.DrawString(font, Text, new Vector2(0, 0), fontColor);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }
        #endregion
    }
}
