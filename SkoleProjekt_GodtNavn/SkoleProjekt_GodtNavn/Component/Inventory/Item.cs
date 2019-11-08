using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{
    public class Item
    {
        public ItemType ItemType;
        public Rarity rarity;
        public int health;
        public int mana;
        public float armor;
        public int weaponDamage;
        // Stat
        public int strength;
        public int intelligence;
        public int agility;
        // Gold value
        public int goldValue;

        Random random = new Random();
        private int playerLevel;
        public void RandomStats()
        {
            playerLevel = Gameworld.player.level;
            Array itemValues = Enum.GetValues(typeof(ItemType));
            ItemType = (ItemType)itemValues.GetValue(random.Next(itemValues.Length));

            rarity = RarityItem();
            AddRandomStat();
            GoldValue();
        }

        public void GoldValue()
        {
            if (Rarity.common == rarity)
            {
                goldValue = 1 * playerLevel;
            }
            if (Rarity.uncommon == rarity)
            {
                goldValue = 2 * playerLevel;
            }
            if (Rarity.rare == rarity)
            {
                goldValue = 3 * playerLevel;
            }
            if (Rarity.epic == rarity)
            {
                goldValue = 4 * playerLevel;
            }
        }

        public void AddRandomStat()
        {
            if (ItemType.WeaponMelee == ItemType || ItemType.WeaponRange == ItemType)
            {
                weaponDamage = (int)(RarityStatNumber(rarity, "Weapon") * playerLevel);
            }
            else
            {
                armor = (RarityStatNumber(rarity, "Armor") * playerLevel);
            }

            if (Rarity.common == rarity)
            {

            }
            if (Rarity.uncommon == rarity)
            {
                int randomS = random.Next(1, 2);
                if (randomS == 1)
                {
                    AddHealthMana(RarityStatNumber(rarity,"Stat"));
                }
                if (randomS == 2)
                {
                    AddStat(RarityStatNumber(rarity, "Stat"));
                }
            }
            if (Rarity.rare == rarity)
            {
                AddHealthMana(RarityStatNumber(rarity, "Stat"));
                AddStat(RarityStatNumber(rarity, "Stat"));
            }
            if (Rarity.epic == rarity)
            {
                AddHealthMana(RarityStatNumber(rarity, "Stat"));
                AddStat(RarityStatNumber(rarity, "Stat"));
                AddStat(RarityStatNumber(rarity, "Stat"));
            }
        }

        public void AddStat(float add)
        {
            int _stat = random.Next(1, 3);
            if (_stat == 1)
            {
                strength = (int)add * playerLevel;
            }
            if (_stat == 2)
            {
                intelligence = (int)add * playerLevel;
            }
            if (_stat == 3)
            {
                agility = (int)add * playerLevel;
            }
        }
        public void AddHealthMana(float add)
        {
            int _healthMana = random.Next(1, 2);
            if (_healthMana == 1)
            {
                health = (int)add * playerLevel;
            }
            if (_healthMana == 2)
            {
                mana = (int)add * playerLevel;
            }
        }

        public Rarity RarityItem()
        {
            float rarityRandom = random.Next(0, 10000) / 100;

            if (rarityRandom <= 60)
            {
                return Rarity.common;
            }
            else if (rarityRandom <= 80)
            {
                return Rarity.uncommon;
            }
            else if (rarityRandom <= 95)
            {
                return Rarity.rare;
            }
            else
            {
                return Rarity.epic;
            }
        }

        public float RarityStatNumber(Rarity RarityNumber, string type)
        {
            if (type == "Armor")
            {
                if (Rarity.common == RarityNumber)
                {
                    return ((float)random.Next(1, 3) / 10);
                }
                if (Rarity.uncommon == RarityNumber)
                {
                    return ((float)random.Next(4, 5) / 10);
                }
                if (Rarity.rare == RarityNumber)
                {
                    return ((float)random.Next(6, 9) / 10);
                }
                if (Rarity.epic == RarityNumber)
                {
                    return ((float)random.Next(1, 15) / 10);
                }
            }
            if (type == "Weapon")
            {
                if (Rarity.common == RarityNumber)
                {
                    return ((float)random.Next(5, 7) / 10);
                }
                if (Rarity.uncommon == RarityNumber)
                {
                    return ((float)random.Next(8, 10) / 10);
                }
                if (Rarity.rare == RarityNumber)
                {
                    return ((float)random.Next(10, 15) / 10);
                }
                if (Rarity.epic == RarityNumber)
                {
                    return ((float)random.Next(15, 20) / 10);
                }
            }
            if (type == "Stat")
            {
                if (Rarity.uncommon == RarityNumber)
                {
                    return ((float)random.Next(10, 12) / 10);
                }
                if (Rarity.rare == RarityNumber)
                {
                    return ((float)random.Next(12, 15) / 10);
                }
                if (Rarity.epic == RarityNumber)
                {
                    return ((float)random.Next(15, 20) / 10);
                }
            }
            return 0;
        }
    }
}
