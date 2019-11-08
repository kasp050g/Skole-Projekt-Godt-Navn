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
        public Inventory inventory = new Inventory();
        public GUI_Inventory GUI_Inventory = new GUI_Inventory();
        public GUI_Equipment GUI_Equipment = new GUI_Equipment();
        private bool canOpenUI;

        public int level = 10;

        Facing facing = Facing.Down;

        public override void Initialize()
        {
            base.Initialize();
            transform.scale = 1.0f;
            canOpenUI = true;

            // Add item 01
            Item item01 = new Item();
            item01.RandomStats();
            inventory.AddItem(item01);

            Item item02 = new Item();
            item02.RandomStats();
            inventory.AddItem(item02);

            Item item03 = new Item();
            item03.RandomStats();
            inventory.AddItem(item03);
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Texture/Player/tmp");
            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            layerDepth = 0.5f;
        }





        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
        }

        public void HandleInput()
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


            if(keyState.IsKeyUp(Keys.I) && keyState.IsKeyUp(Keys.P))
            {
                canOpenUI = true;
            }
        }
    }
}
