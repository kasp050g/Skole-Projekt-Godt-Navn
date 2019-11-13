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
    public class GUI_ShowItemInfo : GUI_Component
    {

        private MouseState currentMouse;
        private MouseState previousMouse;
        private bool isHovering;

        private SpriteFont font;
        private Texture2D sprite;




        private Vector2 origin { get; set; }
        public GUI_OriginPosition Origin { get; set; }
        public float layerDepth = 0.91f;
        public Vector2 Position = new Vector2(0, 0);
        public Vector2 OffSetPositin = new Vector2(0, 0);
        public Vector2 Scale { get; set; }
        public Item item { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(sprite.Width * Scale.X),
                    (int)(sprite.Height * Scale.Y));
            }
        }


        GUI_Font showInfoText = new GUI_Font()
        {
            Position = new Vector2(0, 0),
            FontScale = new Vector2(0.5f, 0.5f),
            layerDepth = 0.93f,
            fontColor = Color.CadetBlue,
           
        };

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ShowGUI == true)
            {

                if (font == null)
                {
                    font = Gameworld.spriteContainer.spriteFont;
                }
                if (sprite == null)
                {
                    sprite = Gameworld.spriteContainer.soleSprite["frame_1"];
                    SetOrigin();
                }
                else
                {
                    spriteBatch.Draw(sprite, Position, null, Color.White, 0f, origin, Scale, SpriteEffects.None, layerDepth);
                    if (showInfoText.Font == null)
                    {
                        showInfoText.Font = font;
                    }
                    showInfoText.Draw(gameTime, spriteBatch);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            showInfoText.ShowGUI = ShowGUI;
            if (ShowGUI == true)
            {
                showInfoText.Position = new Vector2(Position.X + 20 + OffSetPositin.X, Position.Y + 10 + OffSetPositin.Y);

                if (item != null)
                {
                    MakeTextToShow();
                }
            }
        }

        public void MakeTextToShow()
        {
            string tmp = ""; 

            tmp += $"{item.name} \n";
     
            if (item.itemType == ItemType.WeaponMelee || item.itemType == ItemType.WeaponRange)
            {
                tmp += $"Weapon Damage: {item.weaponDamage} \n";
            }
            else if (item.itemType != ItemType.Consumable)
            {
                tmp += $"Armor: {item.armor} \n";
            }

            if (item.health > 0)
            {
                tmp += $"Health: {item.health} \n";
            }
            if (item.mana > 0)
            {
                tmp += $"Mana: {item.mana} \n";
            }

            if (item.itemType != ItemType.Consumable)
            {
                if (item.strength > 0)
                {
                    tmp += $"Strength: {item.strength} \n";
                }
                if (item.agility > 0)
                {
                    tmp += $"Agility: {item.agility} \n";
                }
                if (item.intelligence > 0)
                {
                    tmp += $"Intelligence: {item.intelligence} \n";
                }
            }
            if (item.goldValue > 0)
            {
                tmp += $"Gold Value: {item.goldValue} \n";
            }

            showInfoText.Text = tmp;
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
    }
}
