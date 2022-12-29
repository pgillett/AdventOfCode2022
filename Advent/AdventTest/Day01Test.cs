using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day01Test
{
    private Day01 _day01;

    [TestInitialize]
    public void Setup()
    {
        _day01 = new Day01();
    }

    [TestMethod]
    public void InputDataShouldBe24000()
    {
        var expectedResult = 24000;

        var actualResult = _day01.ElfWithMost(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputTop3ShouldBe45000()
    {
        var expectedResult = 45000;

        var actualResult = _day01.ThreeElvesWithMost(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";

}
