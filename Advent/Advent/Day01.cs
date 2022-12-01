using System;
using System.Linq;

namespace Advent;

public class Day01
{
    public int ElfWithMost(string input) => MostElves(input, 1);

    public int ThreeElvesWithMost(string input) => MostElves(input, 3);

    private int MostElves(string input, int number) => input
            .Split(Environment.NewLine + Environment.NewLine)
            .Select(s => s.Split(Environment.NewLine)
                .Sum(int.Parse))
            .OrderByDescending(c => c)
            .Take(number)
            .Sum();
}