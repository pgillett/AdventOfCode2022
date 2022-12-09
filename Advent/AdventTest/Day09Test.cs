using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day09Test
{
    private Day09 _day09;

    [TestInitialize]
    public void Setup()
    {
        _day09 = new Day09();
    }

    [TestMethod]
    public void InputDataShouldBe13()
    {
        var expectedResult = 13;

        var actualResult = _day09.CoversShort(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputLongRopeShouldBe1()
    {
        var expectedResult = 1;

        var actualResult = _day09.CoversLong(_input);

        actualResult.Should().Be(expectedResult);
    }
    
    [TestMethod]
    public void Input2LongRopeShouldBe36()
    {
        var expectedResult = 36;

        var actualResult = _day09.CoversLong(_input2);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

    private string _input2 = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

}
