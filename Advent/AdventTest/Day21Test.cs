using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day21Test
{
    private Day21 _day21;

    [TestInitialize]
    public void Setup()
    {
        _day21 = new Day21();
    }

    [TestMethod]
    public void InputDataShouldBe152()
    {
        var expectedResult = 152;

        var actualResult = _day21.Root(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputMeBe301()
    {
        var expectedResult = 301;

        var actualResult = _day21.Me(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32";

}
