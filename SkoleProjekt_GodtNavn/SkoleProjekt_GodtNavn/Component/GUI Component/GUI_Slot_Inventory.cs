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
    public class GUI_Slot_Inventory : GUI_Component
    {

        private MouseState currentMouse;
        private MouseState previousMouse;
        private bool isHovering;

        private Texture2D slot;
        private Texture2D slotFram;

        private Color hoverColor = Color.LightGray;
        private Color defaultColor = Color.White;


        public event EventHandler Click;
        public float layerDepth;
        public Item item;
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)(slot.Width * Scale.X),
                    (int)(slot.Height * Scale.Y));
            }
        }

        public GUI_Slot_Inventory(Texture2D slot, Texture2D slotFram)
        {
            this.slot = slot;
            this.slotFram = slotFram;
        }

        public GUI_Slot_Inventory(Texture2D slot, Texture2D slotFram, Color defaultColor, Color hoverColor)
        {
            this.slot = slot;
            this.slotFram = slotFram;
            this.defaultColor = defaultColor;
            this.hoverColor = hoverColor;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ShowGUI == true)
            {
                var colour = defaultColor;

                if (isHovering)
                {
                    colour = hoverColor;
                }

                if (item != null)
                {
                    Vector2 itemSpritePosition = new Vector2(Position.X + (slotFram.Width - item.itemSprite.Width) / 2, Position.Y + (slotFram.Height - item.itemSprite.Height) / 2);
                    spriteBatch.Draw(item.itemSprite, itemSpritePosition, null, item.rarityColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, layerDepth);
                    spriteBatch.Draw(slotFram, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, layerDepth);
                }
                else
                {
                    spriteBatch.Draw(slot, Position, null, colour, 0f, Vector2.Zero, Scale, SpriteEffects.None, layerDepth);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ShowGUI == true)
            {
                previousMouse = currentMouse;
                currentMouse = Mouse.GetState();

                var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

                isHovering = false;

                if (mouseRectangle.Intersects(Rectangle))
                {
                    isHovering = true;

                    if (currentMouse.RightButton == ButtonState.Released && previousMouse.RightButton == ButtonState.Pressed)
                    {
                        Click?.Invoke(this, new EventArgs());
                        if (item != null)
                        {
                            Gameworld.player.equipment.EquipItem(item);
                            Gameworld.player.GUI_Inventory.UpdateGUI01();
                            Gameworld.player.GUI_Equipment.UpdateGUI02();
                        }
                    }
                }
            }
        }
    }
}
