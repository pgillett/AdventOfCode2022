using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day24Test
{
    private Day24 _day24;

    [TestInitialize]
    public void Setup()
    {
        _day24 = new Day24();
    }

    [TestMethod]
    public void InputDataShouldBe18()
    {
        var expectedResult = 18;

        var actualResult = _day24.Method(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputSlidingShouldBe54()
    {
        var expectedResult = 54;

        var actualResult = _day24.Method2(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"#.######
#>>.<^<#
#.<..<<#
#>v.><>#
#<^v^^>#
######.#";

}
