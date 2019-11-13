using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SkoleProjekt_GodtNavn
{
    class GUI_Font_World : GameObject
    {
        #region Fields

        private SpriteFont font;
        public Color fontColor = Color.Black;
        public bool ShowGUI { get; set; }
        #endregion



        #region Properties
        public SpriteFont Font { get { return font; } set { font = value; } }
        public float layerDepth;
        public Vector2 Position { get; set; }
        public Vector2 FontScale { get; set; }

        public string Text { get; set; }

        #endregion

        #region Methods

        public GUI_Font_World()
        {

        }
        public GUI_Font_World(SpriteFont font)
        {
            this.font = font;
        }
        public GUI_Font_World(SpriteFont font, Color fontColor)
        {
            this.font = font;
            this.fontColor = fontColor;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ShowGUI == true)
            {
                //spriteBatch.DrawString(font, Text, Position, fontColor);
                spriteBatch.DrawString(font, Text, Position, fontColor, 0f, Vector2.Zero, FontScale, SpriteEffects.None, layerDepth);
                //spriteBatch.DrawString(font, Text, new Vector2(0, 0), fontColor);
            }
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    0, 0, 0, 0
                    );
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void OnCollision(GameObject other)
        {
           
        }
        #endregion
    }
}
