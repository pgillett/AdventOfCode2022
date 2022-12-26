using System;
using System.Linq;

namespace Advent;

public class Day25
{
    public string Method(string input)
    {
        var numbers = input.Split(Environment.NewLine).Select(Parse).ToArray();

        return To5(numbers.Sum());
    }

    public long Parse(string input)
    {
        var value = 0L;
        foreach (var c in input)
        {
            value *= 5;
            value += "=-012".IndexOf(c) - 2;
        }

        return value;
    }

    public string To5(long value)
    {
        var snafu = "";
        while (value > 0)
        {
            var v = value % 5;
            value /= 5;
            if (v > 2)
            {
                value += 1;
            }

            snafu = "012=-"[(int)v] + snafu;
        }
        
        return snafu;
    }
}