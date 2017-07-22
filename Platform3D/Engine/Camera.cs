using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DPlatformer.Engine.Entities;

namespace _3DPlatformer.Engine
{
    public class Camera : GameComponent
    {
        private Vector3 position;
        private Vector3 lookAt;
        private Entity entity;
        
        public new Game3DPlatformer Game { get { return Game3DPlatformer.Instance; } }
        public Matrix View { get { return Matrix.CreateLookAt(position, lookAt, Vector3.Up); } }
        public BoundingFrustum Frustum { get { return new BoundingFrustum(View * Game.BasicEffect.Projection); } }

        public Camera()
            : base(Game3DPlatformer.Instance)
        {

        }

        public void Follow(Entity entity)
        {
            this.entity = entity;
        }

        public void Apply(IEffectMatrices effect)
        {
            effect.View = View;
        }

        public override void Update(GameTime gameTime)
        {
            if(entity != null)
            {
                lookAt = entity.Position;

                position = lookAt + new Vector3(0, 3.5f, 7.5f);
            }

            base.Update(gameTime);
        }
    }
}
