using _3DPlatformer.Engine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.Entities
{
    public class EntityManager : DrawableGameComponent
    {
        private List<Entity> entities;
        public new Game3DPlatformer Game { get { return (Game3DPlatformer)base.Game; } }

        public EntityManager()
            : base(Game3DPlatformer.Instance)
        {
            entities = new List<Entity>();
        }

        public void Add(Entity entity)
        {
            entities.Add(entity);
        }

        public List<T> GetEntities<T>()
        {
            return entities.OfType<T>().ToList();
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++ )
            {
                entities[i].Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }

            base.Update(gameTime);
        }
    }
}
