using algorithms.MapRepresentation;
using algorithms.PathFinding;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace tests.PathFinding
{
    public class AStarTests
    {
        [Fact]
        public void Given_AStar_When_SearchingForPathThatExists_Then_ShortestPathReturned()
        {
            var map = OceanNavigator.FromString(new string[]
            {
                "    ##  ##   ##",
                "##           ##",
                "#### ##  ##   #",
                "  ## ######   #",
                "      ### ##   ",
                " ##   ### ###  ",
                " ###  ## ####  ",
                "#######  ####  ",
                "##   ####     #",
                "       ##   ###",
                "               ",
                "               ",
                "#####    ##   #",
                "#####  ####  ##",
                "       ##    ##"
            });

            var pf = new AStar(map);
            var start = new Location(0, 0);
            var end = new Location(12, 14);

            var path = pf.FindPath(start, end);

            path.Count.Should().Be(31);
        }

        [Fact]
        public void Given_AStar_When_SearchingForPathThatDoesNotExist_Then_EmptyListReturned()
        {
            var map = OceanNavigator.FromString(new string[]
            {
                "### #",
                "    #",
                "#####",
                " #   ",
                "   # ",
            });

            var pf = new AStar(map);
            var start = new Location(4, 4);
            var end = new Location(3, 0);

            var path = pf.FindPath(start, end);

            path.Count.Should().Be(0);
        }

        [Fact]
        public void Given_AStar_When_SearchingForPath_Then_FirstLocationInPathIsStartTile()
        {
            var map = OceanNavigator.FromString(new string[]
            {
                "### #",
                "    #",
                " ####",
                " #   ",
                "   # ",
            });

            var pf = new AStar(map);
            var start = new Location(4, 4);
            var end = new Location(3, 0);

            var path = pf.FindPath(start, end);

            path.First().X.Should().Be(4);
            path.First().Y.Should().Be(4);

            path.Last().X.Should().Be(3);
            path.Last().Y.Should().Be(0);
        }

        [Fact]
        public void Given_AStar_When_SearchingWithCustomEdgeWeights_Then_CorrectPathFound()
        {
            var map = OceanNavigator.FromString(new string[]
            {
                "    ##  ##   ##",
                "##           ##",
                "#### ##  ##   #",
                "  ## ######   #",
                "      ### ##   ",
                " ##   ### ###  ",
                " ###  ## ####  ",
                "#######  ####  ",
                "##   ####     #",
                "       ##   ###",
                "               ",
                "               ",
                "#####    ##   #",
                "#####  ####  ##",
                "       ##    ##"
            });

            var pf = new AStar(map);

            pf.GetDistance = (board, source, dest) =>
            {
                // Prefer paths closer to the center
                return 1000 + dest.ManhattanDistanceTo(board.Width / 2, board.Height / 2);
            };

            var start = new Location(0, 0);
            var end = new Location(12, 14);

            var path = pf.FindPath(start, end);

            path.Count.Should().Be(31);

            path.Should().Contain(p => p.X == 11 && p.Y == 3);
        }
    }
}