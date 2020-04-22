namespace algorithms.MapRepresentation
{
    internal class MoveDelta
    {
        public int DX { get; set; }
        public int DY { get; set; }

        public MoveDelta(int dx, int dy)
        {
            DX = dx;
            DY = dy;
        }
    }
}