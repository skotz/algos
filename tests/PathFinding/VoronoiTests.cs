using algorithms.MapRepresentation;
using algorithms.PathFinding;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace tests.PathFinding
{
    public class VoronoiTests
    {
        [Fact]
        public void Given_Voronoi_When_FillingFromTwoPoints_Then_CorrectBordersDiscovered()
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

            var voronoi = new Voronoi(map);

            voronoi.Add(Player.One, 3, 1);
            voronoi.Add(Player.One, 2, 14);

            voronoi.Add(Player.Two, 6, 10);
            voronoi.Add(Player.Two, 12, 11);

            var grid = voronoi.Fill();

            grid[4, 2].Should().Be(Player.One);
            grid[5, 10].Should().Be(Player.Two);

            grid[5, 13].Should().Be(Player.Both);
            grid[6, 14].Should().Be(Player.Both);
            grid[12, 4].Should().Be(Player.Both);
            grid[13, 3].Should().Be(Player.Both);
        }
    }
}