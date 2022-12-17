using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day17Test
{
    private Day17 _day17;

    [TestInitialize]
    public void Setup()
    {
        _day17 = new Day17();
    }

    [TestMethod]
    public void InputDataShouldBe3068()
    {
        var expectedResult = 3068;

        var actualResult = _day17.UnitsTall(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputLargeShouldBe1514285714288()
    {
        var expectedResult = 1514285714288L;

        var actualResult = _day17.Trillion(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";

}
