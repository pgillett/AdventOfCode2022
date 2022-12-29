using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day18Test
{
    private Day18 _day18;

    [TestInitialize]
    public void Setup()
    {
        _day18 = new Day18();
    }

    [TestMethod]
    public void InputDataShouldBe64()
    {
        var expectedResult = 64;

        var actualResult = _day18.OpenSides(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputVisibleShouldBe58()
    {
        var expectedResult = 58;

        var actualResult = _day18.VisibleSides(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5";

}
