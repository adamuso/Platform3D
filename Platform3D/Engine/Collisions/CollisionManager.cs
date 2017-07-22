using _3DPlatformer.Engine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.Collisions
{
    public class CollisionManager
    {
        private EntityManager entityManager;
        public Game3DPlatformer Game { get { return (Game3DPlatformer)entityManager.Game; } }

        public CollisionManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        public CollisionCollection GetCollisions(ICollidable collider)
        {
            List<ICollidable> collidables = entityManager.GetEntities<ICollidable>();
            List<Collision> result = new List<Collision>();

            collidables.Add(Game.World);

            foreach(ICollidable collidable in collidables)
            {
                Collision collision;

                if (collidable != collider)
                {
                    if ((collision = GetCollision(collider, collidable)) != null)
                    {
                        result.Add(collision);
                    }
                }
            }

            return new CollisionCollection(result);
        }

        public Collision GetCollision(ICollidable collider, ICollidable collidable)
        {
            if (collider is ISimpleCollidable && collidable is ISimpleCollidable)
            {
                if (((ISimpleCollidable)collider).BoundingBox.Intersects(((ISimpleCollidable)collidable).BoundingBox))
                {
                    return collidable.CheckCollision(collider);
                }
            }
            else
            {
                return collidable.CheckCollision(collider);
            }

            return null;
        }
    }
}
