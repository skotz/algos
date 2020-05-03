using System.Collections.Generic;

namespace algorithms.MapRepresentation
{
    /// <summary>
    /// Represents the state of a game at a given point in time
    /// </summary>
    public interface IState
    {
        Player PlayerToMove { get; set; }

        Player PreviousPlayerToMove { get; }

        IState Clone();

        List<Move> GetAllMoves();

        void MakeMove(Move move);

        Player GetWinner();
    }
}