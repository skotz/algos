using algorithms.MapRepresentation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace algorithms.TreeSearching
{
    public class BeamSearch
    {
        private int _visitedNodes;
        private int _beamWidth;

        private SearchCancellationToken cancel;

        public BeamSearch(int beamWidth)
        {
            _beamWidth = beamWidth;
        }

        public SearchResult GetBestMove(IState state, SearchCancellationToken token)
        {
            var timer = Stopwatch.StartNew();

            _visitedNodes = 0;

            cancel = token;

            var best = GetBestMove(state);

            return new SearchResult
            {
                BestMove = best,
                Evaluations = _visitedNodes,
                Milliseconds = timer.ElapsedMilliseconds
            };
        }

        private Move GetBestMove(IState start)
        {
            var root = new BeamNode(null, null, start, 0, 0);

            var set = new List<BeamNode>();
            var beam = new List<BeamNode>();
            beam.Add(root);

            var startingPlayer = start.PlayerToMove;
            var terminateSearch = false;

            while (beam.Count != 0 && !terminateSearch)
            {
                set.Clear();

                var playerToMove = Player.None;
                if (beam.Count > 0)
                {
                    playerToMove = beam[0].State.PlayerToMove;
                }

                for (int i = 0; i < beam.Count; i++)
                {
                    _visitedNodes++;

                    var node = beam[i];

                    if (cancel.Cancelled)
                    {
                        terminateSearch = true;
                        break;
                    }

                    var moves = node.State.GetAllMoves();

                    foreach (var move in moves)
                    {
                        var copy = node.State.Clone();

                        copy.MakeMove(move);

                        var eval = copy.Evaluate(node.Depth + 1);
                        var winner = copy.GetWinner();

                        if (winner == startingPlayer && winner == playerToMove)
                        {
                            terminateSearch = true;
                        }

                        var child = new BeamNode(move, node, copy, node.Depth + 1, eval);

                        node.Children.Add(child);
                        set.Add(child);
                    }
                }

                beam.Clear();

                if (playerToMove == Player.One)
                {
                    beam.AddRange(set.OrderByDescending(x => x.Evaluation).Take(_beamWidth));
                }
                else
                {
                    beam.AddRange(set.OrderBy(x => x.Evaluation).Take(_beamWidth));
                }
            }

            // Perform a full minimax search, but only on the tree built out by a depth-first search
            return MiniMax(root).Move;
        }

        private MiniMaxNode MiniMax(BeamNode node)
        {
            if (node.Children.Count == 0)
            {
                return new MiniMaxNode(node.Evaluation);
            }

            var best = new MiniMaxNode(node.State.PlayerToMove == Player.One ? int.MinValue : int.MaxValue);

            foreach (var child in node.Children)
            {
                var test = MiniMax(child);

                if (node.State.PlayerToMove == Player.One)
                {
                    if (test.Evaluation > best.Evaluation)
                    {
                        best.Move = child.Move;
                        best.Evaluation = test.Evaluation;
                    }
                }
                else
                {
                    if (test.Evaluation < best.Evaluation)
                    {
                        best.Move = child.Move;
                        best.Evaluation = test.Evaluation;
                    }
                }
            }

            return best;
        }

        private class MiniMaxNode
        {
            public Move Move;

            public int Evaluation;

            public MiniMaxNode(int eval)
            {
                Evaluation = eval;
            }

            public MiniMaxNode(Move move, int eval)
            {
                Move = move;
                Evaluation = eval;
            }
        }

        private class BeamNode
        {
            public Move Move;

            public BeamNode Parent;

            public List<BeamNode> Children;

            public IState State;

            public int Depth;

            public int Evaluation;

            public BeamNode(Move move, BeamNode parent, IState state, int depth, int evaluation)
            {
                Move = move;
                Parent = parent;
                Children = new List<BeamNode>();
                State = state;
                Depth = depth;
                Evaluation = evaluation;
            }
        }
    }
}