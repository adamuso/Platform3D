using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DPlatformer.Engine.Entities
{
    public class Entity : DrawableGameComponent
    {
        public Vector3 Position { get; set; }
        public new Game3DPlatformer Game { get { return Game3DPlatformer.Instance; } }

        public Entity()
            : base(Game3DPlatformer.Instance)
        {
            
        }
    }
}
