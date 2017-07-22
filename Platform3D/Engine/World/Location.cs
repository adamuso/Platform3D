using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPlatformer.Engine.World
{
    public struct Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public bool IsChunkBorder
        {
            get
            {
                return X % Chunk.Size == 0 || Y % Chunk.Size == 0 || Z % Chunk.Size == 0
                    || X % Chunk.Size == Chunk.Size - 1 || Y % Chunk.Size == Chunk.Size - 1 || Z % Chunk.Size == Chunk.Size - 1; 
            }
        }

        public Location(int x, int y, int z)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Chunk[] GetAdjacentChunks(World world)
        {
            List<Chunk> chunks = new List<Chunk>();

            if(X % Chunk.Size == 0)
                chunks.Add((this - new Location(1, 0, 0)).GetChunk(world));
            else if (X % Chunk.Size == Chunk.Size - 1)
                chunks.Add((this + new Location(1, 0, 0)).GetChunk(world));

            if(Y % Chunk.Size == 0)
                chunks.Add((this - new Location(0, 1, 0)).GetChunk(world));
            else if (Y % Chunk.Size == Chunk.Size - 1)
                chunks.Add((this + new Location(0, 1, 0)).GetChunk(world));

            if(Z % Chunk.Size == 0)
                chunks.Add((this - new Location(0, 0, 1)).GetChunk(world));
            else if(Z % Chunk.Size == Chunk.Size - 1)
                chunks.Add((this + new Location(0, 0, 1)).GetChunk(world));

            chunks.RemoveAll(c => c == null);

            return chunks.ToArray();
        }

        public Location GetChunkLocation()
        {
            return this / Chunk.Size;
        }

        public Chunk GetChunk(World world)
        {
            return world.GetChunk(this);
        }

        public Location AsChunkLocal()
        {
            return this % Chunk.Size;
        }

        #region Operators
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        
        public static bool operator ==(Location location, Location location2)
        {
            return location.Equals(location2);
        }

        public static bool operator !=(Location location, Location location2)
        {
            return !location.Equals(location2);
        }

        public static Location operator +(Location location, int value)
        {
            return new Location(location.X + value, location.Y + value, location.Z + value);
        }

        public static Location operator +(Location location, Location location2)
        {
            return new Location(location.X + location2.X, location.Y + location2.Y, location.Z + location2.Z);
        }

        public static Location operator -(Location location, int value)
        {
            return new Location(location.X - value, location.Y - value, location.Z - value);
        }

        public static Location operator -(Location location, Location location2)
        {
            return new Location(location.X - location2.X, location.Y - location2.Y, location.Z - location2.Z);
        }


        public static Location operator %(Location location, int value)
        {
            return new Location(location.X % value, location.Y % value, location.Z % value);
        }

        public static Location operator *(Location location, int value)
        {
            return new Location(location.X * value, location.Y * value, location.Z * value);
        }

        public static Location operator /(Location location, int value)
        {
            return new Location(location.X < 0 ? location.X / value - 1 : location.X / value,
                                location.Y < 0 ? location.Y / value - 1 : location.Y / value,
                                location.Z < 0 ? location.Z / value - 1 : location.Z / value);
        }

        public static bool operator >(Location location, Location location2)
        {
            return location.X > location2.X && location.Y > location2.Y && location.Z > location2.Z;
        }

        public static bool operator >=(Location location, Location location2)
        {
            return location.X >= location2.X && location.Y >= location2.Y && location.Z >= location2.Z;
        }

        public static bool operator <(Location location, Location location2)
        {
            return location.X < location2.X && location.Y < location2.Y && location.Z < location2.Z;
        }

        public static bool operator <=(Location location, Location location2)
        {
            return location.X <= location2.X && location.Y <= location2.Y && location.Z <= location2.Z;
        }

        public static implicit operator Vector3(Location location)
        {
            return new Vector3(location.X, location.Y, location.Z);
        }
        #endregion
    }
}
