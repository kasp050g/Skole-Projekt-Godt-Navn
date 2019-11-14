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
    public class SpriteContainer
    {
        public Dictionary<string, Texture2D> soleSprite = new Dictionary<string, Texture2D>();
        public Dictionary<string, Texture2D> spriteSheet = new Dictionary<string, Texture2D>();
        public Dictionary<string, List<Texture2D>> spriteList = new Dictionary<string, List<Texture2D>>();

        public SpriteFont spriteFont;

        public void LoadContent(ContentManager content)
        {
            AddSpriteSheet(content.Load<Texture2D>("Texture/Enemy/Goblin/Enemy_Goblin_Sheet"), "Enemy_Goblin_Sheet");
            AddSpriteSheet(content.Load<Texture2D>("Texture/Enemy/Golem/Enemy_Golem_Walk_Sheet"), "Enemy_Golem_Walk_Sheet");
            AddSpriteSheet(content.Load<Texture2D>("Texture/Enemy/Golem/Enemy_Golem_Attack_Sheet"), "Enemy_Golem_Attack_Sheet");
            AddSpriteSheet(content.Load<Texture2D>("Texture/Enemy/Golem/Enemy_Golem_Die_Sheet"), "Enemy_Golem_Die_Sheet");
            // Effect
            AddSpriteSheet(content.Load<Texture2D>("Texture/Effect/bloodBoom"), "bloodBoom");

            // Font
            spriteFont = content.Load<SpriteFont>("Font/NormalFont");

            // Spell
            AddSoleSprite(content.Load<Texture2D>("Texture/Spell/fire_"), "fire_");
            AddSoleSprite(content.Load<Texture2D>("Texture/Spell/ice"), "ice");
            AddSoleSprite(content.Load<Texture2D>("Texture/Spell/lightning"), "lightning");

            // BackGround
            AddSoleSprite(content.Load<Texture2D>("Texture/Background/Background1"), "Background1");
            AddSoleSprite(content.Load<Texture2D>("Texture/Background/Background2"), "Background2");

            AddSoleSprite(content.Load<Texture2D>("Texture/Component/CollisionTexture"), "CollisionTexture");

            // Load in Image
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/helmet_slot"), "helmet_slot");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/chest_slot"), "chest_slot");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/pants_slot"), "pants_slot");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/gloves_slot"), "gloves_slot");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/boots_slot"), "boots_slot");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/sword_slot"), "sword_slot");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/staff_slot"), "staff_slot");

            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/helmet"), "helmet");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/chest"), "chest");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/pants"), "pants");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/gloves"), "gloves");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/boots"), "boots");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/sword"), "sword");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Character panel/staff"), "staff");

            // Inventory
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/apple"), "apple");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/background_bag"), "background_bag");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/bag"), "bag");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/empty_slot"), "empty_slot");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/empty_slot_bagbar"), "empty_slot_bagbar");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/epmtyFrame"), "epmtyFrame");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/full_slot_bag"), "full_slot_bag");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/healthPotion"), "healthPotion");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Inventory/key"), "key");


            // Quest Log
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/arrow_down"), "arrow_down");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/arrow_up"), "arrow_up");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/button_1"), "button_1");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/button_2"), "button_2");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/close_button_"), "close_button_");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/frame_1"), "frame_1");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/frame_2"), "frame_2");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/frame_3"), "frame_3");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/quest_log"), "quest_log");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/slider"), "slider");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/slider_bar"), "slider_bar");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/text_frame_1"), "text_frame_1");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Quest log/text_frame_2"), "text_frame_2");

            // Player UI
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Player UI/Health bar Empty"), "HealthBar");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Player UI/Mana Bar Empty"), "ManaBar");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Player UI/Circle white"), "CircleWhite");
            AddSoleSprite(content.Load<Texture2D>("Texture/UI/Player UI/Circle white _ 4 timer"), "CircleWhite4");


            /// ---Player Image---
            List<Texture2D> tmpList = new List<Texture2D>();
            #region Walk
            // Walk Down
            for (int i = 0; i < 11; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/down/down_walk/down_walk_00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Walk_Down");
            tmpList = new List<Texture2D>();
            // Walk Up
            for (int i = 0; i < 11; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/up/up_walk/UP walk00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Walk_Up");
            tmpList = new List<Texture2D>();
            // Walk Left
            for (int i = 0; i < 13; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/left/left_walk/left_walk00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Walk_Left");
            tmpList = new List<Texture2D>();
            // Walk Rigth
            for (int i = 0; i < 13; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/right/right_walk/right_walk00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Walk_Rigth");
            tmpList = new List<Texture2D>();
            #endregion
            #region Attack
            // Attack Down
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/down/down_attack_sword/down_attack_sword00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Attack_Down");
            tmpList = new List<Texture2D>();
            // Attack Up
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/up/up_attack_sword/UP attack_sword00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Attack_Up");
            tmpList = new List<Texture2D>();
            // Attack Left
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/left/left_attack_sword/left_attack_sword00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Attack_Left");
            tmpList = new List<Texture2D>();
            // Attack Rigth
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/right/right_attack_sword/right_attack_sword00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Attack_Rigth");
            tmpList = new List<Texture2D>();
            #endregion
            #region Cast
            // Cast Down
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/down/down_attack_shield/down_attack_shield00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Cast_Down");
            tmpList = new List<Texture2D>();
            // Cast Up
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/up/up_attack_shield/UP attack_shield00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Cast_Up");
            tmpList = new List<Texture2D>();
            // Cast Left
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/left/left_attack_shield/left_attack_shield00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Cast_Left");
            tmpList = new List<Texture2D>();
            // Cast Rigth
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/right/right_attack_shield/right_attack_shield00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Cast_Rigth");
            tmpList = new List<Texture2D>();
            #endregion
            #region Idle
            // Idle Down
            for (int i = 0; i < 9; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/down/down_idle/down_idle00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Idle_Down");
            tmpList = new List<Texture2D>();
            // Idle Up
            for (int i = 0; i < 9; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/up/up_idle/UP idle00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Idle_Up");
            tmpList = new List<Texture2D>();
            // Idle Left
            for (int i = 0; i < 9; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/left/left_idle/left_idle00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Idle_Left");
            tmpList = new List<Texture2D>();
            // Idle Rigth
            for (int i = 0; i < 9; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/right/right_idle/right_idle00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Idle_Rigth");
            tmpList = new List<Texture2D>();
            #endregion
            #region Death
            // Death Down
            for (int i = 0; i < 9; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/down/down_death/down_death00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Death_Down");
            tmpList = new List<Texture2D>();
            // Death Up
            for (int i = 0; i < 9; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/up/up_death/UP death00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Death_Up");
            tmpList = new List<Texture2D>();
            // Death Left
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/left/left_death/left_death00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Death_Left");
            tmpList = new List<Texture2D>();
            // Death Rigth
            for (int i = 0; i < 8; i++)
            {
                tmpList.Add(content.Load<Texture2D>($"Texture/Player/right/right_death/right_death00{(i + 1 >= 10 ? "" : "0")}{i + 1}"));
            }
            AddSpriteSheet(tmpList, "Player_Death_Rigth");
            tmpList = new List<Texture2D>();
            #endregion
        }


        public void AddSoleSprite(Texture2D texture2D, string name)
        {
            soleSprite.Add(name, texture2D);
        }

        public void AddSpriteSheet(Texture2D texture2D, string name)
        {
            spriteSheet.Add(name, texture2D);
        }

        public void AddSpriteSheet(List<Texture2D> texture2D, string name)
        {
            spriteList.Add(name, texture2D);
        }
    }
}
