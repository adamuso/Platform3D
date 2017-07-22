using _3DPlatformer.Graphics.Meshes;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.World.Blocks
{
    public class BlockAir : Block
    {
        public BlockAir()
            : base()
        {
        }

        public override void GetMeshData(MeshData data, Chunk chunk, int x, int y, int z)
        {

        }

        public override bool IsSolid(Direction direction)
        {
            return false;
        }
    }
}
