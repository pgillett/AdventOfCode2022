using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day18
{
    public int VisibleSides(string input)
    {
        var cubes = input.Split(Environment.NewLine)
            .Select(Parse).ToHashSet();

        var min = new XYZ(cubes.Min(c => c.X) - 1, cubes.Min(c => c.Y) - 1, cubes.Min(c => c.Z) - 1);
        var max = new XYZ(cubes.Max(c => c.X) + 1, cubes.Max(c => c.Y) + 1, cubes.Max(c => c.Z) + 1);

        var queue = new Queue<XYZ>();
        queue.Enqueue(min);

        var seen = new HashSet<XYZ>();
        
        var edges = 0;

        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();
            
            if(seen.Contains(pos))
                continue;

            seen.Add(pos);

            foreach (var check in Adjacent.Select(d => pos.Add(d)))
            {
                if (check.X >= min.X && check.X <= max.X
                                    && check.Y >= min.Y && check.Y <= max.Y
                                    && check.Z >= min.Z && check.Z <= max.Z)
                {
                    if (cubes.Contains(check))
                        edges++;
                    else
                        queue.Enqueue(check);
                }
            }
            
        }

        return edges;
    }

    public XYZ[] Adjacent =
    {
        new(-1, 0, 0),
        new(1, 0, 0), 
        new(0, -1, 0),
        new(0, 1, 0),
        new(0, 0, -1),
        new(0, 0, 1)
    };
    
    
    public int OpenSides(string input)
    {
        var cubes = input.Split(Environment.NewLine)
            .Select(Parse).ToArray();

        var sides = new Dictionary<(XYZ from, XYZ to), int>();

        foreach (var cube in cubes)
        {
            foreach (var side in Matrix.Select(m => (cube.Add(m.from), cube.Add(m.to))))
            {
                sides[side] = (sides.ContainsKey(side) ? sides[side] : 0) + 1;
            }
        }

        return sides.Count(s => s.Value == 1);
    }

    public (XYZ from, XYZ to)[] Matrix = {
        (new XYZ(0, 0, 0), new XYZ(1, 1, 0)),
        (new XYZ(0, 0, 1), new XYZ( 1, 1, 1)),
        (new XYZ(0, 0, 0), new XYZ(1, 0, 1)),
        (new XYZ(0, 1, 0), new XYZ(1, 1, 1)),
        (new XYZ(0, 0, 0), new XYZ(0, 1, 1)),
        (new XYZ(1, 0, 0), new XYZ(1, 1, 1))
    };

    public XYZ Parse(string input)
    {
        var split = input.Split(',').Select(int.Parse).ToArray();
        return new XYZ(split[0], split[1], split[2]);
    }

    public record XYZ(int X, int Y, int Z)
    {
        public XYZ Add(XYZ toAdd) => new XYZ(X + toAdd.X, Y + toAdd.Y, Z + toAdd.Z);
    }
}