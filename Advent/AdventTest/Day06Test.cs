using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day06Test
{
    private Day06 _day06;

    [TestInitialize]
    public void Setup()
    {
        _day06 = new Day06();
    }

    [TestMethod]
    [DataRow("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [DataRow("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [DataRow("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [DataRow("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void InputDataShouldDetectCharacters4(string input, int expectedResult)
    {
        var actualResult = _day06.DetectStart4(input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    [DataRow("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [DataRow("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [DataRow("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [DataRow("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [DataRow("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void InputDataShouldDetectCharacters14(string input, int expectedResult)
    {
        var actualResult = _day06.DetectStart14(input);

        actualResult.Should().Be(expectedResult);
    }
}