using System;

namespace Advent;

public class Day10
{
    public int Strength(string input) => CPU(input).strength;

    public string Render(string input) => CPU(input).output;

    private (int strength, string output) CPU(string input)
    {
        var output = "";
        
        var x = 1;
        var cycle = 0;
        var strength = 0;

        foreach (var line in input.Split(Environment.NewLine))
        {
            var split = line.Split(' ');
            (strength, output, cycle) = Process(output, strength, cycle, x);
            if (split[0] == "addx")
            {
                (strength, output, cycle) = Process(output, strength, cycle, x);
                x += int.Parse(split[1]);
            }
        }

        return (strength, output);
    }

    private (int, string, int) Process(string screen, int strength, int cycle, int value)
    {
        var xp = cycle % 40;
        if (xp == 0) screen += Environment.NewLine;
        screen += xp >= value - 1 && xp <= value + 1 ? "#" : ".";
        cycle++;
        if ((cycle + 20) % 40 == 0) strength += cycle * value;
        return (strength, screen, cycle);
    }
}