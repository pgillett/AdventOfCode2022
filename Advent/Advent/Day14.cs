using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day14
{
    public int RestingSandWithFloor(string input) => RestingSand2(input, true);

    public int RestingSand(string input) => RestingSand2(input, false);

    private int RestingSand2(string input, bool withFloor)
    {
        var map = new HashSet<(int x, int y)>();

        foreach (var line in input.Split(Environment.NewLine))
        {
            var split = line.Split(" -> ").Select(ParsePoint).ToArray();
            var pos = split[0];

            map.Add(pos);

            foreach (var point in split.Skip(1))
            {
                while (pos != point)
                {
                    pos = (pos.x + point.x.CompareTo(pos.x), pos.y + point.y.CompareTo(pos.y));
                    map.Add(pos);
                }
            }
        }

        var maxy = map.Max(p => p.y);

        var count = 0;

        while (Rests(map, maxy, withFloor))
        {
            count++;
        }

        return count;
    }

    private bool Rests(HashSet<(int x, int y)> map, int maxy, bool withFloor)
    {
        (int x, int y) point = (500, 0);

        if (map.Contains(point)) return false;

        while (point.y < maxy + 2)
        {
            if (withFloor && point.y == maxy + 1)
            {
                map.Add(point);
                return true;
            }
            
            if (!map.Contains((point.x, point.y + 1)))
            {
                point = (point.x, point.y + 1);
            }
            else if (!map.Contains((point.x - 1, point.y + 1)))
            {
                point = (point.x - 1, point.y + 1);
            }
            else if (!map.Contains((point.x + 1, point.y + 1)))
            {
                point = (point.x + 1, point.y + 1);
            }
            else
            {
                map.Add(point);
                return true;
            }
        }

        return false;
    }

    private (int x, int y) ParsePoint(string input)
    {
        var split = input.Split(",").Select(int.Parse).ToArray();
        return (split[0], split[1]);
    }
}