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
        public GUI_Inventory GUI_Inventory = new GUI_Inventory();
        Facing facing = Facing.Down;

        public override void Initialize()
        {
            base.Initialize();
            transform.scale = 1.0f;
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
           
        }

        public void HandleInput()
        {
            
        }
    }
}
