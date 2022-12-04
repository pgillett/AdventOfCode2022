using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day02Test
{
    private Day02 _day02;

    [TestInitialize]
    public void Setup()
    {
        _day02 = new Day02();
    }
    
    [TestMethod]
    public void InputScoreShouldBe15()
    {
        var expected = 15;

        var result = _day02.Score(Input);

        result.Should().Be(expected);
    }
    
    [TestMethod]
    public void ScoreWithLoseBe12()
    {
        var expected = 12;

        var result = _day02.ScoreForceOutcome(Input);

        result.Should().Be(expected);
    }
    
    public string Input = @"A Y
B X
C Z";
}