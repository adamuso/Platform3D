using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DPlatformer.Engine.Collisions
{
    public class Collision
    {
        private ICollidable collidable;

        public Collision(ICollidable collidable)
        {
            // TODO: Complete member initialization
            this.collidable = collidable;
        }
    }
}
