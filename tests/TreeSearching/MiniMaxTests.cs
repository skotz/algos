using algorithms.MapRepresentation;
using algorithms.TreeSearching;
using FluentAssertions;
using games.TicTacToe;
using Xunit;

namespace tests.TreeSearching
{
    public class MiniMaxTests
    {
        [Fact]
        public void Given_MiniMax_When_WinningMoveAvailable_Then_GetBestMove()
        {
            var ttt = TicTacToe.FromString(new string[]
            {
                "O O",
                "XX ",
                "   ",
            }, Player.One);

            var mcts = new MiniMax();
            var move = mcts.GetBestMove(ttt, 9);

            move.BestMove.X.Should().Be(2);
            move.BestMove.Y.Should().Be(1);
        }

        [Fact]
        public void Given_MiniMax_When_OpponentAboutToWin_Then_BlockOpponent()
        {
            var ttt = TicTacToe.FromString(new string[]
            {
                "OXO",
                "XXO",
                "  X",
            }, Player.Two);

            var mcts = new MiniMax();
            var move = mcts.GetBestMove(ttt, 9);

            move.BestMove.X.Should().Be(1);
            move.BestMove.Y.Should().Be(2);
        }

        [Fact]
        public void Given_MiniMax_When_OpponentAboutToWinInEarlyGame_Then_BlockOpponent()
        {
            var ttt = TicTacToe.FromString(new string[]
            {
                "  O",
                " XX",
                "   ",
            }, Player.Two);

            var mcts = new MiniMax();
            var move = mcts.GetBestMove(ttt, 9);

            move.BestMove.X.Should().Be(0);
            move.BestMove.Y.Should().Be(1);
        }
    }
}