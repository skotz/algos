namespace algorithms.MapRepresentation
{
    public class Tile
    {
        public bool Traversable { get; set; }

        public Tile(bool traversable)
        {
            Traversable = traversable;
        }

        public Tile Clone()
        {
            return new Tile(Traversable);
        }
    }
}