using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day11Test
{
    private Day11 _day11;

    [TestInitialize]
    public void Setup()
    {
        _day11 = new Day11();
    }

    [TestMethod]
    public void InputDataShouldBe10605()
    {
        var expectedResult = 10605;

        var actualResult = _day11.MonkeyBusiness(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputNoWorryShouldBe2713310158()
    {
        var expectedResult = 2713310158;

        var actualResult = _day11.NoWorryFactor(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

}
