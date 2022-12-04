using System;
using System.Linq;

namespace Advent;

public class Day04
{
    public int FullyContain(string input) => Count(input, OverlapFull);

    public int Overlaps(string input) => Count(input, OverlapPart);

    private int Count(string input, Func<int[], int[], bool> compare) =>
        input.Split(Environment.NewLine)
            .Select(l => l.Split(',')
                .Select(p => p.Split('-')
                    .Select(int.Parse)
                    .ToArray())
                .ToArray())
            .Count(p => compare(p[0], p[1]));

    private bool OverlapFull(int[] first, int[] second) =>
        ((first[0] >= second[0] && first[1] <= second[1]) ||
         (second[0] >= first[0] && second[1] <= first[1]));

    private bool OverlapPart(int[] first, int[] second) =>
        first[0] <= second[1] && first[1] >= second[0];
}