using _3DPlatformer.Graphics.Meshes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.World
{
    public class Chunk
    {
        public static readonly int Size = 16;
        private Block[, ,] blocks;
        private ModelMeshPart mesh;

        public bool NeedsUpdate { get; set; }
        public World World { get; set; }
        public Location Location { get; set; }
        public BoundingBox BoundingBox { get { return new BoundingBox(Location * Chunk.Size, Location * Chunk.Size + Chunk.Size); } }

        public Block this[int x, int y, int z]
        {
            get { return GetBlock(new Location(x, y, z) + Location * Chunk.Size).Block; }
            set { SetBlock(new Location(x, y, z) + Location * Chunk.Size, value); }
        }

        public Block this[Location location]
        {
            get { return GetBlock(location).Block; }
            set { SetBlock(location, value); }
        }

        public Chunk(World world)
        {
            this.World = world;
            blocks = new Block[Size, Size, Size];

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    for (int z = 0; z < Size; z++)
                    {
                        blocks[x, y, z] = new Blocks.BlockAir();
                    }
                }
            }
        }

        #region Blocks
        public BlockInfo GetBlock(int x, int y, int z)
        {
            return new BlockInfo(World, this[x, y, z], this.Location * Chunk.Size + new Location(x, y, z));
        }

        public BlockInfo GetBlock(Location location)
        {
            if (IsLocationInChunk(location))
                return new BlockInfo(World, blocks[location.X % Chunk.Size, location.Y % Chunk.Size, location.Z % Chunk.Size] ?? new Blocks.BlockAir(), location);
            else
                return World.GetBlock(location);
        }

        public void SetBlock(int x, int y, int z, Block block)
        {
            this[x, y, z] = block;
        }

        public void SetBlock(Location location, Block block)
        {
            if(IsLocationInChunk(location))
            {
                blocks[location.X % Chunk.Size, location.Y % Chunk.Size, location.Z % Chunk.Size] = block;
            }
            else
            {
                World.SetBlock(location, block);
            }
        }
        #endregion

        public bool IsLocationInChunk(Location location)
        {
            return location >= Location * Chunk.Size && location < Location * Chunk.Size + Chunk.Size;
        }

        public void Update(GameTime gameTime)
        {
            if(NeedsUpdate)
            {
                NeedsUpdate = false;
                RecreateMesh();
            }
        }

        public void RecreateMesh()
        {
            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();

            s.Start();

            if(mesh != null)
            {
                mesh.VertexBuffer.Dispose();
                mesh.IndexBuffer.Dispose();
                mesh = null;
            }

            //s.Stop();

            //s.Start();
            
            MeshData data = new MeshData();

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    for (int z = 0; z < Size; z++)
                    {
                        blocks[x, y, z].GetMeshData(data, this, x, y, z);
                    }
                }
            }

            s.Stop();

            Console.WriteLine(s.Elapsed.Ticks);

            s.Restart();
            
            data.CalculateNormals();

            if (data.Vertices.Count > 0)
            {
                mesh = new ModelMeshPart();

                mesh.VertexBuffer = new VertexBuffer(Game3DPlatformer.Instance.GraphicsDevice, typeof(VertexPositionNormalTexture), data.Vertices.Count, BufferUsage.WriteOnly);
                mesh.IndexBuffer = new IndexBuffer(Game3DPlatformer.Instance.GraphicsDevice, typeof(short), data.Indices.Count, BufferUsage.WriteOnly);

                mesh.VertexBuffer.SetData(data.Vertices.Select((v, i) => new VertexPositionNormalTexture(v, data.Normals[i], data.UV[i])).ToArray());
                mesh.IndexBuffer.SetData(data.Indices.ToArray());

                mesh.NumVertices = data.Vertices.Count;
                mesh.PrimitiveCount = data.Indices.Count / 3;

                //meshPart.Effect = Game3DPlatformer.Instance.BasicEffect;

                //mesh = new ModelMesh(Game3DPlatformer.Instance.GraphicsDevice, new List<ModelMeshPart> { meshPart });
                //mesh.Effects = new ModelEffectCollection(new List<Effect> { Game3DPlatformer.Instance.BasicEffect });
            }
            else
            {
                mesh = null;
            }

            s.Stop();

            Console.WriteLine(s.Elapsed.Ticks);
            Console.WriteLine("--");
        }

        public void Render()
        {
            if (mesh != null)
            {
                Game3DPlatformer.Instance.BasicEffect.World = Matrix.CreateTranslation(Location * Chunk.Size);
                Game3DPlatformer.Instance.BasicEffect.Texture = Game3DPlatformer.Instance.test;
                Game3DPlatformer.Instance.BasicEffect.CurrentTechnique.Passes[0].Apply();
                Game3DPlatformer.Instance.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

                Game3DPlatformer.Instance.GraphicsDevice.SetVertexBuffer(mesh.VertexBuffer);
                Game3DPlatformer.Instance.GraphicsDevice.Indices = mesh.IndexBuffer;

                Game3DPlatformer.Instance.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, mesh.PrimitiveCount);
            }
        }
    }
}
