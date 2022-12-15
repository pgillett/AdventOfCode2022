using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day14Test
{
    private Day14 _day14;

    [TestInitialize]
    public void Setup()
    {
        _day14 = new Day14();
    }

    [TestMethod]
    public void InputDataShouldBe24()
    {
        var expectedResult = 24;

        var actualResult = _day14.RestingSand(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputWithFloorShouldBe93()
    {
        var expectedResult = 93;

        var actualResult = _day14.RestingSandWithFloor(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9";

}
