using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day25Test
{
    private Day25 _day25;

    [TestInitialize]
    public void Setup()
    {
        _day25 = new Day25();
    }

    [TestMethod]
    public void InputDataShouldBeResult()
    {
        var expectedResult = "2=-1=0";

        var actualResult = _day25.Snafu(_input);

        actualResult.Should().Be(expectedResult);
    }
    
    private string _input = @"1=-0-2
12111
2=0=
21
2=01
111
20012
112
1=-1=
1-12
12
1=
122";

}
