using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day13Test
{
    private Day13 _day13;

    [TestInitialize]
    public void Setup()
    {
        _day13 = new Day13();
    }

    [TestMethod]
    public void InputDataShouldBe13()
    {
        var expectedResult = 13;

        var actualResult = _day13.SumIndices(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputDecoderShouldBe140()
    {
        var expectedResult = 140;

        var actualResult = _day13.Decoder(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]";

}
