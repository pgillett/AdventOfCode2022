using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day15Test
{
    private Day15 _day15;

    [TestInitialize]
    public void Setup()
    {
        _day15 = new Day15();
    }

    [TestMethod]
    public void InputDataShouldBe26()
    {
        var expectedResult = 26;

        var actualResult = _day15.NoBeacons(_input,10);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputTuningShouldBe56000011()
    {
        var expectedResult = 56000011;

        var actualResult = _day15.Tuning(_input,20);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3";

}
