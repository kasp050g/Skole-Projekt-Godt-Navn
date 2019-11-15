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
            Sprite = Gameworld.spriteContainer.soleSprite["Background2"];
            Transform.Scale = 5;
            Transform.Position = new Vector2(-2400, -1500);
            LayerDepth = 0.1f;
        }
        public override void LoadContent(ContentManager content)
        {
            
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)Transform.Position.X ,
                    (int)Transform.Position.Y ,
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

        public override void Awake()
        {
        }
    }
}
