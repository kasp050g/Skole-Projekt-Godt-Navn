using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn.Bundles
{
    class EnemyBundle
    {
        // Animation array list for skeletons.
        public Texture2D[] SkeletonSprites { get; set; }
        public Texture2D[] GoblinSprites { get; set; }
        public Texture2D[] WolfSprites { get; set; }
        public Texture2D[] GolemSprites { get; set; }

        public void LoadContent(ContentManager content)
        {
            SkeletonSprites = new Texture2D[1];
            for (int i = 0; i < SkeletonSprites.Length; i++)
            {
                SkeletonSprites[i] = content.Load<Texture2D>("Skeleton" + (i + 1));
            }

            GoblinSprites = new Texture2D[1];
            for (int i = 0; i < GoblinSprites.Length; i++)
            {
                GoblinSprites[i] = content.Load<Texture2D>("Goblin" + (i + 1));
            }

            WolfSprites = new Texture2D[1];
            for (int i = 0; i < WolfSprites.Length; i++)
            {
                WolfSprites[i] = content.Load<Texture2D>("Wolf" + (i + 1));
            }

            GolemSprites = new Texture2D[1];
            for (int i = 0; i < GolemSprites.Length; i++)
            {
                GolemSprites[i] = content.Load<Texture2D>("Golem" + (i + 1));
            }
        }

        public void UnloadContent(ContentManager content)
        {

        }
    }
}
