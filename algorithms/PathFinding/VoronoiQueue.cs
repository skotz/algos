using algorithms.MapRepresentation;
using System.Collections.Generic;

namespace algorithms.PathFinding
{
    internal class VoronoiQueue : Queue<int[]>
    {
        public int X;
        public int Y;
        public Player Player;
        public bool DoneAtDistance;

        public VoronoiQueue(Player player, int x, int y)
        {
            Player = player;
            X = x;
            Y = y;
        }
    }
}