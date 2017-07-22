using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.Primitives
{
    public class Plane : DrawableGameComponent
    {
        private float x, y, z, width, length;
        private Game3DPlatformer game;

        private static VertexBuffer vertexTemplate;
        private static VertexPositionNormalTexture[] vertices;
        private Texture2D test;

        static Plane()
        {
            vertices = new VertexPositionNormalTexture[4];

            vertices[0] = new VertexPositionNormalTexture(new Vector3(-1, 0, -1), Vector3.Up, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(1, 0, -1), Vector3.Up, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(-1, 0, 1), Vector3.Up, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(new Vector3(1, 0, 1), Vector3.Up, new Vector2(1, 1));
        }

        public Plane(Game3DPlatformer game, float x, float y, float z, float width, float length)
            : base(game)
        {
            this.game = game;

            if(vertexTemplate == null)
            {
                vertexTemplate = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
                vertexTemplate.SetData(vertices);

                test = new Texture2D(GraphicsDevice, 3, 2);
                test.SetData<Color>(new Color[] { Color.Red, Color.Blue, Color.Cyan, Color.Green, Color.Yellow, Color.Magenta});
            }

            this.x = x;
            this.y = y;
            this.z = z;
            this.width = width;
            this.length = length;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.SetVertexBuffer(vertexTemplate);
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            game.BasicEffect.World = Matrix.CreateScale(width, 1, length) *
                                     Matrix.CreateTranslation(x, y, z);

            game.BasicEffect.Texture = test;
            game.BasicEffect.TextureEnabled = true;
            game.BasicEffect.VertexColorEnabled = false;

            foreach(EffectPass pass in game.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            }

            game.BasicEffect.TextureEnabled = false;
            game.BasicEffect.VertexColorEnabled = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
