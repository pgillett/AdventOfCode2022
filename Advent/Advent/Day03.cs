using System;
using System.Linq;

namespace Advent;

public class Day03
{
    public int PrioritySum(string input) => input.Split(Environment.NewLine).Sum(PriorityLine);

    public int PriorityGroup(string input) => input.Split(Environment.NewLine)
            .Select((line, count) => (line, count))
            .GroupBy(pair => pair.count / 3)
            .Select(group => group.Select(pair => pair.line).ToArray())
            .Sum(lines => PrioritySingleGroup(lines[0], lines[1], lines[2]));

    private int PrioritySingleGroup(string line1, string line2, string line3) => PriorityItem(line3.First(item =>
            line1.Contains(item) && line2.Contains(item)));

    public int PriorityLine(string line) => PriorityItem(line[(line.Length / 2)..]
        .First(item => line[..(line.Length / 2)].Contains(item)));

    private int PriorityItem(char item) => item > 'Z' ? item - 'a' + 1 : item - 'A' + 27;
}