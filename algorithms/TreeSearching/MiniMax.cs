using algorithms.MapRepresentation;
using System.Diagnostics;

namespace algorithms.TreeSearching
{
    public class MiniMax
    {
        private int evaluations;
        private SearchCancellationToken cancel;

        public MiniMax()
        {
        }

        public SearchResult GetBestMove(IState state, int depth)
        {
            var timer = Stopwatch.StartNew();
            evaluations = 0;
            cancel = new SearchCancellationToken(() => false);

            var best = MiniMaxAlphaBeta(state, depth, int.MinValue, int.MaxValue);

            return new SearchResult
            {
                BestMove = best.Move,
                Milliseconds = timer.ElapsedMilliseconds,
                Evaluations = evaluations
            };
        }

        public SearchResult GetBestMove(IState state, SearchCancellationToken token)
        {
            var timer = Stopwatch.StartNew();
            evaluations = 0;
            cancel = token;

            var best = MiniMaxAlphaBeta(state, 2, int.MinValue, int.MaxValue);

            // Iterative deepening
            for (int i = 4; i < 100 && !cancel.Cancelled; i++)
            {
                var test = MiniMaxAlphaBeta(state, i, int.MinValue, int.MaxValue);
                if (!test.Cancelled)
                {
                    best = test;
                }
            }

            return new SearchResult
            {
                BestMove = best.Move,
                Milliseconds = timer.ElapsedMilliseconds,
                Evaluations = evaluations
            };
        }

        private MiniMaxNode MiniMaxAlphaBeta(IState state, int depth, int alpha, int beta)
        {
            if (depth <= 0)
            {
                evaluations++;
                return new MiniMaxNode(state.Evaluate());
            }

            if (cancel.Cancelled)
            {
                return new MiniMaxNode(true);
            }

            var moves = state.GetAllMoves();
            if (moves.Count == 0)
            {
                evaluations++;
                return new MiniMaxNode(state.Evaluate());
            }

            var best = new MiniMaxNode(state.PlayerToMove == Player.One ? int.MinValue : int.MaxValue);

            foreach (var move in moves)
            {
                var copy = state.Clone();
                copy.MakeMove(move);

                var test = MiniMaxAlphaBeta(copy, depth - 1, alpha, beta);

                if (test.Cancelled)
                {
                    return test;
                }

                if (state.PlayerToMove == Player.One)
                {
                    if (test.Evaluation > best.Evaluation)
                    {
                        best.Evaluation = test.Evaluation;
                        best.Move = move;
                    }
                    if (test.Evaluation > alpha)
                    {
                        alpha = test.Evaluation;
                    }
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                else
                {
                    if (test.Evaluation < best.Evaluation)
                    {
                        best.Evaluation = test.Evaluation;
                        best.Move = move;
                    }
                    if (test.Evaluation < beta)
                    {
                        beta = test.Evaluation;
                    }
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
            }

            return best;
        }

        private class MiniMaxNode
        {
            public Move Move { get; set; }

            public int Evaluation { get; set; }

            public bool Cancelled { get; set; }

            public MiniMaxNode(int eval)
            {
                Evaluation = eval;
            }

            public MiniMaxNode(Move move)
            {
                Move = move;
            }

            public MiniMaxNode(bool cancel)
            {
                Cancelled = cancel;
            }
        }
    }
}