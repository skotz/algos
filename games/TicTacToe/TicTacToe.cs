using algorithms.MapRepresentation;
using System.Collections.Generic;

namespace games.TicTacToe
{
    public class TicTacToe : IState
    {
        public Player PlayerToMove { get; set; }

        public Player PreviousPlayerToMove { get; private set; }

        public Player[,] Board { get; set; }

        public TicTacToe()
        {
            Board = new Player[3, 3];
            PlayerToMove = Player.One;
        }

        public IState Clone()
        {
            var copy = (TicTacToe)MemberwiseClone();
            copy.Board = (Player[,])Board.Clone();
            return copy;
        }

        public List<Move> GetAllMoves()
        {
            var moves = new List<Move>();

            if (GetWinner() == Player.None)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        if (Board[x, y] == Player.None)
                        {
                            moves.Add(new Move(x, y));
                        }
                    }
                }
            }

            return moves;
        }

        public Player GetWinner()
        {
            // Rows
            for (var y = 0; y < 3; y++)
            {
                if (Board[0, y] != Player.None && Board[0, y] == Board[1, y] && Board[1, y] == Board[2, y])
                {
                    return Board[0, y];
                }
            }

            // Columns
            for (var x = 0; x < 3; x++)
            {
                if (Board[x, 0] != Player.None && Board[x, 0] == Board[x, 1] && Board[x, 1] == Board[x, 2])
                {
                    return Board[x, 0];
                }
            }

            // Diagonals
            if (Board[1, 1] != Player.None)
            {
                if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
                {
                    return Board[1, 1];
                }
                if (Board[2, 0] == Board[1, 1] && Board[1, 1] == Board[0, 2])
                {
                    return Board[1, 1];
                }
            }

            return Player.None;
        }

        public void MakeMove(Move move)
        {
            Board[move.X, move.Y] = PlayerToMove;
            PreviousPlayerToMove = PlayerToMove;
            PlayerToMove = PlayerToMove == Player.One ? Player.Two : Player.One;
        }

        public static TicTacToe FromString(string[] map, Player toMove)
        {
            var ttt = new TicTacToe();
            ttt.PlayerToMove = toMove;
            ttt.PreviousPlayerToMove = toMove == Player.Two ? Player.One : Player.Two;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    ttt.Board[x, y] = map[y][x] == 'X' ? Player.One : (map[y][x] == 'O' ? Player.Two : Player.None);
                }
            }

            return ttt;
        }

        public int Evaluate()
        {
            var score = 0;

            // Value winning
            var winner = GetWinner();
            score += winner == Player.One ? 100 : 0;
            score += winner == Player.Two ? -100 : 0;

            // Value winning fast
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (Board[x, y] == Player.None)
                    {
                        score++;
                    }
                }
            }

            return score;
        }
    }
}