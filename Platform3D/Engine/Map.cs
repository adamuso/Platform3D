using _3DPlatformer.Engine.Collisions;
using _3DPlatformer.Engine.Primitives;
using _3DPlatformer.Graphics.Vertices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine
{
    public class Map : DrawableGameComponent, ICollidable
    {
        private MapPart[,,] map;
        private VertexBufferBinding[] bindings;
        private DynamicVertexBuffer parts;
        private int partCount;

        public MapPart this[int x, int y, int z] { get { return IsValidCoordinate(x + Width / 2, y + Height / 2, z + Length / 2) ? map[x + Width / 2, y + Height / 2, z + Length / 2] : MapPart.Empty; } }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Length { get; private set; }
        public new Game3DPlatformer Game { get { return Game3DPlatformer.Instance; } }
        
        Random r = new Random();

        private Texture2D test;

        public Map(int width, int height, int length)
            : base(Game3DPlatformer.Instance)
        {
            this.Width = width;
            this.Height = height;
            this.Length = length;
            this.map = new MapPart[width, height, length];

            test = new Texture2D(GraphicsDevice, 12, 2);
            test.SetData<Color>(new Color[] { Color.Red, Color.Red, Color.Blue, Color.Blue, Color.Cyan, Color.Cyan, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Magenta, Color.Magenta, 
                                                  Color.Red, Color.Red, Color.Blue, Color.Blue, Color.Cyan, Color.Cyan, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Magenta, Color.Magenta });

            for (int z = 0; z < Length; z++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (y > 1 && y <= 2 && r.Next(0, 20) == 0)
                        {
                            map[x, y, z] = new MapPart(this)
                            {
                                X = x - Width / 2,
                                Y = y - Height / 2,
                                Z = z - Length / 2,
                                BoundingBox = new BoundingBox(new Vector3(x - Width / 2 - 0.5f, y - Height / 2 - 0.5f, z - Length / 2 - 0.5f),
                                                              new Vector3(x - Width / 2 + 0.5f, y - Height / 2 + 0.6f, z - Length / 2 + 0.5f)),
                                IsEmpty = y > 2
                            };
                        }
                        else
                        {
                            map[x, y, z] = new MapPart(this)
                            {
                                X = x - Width / 2,
                                Y = y - Height / 2,
                                Z = z - Length / 2,
                                BoundingBox = new BoundingBox(new Vector3(x - Width / 2 - 0.5f, y - Height / 2 - 0.5f, z - Length / 2 - 0.5f),
                                                              new Vector3(x - Width / 2 + 0.5f, y - Height / 2 + 0.6f, z - Length / 2 + 0.5f)),
                                IsEmpty = y > 1
                            };
                        }
                    }
                }
            }

            GenerateMapVoxels();

            bindings = new VertexBufferBinding[2];
            bindings[0] = new VertexBufferBinding(Game.BaseCube.Meshes[0].MeshParts[0].VertexBuffer);
            bindings[1] = new VertexBufferBinding(parts, 0, 1);
        }

        public bool IsValidCoordinate(int x, int y, int z)
        {
            return x >= 0 && y >= 0 && z >= 0 && x < Width && y < Height && z < Length;
        }

        public void GenerateMapVoxels()
        {
            if (parts != null)
                parts.Dispose();

            List<MapPosition> data = new List<MapPosition>();

            for (int z = 0; z < Length; z++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        MapPart part = map[x, y, z];

                        if (!part.IsEmpty)
                        {
                            data.Add(new MapPosition(new Vector3(part.X * 2, part.Y * 2, part.Z * 2)));
                        }
                    }
                }
            }

            partCount = data.Count;
            parts = new DynamicVertexBuffer(GraphicsDevice, typeof(MapPosition), Width * Height * Length, BufferUsage.WriteOnly);
            parts.SetData(data.ToArray());
        }

        public override void Draw(GameTime gameTime)
        {
            //Cuboid voxel = new Cuboid(Game3DPlatformer.Instance, 0, 0, 0, 1, 1, 1);
            //GenerateMapVoxels();

            GraphicsDevice.Indices = Game.BaseCube.Meshes[0].MeshParts[0].IndexBuffer;
            GraphicsDevice.SetVertexBuffers(bindings);

            //Game.BasicEffect.EnableDefaultLighting();

            Game.CubeEffect.Texture = test;
            //Game.CubeEffect.TextureEnabled = true;

            Game.CubeEffect.CurrentTechnique.Passes[0].Apply();
            //GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, 46, 0, 20, partCount);
            GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, 20, partCount);


            //for (int z = 0; z < Length; z++)
            //{
            //    for (int y = 0; y < Height; y++)
            //    {
            //        for (int x = 0; x < Width; x++)
            //        {
            //            if (!map[x, y, z].IsEmpty && Game.Camera.Frustum.Intersects(map[x, y, z].BoundingBox) && !map[x, y, z].IsCovered)
            //            {
            //                voxel.Position = (map[x, y, z].BoundingBox.Min + map[x, y, z].BoundingBox.Max) / 2 ;
            //                voxel.Draw(gameTime);
            //            }
            //        }
            //    }
            //}

            base.Draw(gameTime);
        }

        public Collision CheckCollision(ICollidable collider)
        {
            if(collider is ISimpleCollidable)
            {
                return collider.CheckCollision(this);
            }

            return null;
        }
    }
}
