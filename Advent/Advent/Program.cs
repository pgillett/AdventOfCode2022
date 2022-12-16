using System;
using System.Diagnostics;

namespace Advent;

class Program
{
    private static Stopwatch _stopwatch;

    private const int From = 1;
    private const int To = 25;

    private static readonly int[,] Times = new int[25, 2];

    static void Main(string[] args)
    {
        _stopwatch = new Stopwatch();

        if (IncludeDay(1))
        {
            var day1 = new Day01();
            Output(1, 1, "Elf with max", day1.ElfWithMost(InputData.Day01Elves));
            Output(1, 2, "Three elves with max", day1.ThreeElvesWithMost(InputData.Day01Elves));
        }
        
        if (IncludeDay(2))
        {
            var day2 = new Day02();
            Output(2, 1, "Score", day2.Score(InputData.Day02Rock));
            Output(2, 2, "Score", day2.ScoreForceOutcome(InputData.Day02Rock));
        }
        
        if (IncludeDay(3))
        {
            var day3 = new Day03();
            Output(3, 1, "Priority sum", day3.PrioritySum(InputData.Day03Rucksack));
            Output(3, 2, "Priority sum groups", day3.PriorityGroup(InputData.Day03Rucksack));
        }

        if (IncludeDay(4))
        {
            var day4 = new Day04();
            Output(4, 1, "Fully contains", day4.FullyContain(InputData.Day04Assignment));
            Output(4, 2, "Partial overlaps", day4.Overlaps(InputData.Day04Assignment));
        }
        
        if (IncludeDay(5))
        {
            var day5 = new Day05();
            Output(5, 1, "Top crates 9000", day5.FinalTop9000(InputData.Day05Crates));
            Output(5, 2, "Top crates 9001", day5.FinalTop9001(InputData.Day05Crates));
        }
        
        if (IncludeDay(6))
        {
            var day6 = new Day06();
            Output(6, 1, "Start of packet 4", day6.DetectStart4(InputData.Day06Tuning));
            Output(6, 2, "Start of packet 14", day6.DetectStart14(InputData.Day06Tuning));
        }
        
        if (IncludeDay(7))
        {
            var day7 = new Day07();
            Output(7, 1, "Directories under 100000", day7.SumMost100000(InputData.Day07Terminal));
            Output(7, 2, "Free space", day7.FreeUp(InputData.Day07Terminal));
        }
        
        if (IncludeDay(8))
        {
            var day8 = new Day08();
            Output(8, 1, "Visible trees", day8.VisibleTrees(InputData.Day08Trees));
            Output(8, 2, "Highest scenic", day8.HighestScenic(InputData.Day08Trees));
        }

        if (IncludeDay(9))
        {
            var day9 = new Day09();
            Output(9, 1, "Covers short", day9.CoversShort(InputData.Day09Rope));
            Output(9, 2, "Covers long", day9.CoversLong(InputData.Day09Rope));
        }
        
        if (IncludeDay(10))
        {
            var day10 = new Day10();
            Output(10, 1, "Strength", day10.Strength(InputData.Day10Cathode));
            Output(10, 2, "Render", day10.Render(InputData.Day10Cathode));
        }
        
        if (IncludeDay(11))
        {
            var day11 = new Day11();
            Output(11, 1, "Monkey business", day11.MonkeyBusiness(InputData.Day11Monkey));
            Output(11, 2, "No worry factor", day11.NoWorryFactor(InputData.Day11Monkey));
        }
        
        if (IncludeDay(12))
        {
            var day11 = new Day12();
            Output(12, 1, "Shortest path start", day11.StepsFromStart(InputData.Day12Heightmap));
            Output(12, 2, "Shortest path any", day11.StepsAny(InputData.Day12Heightmap));
        }
        
        if (IncludeDay(13))
        {
            var day13 = new Day13();
            Output(13, 1, "Sum indices", day13.SumIndices(InputData.Day13Distress));
            Output(13, 2, "Decoder", day13.Decoder(InputData.Day13Distress));
        }

        if (IncludeDay(14))
        {
            var day14 = new Day14();
            Output(14, 1, "Resting sand", day14.RestingSand(InputData.Day14Regolith));
            Output(14, 2, "Resting sand with floor", day14.RestingSandWithFloor(InputData.Day14Regolith));
        }
        
        if (IncludeDay(15))
        {
            var day15 = new Day15();
            Output(15, 1, "No beacons on line", day15.NoBeacons(InputData.Day15Beacons, 2000000));
            Output(15, 2, "Tuning", day15.Tuning(InputData.Day15Beacons, 4000000));
        }

        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Day       Part 1    Part 2");
        for (int i = 0; i < 25; i++)
        {
            Console.WriteLine($"{i + 1,-10}{Times[i, 0],5} ms  {Times[i, 1],5} ms");
        }
    }

    static bool IncludeDay(int day)
    {
        if (day < From || day > To) return false;

        _stopwatch.Reset();
        _stopwatch.Start();
        Console.WriteLine();
        Console.WriteLine($"DAY {day}");
        Console.WriteLine($"==========");

        return true;
    }

    static void Output(int day, int part, string name, object answer)
    {
        var time = _stopwatch.ElapsedMilliseconds;
        Times[day - 1, part - 1] = (int) time;
        Console.WriteLine($"{time} ms - {name}: {answer}");
        _stopwatch.Reset();
        _stopwatch.Start();
    }
}
