using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day12
{
    public int StepsFromStart(string input)
    {
        var rows = input.Split(Environment.NewLine);
        var ends = Find(rows, 'S', 'a');
        return MinSteps(rows, ends);
    }

    public int StepsAny(string input)
    {
        var rows = input.Split(Environment.NewLine);
        Find(rows, 'S', 'a');
        var ends = Find(rows, 'a', 'a');
        return MinSteps(rows, ends);
    }

    private int MinSteps(string[] rows, List<(int x, int y)> checks)
    {
        var start = Find(rows, 'E', 'z')[0];
        var steps = new int[rows[0].Length, rows.Length];

        var queue = new Queue<(int x, int y)>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();
            var moves = steps[pos.x, pos.y] + 1;
            var height = rows[pos.y][pos.x];

            foreach (var delta in new[] {(1, 0), (-1, 0), (0, 1), (0, -1)})
            {
                var nx = pos.x + delta.Item1;
                var ny = pos.y + delta.Item2;

                if (nx >= 0 && nx < rows[0].Length && ny >= 0 && ny < rows.Length)
                {
                    if (rows[ny][nx] >= (height - 1))
                    {
                        if (steps[nx, ny] == 0 || steps[nx, ny] > moves)
                        {
                            steps[nx, ny] = moves;
                            queue.Enqueue((nx, ny));
                        }
                    }
                }
            }
        }

        return checks.Min(end => steps[end.x, end.y]);
    }

    private List<(int x, int y)> Find(string[] rows, char toFind, char replace)
    {
        var positions = new List<(int, int)>();
        for (var r = 0; r < rows.Length; r++)
        {
            var a = rows[r].IndexOf(toFind);
            if (a >= 0)
            {
                rows[r] = rows[r][..a] + replace + rows[r][(a + 1)..];
                positions.Add((a, r));
            }
        }

        return positions;
    }
}