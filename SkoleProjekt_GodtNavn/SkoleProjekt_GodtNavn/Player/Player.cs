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
        // Animation
        private AnimationContainer_Array animation_walk;
        private AnimationContainer_Array animation_attack;
        private AnimationContainer_Array animation_cast;
        private AnimationContainer_Array animation_idle;
        private AnimationContainer_Array animation_death;

        private DoAnimation_Array doAnimation_Array = new DoAnimation_Array(10);
        private bool playDeadAnimationOnes = true;

        public Inventory Inventory { get; set; } = new Inventory();
        public Equipment Equipment { get; set; } = new Equipment();
        public GUI_Inventory GUI_Inventory { get; set; } = new GUI_Inventory();
        public GUI_Equipment GUI_Equipment { get; set; } = new GUI_Equipment();
        public GUI_HealthAndMana HealthAndMana { get; set; } = new GUI_HealthAndMana();
        public GUI_Spell_Bar GUI_Spell_Bar { get; set; } = new GUI_Spell_Bar();
        private bool canOpenUI;

        /// <summary>
        /// Defines the Player's Level
        /// </summary>
        public int Level { get; set; } = 1;

        /// <summary>
        /// Defines the amount of gold the Player has.
        /// </summary>
        public int Gold { get; set; } = 0;
        public bool IsSell { get; set; } = false;
        private bool isAttacking = false;

        #region --- Player Stats ---
        // XP
        public int MaxXP { get; set; } = 50;
        public int CurrentXP { get; set; } = 0;
        // Health And Mana

        public Stat Mana { get; set; } = new Stat();
        public float Armor { get; set; }
        public int WeaponDamage { get; set; }
        // Strength Agility Intelligence
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }

        #endregion

        /// <summary>
        /// Defines the attack zone the Player's attacks register.
        /// </summary>
        private PlayerAttackZone attackZone = new PlayerAttackZone();

        public override Rectangle rectangle
        {
            get
            {
                return new Rectangle(
                    0,
                    0,
                    Sprite.Width,
                    Sprite.Height
                    );
            }
        }

        /// <summary>
        /// Player's collision box.
        /// </summary>
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)Transform.Position.X - (int)(0) + 20,
                    (int)Transform.Position.Y - (int)(0) + 30,
                    (int)(350 * Transform.Scale),
                    (int)(350 * Transform.Scale)
                    );
            }
        }

        /// <summary>
        /// Player's initialization.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            Transform.Scale = 0.2f;
            canOpenUI = true;
            attackZone.canDamage = isAttacking;
            attackZone.Sprite = Gameworld.spriteContainer.soleSprite["CollisionTexture"];
            Gameworld.Instatiate(attackZone);


            #region Set Animation
            animation_walk = new AnimationContainer_Array()
            {
                up = Gameworld.spriteContainer.spriteList["Player_Walk_Up"],
                down = Gameworld.spriteContainer.spriteList["Player_Walk_Down"],
                left = Gameworld.spriteContainer.spriteList["Player_Walk_Left"],
                rigth = Gameworld.spriteContainer.spriteList["Player_Walk_Rigth"],
                stopAtEnd = false
            };
            animation_attack = new AnimationContainer_Array()
            {
                up = Gameworld.spriteContainer.spriteList["Player_Attack_Up"],
                down = Gameworld.spriteContainer.spriteList["Player_Attack_Down"],
                left = Gameworld.spriteContainer.spriteList["Player_Attack_Left"],
                rigth = Gameworld.spriteContainer.spriteList["Player_Attack_Rigth"],
                stopAtEnd = true
            };
            animation_cast = new AnimationContainer_Array()
            {
                up = Gameworld.spriteContainer.spriteList["Player_Cast_Up"],
                down = Gameworld.spriteContainer.spriteList["Player_Cast_Down"],
                left = Gameworld.spriteContainer.spriteList["Player_Cast_Left"],
                rigth = Gameworld.spriteContainer.spriteList["Player_Cast_Rigth"],
                stopAtEnd = true
            };
            animation_idle = new AnimationContainer_Array()
            {
                up = Gameworld.spriteContainer.spriteList["Player_Idle_Up"],
                down = Gameworld.spriteContainer.spriteList["Player_Idle_Down"],
                left = Gameworld.spriteContainer.spriteList["Player_Idle_Left"],
                rigth = Gameworld.spriteContainer.spriteList["Player_Idle_Rigth"],
                stopAtEnd = false
            };
            animation_death = new AnimationContainer_Array()
            {
                up = Gameworld.spriteContainer.spriteList["Player_Death_Up"],
                down = Gameworld.spriteContainer.spriteList["Player_Death_Down"],
                left = Gameworld.spriteContainer.spriteList["Player_Death_Left"],
                rigth = Gameworld.spriteContainer.spriteList["Player_Death_Rigth"],
                stopAtEnd = true
            };
            #endregion

            doAnimation_Array.SetAnimation(animation_walk, facing);

            #region Set Up player stats
            // level up stats
            Health.baseValue = 100;
            Mana.baseValue = 25;

            // level up stats
            Health.levelValue = 10;
            Mana.levelValue = 5;

            UpdatePlayerStatOnLevelUp();

            #endregion
        }

        /// <summary>
        /// Player's content to load.
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>("Texture/Player/tmp");
            this.origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            LayerDepth = 0.5f;
        }

        /// <summary>
        /// Player's update.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            GUI_Spell_Bar.Update_UI();
            HandleInput();
            UpdatePlayerStats();
            GUI_Equipment.UpdateGUI03();
            GUI_Inventory.UpdateGUI01();
            Move(gameTime);
            isAttacking = doAnimation_Array.Animate(gameTime, facing);
            Sprite = doAnimation_Array.currentSprite;
            ImagePositionSet();
            SetPositionOfAttackZone();
        }

        /// <summary>
        /// When the player takes damage, includes armor calculations.
        /// </summary>
        /// <param name="damage"></param>
        public override void TakeDamage(int damage)
        {
            if (IsAlive)
            {
                IsAlive = Health.LowerValueBool(ArmorPercentCalculation(damage));
                BloodEffect bloodEffect = new BloodEffect(this, bloodColor);
                bloodEffect.Initialize();
                Gameworld.Instatiate(bloodEffect);
                Gameworld.audioPlayer.SoundEffect_Play("SoundEffect_Hit", 0.1f);
            }
        }

        /// <summary>
        /// Calcuates the damage reduction from armor.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public int ArmorPercentCalculation(int damage)
        {
            float armorPercent = Armor / Level * 10;
            int newDamage = (int)(damage * (armorPercent / 100));
            return damage - newDamage;
        }

        /// <summary>
        /// Creates an attack zone, where the player's hits get registered.
        /// </summary>
        public void SetPositionOfAttackZone()
        {
            switch (facing)
            {
                case Facing.Up:
                    attackZone.Transform.Position = Transform.Position + (new Vector2(-10, -40));
                    attackZone.CollisionBoxSize = (new Vector2(120, 70));
                    break;
                case Facing.Down:
                    attackZone.Transform.Position = Transform.Position + (new Vector2(0, 100));
                    attackZone.CollisionBoxSize = (new Vector2(120, 70));
                    break;
                case Facing.Left:
                    attackZone.Transform.Position = Transform.Position + (new Vector2(-50, 0));
                    attackZone.CollisionBoxSize = (new Vector2(70, 120));
                    break;
                case Facing.Right:
                    attackZone.Transform.Position = Transform.Position + (new Vector2(90, 0));
                    attackZone.CollisionBoxSize = (new Vector2(70, 120));
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Updates the stats on the player, based on equipment.
        /// </summary>
        public void UpdatePlayerStats()
        {
            UpdatePlayerStat(Health);
            UpdatePlayerStat(Mana);

            Armor = 0;
            WeaponDamage = 4;

            Strength = 1;
            Agility = 1;
            Intelligence = 1;

            List<Item> tmp = Equipment.ReturnEquipItems();

            // Updates the player's stats based on what equipment the player has.
            foreach (Item x in Equipment.ReturnEquipItems())
            {
                if (x != null)
                {
                    Health.maxValue += x.health;
                    Mana.maxValue += x.mana;

                    Armor += x.armor;
                    WeaponDamage += x.weaponDamage;

                    Strength += x.strength;
                    Agility += x.agility;
                    Intelligence += x.intelligence;
                }
            }

            Health.AddValue(0);
            Mana.AddValue(0);
        }

        public void UpdatePlayerStat(Stat stat)
        {
            stat.maxValue = stat.baseValue + stat.levelValue * Level;
        }

        /// <summary>
        /// Updates the player's stats based on level.
        /// </summary>
        /// <param name="stat"></param>
        public void UpdatePlayerStatOnLevelUp()
        {
            UpdatePlayerStats();
            Health.currentValue = Health.maxValue;
            Mana.currentValue = Mana.maxValue;
        }

        /// <summary>
        /// Calculates how the player gains xp, how much xp the player needs to level up and what happens when the player levels up.
        /// </summary>
        /// <param name="newXP"></param>
        public void GetXP(int newXP)
        {
            CurrentXP += newXP;
            if (CurrentXP >= MaxXP)
            {
                MaxXP = (int)(MaxXP * 1.25f);
                CurrentXP = 0;
                Level += 1;
                UpdatePlayerStatOnLevelUp();
            }
        }

        public override void OnCollision(GameObject other)
        {
            // Checks if the player is holding down the F key, and if the object the player is colliding with is a dropped item. If true then pick it up.
            KeyboardState keyState = Keyboard.GetState();
            if (other is LootDrop && keyState.IsKeyDown(Keys.F))
            {
                for (int i = 0; i < Inventory.items.Length; i++)
                {
                    if(Inventory.items[i] == null)
                    {
                        LootDrop tmp = (other as LootDrop);
                        Inventory.AddItem(tmp.item);
                        tmp.Destroy();
                        break;
                    }
                }
            }
        }



        private void ImagePositionSet()
        {
            switch (facing)
            {
                case Facing.Up:
                    if (doAnimation_Array.animationContainer == animation_attack)
                    {
                        SpritePositionOffset = new Vector2(10, -20);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        SpritePositionOffset = new Vector2(10, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        SpritePositionOffset = new Vector2(10, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        SpritePositionOffset = new Vector2(-3, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        SpritePositionOffset = new Vector2(-10, -20);
                    }
                    break;

                case Facing.Down:
                    if (doAnimation_Array.animationContainer == animation_attack)
                    {
                        SpritePositionOffset = new Vector2(-33, -20);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        SpritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        SpritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        SpritePositionOffset = new Vector2(+2, -5);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        SpritePositionOffset = new Vector2(-30, -10);
                    }
                    break;

                case Facing.Left:
                    if (doAnimation_Array.animationContainer == animation_attack)
                    {
                        SpritePositionOffset = new Vector2(-10, -5);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        SpritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        SpritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        SpritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        SpritePositionOffset = new Vector2(-10, 0);
                    }
                    break;

                case Facing.Right:
                    if (doAnimation_Array.animationContainer == animation_attack)
                    {
                        SpritePositionOffset = new Vector2(-20, -5);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        SpritePositionOffset = new Vector2(30, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        SpritePositionOffset = new Vector2(30, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        SpritePositionOffset = new Vector2(20, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        SpritePositionOffset = new Vector2(-15, 0);
                    }
                    break;

                default:
                    break;

            }
        }

        /// <summary>
        /// Handles the player's input.
        /// </summary>
        public void HandleInput()
        {
            if (IsAlive)
            {
                MoveInput();
                UI_Input();
                Attack_Input();
            }
            else
            {
                if (playDeadAnimationOnes)
                {
                    playDeadAnimationOnes = false;
                    doAnimation_Array.isDead = true;
                    doAnimation_Array.SetAnimation(animation_death, facing);
                }
                velocity = Vector2.Zero;
            }
        }

        public void Attack_Input()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (isAttacking == false)
            {
                if (keyState.IsKeyDown(Keys.Space))
                {
                    attackZone.ClearAttackList();
                    isAttacking = true;
                    doAnimation_Array.SetAnimation(animation_attack, facing);
                    Gameworld.audioPlayer.SoundEffect_Play("SoundEffect_sword_swing", 0.5f);
                    attackZone.canDamage = true;
                    attackZone.damage = WeaponDamage;
                }

                if (keyState.IsKeyDown(Keys.E))
                {
                    int spellDamage = 10 + (Strength * 1);
                    int manaCost = 5;
                    Texture2D image = Gameworld.spriteContainer.soleSprite["fire_"]; 
                    CastSpell(spellDamage, manaCost, image, new Vector2(50, 50));
                }
                if (keyState.IsKeyDown(Keys.W))
                {
                    int spellDamage = 5 + (int)(Agility * 1.5f);
                    int manaCost = 5;
                    Texture2D image = Gameworld.spriteContainer.soleSprite["lightning"];
                    CastSpell(spellDamage, manaCost, image, new Vector2(50, 50));
                }
                if (keyState.IsKeyDown(Keys.Q))
                {
                    int spellDamage = 1 + (Intelligence * 2);
                    int manaCost = 5;
                    Texture2D image = Gameworld.spriteContainer.soleSprite["ice"];
                    CastSpell(spellDamage, manaCost, image, new Vector2(50, 50));
                }


                if (isAttacking == false)
                {
                    attackZone.canDamage = false;
                }
            }
        }

        public void CastSpell(int spellDamage , int manaCost,Texture2D image,Vector2 imageOffSet)
        {
            if (Mana.currentValue >= manaCost)
            {
                Mana.currentValue -= manaCost;
                Spell spell = new Spell(facing, manaCost, spellDamage, true);
                spell.Sprite = image;
                Vector2 positionOffSet = SpellPositionOffset();
                spell.Transform.Position = new Vector2(Transform.Position.X, Transform.Position.Y) + positionOffSet;
                spell.LayerDepth = 0.7f;
                spell.SpritePositionOffset = imageOffSet;
                isAttacking = true;
                doAnimation_Array.SetAnimation(animation_cast, facing);
                Gameworld.Instatiate(spell);

                if (image == Gameworld.spriteContainer.soleSprite["ice"])
                {
                    Gameworld.audioPlayer.SoundEffect_Play("ice_spell", 0f);
                }
                if (image == Gameworld.spriteContainer.soleSprite["lightning"])
                {
                    Gameworld.audioPlayer.SoundEffect_Play("thunderbolt", 0f);
                }
                if (image == Gameworld.spriteContainer.soleSprite["fire_"])
                {
                    Gameworld.audioPlayer.SoundEffect_Play("fireball", 0f);
                }
            }
        }

        public Vector2 SpellPositionOffset()
        {
            switch (facing)
            {
                case Facing.Up:
                    return new Vector2(0, -50);
                case Facing.Down:
                    return new Vector2(0, 50);
                case Facing.Left:
                    return new Vector2(-50, 0);
                case Facing.Right:
                    return new Vector2(50, 0);
                default:
                    return new Vector2(0, 0);
            }
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
                Inventory.AddItem(tmpItem);
            }

            if (keyState.IsKeyDown(Keys.K) && canOpenUI == true)
            {
                IsSell = !IsSell;
                canOpenUI = false;
            }

            if (keyState.IsKeyDown(Keys.H) && canOpenUI == true)
            {
                canOpenUI = false;
                for (int i = 0; i < Inventory.items.Length; i++)
                {
                    if (Inventory.items[i] != null)
                        if (Inventory.items[i].itemType == ItemType.Consumable)
                        {
                            Health.currentValue += 50;
                            Mana.currentValue += 50;
                            Inventory.items[i] = null;
                            Gameworld.audioPlayer.SoundEffect_Play("potion_sound", 0f);
                            break;
                        }
                }
            }

            if (keyState.IsKeyUp(Keys.I) && keyState.IsKeyUp(Keys.P) && keyState.IsKeyUp(Keys.L) && keyState.IsKeyUp(Keys.K) && keyState.IsKeyUp(Keys.H))
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
                if ( keyState.IsKeyDown(Keys.Up))
                {
                    velocity += new Vector2(0, -1);

                    facing = Facing.Up;
                }
                if ( keyState.IsKeyDown(Keys.Down))
                {
                    velocity += new Vector2(0, 1);

                    facing = Facing.Down;
                }

                if ( keyState.IsKeyDown(Keys.Left))
                {
                    velocity += new Vector2(-1, 0);
                    // dont play if we move Up or Down
                    if (velocity.Y == 0)
                    {

                    }
                    facing = Facing.Left;
                }
                if ( keyState.IsKeyDown(Keys.Right))
                {
                    velocity += new Vector2(1, 0);
                    // dont play if we move Up or Down
                    if (velocity.Y == 0)
                    {

                    }
                    facing = Facing.Right;
                }

                if (keyState.IsKeyDown(Keys.LeftShift))
                {
                    speed = 250 * 4;
                }
                else
                {
                    speed = 250;
                }

                if (velocity != Vector2.Zero)
                {
                    doAnimation_Array.SetAnimation(animation_walk, facing);
                    velocity.Normalize();
                }
                else
                {
                    doAnimation_Array.SetAnimation(animation_idle, facing);
                }
            }
        }

        public override void Awake()
        {
        }
    }
}
