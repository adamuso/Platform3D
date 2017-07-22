using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.World.Blocks
{
    public class BlockGrass : Block
    {
        public BlockGrass()
            : base()
        {
        }
        public override Point GetTexturePosition(Direction faceDirection)
        {
            Point position = new Point();

            switch (faceDirection)
            {
                case Direction.Up:
                    position.X = 2;
                    position.Y = 3;
                    return position;

                case Direction.Down:
                    position.X = 1;
                    position.Y = 3;
                    return position;
            }

            position.X = 3;
            position.Y = 3;

            return position;
        }
    }
}
