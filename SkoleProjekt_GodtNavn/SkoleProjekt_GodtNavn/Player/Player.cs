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
        AnimationContainer_Array animation_walk;
        AnimationContainer_Array animation_attack;
        AnimationContainer_Array animation_cast;
        AnimationContainer_Array animation_idle;
        AnimationContainer_Array animation_death;

        DoAnimation_Array doAnimation_Array = new DoAnimation_Array(10);
        private bool playDeadAnimationOnes = true;

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
        public int maxXP = 100;
        public int currentXP = 0;
        // Health And Mana

        public Stat mana = new Stat();
        public float armor;
        public int weaponDamage;
        // Strength Agility Intelligence
        public int strength;
        public int agility;
        public int intelligence;

        #endregion

        public float moveSpeed = 5;

        PlayerAttackZone attackZone = new PlayerAttackZone();

        public override Rectangle rectangle
        {
            get
            {
                return new Rectangle(
                    0,
                    0,
                    sprite.Width,
                    sprite.Height
                    );
            }
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)transform.position.X - (int)(0) + 20,
                    (int)transform.position.Y - (int)(0) + 30,
                    (int)(350 * transform.scale),
                    (int)(350 * transform.scale)
                    );
            }
        }


        public override void Initialize()
        {
            base.Initialize();
            transform.scale = 0.2f;
            canOpenUI = true;
            attackZone.canDamage = isAttacking;
            attackZone.sprite = Gameworld.spriteContainer.soleSprite["CollisionTexture"];
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
        public override void Update(GameTime gameTime)
        {
            HandleInput();
            UpdatePlayerStats();
            GUI_Equipment.UpdateGUI03();
            GUI_Inventory.UpdateGUI01();
            Move(gameTime);
            isAttacking = doAnimation_Array.Animate(gameTime, facing);
            sprite = doAnimation_Array.currentSprite;
            ImagePositionSet();
            SetPositionOfAttackZone();
        }

        public override void TakeDamage(int damage)
        {
            if (isAlive)
            {
                isAlive = health.LowerValueBool(ArmorPercentCalculation(damage));
                BloodEffect bloodEffect = new BloodEffect(this, bloodColor);
                bloodEffect.Initialize();
                Gameworld.Instatiate(bloodEffect);
                Gameworld.audioPlayer.SoundEffect_Play("SoundEffect_Hit", 0.1f);
            }
        }

        public int ArmorPercentCalculation(int damage)
        {
            float armorPercent = armor / level * 10;
            int newDamage = (int)(damage * (armorPercent / 100));
            return damage - newDamage;
        }

        public void SetPositionOfAttackZone()
        {
            switch (facing)
            {
                case Facing.Up:
                    attackZone.transform.position = transform.position + (new Vector2(-10, -40));
                    attackZone.CollisionBoxSize = (new Vector2(120, 70));
                    break;
                case Facing.Down:
                    attackZone.transform.position = transform.position + (new Vector2(0, 100));
                    attackZone.CollisionBoxSize = (new Vector2(120, 70));
                    break;
                case Facing.Left:
                    attackZone.transform.position = transform.position + (new Vector2(-50, 0));
                    attackZone.CollisionBoxSize = (new Vector2(70, 120));
                    break;
                case Facing.Rigth:
                    attackZone.transform.position = transform.position + (new Vector2(90, 0));
                    attackZone.CollisionBoxSize = (new Vector2(70, 120));
                    break;
                default:
                    break;
            }

        }

        public void UpdatePlayerStats()
        {
            UpdatePlayerStat(health);
            UpdatePlayerStat(mana);

            armor = 0;
            weaponDamage = 2;

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
            KeyboardState keyState = Keyboard.GetState();
            if (other is LootDrop && keyState.IsKeyDown(Keys.F))
            {
                for (int i = 0; i < inventory.items.Length; i++)
                {
                    if(inventory.items[i] == null)
                    {
                        LootDrop tmp = (other as LootDrop);
                        inventory.AddItem(tmp.item);
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
                        spritePositionOffset = new Vector2(10, -20);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        spritePositionOffset = new Vector2(10, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        spritePositionOffset = new Vector2(10, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        spritePositionOffset = new Vector2(-3, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        spritePositionOffset = new Vector2(-10, -20);
                    }
                    break;

                case Facing.Down:
                    if (doAnimation_Array.animationContainer == animation_attack)
                    {
                        spritePositionOffset = new Vector2(-33, -20);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        spritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        spritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        spritePositionOffset = new Vector2(+2, -5);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        spritePositionOffset = new Vector2(-30, -10);
                    }
                    break;

                case Facing.Left:
                    if (doAnimation_Array.animationContainer == animation_attack)
                    {
                        spritePositionOffset = new Vector2(-10, -5);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        spritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        spritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        spritePositionOffset = new Vector2(0, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        spritePositionOffset = new Vector2(-10, 0);
                    }
                    break;

                case Facing.Rigth:
                    if (doAnimation_Array.animationContainer == animation_attack)
                    {
                        spritePositionOffset = new Vector2(-20, -5);
                    }
                    if (doAnimation_Array.animationContainer == animation_walk)
                    {
                        spritePositionOffset = new Vector2(30, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_idle)
                    {
                        spritePositionOffset = new Vector2(30, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_cast)
                    {
                        spritePositionOffset = new Vector2(20, 0);
                    }
                    if (doAnimation_Array.animationContainer == animation_death)
                    {
                        spritePositionOffset = new Vector2(-15, 0);
                    }
                    break;

                default:
                    break;

            }
        }

        public void HandleInput()
        {
            if (isAlive)
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
                    attackZone.damage = weaponDamage;
                }

                if (keyState.IsKeyDown(Keys.E))
                {
                    int spellDamage = 1 + (strength * 1);
                    int manaCost = 5;
                    Texture2D image = Gameworld.spriteContainer.soleSprite["fire_"]; 
                    CastSpell(spellDamage, manaCost, image, new Vector2(50, 50));
                }
                if (keyState.IsKeyDown(Keys.W))
                {
                    int spellDamage = 1 + (agility * 1);
                    int manaCost = 5;
                    Texture2D image = Gameworld.spriteContainer.soleSprite["lightning"];
                    CastSpell(spellDamage, manaCost, image, new Vector2(50, 50));
                }
                if (keyState.IsKeyDown(Keys.Q))
                {
                    int spellDamage = 1 + (intelligence * 1);
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
            if (mana.currentValue >= manaCost)
            {
                mana.currentValue -= manaCost;
                Spell spell = new Spell(facing, manaCost, spellDamage, true);
                spell.sprite = image;
                Vector2 positionOffSet = SpellPositionOffset();
                spell.transform.position = new Vector2(transform.position.X, transform.position.Y) + positionOffSet;
                spell.layerDepth = 0.7f;
                spell.spritePositionOffset = imageOffSet;
                isAttacking = true;
                doAnimation_Array.SetAnimation(animation_cast, facing);
                Gameworld.Instatiate(spell);
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
                case Facing.Rigth:
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
                    facing = Facing.Rigth;
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
    }
}
