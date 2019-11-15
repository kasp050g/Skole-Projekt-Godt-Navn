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
    public class GUI_Spell_Bar : GUI_Component
    {
        Texture2D sprite_SpellBar;

        Texture2D sprite_Icon_Fire;
        Texture2D sprite_Icon_Ice;
        Texture2D sprite_Icon_Lightning;

        Texture2D sprite_Icon_Sword;
        Texture2D sprite_Icon_HealthPotion;

        Texture2D sprite_XP_Bar_01;
        Texture2D sprite_XP_Bar_02 ;
        Texture2D sprite_XP_Bar_03 ;

        SpriteFont spriteFont;

        List<GUI_Component> gui_Components = new List<GUI_Component>();

        GUI_Panel ui_SpellBar;

        GUI_Panel ui_Icon_Fire;
        GUI_Panel ui_Icon_Ice;
        GUI_Panel ui_Icon_Lightning;

        GUI_Panel ui_Icon_Sword;
        GUI_Panel ui_Icon_HealthPotion;

        GUI_Panel ui_XP_Bar_01;
        GUI_Panel ui_XP_Bar_02;
        GUI_Panel ui_XP_Bar_03;


        GUI_Font health_font;
        GUI_Font icon_Fire_font;
        GUI_Font icon_Ice_font;
        GUI_Font icon_Lightning_font;
        GUI_Font icon_Sword_font;


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public void SetUp()
        {
            #region Image set up
            sprite_SpellBar = Gameworld.spriteContainer.soleSprite["text_frame_1"];

            sprite_Icon_Fire = Gameworld.spriteContainer.soleSprite["fire_1"];
            sprite_Icon_Ice = Gameworld.spriteContainer.soleSprite["freeze_1"];
            sprite_Icon_Lightning = Gameworld.spriteContainer.soleSprite["lightning_1"];

            sprite_Icon_Sword = Gameworld.spriteContainer.soleSprite["sword_3"];
            sprite_Icon_HealthPotion = Gameworld.spriteContainer.soleSprite["healthPotion"];

            sprite_XP_Bar_01 = Gameworld.spriteContainer.soleSprite["XP_Bar_1"];
            sprite_XP_Bar_02 = Gameworld.spriteContainer.soleSprite["XP_Bar_2"];
            sprite_XP_Bar_03 = Gameworld.spriteContainer.soleSprite["XP_Bar_3"];

            spriteFont = Gameworld.spriteContainer.spriteFont;
            #endregion

            #region Icon
            ui_XP_Bar_01 = new GUI_Panel(sprite_XP_Bar_01)
            {
                Position = new Vector2(Gameworld.ScreenSize.X / 2, Gameworld.ScreenSize.Y ),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(3.6f, 0.8f),
                layerDepth = 0.9f,
                ShowGUI = true
            };
            ui_XP_Bar_01.SetOrigin();
            gui_Components.Add(ui_XP_Bar_01);

            ui_XP_Bar_02 = new GUI_Panel(sprite_XP_Bar_02)
            {
                Position = new Vector2(Gameworld.ScreenSize.X / 2, Gameworld.ScreenSize.Y),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(3.6f, 0.8f),
                layerDepth = 0.91f,
                ShowGUI = true
            };
            ui_XP_Bar_02.SetOrigin();
            gui_Components.Add(ui_XP_Bar_02);

            ui_XP_Bar_03 = new GUI_Panel(sprite_XP_Bar_03)
            {
                Position = new Vector2(Gameworld.ScreenSize.X / 2, Gameworld.ScreenSize.Y),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(3.6f, 0.8f),
                layerDepth = 0.92f,
                ShowGUI = true
            };
            ui_XP_Bar_03.SetOrigin();
            gui_Components.Add(ui_XP_Bar_03);

            ui_SpellBar = new GUI_Panel(sprite_SpellBar)
            {
                Position = new Vector2(Gameworld.ScreenSize.X / 2, Gameworld.ScreenSize.Y - 0),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(1.5f, 1.5f),
                layerDepth = 0.7f,
                ShowGUI = true
            };
            ui_SpellBar.SetOrigin();
            gui_Components.Add(ui_SpellBar);

            // Fire Icon
            ui_Icon_Fire = new GUI_Panel(sprite_Icon_Fire)
            {
                Position = new Vector2(ui_SpellBar.Position.X - 350, ui_SpellBar.Position.Y - 40),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(1.3f, 1.3f),
                layerDepth = 0.8f,
                ShowGUI = true
            };
            ui_Icon_Fire.SetOrigin();
            gui_Components.Add(ui_Icon_Fire);

            // Ice Icon
            ui_Icon_Ice = new GUI_Panel(sprite_Icon_Ice)
            {
                Position = new Vector2(ui_SpellBar.Position.X - 230, ui_SpellBar.Position.Y - 40),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(1.3f, 1.3f),
                layerDepth = 0.8f,
                ShowGUI = true
            };
            ui_Icon_Ice.SetOrigin();
            gui_Components.Add(ui_Icon_Ice);

            // Lightning Icon
            ui_Icon_Lightning = new GUI_Panel(sprite_Icon_Lightning)
            {
                Position = new Vector2(ui_SpellBar.Position.X - 110, ui_SpellBar.Position.Y - 40),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(1.3f, 1.3f),
                layerDepth = 0.8f,
                ShowGUI = true
            };
            ui_Icon_Lightning.SetOrigin();
            gui_Components.Add(ui_Icon_Lightning);

            // Sword Icon
            ui_Icon_Sword = new GUI_Panel(sprite_Icon_Sword)
            {
                Position = new Vector2(ui_SpellBar.Position.X + 10, ui_SpellBar.Position.Y - 40),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(1.3f, 1.3f),
                layerDepth = 0.8f,
                ShowGUI = true
            };
            ui_Icon_Sword.SetOrigin();
            gui_Components.Add(ui_Icon_Sword);

            // Health Pot Icon
            ui_Icon_HealthPotion = new GUI_Panel(sprite_Icon_HealthPotion)
            {
                Position = new Vector2(ui_SpellBar.Position.X + 350, ui_SpellBar.Position.Y - 40),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(1.3f, 1.3f),
                layerDepth = 0.8f,
                color = Color.Gray,
                ShowGUI = true
            };
            ui_Icon_HealthPotion.SetOrigin();
            gui_Components.Add(ui_Icon_HealthPotion);
            #endregion


            #region Font
            // HealthPotion
            health_font = new GUI_Font(spriteFont, Color.White)
            {
                Position = new Vector2(ui_Icon_HealthPotion.Position.X +30, ui_Icon_HealthPotion.Position.Y - 30),
                Text = $"H",
                FontScale = new Vector2(0.7f, 0.7f),
                layerDepth = 0.95f,
                ShowGUI = true
            };
            gui_Components.Add(health_font);


            GUI_Font icon_Fire_font;
            GUI_Font icon_Ice_font;
            GUI_Font icon_Lightning_font;
            GUI_Font icon_Sword_font;

            // Fire_font
            icon_Fire_font = new GUI_Font(spriteFont, Color.White)
            {
                Position = new Vector2(ui_Icon_Fire.Position.X + 30, ui_Icon_Fire.Position.Y - 30),
                Text = $"E",
                FontScale = new Vector2(0.7f, 0.7f),
                layerDepth = 0.95f,
                ShowGUI = true
            };
            gui_Components.Add(icon_Fire_font);
            // Ice_font
            icon_Ice_font = new GUI_Font(spriteFont, Color.White)
            {
                Position = new Vector2(ui_Icon_Ice.Position.X + 30, ui_Icon_Ice.Position.Y - 30),
                Text = $"Q",
                FontScale = new Vector2(0.7f, 0.7f),
                layerDepth = 0.95f,
                ShowGUI = true
            };
            gui_Components.Add(icon_Ice_font);
            // Lightning_font
            icon_Lightning_font = new GUI_Font(spriteFont, Color.White)
            {
                Position = new Vector2(ui_Icon_Lightning.Position.X + 30, ui_Icon_Lightning.Position.Y - 30),
                Text = $"W",
                FontScale = new Vector2(0.7f, 0.7f),
                layerDepth = 0.95f,
                ShowGUI = true
            };
            gui_Components.Add(icon_Lightning_font);
            // Sword_font
            icon_Sword_font = new GUI_Font(spriteFont, Color.White)
            {
                Position = new Vector2(ui_Icon_Sword.Position.X - 50, ui_Icon_Sword.Position.Y - 20),
                Text = $"Space",
                FontScale = new Vector2(0.4f, 0.4f),
                layerDepth = 0.95f,
                ShowGUI = true
            };
            gui_Components.Add(icon_Sword_font);
            #endregion

            foreach (GUI_Component x in gui_Components)
            {
                Gameworld.AddGUI(x);
            }
        }

        public void Update_UI()
        {            
            ui_XP_Bar_02.Rectangle = new Rectangle(
                    (int)ui_XP_Bar_02.Position.X,
                    (int)ui_XP_Bar_02.Position.Y,
                    ui_XP_Bar_02.Sprite.Width * (Gameworld.Player.CurrentXP / Gameworld.Player.MaxXP),
                    ui_XP_Bar_02.Sprite.Height - 1);

            HowManyPot();


        }


        public void HowManyPot()
        {
            int tmp = 0;
            for (int i = 0; i < Gameworld.Player.Inventory.items.Length; i++)
            {
                if (Gameworld.Player.Inventory.items[i] != null)
                    if (Gameworld.Player.Inventory.items[i].itemType == ItemType.Consumable)
                    {
                        tmp += 1;
                    }
            }

            if(tmp > 0)
            {
                ui_Icon_HealthPotion.color = Color.White;
                health_font.fontColor = Color.White;
            }
            else
            {
                ui_Icon_HealthPotion.color = Color.Gray;
                health_font.fontColor = Color.Gray;
            }
        }
    }
}
