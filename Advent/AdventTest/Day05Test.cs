using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day05Test
{
    private Day05 _day05;

    [TestInitialize]
    public void Setup()
    {
        _day05 = new Day05();
    }

    [TestMethod]
    public void InputDataOn9000ShouldBeCMZ()
    {
        var expectedResult = "CMZ";

        var actualResult = _day05.FinalTop9000(_input);

        actualResult.Should().Be(expectedResult);
    }
    
    [TestMethod]
    public void InputDataOn9001ShouldBeMCD()
    {
        var expectedResult = "MCD";

        var actualResult = _day05.FinalTop9001(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

}
