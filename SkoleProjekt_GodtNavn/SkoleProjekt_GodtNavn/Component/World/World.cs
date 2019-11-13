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
    public class World : GameObject
    {

        public override void Initialize()
        {
            base.Initialize();
            sprite = Gameworld.spriteContainer.soleSprite["Background2"];
            transform.scale = 5;
            transform.position = new Vector2(-2400, -1500);
            layerDepth = 0.1f;
        }
        public override void LoadContent(ContentManager content)
        {
            
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)transform.position.X ,
                    (int)transform.position.Y ,
                    (int)(0),
                    (int)(0)
                    );
            }
        }

        public override void OnCollision(GameObject other)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
