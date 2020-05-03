using System.Collections.Generic;

namespace algorithms.MapRepresentation
{
    /// <summary>
    /// Represents a game board where actors move relative to their current location (e.g., a character on a map)
    /// </summary>
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