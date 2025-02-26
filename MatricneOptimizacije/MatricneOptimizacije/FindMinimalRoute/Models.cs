using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricneOptimizacije.FindMinimalRoute
{
    public class Models
    {
        #region models
        public enum Direction
        {
            Right = 1,
            Down = 2,
            Left = 3,
            Up = 4
        }

        public record Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override string ToString()
            {
                return $"[{X},{Y}]";
            }
        }


        public class RouteWithSum
        {
            public required Path Path { get; set; }
            public double Sum { get; set; }

            public override bool Equals(object? obj) // Fixing CS8765 by allowing obj to be nullable
            {
                if (obj is not RouteWithSum other) return false;
                return Path.Equals(other.Path) && Sum == other.Sum;
            }

            public override int GetHashCode() => HashCode.Combine(Path, Sum);
        }

        public class Path
        {
            public required Point[] Points { get; set; }

            public override bool Equals(object? obj)
            {
                if (obj is not Path other) return false;
                return Points.SequenceEqual(other.Points);
            }

            public override int GetHashCode() => HashCode.Combine(Points);
        }

        #endregion
    }
}
