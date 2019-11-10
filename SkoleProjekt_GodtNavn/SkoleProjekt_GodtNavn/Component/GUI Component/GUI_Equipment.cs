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


        private GUI_EquipButton EB_Hemlet;
        private GUI_EquipButton EB_Chest;
        private GUI_EquipButton EB_Leg;
        private GUI_EquipButton EB_Gloves;
        private GUI_EquipButton EB_Boots;
        private GUI_EquipButton EB_Weapon;

        private bool showGUI;
        public bool ShowGUI { get { return showGUI; } set { showGUI = value; } }

        public List<GUI_Component> gui_equipment = new List<GUI_Component>();


        public void LoadContent(ContentManager content)
        {
            // Load in Image
            panel = Gameworld.spriteContainer.soleSprite["quest_log"];

            unEquip_Hemlet = Gameworld.spriteContainer.soleSprite["helmet_slot"];
            unEquip_Chest = Gameworld.spriteContainer.soleSprite["chest_slot"];
            unEquip_Leg = Gameworld.spriteContainer.soleSprite["pants_slot"];
            unEquip_Gloves = Gameworld.spriteContainer.soleSprite["gloves_slot"];
            unEquip_Boots = Gameworld.spriteContainer.soleSprite["boots_slot"];
            unEquip_Weapon = Gameworld.spriteContainer.soleSprite["sword_slot"];

            // Load in Font
            spriteFont = content.Load<SpriteFont>("Font/NormalFont");
        }



        public void GUI_Setup()
        {
            var panel01 = new GUI_Panel(panel)
            {
                Position = new Vector2(50, 190),
                Origin = GUI_OriginPosition.TopLeft,
                Scale = new Vector2(0.6f, 0.9f)
            };
            panel01.SetOrigin();
            gui_equipment.Add(panel01);


            // Hemlet
            EB_Hemlet = new GUI_EquipButton(unEquip_Hemlet, unEquip_Hemlet)
            {
                Position = new Vector2(panel01.Position.X + 50, panel01.Position.Y + 50),
                Scale = new Vector2(1.0f, 1.0f),
                item = Gameworld.player.equipment.helmet
            };
            gui_equipment.Add(EB_Hemlet);


            // Chest
            EB_Chest = new GUI_EquipButton(unEquip_Chest, unEquip_Chest)
            {
                Position = new Vector2(panel01.Position.X + 50, panel01.Position.Y + 150),
                Scale = new Vector2(1.0f, 1.0f),
                item = Gameworld.player.equipment.chest
            };
            gui_equipment.Add(EB_Chest);


            // Leg
            EB_Leg = new GUI_EquipButton(unEquip_Leg, unEquip_Leg)
            {
                Position = new Vector2(panel01.Position.X + 50, panel01.Position.Y + 250),
                Scale = new Vector2(1.0f, 1.0f),
                item = Gameworld.player.equipment.leg
            };
            gui_equipment.Add(EB_Leg);


            // Gloves
            EB_Gloves = new GUI_EquipButton(unEquip_Gloves, unEquip_Gloves)
            {
                Position = new Vector2(panel01.Position.X + 500, panel01.Position.Y + 50),
                Scale = new Vector2(1.0f, 1.0f),
                item = Gameworld.player.equipment.gloves
            };
            gui_equipment.Add(EB_Gloves);


            // Boots
            EB_Boots = new GUI_EquipButton(unEquip_Boots, unEquip_Boots)
            {
                Position = new Vector2(panel01.Position.X + 500, panel01.Position.Y + 150),
                Scale = new Vector2(1.0f, 1.0f),
                item = Gameworld.player.equipment.boots
            };
            gui_equipment.Add(EB_Boots);


            // Weapon
            EB_Weapon = new GUI_EquipButton(unEquip_Weapon, unEquip_Weapon)
            {
                Position = new Vector2(panel01.Position.X + 500, panel01.Position.Y + 250),
                Scale = new Vector2(1.0f, 1.0f),
                item = Gameworld.player.equipment.weapon
            };
            gui_equipment.Add(EB_Weapon);

            #region Show Stats
            // Health
            showHealth = new GUI_Font(spriteFont,Color.IndianRed)
            {
                Position = new Vector2(panel01.Position.X + 40, panel01.Position.Y + 380),
                Text = $"Health: {Gameworld.player.health.currentValue}/{Gameworld.player.health.maxValue}",
                FontScale = new Vector2(0.5f, 0.5f)
            };
            gui_equipment.Add(showHealth);
            // Mana
            showMana = new GUI_Font(spriteFont, Color.Blue)
            {
                Position = new Vector2(panel01.Position.X + 40, panel01.Position.Y + 410),
                Text = $"Mana: {Gameworld.player.mana.currentValue}/{Gameworld.player.mana.maxValue}",
                FontScale = new Vector2(0.5f, 0.5f)
            };
            gui_equipment.Add(showMana);

            // Armor
            showArmor = new GUI_Font(spriteFont, Color.DimGray)
            {
                Position = new Vector2(panel01.Position.X + 40, panel01.Position.Y + 440),
                Text = $"Armor: {Gameworld.player.armor}/{Gameworld.player.armor / (Gameworld.player.level * 10 * 100)}",
                FontScale = new Vector2(0.5f, 0.5f)
            };
            gui_equipment.Add(showArmor);
            // Weapon Damage
            showWeaponDamage = new GUI_Font(spriteFont, Color.DimGray)
            {
                Position = new Vector2(panel01.Position.X + 40, panel01.Position.Y + 470),
                Text = $"Weapon Damage: {Gameworld.player.weaponDamage}",
                FontScale = new Vector2(0.5f, 0.5f)
            };
            gui_equipment.Add(showWeaponDamage);

            // showStrength
            showStrength = new GUI_Font(spriteFont, Color.OrangeRed)
            {
                Position = new Vector2(panel01.Position.X + 400, panel01.Position.Y + 380),
                Text = $"Strength: {Gameworld.player.strength}",
                FontScale = new Vector2(0.5f, 0.5f)
            };
            gui_equipment.Add(showStrength);
            // showAgility
            showAgility = new GUI_Font(spriteFont, Color.LightGreen)
            {
                Position = new Vector2(panel01.Position.X + 400, panel01.Position.Y + 410),
                Text = $"Agility: {Gameworld.player.agility}",
                FontScale = new Vector2(0.5f, 0.5f)
            };
            gui_equipment.Add(showAgility);
            // showStrength
            showIntelligence = new GUI_Font(spriteFont, Color.BlueViolet)
            {
                Position = new Vector2(panel01.Position.X + 400, panel01.Position.Y + 440),
                Text = $"Intelligence: {Gameworld.player.intelligence}",
                FontScale = new Vector2(0.5f, 0.5f)
            };
            gui_equipment.Add(showIntelligence);
            #endregion

            // At the end add all to the UI List.
            foreach (GUI_Component x in gui_equipment)
            {

                Gameworld.AddGUI(x);
            }
        }

        GUI_Font showHealth;
        GUI_Font showMana;

        GUI_Font showArmor;
        GUI_Font showWeaponDamage;

        GUI_Font showStrength;
        GUI_Font showAgility;
        GUI_Font showIntelligence;

        public void UpdateGUI()
        {
            foreach (GUI_Component x in gui_equipment)
            {
                x.ShowGUI = showGUI;
            }
            UpdateGUI02();
        }
        public void UpdateGUI02()
        {
            EB_Hemlet.item = Gameworld.player.equipment.helmet;
            EB_Chest.item = Gameworld.player.equipment.chest;
            EB_Leg.item = Gameworld.player.equipment.leg;
            EB_Gloves.item = Gameworld.player.equipment.gloves;
            EB_Boots.item = Gameworld.player.equipment.boots;
            EB_Weapon.item = Gameworld.player.equipment.weapon;

            UpdateGUI03();
        }

        public void UpdateGUI03()
        {
            if (ShowGUI == true)
            {
                showHealth.Text = $"Health: {Gameworld.player.health.currentValue}/{Gameworld.player.health.maxValue}";
                showMana.Text = $"Mana: {Gameworld.player.mana.currentValue}/{Gameworld.player.mana.maxValue}";

                showArmor.Text = $"Armor: {(int)Gameworld.player.armor}/ {(Gameworld.player.armor) / (float)(Gameworld.player.level * 10) * 100}%";
                showWeaponDamage.Text = $"Weapon Damage: {Gameworld.player.weaponDamage}";

                showStrength.Text = $"Strength: {Gameworld.player.strength}";
                showAgility.Text = $"Agility: {Gameworld.player.agility}";
                showIntelligence.Text = $"Intelligence: {Gameworld.player.intelligence}";
            }
        }
    }
}
