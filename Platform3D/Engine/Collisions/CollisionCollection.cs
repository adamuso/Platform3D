using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DPlatformer.Engine.Collisions
{
    public class CollisionCollection : IEnumerable<Collision>
    {
        private List<Collision> collisions;
        public bool Collided { get { return collisions.Count > 0; } }

        public CollisionCollection(IEnumerable<Collision> collisions)
        {
            this.collisions = new List<Collision>(collisions);
        }

        #region IEnumerable
        public IEnumerator<Collision> GetEnumerator()
        {
            return this.collisions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.collisions.GetEnumerator();
        }
        #endregion
    }
}
