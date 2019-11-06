using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkoleProjekt_GodtNavn
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        private float rotation = 0;
        private float scale = 1;

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }
    }
}
