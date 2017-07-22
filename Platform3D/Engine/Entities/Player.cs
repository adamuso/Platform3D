using _3DPlatformer.Engine.Collisions;
using _3DPlatformer.Engine.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DPlatformer.Engine.World;
using _3DPlatformer.Engine.World.Blocks;

namespace _3DPlatformer.Engine.Entities
{
    public class Player : MovingEntity, ISimpleCollidable
    {
        private Cuboid model;

        public BoundingBox BoundingBox { get { return new BoundingBox(Position - model.Size / 2, Position + model.Size / 2); } }

        public Player()
        {
            this.Position = new Vector3(0, 10, 0);
            this.model = new Cuboid(Game3DPlatformer.Instance, 0, 2, 0, 0.8f, 2, 0.8f);

            this.Acceleration = new Vector3(0, -20f, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 lastPosition = this.Position;

            this.Velocity += new Vector3(Game.InputManager.XAxis * 0.5f, 0, Game.InputManager.ZAxis * 0.5f);

            if(Game.InputManager.ShouldJump)
            {
                this.Velocity += new Vector3(0, 10f, 0);
            }

            this.Velocity *= 0.92f * Vector3.UnitX + Vector3.UnitY + 0.92f * Vector3.UnitZ;

            this.Position += Velocity * Vector3.UnitY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(Game.CollisionManager.GetCollisions(this).Collided)
            {
                this.Position = new Vector3(Position.X, lastPosition.Y, Position.Z);
                this.Velocity = new Vector3(this.Velocity.X, 0, this.Velocity.Z);
            }

            this.Position += Velocity * Vector3.UnitX * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Game.CollisionManager.GetCollisions(this).Collided)
            {
                this.Position = new Vector3(lastPosition.X, Position.Y, Position.Z);
                this.Velocity = new Vector3(0, this.Velocity.Y, this.Velocity.Z);
            }

            this.Position += Velocity * Vector3.UnitZ * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Game.CollisionManager.GetCollisions(this).Collided)
            {
                this.Position = new Vector3(Position.X, Position.Y, lastPosition.Z);
                this.Velocity = new Vector3(this.Velocity.X, this.Velocity.Y, 0);
            }

            base.Update(gameTime);
        }

        public Collision CheckCollision(ICollidable collider)
        {
            if(collider is World.World)
            {
                World.World world = (World.World)collider;

                int sx = (int)this.BoundingBox.Min.X - 1,
                    ex = (int)this.BoundingBox.Max.X + 1,
                    sy = (int)this.BoundingBox.Min.Y - 1,
                    ey = (int)this.BoundingBox.Max.Y + 1,
                    sz = (int)this.BoundingBox.Min.Z - 1,
                    ez = (int)this.BoundingBox.Max.Z + 1;
                
                for(int z = sz; z <= ez; z++)
                {
                    for (int y = sy; y <= ey; y++)
                    {
                        for (int x = sx; x <= ex; x++)
                        {
                            Collision collision;

                            if (!(world[x, y, z] is BlockAir) && (collision = Game.CollisionManager.GetCollision(world.GetBlock(x, y, z), this)) != null)
                            {
                                return collision;
                            }
                        }
                    }
                }

                return null;
            }

            return new Collision(collider);
        }

        public override void Draw(GameTime gameTime)
        {
            model.Position = this.Position;
            model.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
