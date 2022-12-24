using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day23Test
{
    private Day23 _day23;

    [TestInitialize]
    public void Setup()
    {
        _day23 = new Day23();
    }

    [TestMethod]
    public void InputDataShouldBe110()
    {
        var expectedResult = 110;

        var actualResult = _day23.Method(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputSlidingShouldBe20()
    {
        var expectedResult = 20;

        var actualResult = _day23.Method3(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"....#..
..###.#
#...#.#
.#...##
#.###..
##.#.##
.#..#..";

}
