using algorithms.MapRepresentation;
using System.Collections.Generic;
using System.Linq;

namespace algorithms.PathFinding
{
    public class AStar
    {
        private Map _map;
        private int _width;
        private int _height;

        public AStar(Map map)
        {
            _map = map;
            _width = map.Width;
            _height = map.Height;
        }

        public List<Location> FindPath(Location start, Location end)
        {
            var closedSet = new bool[_width, _height];
            var cameFrom = new Location[_width, _height];
            var gScore = new int[_width, _height];

            var openSet = new Queue<Location>();
            openSet.Enqueue(start);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    cameFrom[x, y] = null;
                    gScore[x, y] = int.MaxValue;
                }
            }

            gScore[start.X, start.Y] = 0;

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                // If we found a path from the start to the end, reconstruct the path and return it
                if (current.X == end.X && current.Y == end.Y)
                {
                    var path = new List<Location>();
                    path.Add(current);

                    while (cameFrom[current.X, current.Y] != null)
                    {
                        current = cameFrom[current.X, current.Y];
                        path.Add(current);
                    }

                    path.Reverse();

                    return path;
                }

                closedSet[current.X, current.Y] = true;

                foreach (var neighbor in _map.GetNeighbors(current))
                {
                    if (!_map.IsTraversable(neighbor))
                    {
                        continue;
                    }

                    if (closedSet[neighbor.X, neighbor.Y])
                    {
                        continue;
                    }

                    // The distance from start to a neighbor
                    var tentative_gScore = gScore[current.X, current.Y] + 1;

                    // Discover a new node
                    if (!openSet.Any(s => s.X == neighbor.X && s.Y == neighbor.Y))
                    {
                        openSet.Enqueue(neighbor);
                    }
                    else if (tentative_gScore >= gScore[neighbor.X, neighbor.Y])
                    {
                        continue;
                    }

                    // Best path so far
                    cameFrom[neighbor.X, neighbor.Y] = current;
                    gScore[neighbor.X, neighbor.Y] = tentative_gScore;
                }
            }

            return new List<Location>();
        }
    }
}