using System.Collections.Generic;

namespace algorithms.MapRepresentation
{
    public class OceanNavigator : IBoard
    {
        private List<MoveDelta> moveDeltas;

        public Tile this[int x, int y]
        {
            get
            {
                return Tiles[x, y];
            }
            set
            {
                Tiles[x, y] = value;
            }
        }

        public Tile[,] Tiles { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public OceanNavigator(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];

            moveDeltas = new List<MoveDelta>()
            {
                new MoveDelta(-1, 0),
                new MoveDelta(1, 0),
                new MoveDelta(0, -1),
                new MoveDelta(0, 1)
            };
        }

        public bool IsTraversable(Location location)
        {
            return IsTraversable(location.X, location.Y);
        }

        public bool IsTraversable(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && Tiles[x, y].Traversable;
        }

        public List<Location> GetNeighbors(Location location)
        {
            return GetNeighbors(location.X, location.Y);
        }

        public List<Location> GetNeighbors(int x, int y)
        {
            var neighbors = new List<Location>();

            foreach (var delta in moveDeltas)
            {
                if (IsTraversable(x + delta.DX, y + delta.DY))
                {
                    neighbors.Add(new Location(x + delta.DX, y + delta.DY));
                }
            }

            return neighbors;
        }

        public static OceanNavigator FromString(string[] map, char traversable = ' ')
        {
            var w = map[0].Length;
            var h = map.Length;
            var tiles = new Tile[w, h];

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    tiles[x, y] = new Tile(map[y][x] == traversable);
                }
            }

            return new OceanNavigator(w, h)
            {
                Tiles = tiles
            };
        }
    }
};