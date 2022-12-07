using Advent;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventTest;

[TestClass]
public class Day07Test
{
    private Day07 _day07;

    [TestInitialize]
    public void Setup()
    {
        _day07 = new Day07();
    }

    [TestMethod]
    public void InputDataShouldBe95437()
    {
        var expectedResult = 95437;

        var actualResult = _day07.SumMost100000(_input);

        actualResult.Should().Be(expectedResult);
    }

    [TestMethod]
    public void InputFreeUpShouldBe24933642()
    {
        var expectedResult = 24933642;

        var actualResult = _day07.FreeUp(_input);

        actualResult.Should().Be(expectedResult);
    }

    private string _input = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

}
