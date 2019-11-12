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
        public GUI_HealthAndMana healthAndMana = new GUI_HealthAndMana();
        private bool canOpenUI;

        public int level = 5;
        public int gold = 0;
        public bool isSell = false;
        public bool isAttacking = false;

        #region --- Player Stats ---
        // XP
        int maxXP = 100;
        int currentXP = 0;
        // Health And Mana
        public Stat health = new Stat();
        public Stat mana = new Stat();
        public float armor;
        public int weaponDamage;
        // Strength Agility Intelligence
        public int strength;
        public int agility;
        public int intelligence;

        #endregion

        public float moveSpeed = 5;

        Facing facing = Facing.Down;

        public override void Initialize()
        {
            base.Initialize();
            transform.scale = 1.0f;
            canOpenUI = true;

            #region Set Up player stats
            // level up stats
            health.baseValue = 100;
            mana.baseValue = 25;

            // level up stats
            health.levelValue = 10;
            mana.levelValue = 5;

            UpdatePlayerStatOnLevelUp();

            #endregion
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
                if (x != null)
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

        public void GetXP(int newXP)
        {
            currentXP += newXP;
            if (currentXP >= maxXP)
            {
                maxXP = (int)(maxXP * 1.25f);
                currentXP = 0;
                level += 1;
            }
        }

        public override void OnCollision(GameObject other)
        {

        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            UpdatePlayerStats();
            GUI_Equipment.UpdateGUI03();
            GUI_Inventory.UpdateGUI01();
            Move(gameTime);
        }

        public void HandleInput()
        {
            MoveInput();
            UI_Input();

        }

        public void UI_Input()
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

            if (keyState.IsKeyDown(Keys.K) && canOpenUI == true)
            {
                isSell = !isSell;
                canOpenUI = false;
            }

            if (keyState.IsKeyUp(Keys.I) && keyState.IsKeyUp(Keys.P) && keyState.IsKeyUp(Keys.L) && keyState.IsKeyUp(Keys.K))
            {
                canOpenUI = true;
            }
        }

        public void MoveInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();
            // make sure we cant move befor the attack is done.
            if (isAttacking == false)
            {
                // if we move, move player and play run Animate
                if (keyState.IsKeyDown(Keys.W))
                {
                    velocity += new Vector2(0, -1);

                    facing = Facing.Up;
                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    velocity += new Vector2(0, 1);

                    facing = Facing.Down;
                }

                if (keyState.IsKeyDown(Keys.A))
                {
                    velocity += new Vector2(-1, 0);
                    // dont play if we move Up or Down
                    if (velocity.Y == 0)
                    {

                    }
                    facing = Facing.Left;
                }
                if (keyState.IsKeyDown(Keys.D))
                {
                    velocity += new Vector2(1, 0);
                    // dont play if we move Up or Down
                    if (velocity.Y == 0)
                    {

                    }
                    facing = Facing.Rigth;
                }
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        }
    }
}
