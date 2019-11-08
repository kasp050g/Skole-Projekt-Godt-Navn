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
    public class GUI_Equipment
    {
        private Equipment equipment;

        // Font
        private SpriteFont spriteFont;

        // Image
        private Texture2D panel;
        private Texture2D unEquip_Hemlet;
        private Texture2D unEquip_Chest;
        private Texture2D unEquip_Leg;
        private Texture2D unEquip_Gloves;
        private Texture2D unEquip_Boots;
        private Texture2D unEquip_Weapon;

        private bool showGUI;
        public bool ShowGUI { get { return showGUI; } set { showGUI = value; } }

        public List<GUI_Component> gui_equipment = new List<GUI_Component>();



        public void LoadContent(ContentManager content)
        {
            // Load in Image
            panel = content.Load<Texture2D>("Texture/UI/Equipment/character_panel");

            unEquip_Hemlet = content.Load<Texture2D>("Texture/UI/Equipment/helmet_slot");
            unEquip_Chest = content.Load<Texture2D>("Texture/UI/Equipment/chest_slot");
            unEquip_Leg = content.Load<Texture2D>("Texture/UI/Equipment/pants_slot");
            unEquip_Gloves = content.Load<Texture2D>("Texture/UI/Equipment/gloves_slot");
            unEquip_Boots = content.Load<Texture2D>("Texture/UI/Equipment/boots_slot");
            unEquip_Weapon = content.Load<Texture2D>("Texture/UI/Equipment/slot_1");
            // Load in Font
            spriteFont = content.Load<SpriteFont>("Font/NormalFont");
        }


        public void GUI_Setup()
        {
            var panel01 = new GUI_Panel(panel)
            {
                Position = new Vector2(50, 190),
                Origin = GUI_OriginPosition.TopLeft,
                Scale = new Vector2(0.8f, 0.9f)
            };
            panel01.SetOrigin();
            gui_equipment.Add(panel01);


            // Hemlet
            var EB_Hemlet = new GUI_EquipButton(unEquip_Hemlet, unEquip_Hemlet)
            {
                Position = new Vector2(panel01.Position.X + 50, panel01.Position.Y + 50),
                Scale = new Vector2(1.0f, 1.0f)
            };
            gui_equipment.Add(EB_Hemlet);


            // Chest
            var EB_Chest = new GUI_EquipButton(unEquip_Chest, unEquip_Chest)
            {
                Position = new Vector2(panel01.Position.X + 50, panel01.Position.Y + 150),
                Scale = new Vector2(1.0f, 1.0f)
            };
            gui_equipment.Add(EB_Chest);


            // Leg
            var EB_Leg = new GUI_EquipButton(unEquip_Leg, unEquip_Leg)
            {
                Position = new Vector2(panel01.Position.X + 50, panel01.Position.Y + 250),
                Scale = new Vector2(1.0f, 1.0f)
            };
            gui_equipment.Add(EB_Leg);


            // Gloves
            var EB_Gloves = new GUI_EquipButton(unEquip_Gloves, unEquip_Gloves)
            {
                Position = new Vector2(panel01.Position.X + 500, panel01.Position.Y + 50),
                Scale = new Vector2(1.0f, 1.0f)
            };
            gui_equipment.Add(EB_Gloves);


            // Boots
            var EB_Boots = new GUI_EquipButton(unEquip_Boots, unEquip_Boots)
            {
                Position = new Vector2(panel01.Position.X + 500, panel01.Position.Y + 150),
                Scale = new Vector2(1.0f, 1.0f)
            };
            gui_equipment.Add(EB_Boots);


            // Weapon
            var EB_Weapon = new GUI_EquipButton(unEquip_Weapon, unEquip_Weapon)
            {
                Position = new Vector2(panel01.Position.X + 500, panel01.Position.Y + 250),
                Scale = new Vector2(1.0f, 1.0f)
            };
            gui_equipment.Add(EB_Weapon);



            // At the end add all to the UI List.
            foreach (GUI_Component x in gui_equipment)
            {

                Gameworld.AddGUI(x);
            }
        }

        public void UpdateGUI()
        {
            foreach (GUI_Component x in gui_equipment)
            {
                x.ShowGUI = showGUI;
            }
        }
    }
}
