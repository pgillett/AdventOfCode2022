using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day19Test
{
    private Day19 _day19;

    [TestInitialize]
    public void Setup()
    {
        _day19 = new Day19();
    }

    [TestMethod]
    public void InputDataShouldBe33()
    {
        var expectedResult = 33;

        var actualResult = _day19.QualityLevels(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputLongerShouldBe45000()
    {
        var expectedResult = 62 * 56;

        var actualResult = _day19.QualityLevels3(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"Blueprint 1:
  Each ore robot costs 4 ore.
  Each clay robot costs 2 ore.
  Each obsidian robot costs 3 ore and 14 clay.
  Each geode robot costs 2 ore and 7 obsidian.

Blueprint 2:
  Each ore robot costs 2 ore.
  Each clay robot costs 3 ore.
  Each obsidian robot costs 3 ore and 8 clay.
  Each geode robot costs 3 ore and 12 obsidian.";

}
