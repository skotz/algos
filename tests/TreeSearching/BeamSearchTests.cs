using algorithms.MapRepresentation;
using algorithms.TreeSearching;
using FluentAssertions;
using games.TicTacToe;
using Xunit;

namespace tests.TreeSearching
{
    public class BeamSearchTests
    {
        [Fact]
        public void Given_BeamSearch_When_WinningMoveAvailable_Then_GetBestMove()
        {
            var ttt = TicTacToe.FromString(new string[]
            {
                "O O",
                "XX ",
                "   ",
            }, Player.One);

            var beam = new BeamSearch(10);
            var move = beam.GetBestMove(ttt, new SearchCancellationToken(100));

            move.BestMove.X.Should().Be(2);
            move.BestMove.Y.Should().Be(1);
        }

        [Fact]
        public void Given_BeamSearch_When_OpponentAboutToWin_Then_BlockOpponent()
        {
            var ttt = TicTacToe.FromString(new string[]
            {
                "OXO",
                "XXO",
                "  X",
            }, Player.Two);

            var beam = new BeamSearch(10);
            var move = beam.GetBestMove(ttt, new SearchCancellationToken(100));

            move.BestMove.X.Should().Be(1);
            move.BestMove.Y.Should().Be(2);
        }

        [Fact]
        public void Given_BeamSearch_When_OpponentAboutToWinInEarlyGame_Then_BlockOpponent()
        {
            var ttt = TicTacToe.FromString(new string[]
            {
                "  O",
                " XX",
                "   ",
            }, Player.Two);

            var beam = new BeamSearch(10);
            var move = beam.GetBestMove(ttt, new SearchCancellationToken(100));

            move.BestMove.X.Should().Be(0);
            move.BestMove.Y.Should().Be(1);
        }
    }
}