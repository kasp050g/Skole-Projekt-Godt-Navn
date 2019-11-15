using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{
    public class Equipment
    {
        public Item helmet = null;
        public Item chest = null;
        public Item leg = null;
        public Item gloves = null;
        public Item boots = null;
        public Item weapon = null;
        private Player player;


        public void EquipItem(Item item)
        {
            for (int i = 0; i < Gameworld.Player.Inventory.items.Length; i++)
            {
                if(Gameworld.Player.Inventory.items[i] == item)
                {
                    Gameworld.Player.Inventory.items[i] = null;
                }
            }

            switch (item.itemType)
            {
                case ItemType.Helmet:
                    SwitchItem(item, ItemType.Helmet);
                    break;
                case ItemType.Chest:
                    SwitchItem(item, ItemType.Chest);
                    break;
                case ItemType.Leg:
                    SwitchItem(item, ItemType.Leg);
                    break;
                case ItemType.Gloves:
                    SwitchItem(item, ItemType.Gloves);
                    break;
                case ItemType.Boots:
                    SwitchItem(item, ItemType.Boots);
                    break;
                case ItemType.WeaponRange:
                    SwitchItem(item, ItemType.WeaponRange);
                    break;
                case ItemType.WeaponMelee:
                    SwitchItem(item, ItemType.WeaponMelee);
                    break;
                case ItemType.Consumable:
                    Gameworld.Player.Health.AddValue(50);
                    Gameworld.Player.Mana.AddValue(50);
                    break;
                default:
                    Console.WriteLine("Equipment Error");
                    break;
            }
        }

        public void SwitchItem(Item item, ItemType itemType)
        {
            #region Helment
            if(ItemType.Helmet == itemType)
            if (helmet == null)
            {
                helmet = item;
            }
            else
            {
                Gameworld.Player.Inventory.AddItem(helmet);
                helmet = item;
            }
            #endregion
            #region Chest
            if (ItemType.Chest == itemType)
                if (chest == null)
                {
                    chest = item;
                }
                else
                {
                    Gameworld.Player.Inventory.AddItem(chest);
                    chest = item;
                }
            #endregion
            #region Leg
            if (ItemType.Leg == itemType)
                if (leg == null)
                {
                    leg = item;
                }
                else
                {
                    Gameworld.Player.Inventory.AddItem(leg);
                    leg = item;
                }
            #endregion
            #region Gloves
            if (ItemType.Gloves == itemType)
                if (gloves == null)
                {
                    gloves = item;
                }
                else
                {
                    Gameworld.Player.Inventory.AddItem(gloves);
                    gloves = item;
                }
            #endregion
            #region Boots
            if (ItemType.Boots == itemType)
                if (boots == null)
                {
                    boots = item;
                }
                else
                {
                    Gameworld.Player.Inventory.AddItem(boots);
                    boots = item;
                }
            #endregion
            #region Weapon
            if (ItemType.WeaponMelee == itemType || ItemType.WeaponRange == itemType)
                if (weapon == null)
                {
                    weapon = item;
                }
                else
                {
                    Gameworld.Player.Inventory.AddItem(weapon);
                    weapon = item;
                }
            #endregion
        }

        public void UnEquipItem(Item item)
        {
            switch (item.itemType)
            {
                case ItemType.Helmet:
                    Gameworld.Player.Inventory.AddItem(helmet);
                    helmet = null;
                    break;
                case ItemType.Chest:
                    Gameworld.Player.Inventory.AddItem(chest);
                    chest = null;
                    break;
                case ItemType.Leg:
                    Gameworld.Player.Inventory.AddItem(leg);
                    leg = null;
                    break;
                case ItemType.Gloves:
                    Gameworld.Player.Inventory.AddItem(gloves);
                    gloves = null;
                    break;
                case ItemType.Boots:
                    Gameworld.Player.Inventory.AddItem(boots);
                    boots = null;
                    break;
                case ItemType.WeaponRange:
                    Gameworld.Player.Inventory.AddItem(weapon);
                    weapon = null;
                    break;
                case ItemType.WeaponMelee:
                    Gameworld.Player.Inventory.AddItem(weapon);
                    weapon = null;
                    break;
                default:
                    Console.WriteLine("Equipment Error");
                    break;
            }
        }

        public List<Item> ReturnEquipItems()
        {
            List<Item> tmpList = new List<Item>();

            tmpList.Add(helmet);
            tmpList.Add(chest);
            tmpList.Add(leg);
            tmpList.Add(gloves);
            tmpList.Add(boots);
            tmpList.Add(weapon);

            return tmpList;
        }
    }
}
