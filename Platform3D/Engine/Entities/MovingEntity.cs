using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.Entities
{
    public class MovingEntity : Entity
    {
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
    }
}
