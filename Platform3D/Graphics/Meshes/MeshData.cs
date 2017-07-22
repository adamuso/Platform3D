using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Graphics.Meshes
{
    public class MeshData
    {
        public List<Vector3> Vertices { get; private set; }
        public List<short> Indices { get; private set; }
        public List<Vector3> Normals { get; private set; }
        public List<Vector2> UV { get; private set; }

        public MeshData()
        {
            Vertices = new List<Vector3>();
            Indices = new List<short>();
            Normals = new List<Vector3>();
            UV = new List<Vector2>();
        }

        public void CalculateNormals()
        {
            Normals.Clear();

            Microsoft.Xna.Framework.Graphics.VertexPositionNormalTexture[] vp = new Microsoft.Xna.Framework.Graphics.VertexPositionNormalTexture[46];
            Game3DPlatformer.Instance.BaseCube.Meshes[0].MeshParts[0].VertexBuffer.GetData(vp);

            for(int i = 0; i < Indices.Count; i += 3)
            {
                Normals.Add(-Vector3.Cross(Vertices[Indices[i + 1]] - Vertices[Indices[i]], Vertices[Indices[i + 2]] - Vertices[Indices[i + 1]]));
                Normals.Add(-Vector3.Cross(Vertices[Indices[i + 1]] - Vertices[Indices[i]], Vertices[Indices[i + 2]] - Vertices[Indices[i + 1]]));

                Normals[Normals.Count - 2].Normalize();
                Normals[Normals.Count - 1].Normalize();
            }
        }

        public void AddQuadIndices()
        {
            Indices.Add((short)(Vertices.Count - 4));
            Indices.Add((short)(Vertices.Count - 2));
            Indices.Add((short)(Vertices.Count - 3));
            Indices.Add((short)(Vertices.Count - 4));
            Indices.Add((short)(Vertices.Count - 1));
            Indices.Add((short)(Vertices.Count - 2));
        }

        public static readonly MeshData Empty = new MeshData();
    }
}
