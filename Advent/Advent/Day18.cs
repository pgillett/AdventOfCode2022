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

        var minX = cubes.Min(c => c.x)-1;
        var maxX = cubes.Max(c => c.x)+1;
        var minY = cubes.Min(c => c.y)-1;
        var maxY = cubes.Max(c => c.y)+1;
        var minZ = cubes.Min(c => c.z)-1;
        var maxZ = cubes.Max(c => c.z)+1;

        var queue = new Queue<(int x, int y, int z)>();

        var seen = new HashSet<(int x, int y, int z)>();
        
        queue.Enqueue((minX, minY, minZ));

        var edges = 0;

        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();
            
            if(seen.Contains(pos))
                continue;

            seen.Add(pos);

            foreach (var direction in Adjacent)
            {
                (int x, int y, int z) check = (pos.x + direction.x, pos.y + direction.y, pos.z + direction.z);
                if (check.x >= minX && check.x <= maxX
                                    && check.y >= minY && check.y <= maxY
                                    && check.z >= minZ && check.z <= maxZ)
                {
                    if (cubes.Contains(check))
                        edges++;
                    else
                    {
                        queue.Enqueue(check);
                    }
                }
            }
            
        }

        return edges;
    }

    public (int x, int y, int z)[] Adjacent = 
        {(-1, 0, 0), (1, 0, 0), (0, -1, 0), (0, 1, 0), (0, 0, -1), (0, 0, 1)};
    
    
    public int OpenSides(string input)
    {
        var cubes = input.Split(Environment.NewLine)
            .Select(Parse).ToArray();

        var sides = new Dictionary<(int fx, int fy, int fz, int tx, int ty, int tz), int>();

        foreach (var cube in cubes)
        {
            foreach (var m in Matrix)
            {
                var side = (cube.x + m.fx, cube.y + m.fy, cube.z + m.fz,
                    cube.x + m.tx, cube.y + m.ty, cube.z + m.tz);
                sides[side] = (sides.ContainsKey(side) ? sides[side] : 0) + 1;
            }
        }

        return sides.Count(s => s.Value == 1);
    }

    public (int fx, int fy, int fz, int tx, int ty, int tz)[] Matrix = new[]
    {
        (0, 0, 0, 1, 1, 0),
        (0, 0, 1, 1, 1, 1),
        (0, 0, 0, 1, 0, 1),
        (0, 1, 0, 1, 1, 1),
        (0, 0, 0, 0, 1, 1),
        (1, 0, 0, 1, 1, 1)
    };

    public (int x, int y, int z) Parse(string input)
    {
        var split = input.Split(',').Select(int.Parse).ToArray();
        return (split[0], split[1], split[2]);
    }
}