using algorithms.MapRepresentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace algorithms.TreeSearching
{
    public class MonteCarloTreeSearch
    {
        private Random random;
        private int simulationCount;
        private int visitedNodes;
        private bool debugMode;
        private string debugStatus;

        private SearchCancellationToken cancel;

        public MonteCarloTreeSearch()
            : this(false)
        {
        }

        public MonteCarloTreeSearch(bool debug)
        {
            random = new Random();
            debugMode = debug;
        }

        public SearchResult GetBestMove(IState state, SearchCancellationToken token)
        {
            var timer = Stopwatch.StartNew();

            visitedNodes = 0;
            simulationCount = 0;

            cancel = token;

            var best = GetBestMove(state);

            return new SearchResult
            {
                BestMove = best,
                Evaluations = visitedNodes,
                Simulations = simulationCount,
                Milliseconds = timer.ElapsedMilliseconds,
                DebugStatus = debugStatus
            };
        }

        protected virtual List<Move> GetMoves(IState state)
        {
            return state.GetAllMoves();
        }

        private Move GetBestMove(IState rootState)
        {
            var rootNode = new MonteCarloTreeSearchNode(rootState, GetMoves);

            while (!cancel.Cancelled)
            {
                var node = rootNode;
                var state = rootState.Clone();

                simulationCount++;

                // Select
                while (node.Untried.Count == 0 && node.Children.Count > 0)
                {
                    node = node.SelectChild();
                    state.MakeMove(node.Move);
                    visitedNodes++;
                }

                // Expand
                if (node.Untried.Count > 0)
                {
                    var move = node.Untried[random.Next(node.Untried.Count)];
                    state.MakeMove(move);
                    node = node.AddChild(state, move);
                    visitedNodes++;
                }

                // Simulate
                while (state.GetWinner() == Player.None)
                {
                    var moves = state.GetAllMoves();
                    if (moves.Count == 0)
                    {
                        break;
                    }
                    state.MakeMove(moves[random.Next(moves.Count)]);
                    visitedNodes++;
                }

                // Backpropagate
                while (node != null)
                {
                    var winner = state.GetWinner();
                    node.Update(winner == node.LastToMove ? 1.0 : (winner == Player.None ? 0.5 : 0.0));
                    node = node.Parent;
                    visitedNodes++;
                }
            }

            if (debugMode)
            {
                var builder = new StringBuilder();
                DebugTree(builder, rootNode, 0, 5);
                debugStatus = builder.ToString();
            }

            return rootNode.Children.OrderBy(x => x.Visits).LastOrDefault().Move;
        }

        private void DebugTree(StringBuilder sb, MonteCarloTreeSearchNode node, int indent, int max)
        {
            foreach (var c in node.Children.OrderByDescending(x => x.Visits).ThenByDescending(x => x.Wins))
            {
                sb.Append(new string(' ', indent * 4));
                sb.AppendLine(c.Move?.X + "," + c.Move?.Y + " (" + c.Wins + "/" + c.Visits + ")");

                if (indent < max)
                {
                    DebugTree(sb, c, indent + 1, max);
                }
            }
        }

        private class MonteCarloTreeSearchNode
        {
            private Func<IState, List<Move>> _getMoves;

            public double Wins;
            public double Visits;
            public MonteCarloTreeSearchNode Parent;
            public Player LastToMove;
            public Move Move;
            public List<MonteCarloTreeSearchNode> Children;
            public List<Move> Untried;

            public MonteCarloTreeSearchNode(IState state, Func<IState, List<Move>> getMoves)
                : this(state, null, null, getMoves)
            {
            }

            public MonteCarloTreeSearchNode(IState state, Move move, MonteCarloTreeSearchNode parent, Func<IState, List<Move>> getMoves)
            {
                _getMoves = getMoves;

                Move = move;
                Parent = parent;

                Children = new List<MonteCarloTreeSearchNode>();
                Wins = 0.0;
                Visits = 0.0;

                if (state != null)
                {
                    Untried = _getMoves(state);
                    LastToMove = state.PreviousPlayerToMove;
                }
                else
                {
                    Untried = new List<Move>();
                }
            }

            public MonteCarloTreeSearchNode SelectChild()
            {
                return Children.OrderBy(x => UpperConfidenceBound(x)).LastOrDefault();
            }

            public MonteCarloTreeSearchNode AddChild(IState state, Move move)
            {
                var newNode = new MonteCarloTreeSearchNode(state, move, this, _getMoves);
                Untried.Remove(move);
                Children.Add(newNode);
                return newNode;
            }

            public void Update(double result)
            {
                Visits++;
                Wins += result;
            }

            private double UpperConfidenceBound(MonteCarloTreeSearchNode node)
            {
                return node.Wins / node.Visits + Math.Sqrt(2.0 * Math.Log(Visits) / node.Visits);
            }
        }
    }
}