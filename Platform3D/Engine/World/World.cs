using _3DPlatformer.Engine.Collisions;
using _3DPlatformer.Engine.World.Blocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.World
{
    public class World : ICollidable
    {
        private Dictionary<Location, Chunk> chunks;
        private Queue<Location> chunkQueue;

        public Block this[int x, int y, int z]
        {
            get { return GetBlock(new Location(x, y, z)).Block; }
            set { SetBlock(new Location(x, y, z), value); }
        }

        public Block this[Location location]
        {
            get { return GetBlock(location).Block; }
            set { SetBlock(location, value); }
        }

        public World()
        {
            chunks = new Dictionary<Location, Chunk>();
            chunkQueue = new Queue<Location>();
        }

        #region Chunks
        public void InitializeChunk(int x, int y, int z)
        {
            InitializeChunk(new Location(x, y, z) * Chunk.Size);
        }

        public void InitializeChunk(Location location)
        {
            if (!chunks.ContainsKey(location.GetChunkLocation()))
            {
                SetChunk(location, new Chunk(this));
            }
        }

        public void EnqueueChunks()
        {
            for (int x = 0; x < 24; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int z = 0; z < 24; z++)
                    {
                        chunkQueue.Enqueue(new Location(x, y, z) * Chunk.Size);
                    }
                }
            }
        }

        public void CreateChunk(int x, int y, int z)
        {
            CreateChunk(new Location(x, y, z) * Chunk.Size);
        }

        public void CreateChunk(Location location)
        {
            location = location.GetChunkLocation() * Chunk.Size;
            InitializeChunk(location);

            for (int x = 0; x < Chunk.Size; x++)
            {
                for (int y = 0; y < Chunk.Size; y++)
                {
                    for (int z = 0; z < Chunk.Size; z++)
                    {
                        if (y <= 6)
                        {
                            SetBlock(location.X + x, location.Y + y, location.Z + z, new BlockDirt());
                        }
                        else if (y <= 7)
                        {
                            SetBlock(location.X + x, location.Y + y, location.Z + z, new BlockGrass());
                        }
                        else
                        {
                            SetBlock(location.X + x, location.Y + y, location.Z + z, new BlockAir());
                        }
                    }
                }
            }
        }

        public Chunk GetChunk(int x, int y, int z)
        {
            return GetChunk(new Location(x, y, z) * Chunk.Size);
        }

        public Chunk GetChunk(Location location)
        {
            return chunks.ContainsKey(location.GetChunkLocation()) ? chunks[location.GetChunkLocation()] : null;
        }

        public void SetChunk(int x, int y, int z, Chunk chunk)
        {
            SetChunk(new Location(x, y, z) * Chunk.Size, chunk);
        }

        public void SetChunk(Location location, Chunk chunk)
        {
            location = location.GetChunkLocation();
            chunk.Location = location;
            chunks[location] = chunk;
        }
        #endregion

        #region Blocks
        public BlockInfo GetBlock(int x, int y, int z)
        {
            return GetBlock(new Location(x, y, z));
        }

        public BlockInfo GetBlock(Location location)
        {
            Chunk chunk = GetChunk(location);

            if (chunk != null)
            {   
                return chunk.GetBlock(location);
            }

            return new BlockInfo(this, new BlockAir(), location);
        }

        public void SetBlock(int x, int y, int z, Block block)
        {
            SetBlock(new Location(x, y, z), block);
        }

        public void SetBlock(Location location, Block block)
        {
            Chunk chunk = GetChunk(location);

            if (chunk != null)
            {
                chunk.SetBlock(location, block);
                chunk.NeedsUpdate = true;

                if(location.IsChunkBorder)
                {
                    Chunk[] chunks = location.GetAdjacentChunks(this);

                    for(int i = 0; i < chunks.Length; i++)
                    {
                        chunks[i].NeedsUpdate = true;
                    }
                }
            }
        }
        #endregion

        double acc = 0;
        double acc2 = 0;

        public void Update(GameTime gameTime)
        {
            if (chunkQueue.Count > 0 && acc > 0.1)
            {
                Console.WriteLine(chunkQueue.Count);
                acc -= 0.1;
                Location l = chunkQueue.Dequeue();
                CreateChunk(l);
                GetChunk(l).NeedsUpdate = true;
            }

            foreach (Chunk chunk in chunks.Values)
            {
                if (chunk.NeedsUpdate && acc2 > 0.05)
                {
                    acc2 -= 0.05;
                    chunk.Update(gameTime);
                    break;
                }
            }

            acc2 += gameTime.ElapsedGameTime.TotalSeconds;
            acc += gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw()
        {
            foreach(Chunk chunk in chunks.Values)
            {
                if (Game3DPlatformer.Instance.Camera.Frustum.Contains(chunk.BoundingBox) != ContainmentType.Disjoint)
                {
                    chunk.Render();
                }
            }
        }

        public Collision CheckCollision(ICollidable collider)
        {
            if (collider is ISimpleCollidable)
            {
                return collider.CheckCollision(this);
            }

            return null;
        }
    }
}
