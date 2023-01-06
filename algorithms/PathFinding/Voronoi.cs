using algorithms.MapRepresentation;
using System.Collections.Generic;
using System.Linq;

namespace algorithms.PathFinding
{
    public class Voronoi
    {
        private IBoard _map;
        private int _width;
        private int _height;

        private List<VoronoiQueue> _queues;

        public Voronoi(IBoard map)
        {
            _map = map;
            _width = map.Width;
            _height = map.Height;

            _queues = new List<VoronoiQueue>();
        }

        public void Add(Player player, int x, int y)
        {
            var queue = new VoronoiQueue(player, x, y);
            queue.Enqueue(new int[] { x, y, 0 });

            _queues.Add(queue);
        }

        public void Add(Player player, Location location)
        {
            Add(player, location.X, location.Y);
        }

        public Player[,] Fill()
        {
            var voronoi = new Player[_width, _height];
            var visited = new bool[_width, _height];

            var nextDist = 0;

            while (_queues.Any(x => x.Count > 0))
            {
                if (_queues.All(x => x.DoneAtDistance))
                {
                    _queues.ForEach(x => x.DoneAtDistance = false);
                    nextDist++;
                }

                for (int q = 0; q < _queues.Count; q++)
                {
                    if (_queues[q].Count > 0)
                    {
                        if (_queues[q].Peek()[2] > nextDist)
                        {
                            _queues[q].DoneAtDistance = true;
                        }
                        else
                        {
                            var point = _queues[q].Dequeue();

                            if (point.Length == 5)
                            {
                                if (voronoi[point[3], point[4]] == Player.Both)
                                {
                                    continue;
                                }
                            }

                            if (visited[point[0], point[1]])
                            {
                                if (voronoi[point[0], point[1]] != _queues[q].Player)
                                {
                                    voronoi[point[0], point[1]] = Player.Both;
                                }

                                continue;
                            }

                            voronoi[point[0], point[1]] = _queues[q].Player;
                            visited[point[0], point[1]] = true;

                            foreach (var neighbor in _map.GetNeighbors(point[0], point[1]))
                            {
                                if (_map.IsTraversable(neighbor) && !visited[neighbor.X, neighbor.Y])
                                {
                                    _queues[q].Enqueue(new int[] {
                                        neighbor.X,
                                        neighbor.Y,
                                        nextDist + 1,
                                        point[0],
                                        point[1],
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
                        _queues[q].DoneAtDistance = true;
                    }
                }
            }

            return voronoi;
        }
    }
}