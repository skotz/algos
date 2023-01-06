using algorithms.MapRepresentation;
using System.Collections.Generic;

namespace algorithms.PathFinding
{
    public class FloodFill
    {
        private IBoard _map;
        private int _width;
        private int _height;

        public FloodFill(IBoard map)
        {
            _map = map;
            _width = map.Width;
            _height = map.Height;
        }

        public bool[,] GetRegion(Location origin)
        {
            Fill(origin, out bool[,] filled, out _);
            return filled;
        }

        public int GetRegionSize(Location origin)
        {
            Fill(origin, out _, out int size);
            return size;
        }

        private void Fill(Location origin, out bool[,] filled, out int size)
        {
            size = 0;
            filled = new bool[_width, _height];

            if (!_map.IsTraversable(origin))
            {
                return;
            }

            var stack = new Stack<Location>();
            stack.Push(origin);

            while (stack.Count > 0)
            {
                var point = stack.Pop();

                if (!filled[point.X, point.Y])
                {
                    filled[point.X, point.Y] = true;
                    size++;

                    foreach (var neighbor in _map.GetNeighbors(point))
                    {
                        if (_map.IsTraversable(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }
        }
    }
}