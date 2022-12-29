using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day20Test
{
    private Day20 _day20;

    [TestInitialize]
    public void Setup()
    {
        _day20 = new Day20();
    }

    [TestMethod]
    public void InputDataShouldBe3()
    {
        var expectedResult = 3;

        var actualResult = _day20.GroveCoordinate(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputWithDecryptionShouldBe45000()
    {
        var expectedResult = 1623178306L;

        var actualResult = _day20.GroveDecryption(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"1
2
-3
3
-2
0
4";

}
