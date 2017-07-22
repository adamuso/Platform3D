using _3DPlatformer.Engine.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.World
{
    public class BlockInfo : ISimpleCollidable
    {
        private Block block;
        public World World { get; private set; }
        public Block Block { get { return block; } set { Set(value); } }
        public Location Location { get; private set; }
        public Chunk Chunk { get { return Location.GetChunk(World); } }
        public BoundingBox BoundingBox { get { return new BoundingBox(block.BoundingBox.Min + Location, block.BoundingBox.Max + Location); } }

        public BlockInfo(World world, Block block, Location location)
        {
            this.World = world;
            this.block = block;
            this.Location = location;
        }

        private void Set(Block block)
        {
            World.SetBlock(Location, block);
        }

        public Collision CheckCollision(ICollidable collider)
        {
            return block.CheckCollision(collider) ?? collider.CheckCollision(this);
        }
    }
}
