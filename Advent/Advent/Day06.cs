using System;
using System.Linq;

namespace Advent;

public class Day06
{
    public int DetectStart4(string input) => DetectStart(input, 4);

    public int DetectStart14(string input) => DetectStart(input, 14);

    private int DetectStart(string input, int number)
    {
        for (var i = 0; i < input.Length; i++)
        {
            if (input.Substring(i, number).Distinct().Count() == number) return i + number;
        }

        throw new Exception("Not found");
    }
    
    // Linq version of part 1
    
    public int DetectStartLinq(string input) =>
        input.Select((c, i) => (c, i))
            .Zip(input.Skip(1)).Zip(input.Skip(2)).Zip(input.Skip(3))
            .Select(c => (c.First.First.First.i,
                new[] {c.First.First.First.c, c.First.First.Second, c.First.Second, c.Second}))
            .First(c => c.Item2.Distinct().Count() == 4).i + 4;
}