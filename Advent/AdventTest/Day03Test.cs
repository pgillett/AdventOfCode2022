using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day03Test
{
    private Day03 _day03;

    [TestInitialize]
    public void Setup()
    {
        _day03 = new Day03();
    }
    
    [TestMethod]
    public void TestInputShouldBe157()
    {
        var expected = 157;

        var result = _day03.PrioritySum(Input);

        result.Should().Be(expected);
    }

    [TestMethod]
    public void TestPriorityItemShouldBe16()
    {
        var expected = 16;

        var result = _day03.PriorityLine("vJrwpWtwJgWrhcsFMMfFFhFp");

        result.Should().Be(expected);
    }
    
    [TestMethod]
    public void TestPriorityGroupShouldBe70()
    {
        var expected = 70;

        var result = _day03.PriorityGroup(Input);

        result.Should().Be(expected);
    }
    
    public string Input = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";
}