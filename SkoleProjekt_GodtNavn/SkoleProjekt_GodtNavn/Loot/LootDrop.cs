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
    public class LootDrop : GameObject
    {
        public Item item = new Item();
        public GUI_Equipment asd = new GUI_Equipment();

        SpriteFont spriteFont;
        Texture2D panelImage;
        GUI_Font_World _Font;
        GUI_Panel_World _Panel;

        List<GameObject> showLootIcon = new List<GameObject>();
        GUI_Font_World _Font_Loot;
        GUI_Panel_World _Panel_Loot;
        public override void Initialize()
        {
            base.Initialize();
            // Load in Font
            spriteFont = Gameworld.spriteContainer.spriteFont;
            panelImage = Gameworld.spriteContainer.soleSprite["frame_1"];
            item.RandomStats();
            sprite = item.itemSprite;
            layerDepth = 0.15f;

            _Panel = new GUI_Panel_World(panelImage)
            {
                Position = new Vector2(transform.position.X + 50, transform.position.Y),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(0.5f, 0.05f),
                layerDepth = 0.16f,
                ShowGUI = true
            };
            _Panel.SetOrigin();
            Gameworld.Instatiate(_Panel);

            _Font = new GUI_Font_World(spriteFont, Color.White)
            {
                Position = new Vector2(_Panel.Position.X -105, _Panel.Position.Y - 27),
                Text = $"{item.rarity} {item.itemType}",
                FontScale = new Vector2(0.5f, 0.5f),
                layerDepth = 0.17f,
                fontColor = item.rarityColor,
                ShowGUI = true
            };
            Gameworld.Instatiate(_Font);


            _Panel_Loot = new GUI_Panel_World(Gameworld.spriteContainer.soleSprite["empty_slot"])
            {
                Position = new Vector2(transform.position.X + 40, transform.position.Y - 35),
                Origin = GUI_OriginPosition.BottomMid,
                Scale = new Vector2(0.5f, 0.5f),
                layerDepth = 0.18f,
                ShowGUI = true
            };
            _Panel_Loot.SetOrigin();
            showLootIcon.Add(_Panel_Loot);
            Gameworld.Instatiate(_Panel_Loot);

            _Font_Loot = new GUI_Font_World(spriteFont, Color.White)
            {
                Position = new Vector2(_Panel_Loot.Position.X -10 , _Panel_Loot.Position.Y - 45),
                Text = $"F",
                FontScale = new Vector2(0.9f, 0.9f),
                layerDepth = 0.19f,
                fontColor = Color.White,
                ShowGUI = true
            };
            showLootIcon.Add(_Font_Loot);
            Gameworld.Instatiate(_Font_Loot);
        }

        public LootDrop(Vector2 dropPosition)
        {
            transform.position = dropPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, transform.position + spritePositionOffset, rectangle, color, transform.rotation, origin, transform.scale, SpriteEffects.None, layerDepth);
        }

        public override void LoadContent(ContentManager content)
        {
            //sprite = Gameworld.spriteContainer.soleSprite["bag"];
            
        }

        public override void OnCollision(GameObject other)
        {

            if (other is Player)
            {
                for (int i = 0; i < showLootIcon.Count; i++)
                {
                    if(showLootIcon[i] is GUI_Font_World)
                    (showLootIcon[i] as GUI_Font_World).ShowGUI = true;

                    if (showLootIcon[i] is GUI_Panel_World)
                        (showLootIcon[i] as GUI_Panel_World).ShowGUI = true;
                }
            }

        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < showLootIcon.Count; i++)
            {
                if (showLootIcon[i] is GUI_Font_World)
                    (showLootIcon[i] as GUI_Font_World).ShowGUI = false;

                if (showLootIcon[i] is GUI_Panel_World)
                    (showLootIcon[i] as GUI_Panel_World).ShowGUI = false;
            }
        }

        public void Destroy()
        {
            Gameworld.Destroy(_Panel_Loot);
            Gameworld.Destroy(_Font_Loot);
            Gameworld.Destroy(_Panel);
            Gameworld.Destroy(_Font);
            Gameworld.Destroy(this);
        }
    }
}
