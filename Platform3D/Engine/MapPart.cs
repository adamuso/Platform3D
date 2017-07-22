using _3DPlatformer.Engine.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine
{
    public class MapPart : ISimpleCollidable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Map Map { get; set; }
        public bool IsEmpty { get; set; }
        public BoundingBox BoundingBox { get; set; }

        public MapPart(Map map)
        {
            this.Map = map;
        }

        public bool IsCovered
        {
            get
            {
                if (X - 1 < Map.Width / 2 || X + 1 > Map.Width / 2 - 1 || Y - 1 < Map.Height / 2 || Y + 1 > Map.Height / 2 - 1 || Z - 1 < Map.Length / 2 || Z + 1 > Map.Length / 2 - 1)
                    return false;

                return !Map[X - 1, Y, Z].IsEmpty && !Map[X, Y - 1, Z].IsEmpty && !Map[X, Y, Z - 1].IsEmpty
                    && !Map[X + 1, Y, Z].IsEmpty && !Map[X, Y + 1, Z].IsEmpty && !Map[X, Y, Z + 1].IsEmpty;
            }
        }

        public Collision CheckCollision(ICollidable collider)
        {
            return collider.CheckCollision(this);
        }

        public static readonly MapPart Empty = new MapPart(null) { IsEmpty = true };
    }
}
