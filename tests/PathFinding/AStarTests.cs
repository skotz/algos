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
            var map = Board.FromString(new string[]
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
            var map = Board.FromString(new string[]
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
            var map = Board.FromString(new string[]
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
    }
}