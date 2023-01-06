using System;

namespace algorithms.MapRepresentation
{
    public class Location
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double EuclideanDistanceTo(int x, int y)
        {
            return Math.Sqrt((x - X) * (x - X) + (y - Y) * (y - Y));
        }

        public int ManhattanDistanceTo(int x, int y)
        {
            return Math.Abs(x - X) + Math.Abs(y - Y);
        }

        public double EuclideanDistanceTo(Location other)
        {
            return EuclideanDistanceTo(other.X, other.Y);
        }

        public int ManhattanDistanceTo(Location other)
        {
            return ManhattanDistanceTo(other.X, other.Y);
        }
    }
}