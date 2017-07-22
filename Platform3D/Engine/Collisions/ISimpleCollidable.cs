using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.Collisions
{
    public interface ISimpleCollidable : ICollidable
    {
        BoundingBox BoundingBox { get; }
    }
}
