using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.World.Blocks
{
    public class BlockDirt : Block
    {
        public BlockDirt()
            : base()
        {

        }

        public override Point GetTexturePosition(Direction faceDirection)
        {
            Point position = new Point();

            position.X = 1;
            position.Y = 3;

            return position;
        }
    }
}
