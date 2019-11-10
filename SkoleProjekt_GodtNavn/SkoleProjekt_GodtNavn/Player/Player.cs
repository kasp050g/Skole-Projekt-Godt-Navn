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
    public class Player : Character
    {
        public Inventory inventory = new Inventory();
        public Equipment equipment = new Equipment();
        public GUI_Inventory GUI_Inventory = new GUI_Inventory();
        public GUI_Equipment GUI_Equipment = new GUI_Equipment();
        private bool canOpenUI;

        public int level = 5;


        // --- Player Stats ---
        // Health And Mana
        public Stat health = new Stat();
        public Stat mana = new Stat();
        public float armor;
        public int weaponDamage;
        // Strength Agility Intelligence
        public int strength;
        public int agility;
        public int intelligence;


        public float moveSpeed = 5;

        Facing facing = Facing.Down;

        public override void Initialize()
        {
            base.Initialize();
            transform.scale = 1.0f;
            canOpenUI = true;


            // level up stats
            health.baseValue = 100;
            mana.baseValue = 25;

            // level up stats
            health.levelValue = 10;
            mana.levelValue = 5;

            UpdatePlayerStatOnLevelUp();

            // Add item 01
            Item item01 = new Item();
            item01.RandomStats();
            inventory.AddItem(item01);

            Item item02 = new Item();
            item02.RandomStats();
            inventory.AddItem(item02);

            Item item03 = new Item();
            item03.RandomStats();
            inventory.AddItem(item03);
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Texture/Player/tmp");
            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            layerDepth = 0.5f;
        }

        public void UpdatePlayerStats()
        {
            UpdatePlayerStat(health);
            UpdatePlayerStat(mana);

            armor = 0;
            weaponDamage = 0;

            strength = 1;
            agility = 1;
            intelligence = 1;

            List<Item> tmp = equipment.RetrunEquipItems();

            foreach (Item x in equipment.RetrunEquipItems())
            {
                if(x != null)
                {
                    health.maxValue += x.health;
                    mana.maxValue += x.mana;

                    armor += x.armor;
                    weaponDamage += x.weaponDamage;

                    strength += x.strength;
                    agility += x.agility;
                    intelligence += x.intelligence;
                }
            }

            health.AddValue(0);
            mana.AddValue(0);
        }
        public void UpdatePlayerStat(Stat stat)
        {
            stat.maxValue = stat.baseValue + stat.levelValue * level;
        }

        public void UpdatePlayerStatOnLevelUp()
        {
            UpdatePlayerStats();
            health.currentValue = health.maxValue;
            mana.currentValue = mana.maxValue;
        }


        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            UpdatePlayerStats();
            GUI_Equipment.UpdateGUI03();
        }

        public void HandleInput()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.I) && canOpenUI == true)
            {
                GUI_Inventory.ShowGUI = !GUI_Inventory.ShowGUI;
                GUI_Inventory.UpdateGUI();
                canOpenUI = false;
            }

            if (keyState.IsKeyDown(Keys.P) && canOpenUI == true)
            {
                GUI_Equipment.ShowGUI = !GUI_Equipment.ShowGUI;
                GUI_Equipment.UpdateGUI();
                canOpenUI = false;
            }

            if (keyState.IsKeyDown(Keys.L) && canOpenUI == true)
            {
                Item tmpItem = new Item();
                tmpItem.RandomStats();
                inventory.AddItem(tmpItem);
            }

            if (keyState.IsKeyUp(Keys.I) && keyState.IsKeyUp(Keys.P) && keyState.IsKeyUp(Keys.L))
            {
                canOpenUI = true;
            }
        }
    }
}
