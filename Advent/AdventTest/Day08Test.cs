using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day08Test
{
    private Day08 _day08;

    [TestInitialize]
    public void Setup()
    {
        _day08 = new Day08();
    }

    [TestMethod]
    public void InputDataShouldBe21()
    {
        var expectedResult = 21;

        var actualResult = _day08.VisibleTrees(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputScenicShouldBe8()
    {
        var expectedResult = 8;

        var actualResult = _day08.HighestScenic(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"30373
25512
65332
33549
35390";

}
