using algorithms.MapRepresentation;

namespace algorithms.TreeSearching
{
    public class SearchResult
    {
        public Move BestMove { get; set; }

        public int Evaluations { get; set; }

        public int Simulations { get; set; }

        public long Milliseconds { get; set; }

        public string DebugStatus { get; set; }
    }
}