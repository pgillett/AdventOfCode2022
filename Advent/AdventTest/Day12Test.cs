using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day12Test
{
    private Day12 _day12;

    [TestInitialize]
    public void Setup()
    {
        _day12 = new Day12();
    }

    [TestMethod]
    public void InputToStartShouldBe31()
    {
        var expectedResult = 31;

        var actualResult = _day12.StepsFromStart(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputShortestShouldBe29()
    {
        var expectedResult = 29;

        var actualResult = _day12.StepsAny(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

}
