using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day22Test
{
    private Day22 _day22;

    [TestInitialize]
    public void Setup()
    {
        _day22 = new Day22();
    }

    [TestMethod]
    public void InputDataShouldBe6032()
    {
        var expectedResult = 6032;

        var actualResult = _day22.Password(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputSlidingShouldBe5031()
    {
        var expectedResult = 5031;

        var actualResult = _day22.Password3d(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5";

}
// Above 27568
