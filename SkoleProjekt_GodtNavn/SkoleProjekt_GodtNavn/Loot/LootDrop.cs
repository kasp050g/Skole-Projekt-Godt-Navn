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

        public override void Initialize()
        {
            base.Initialize();
            // Load in Font
            spriteFont = Gameworld.spriteContainer.spriteFont;
            panelImage = Gameworld.spriteContainer.soleSprite["frame_1"];
            item.RandomStats();
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
                Position = new Vector2(_Panel.Position.X -70, _Panel.Position.Y - 27),
                Text = $"{item.rarity} {item.itemType}",
                FontScale = new Vector2(0.5f, 0.5f),
                layerDepth = 0.17f,
                fontColor = item.rarityColor,
                ShowGUI = true
            };
            Gameworld.Instatiate(_Font);
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
            sprite = Gameworld.spriteContainer.soleSprite["bag"];
            
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void Destroy()
        {
            Gameworld.Destroy(_Panel);
            Gameworld.Destroy(_Font);
            Gameworld.Destroy(this);
        }
    }
}
