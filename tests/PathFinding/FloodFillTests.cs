using algorithms.MapRepresentation;
using algorithms.PathFinding;
using FluentAssertions;
using Xunit;

namespace tests.PathFinding
{
    public class FloodFillTests
    {
        [Fact]
        public void Given_FloodFill_When_FillingValidRegion_Then_EntireRegionIsFilled()
        {
            var map = Map.FromString(new string[]
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

            var ff = new FloodFill(map);
            var start = new Location(0, 0);

            var filled = ff.GetRegion(start);

            filled[0, 0].Should().Be(true);
            filled[3, 10].Should().Be(true);

            // Not part of the original region
            filled[8, 6].Should().Be(false);

            // Not traversable
            filled[4, 0].Should().Be(false);
        }

        [Fact]
        public void Given_FloodFill_When_GettingRegionSize_Then_CorrectSizeReturned()
        {
            var map = Map.FromString(new string[]
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

            var ff = new FloodFill(map);

            var size = ff.GetRegionSize(new Location(0, 0));

            size.Should().Be(124);

            var size2 = ff.GetRegionSize(new Location(8, 6));

            size2.Should().Be(3);
        }
    }
}