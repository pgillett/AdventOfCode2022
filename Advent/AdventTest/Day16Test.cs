using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day16Test
{
    private Day16 _day16;

    [TestInitialize]
    public void Setup()
    {
        _day16 = new Day16();
    }

    [TestMethod]
    public void InputDataShouldBe1651()
    {
        var expectedResult = 1651;

        var actualResult = _day16.MostPressure(_input);

        actualResult.Should().Be(expectedResult);
    }
    
    [TestMethod]
    public void InputWithElephantShouldBe1707()
    {
        var expectedResult = 1707;

        var actualResult = _day16.MostPressureWithElephant(_input);

        actualResult.Should().Be(expectedResult);
    }
    
    private string _input = @"Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II";

}
