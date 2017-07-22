using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Graphics.Effects
{
    public class CubeEffect : Effect, IEffectMatrices
    {
        public Matrix World { get { return Parameters["World"].GetValueMatrix(); } set { Parameters["World"].SetValue(value); WorldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(value)); } }
        public Matrix View { get { return Parameters["View"].GetValueMatrix(); } set { Parameters["View"].SetValue(value); } }
        public Matrix Projection { get { return Parameters["Projection"].GetValueMatrix(); } set { Parameters["Projection"].SetValue(value); } }
        
        private Matrix WorldInverseTransposeMatrix { set { Parameters["WorldInverseTranspose"].SetValue(value); } }

        public Texture2D Texture { get { return Parameters["Texture"].GetValueTexture2D(); } set { Parameters["Texture"].SetValue(value); } }

        public CubeEffect(Effect baseEffect)
            : base(baseEffect)
        {

        }
    }
}
