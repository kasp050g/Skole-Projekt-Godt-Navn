using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SkoleProjekt_GodtNavn
{
    public class PlayerAttackZone : GameObject
    {
        private List<GameObject> cantAttackThisMore = new List<GameObject>();
        public Vector2 CollisionBoxSize = new Vector2(0, 0);
        public int damage;
        public bool canDamage = false;
        public override void Initialize()
        {
            base.Initialize();
            Sprite = Gameworld.spriteContainer.soleSprite["CollisionTexture"];
        }
        public override void LoadContent(ContentManager content)
        {
            Sprite = Gameworld.spriteContainer.soleSprite["CollisionTexture"];
        }

        public override void OnCollision(GameObject other)
        {
            if (canDamage == true)
            {
                if (other is Enemy && (other as Character).IsAlive)
                {
                    bool canBeAttack = true;
                    for (int i = 0; i < cantAttackThisMore.Count; i++)
                    {
                        if (cantAttackThisMore[i] == other)
                        {
                            canBeAttack = false;
                        }
                    }
                    if(canBeAttack == true)
                    {
                        cantAttackThisMore.Add(other);
                        (other as Character).TakeDamage(damage);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public void ClearAttackList()
        {
            cantAttackThisMore.Clear();
        }

        public override void Awake()
        {
        }

        public override Rectangle rectangle
        {
            get
            {
                return new Rectangle(
                    0,
                    0,
                    0,
                    0
                    );
            }
            set { }
        }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    (int)Transform.Position.X - (int)(0) + 0,
                    (int)Transform.Position.Y - (int)(0) + 0,
                    (int)(CollisionBoxSize.X),
                    (int)(CollisionBoxSize.Y)
                    );
            }
        }
    }
}
