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
    public class GUI_Inventory
    {
        private int slotOffSetX = 125;
        private int slotOffSetY = 100;
        Texture2D panel;
        SpriteFont spriteFont;

        GUI_Slot_Inventory[] slot_Inventory = new GUI_Slot_Inventory[20];

        // Show Item
        Texture2D itemSlot;
        Texture2D itemSlotFram;

        GUI_Font goldText;

        private int itemShowOffset = 0;
        private bool showGUI;
        public bool ShowGUI { get { return showGUI; } set { showGUI = value; } }

        public List<GUI_Component> gui_inventory = new List<GUI_Component>();

        public void LoadContent(ContentManager content)
        {
            panel = Gameworld.spriteContainer.soleSprite["quest_log"];
            itemSlot = Gameworld.spriteContainer.soleSprite["empty_slot"];
            itemSlotFram = Gameworld.spriteContainer.soleSprite["epmtyFrame"];
            // Load in Font
            spriteFont = content.Load<SpriteFont>("Font/NormalFont");
        }

        public void GUI_Setup()
        {
            var panel01 = new GUI_Panel(panel)
            {
                Position = new Vector2(Gameworld.ScreenSize.X - 50, 150),
                Origin = GUI_OriginPosition.TopRigth,
                Scale = new Vector2(0.5f, 0.76f),
                layerDepth = 0.8f
            };
            panel01.SetOrigin();
            gui_inventory.Add(panel01);

            // Title
            var panelTitle01 = new GUI_Font(spriteFont)
            {
                Position = new Vector2(panel01.Position.X - 360, panel01.Position.Y + 40),
                Text = "Inventory",
                FontScale = new Vector2(1, 1),
                layerDepth = 0.9f,
                fontColor = Color.BlueViolet
            };
            gui_inventory.Add(panelTitle01);

            // Title
            goldText = new GUI_Font(spriteFont)
            {
                Position = new Vector2(panel01.Position.X - 520, panel01.Position.Y + 505),
                Text = "Inventory",
                FontScale = new Vector2(0.5f, 0.5f),
                layerDepth = 0.90f,
                fontColor = Color.Gold
            };
            gui_inventory.Add(goldText);


            for (int i = 0; i < slot_Inventory.Length; i++)
            {
                var jamen = new GUI_Slot_Inventory(itemSlot, itemSlotFram)
                {
                    Position = new Vector2(panel01.Position.X - slotOffSetX, panel01.Position.Y + slotOffSetY),
                    Scale = new Vector2(1.0f, 1.0f),
                    layerDepth = 0.9f
                };

                slot_Inventory[i] = jamen;
                gui_inventory.Add(jamen);

                slotOffSetX += 100;

                if(slotOffSetX > 600)
                {
                    slotOffSetX = 125;
                    slotOffSetY += 100;
                }
            }

            foreach (Item x in Gameworld.player.inventory.items)
            {
                ShowItemGUI02(panel01.Position, x, itemShowOffset);
                itemShowOffset += 100;
            }


            // at the end.
            foreach (GUI_Component x in gui_inventory)
            {
                Gameworld.AddGUI(x);
            }
        }

        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            var random = new Random();

            Gameworld.backgroundColour = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        public void UpdateGUI()
        {
            foreach (GUI_Component x in gui_inventory)
            {
                x.ShowGUI = showGUI;
            }
            UpdateGUI01();
        }

        public void UpdateGUI01()
        {
            if(showGUI == true)
            {
                for (int i = 0; i < slot_Inventory.Length; i++)
                {
                    slot_Inventory[i].item = Gameworld.player.inventory.items[i];
                }
                goldText.Text = $"Gold: {Gameworld.player.gold}";
            }
        }

        public void ShowItemGUI02(Vector2 mainPanel, Item item, int offset)
        {

        }


        public void ShowItemGUI(Vector2 mainPanel, Item item, int offset)
        {
            var _itemPanel = new GUI_Panel(itemSlot)
            {
                Position = new Vector2(mainPanel.X - 505, mainPanel.Y + (100 + offset)),
                Origin = GUI_OriginPosition.TopLeft,
                Scale = new Vector2(0.8f, 0.5f)
            };
            _itemPanel.SetOrigin();
            gui_inventory.Add(_itemPanel);

            var panelTitle01 = new GUI_Font(spriteFont)
            {
                Position = new Vector2(_itemPanel.Position.X + 10, _itemPanel.Position.Y + 20),
                Text = " Item: " + item.itemType.ToString() + " - Rarity: " + item.rarity.ToString() + "\n Damage: " + item.weaponDamage.ToString() + " -  Armor: " + item.armor.ToString(),
                FontScale = new Vector2(0.1f, 0.1f),
                fontColor = Color.Gold
            };
            gui_inventory.Add(panelTitle01);

            //var randomButton = new GUI_Button(buttonImage, spriteFont)
            //{
            //    Position = new Vector2(_itemPanel.Position.X + 200, _itemPanel.Position.Y + 50),
            //    Text = "Equip",
            //    FontScale = new Vector2(0.5f, 0.5f),
            //    ButtonScale = new Vector2(1, 1)
            //};
            //randomButton.Click += RandomButton_Click;
            //gui_inventory.Add(randomButton);

            //var randomButton01 = new GUI_Button(buttonImage, spriteFont)
            //{
            //    Position = new Vector2(_itemPanel.Position.X + 200, _itemPanel.Position.Y + 100),
            //    Text = "Delete",
            //    FontScale = new Vector2(0.5f, 0.5f),
            //    ButtonScale = new Vector2(1, 1)
            //};
            //randomButton01.Click += RandomButton_Click;
            //gui_inventory.Add(randomButton01);
        }
    }
}
