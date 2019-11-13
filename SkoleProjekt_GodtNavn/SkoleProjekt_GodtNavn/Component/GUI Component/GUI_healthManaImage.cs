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
    public class GUI_healthManaImage : GUI_Component
    {
        #region Fields

        private Texture2D sprite;
        public Color defaultColor = Color.White;
        private Vector2 origin { get; set; }
        #endregion



        #region Properties
        public bool trueHealth_falseMana;
        public float layerDepth;
        public Texture2D Sprite { get { return sprite; } set { sprite = value; } }
        public Vector2 Position { get; set; }
        public GUI_OriginPosition Origin { get; set; }
        public Vector2 Scale { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (sprite.Width) / 2 ,
                    (sprite.Height) / 2,
                    sprite.Width / 2,
                    (int)(sprite.Height / 2 * (trueHealth_falseMana == true ? (Gameworld.Player.health.currentValue / Gameworld.Player.health.maxValue) : (Gameworld.Player.mana.currentValue / Gameworld.Player.mana.maxValue)))
                    );
            }
        }

        #endregion

        #region Methods

        public GUI_healthManaImage(Texture2D sprite)
        {
            this.sprite = sprite;
        }
        public GUI_healthManaImage(Texture2D sprite, Color defaultColor)
        {
            this.sprite = sprite;
            this.defaultColor = defaultColor;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ShowGUI == true)
            {
                spriteBatch.Draw(sprite, Position, Rectangle, defaultColor, 9.425f, origin, Scale, SpriteEffects.None, layerDepth);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }


        public void SetOrigin()
        {
            // --- Top ---

            // top left
            if (GUI_OriginPosition.TopLeft == Origin)
            {
                origin = new Vector2(0, 0);
            }
            // top mid
            if (GUI_OriginPosition.TopMid == Origin)
            {
                origin = new Vector2(sprite.Width / 2, 0);
            }
            // top rigth
            if (GUI_OriginPosition.TopRigth == Origin)
            {
                origin = new Vector2(sprite.Width, 0);
            }

            // --- Mid ---

            // mid left
            if (GUI_OriginPosition.MidLeft == Origin)
            {
                origin = new Vector2(0, sprite.Height / 2);
            }
            // mid 
            if (GUI_OriginPosition.Mid == Origin)
            {
                origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            }
            // mid rigth
            if (GUI_OriginPosition.MidRigth == Origin)
            {
                origin = new Vector2(sprite.Width, sprite.Height / 2);
            }

            // --- Bottom ---

            // bottom left
            if (GUI_OriginPosition.BottomLeft == Origin)
            {
                origin = new Vector2(0, sprite.Height);
            }
            // bottom mid
            if (GUI_OriginPosition.BottomMid == Origin)
            {
                origin = new Vector2(sprite.Width / 2, sprite.Height);
            }
            // bottom rigth
            if (GUI_OriginPosition.BottomRigth == Origin)
            {
                origin = new Vector2(sprite.Width, sprite.Height);
            }
        }

        #endregion
    }
}
