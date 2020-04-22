using algorithms.MapRepresentation;
using System.Collections.Generic;

namespace algorithms.PathFinding
{
    public class FloodFill
    {
        private Map _map;
        private int _width;
        private int _height;

        public FloodFill(Map map)
        {
            _map = map;
            _width = map.Width;
            _height = map.Height;
        }

        public bool[,] GetRegion(Location origin)
        {
            var size = 0;
            var filled = new bool[_width, _height];
            var stack = new Stack<Location>();
            stack.Push(origin);

            while (stack.Count > 0)
            {
                var p = stack.Pop();
                if (p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height)
                {
                    if (!filled[p.X, p.Y] && _map[p.X, p.Y])
                    {
                        filled[p.X, p.Y] = true;
                        size++;

                        if (_map.IsTraversable(p.X + 1, p.Y))
                        {
                            stack.Push(new Location(p.X + 1, p.Y));
                        }
                        if (_map.IsTraversable(p.X - 1, p.Y))
                        {
                            stack.Push(new Location(p.X - 1, p.Y));
                        }
                        if (_map.IsTraversable(p.X, p.Y + 1))
                        {
                            stack.Push(new Location(p.X, p.Y + 1));
                        }
                        if (_map.IsTraversable(p.X, p.Y - 1))
                        {
                            stack.Push(new Location(p.X, p.Y - 1));
                        }
                    }
                }
            }

            return filled;
        }

        public int GetRegionSize(Location origin)
        {
            var size = 0;
            var filled = new bool[_width, _height];
            var stack = new Stack<Location>();
            stack.Push(origin);

            while (stack.Count > 0)
            {
                var p = stack.Pop();
                if (p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height)
                {
                    if (!filled[p.X, p.Y] && _map[p.X, p.Y])
                    {
                        filled[p.X, p.Y] = true;
                        size++;

                        if (_map.IsTraversable(p.X + 1, p.Y))
                        {
                            stack.Push(new Location(p.X + 1, p.Y));
                        }
                        if (_map.IsTraversable(p.X - 1, p.Y))
                        {
                            stack.Push(new Location(p.X - 1, p.Y));
                        }
                        if (_map.IsTraversable(p.X, p.Y + 1))
                        {
                            stack.Push(new Location(p.X, p.Y + 1));
                        }
                        if (_map.IsTraversable(p.X, p.Y - 1))
                        {
                            stack.Push(new Location(p.X, p.Y - 1));
                        }
                    }
                }
            }

            return size;
        }
    }
}