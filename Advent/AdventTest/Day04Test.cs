using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day04Test
{
    private Day04 _day04;

    [TestInitialize]
    public void Setup()
    {
        _day04 = new Day04();
    }

    [TestMethod]
    public void TestInputContainsShouldBe2()
    {
        var expectedResult = 2;

        var actualResult = _day04.FullyContain(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void TestInputOverlapsShouldBe4()
    {
        var expectedResult = 4;

        var actualResult = _day04.Overlaps(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

}
