using System.Collections.Generic;

namespace algorithms.MapRepresentation
{
    public interface IBoard
    {
        int Width { get; set; }

        int Height { get; set; }

        bool IsTraversable(Location location);

        bool IsTraversable(int x, int y);

        List<Location> GetNeighbors(Location location);

        List<Location> GetNeighbors(int x, int y);
    }
}