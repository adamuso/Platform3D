using _3DPlatformer.Engine.Collisions;
using _3DPlatformer.Graphics.Meshes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DPlatformer.Engine.World
{
    public class Block
    {
        private const float tileSize = 0.25f;
        public virtual BoundingBox BoundingBox { get { return new BoundingBox(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f)); } }

        public virtual void GetMeshData(MeshData data, Chunk chunk, int x, int y, int z)
        {
            if (!chunk.GetBlock(x, y + 1, z).Block.IsSolid(Direction.Down))
            {
                GetFaceUpData(data, chunk, x, y, z);
            }

            if (!chunk.GetBlock(x, y - 1, z).Block.IsSolid(Direction.Up))
            {
                GetFaceDownData(data, chunk, x, y, z);
            }

            if (!chunk.GetBlock(x, y, z + 1).Block.IsSolid(Direction.Forward))
            {
                GetFaceBackwardData(data, chunk, x, y, z);
            }

            if (!chunk.GetBlock(x, y, z - 1).Block.IsSolid(Direction.Backward))
            {
                GetFaceForwardData(data, chunk, x, y, z);
            }

            if (!chunk.GetBlock(x + 1, y, z).Block.IsSolid(Direction.Left))
            {
                GetFaceRightData(data, chunk, x, y, z);
            }

            if (!chunk.GetBlock(x - 1, y, z).Block.IsSolid(Direction.Right))
            {
                GetFaceLeftData(data, chunk, x, y, z);
            }
        }

        protected virtual void GetFaceDownData(MeshData data, Chunk chunk, int x, int y, int z)
        {
            //MeshData data = new MeshData();

            //data.AddTriangle(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            //data.AddTriangle(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            data.Vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            data.AddQuadIndices();
            data.UV.AddRange(GetFaceUV(Direction.Down));
        }

        protected virtual void GetFaceUpData(MeshData data, Chunk chunk, int x, int y, int z)
        {
            //MeshData data = new MeshData();

            data.Vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

            data.AddQuadIndices();
            data.UV.AddRange(GetFaceUV(Direction.Up));
        }

        protected virtual void GetFaceBackwardData(MeshData data, Chunk chunk, int x, int y, int z)
        {
            //MeshData data = new MeshData();

            data.Vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            data.AddQuadIndices();
            data.UV.AddRange(GetFaceUV(Direction.Backward));
        }

        protected virtual void GetFaceRightData(MeshData data, Chunk chunk, int x, int y, int z)
        {
            //MeshData data = new MeshData();

            data.Vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            data.AddQuadIndices();
            data.UV.AddRange(GetFaceUV(Direction.Right));
        }

        protected virtual void GetFaceForwardData(MeshData data, Chunk chunk, int x, int y, int z)
        {
            //MeshData data = new MeshData();

            data.Vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

            data.AddQuadIndices();
            data.UV.AddRange(GetFaceUV(Direction.Forward));
        }

        protected virtual void GetFaceLeftData(MeshData data, Chunk chunk, int x, int y, int z)
        {
            //MeshData data = new MeshData();

            data.Vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            data.Vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

            data.AddQuadIndices();
            data.UV.AddRange(GetFaceUV(Direction.Left));
        }

        public virtual Point GetTexturePosition(Direction faceDirection)
        {
            return new Point(0, 3);
        }

        public virtual Vector2[] GetFaceUV(Direction faceDirection)
        {
            Vector2[] UVs = new Vector2[4];
            Point tilePos = GetTexturePosition(faceDirection);

            UVs[0] = new Vector2(tileSize * tilePos.X, tileSize * tilePos.Y + tileSize);
            UVs[1] = new Vector2(tileSize * tilePos.X, tileSize * tilePos.Y);
            UVs[2] = new Vector2(tileSize * tilePos.X + tileSize, tileSize * tilePos.Y);
            UVs[3] = new Vector2(tileSize * tilePos.X + tileSize, tileSize * tilePos.Y + tileSize);

            return UVs;
        }

        public virtual bool IsSolid(Direction direction)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collider"></param>
        /// <returns> returns Collision state or null if collision handling should be redirected to collider</returns>
        public virtual Collision CheckCollision(ICollidable collider)
        {
            return null;
        }
    }
}
