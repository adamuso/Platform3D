using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.Collisions
{
    public interface ICollidable
    {
        Collision CheckCollision(ICollidable collider);
    }
}
