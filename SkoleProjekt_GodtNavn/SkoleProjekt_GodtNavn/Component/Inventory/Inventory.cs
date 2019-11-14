using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{
    public class Inventory
    {
        public Item[] items = new Item[20];

        public void AddItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i] == null)
                {
                    items[i] = item;
                    break;
                }                
            }
        }
        public void SellItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if(item == items[i])
                {
                    Gameworld.Player.gold += items[i].goldValue;
                    items[i] = null;
                }
            }
        }
    }
}
