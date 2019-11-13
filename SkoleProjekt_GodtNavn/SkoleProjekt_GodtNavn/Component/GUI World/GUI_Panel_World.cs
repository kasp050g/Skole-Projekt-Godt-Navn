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
    class GUI_Panel_World : GameObject
    {
        #region Fields

        private Texture2D sprite;
        private Color defaultColor = Color.White;
        private Vector2 origin { get; set; }
        public bool ShowGUI { get; set; }
        #endregion



        #region Properties

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
                    (int)Position.X,
                    (int)Position.Y,
                    sprite.Width,
                    sprite.Height);
            }
        }

        #endregion

        #region Methods

        public GUI_Panel_World(Texture2D sprite)
        {
            this.sprite = sprite;
        }
        public GUI_Panel_World(Texture2D sprite, Color defaultColor)
        {
            this.sprite = sprite;
            this.defaultColor = defaultColor;
        }

        public override void Draw( SpriteBatch spriteBatch)
        {
            if (ShowGUI == true)
            {
                spriteBatch.Draw(sprite, Position, null, Color.White, 0f, origin, Scale, SpriteEffects.None, layerDepth);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    0,0,0,0
                    );
            }
        }

        public void SetOrigin()
        {
            // --- Top ---

            // top left
            if(GUI_OriginPosition.TopLeft == Origin)
            {
                origin = new Vector2(0, 0);
            }
            // top mid
            if (GUI_OriginPosition.TopMid == Origin)
            {
                origin = new Vector2(sprite.Width/2, 0);
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

        public override void LoadContent(ContentManager content)
        {
           
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        #endregion
    }
}
