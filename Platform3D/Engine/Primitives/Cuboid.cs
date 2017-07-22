using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.Primitives
{
    public class Cuboid : DrawableGameComponent
    {
        private static VertexBuffer vertexTemplate;
        private static VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[36];
        private static IndexBuffer boundingIndicesTemplate;
        private static short[] boundingIndices;
        private Game3DPlatformer game;

        public  Vector3 Position { get; set; }
        public Vector3 Size { get; set; }
        private static Texture2D test;

        static Cuboid()
        {
            // front
            vertices[0] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, 0, -0.5f), new Vector2(0f, 0f));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, -0.5f), new Vector2(0.333333f, 0f));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0, -0.5f), new Vector2(0f, 0.5f));

            vertices[3] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0, -0.5f), new Vector2(0.333333f, 0.5f));
            vertices[5] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, 0, -0.5f), new Vector2(0f, 0.5f));
            vertices[4] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0, 0, -0.5f), new Vector2(0.333333f, 0f));

            // back
            vertices[6] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0, 0, 0.5f), new Vector2(0.333333f, 0f));
            vertices[7] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, 0.5f), new Vector2(0.666666f, 0f));
            vertices[8] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 0, 0.5f), new Vector2(0.333333f, 0.5f));

            vertices[9] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 0, 0.5f), new Vector2(0.666666f, 0.5f));
            vertices[10] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, 0, 0.5f), new Vector2(0.333333f, 0.5f));
            vertices[11] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 0, 0.5f), new Vector2(0.666666f, 0f));

            // left
            vertices[12] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0, 0), new Vector2(0.666666f, 0f));
            vertices[13] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0, 0), new Vector2(1f, 0f));
            vertices[14] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, 0, 0), new Vector2(0.666666f, 0.5f));

            vertices[15] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, 0, 0), new Vector2(1f, 0.5f));
            vertices[16] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0, 0), new Vector2(0.666666f, 0.5f));
            vertices[17] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0, 0), new Vector2(1f, 0f));

            //right

            vertices[18] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(-0.5f, 0, 0), new Vector2(0f, 0.5f));
            vertices[20] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0, 0), new Vector2(0.333333f, 0.5f));
            vertices[19] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, 0, 0), new Vector2(0f, 1));

            vertices[21] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, 0, 0), new Vector2(0.333333f, 1));
            vertices[23] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0, 0), new Vector2(0f, 1));
            vertices[22] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-0.5f, 0, 0), new Vector2(0.333333f, 0.5f));

            // top

            vertices[24] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0, 0.5f, 0), new Vector2(0.333333f, 0.5f));
            vertices[25] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 0.5f, 0), new Vector2(0.666666f, 0.5f));
            vertices[26] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0.5f, 0), new Vector2(0.333333f, 1f));

            vertices[27] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0, 0.5f, 0), new Vector2(0.666666f, 1f));
            vertices[28] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0, 0.5f, 0), new Vector2(0.333333f, 1f));
            vertices[29] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0, 0.5f, 0), new Vector2(0.666666f, 0.5f));

            // bottom

            vertices[30] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0, -0.5f, 0), new Vector2(0.666666f, 0.5f));
            vertices[32] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, -0.5f, 0), new Vector2(1f, 0.5f));
            vertices[31] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, -0.5f, 0), new Vector2(0.666666f, 1));

            vertices[33] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0, -0.5f, 0), new Vector2(1f, 1));
            vertices[35] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0, -0.5f, 0), new Vector2(0.666666f, 1));
            vertices[34] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0, -0.5f, 0), new Vector2(1f, 0.5f));
        
        
            boundingIndices = new short[]
            {
                // front
                0, 1,
                1, 4,
                4, 2,
                2, 0,

                // back
                6, 7,
                7, 11,
                11, 8,
                8, 6,

                // sides
                0, 6,
                2, 7,
                4, 11,
                1, 8
            };
        }

        public Cuboid(Game3DPlatformer game, float x, float y, float z, float width, float height, float length)
            : base(game)
        {
            if (vertexTemplate == null)
            {
                vertexTemplate = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.WriteOnly);
                vertexTemplate.SetData(vertices);

                boundingIndicesTemplate = new IndexBuffer(GraphicsDevice, typeof(short), 24, BufferUsage.WriteOnly);
                boundingIndicesTemplate.SetData(boundingIndices);

                test = new Texture2D(GraphicsDevice, 12, 2);
                test.SetData<Color>(new Color[] { Color.Red, Color.Red, Color.Blue, Color.Blue, Color.Cyan, Color.Cyan, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Magenta, Color.Magenta, 
                                                  Color.Red, Color.Red, Color.Blue, Color.Blue, Color.Cyan, Color.Cyan, Color.Green, Color.Green, Color.Yellow, Color.Yellow, Color.Magenta, Color.Magenta });
            }
            
            this.game = game;
            this.Position = new Vector3(x, y, z);
            this.Size = new Vector3(width, height, length);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            foreach(ModelMesh mesh in Game3DPlatformer.Instance.BaseCube.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.CreateScale(this.Size / 2) *
                                   Matrix.CreateTranslation(this.Position);
                    effect.Projection = game.BasicEffect.Projection;
                    effect.View = game.BasicEffect.View;
                    effect.EnableDefaultLighting();

                    effect.Texture = test;
                    effect.TextureEnabled = true;
                }

                mesh.Draw();
            }

            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

            //GraphicsDevice.SetVertexBuffer(vertexTemplate);
            //GraphicsDevice.Indices = boundingIndicesTemplate;

            //GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            //game.BasicEffect.Texture = test;
            //game.BasicEffect.TextureEnabled = true;
            //game.BasicEffect.World = Matrix.CreateScale(this.Size) *
            //                         Matrix.CreateTranslation(this.Position);

            //foreach (EffectPass pass in game.BasicEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
            //}

            //game.BasicEffect.TextureEnabled = false;

            //game.BasicEffect.World = Matrix.CreateScale(this.Size * 1.0001f) *
            //                         Matrix.CreateTranslation(this.Position);

            //foreach(EffectPass pass in game.BasicEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, 12);
            //}

            base.Draw(gameTime);
        }
    }
}
