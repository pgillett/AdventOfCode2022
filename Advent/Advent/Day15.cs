using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day15
{
    public long Tuning(string input, int max)
    {
        var sensorBeacons = Parse(input); 

        for (var y = 0; y <= max; y++)
        {
            var x = FindX(sensorBeacons, y, max);
            if (x >= 0) return x * 4000000L + y;
        }

        return 0;
    }

    private SensorBeacon[] Parse(string input) => input.Split(Environment.NewLine)
        .Select(l => new SensorBeacon(l)).ToArray();
    
    private int FindX(SensorBeacon[] sensorBeacons, int yLine, int max)
    {
        var sets = sensorBeacons.Where(s => s.OnRow(yLine))
            .Select(s => s.WidthOn(yLine, max)).OrderBy(p => p.from).ToArray();

        var x = 0;
        foreach (var set in sets)
        {
            if (x < set.from)
            {
                return x;
            }

            x = Math.Max(x, set.to + 1);
        }

        return -1;
    }

    public int NoBeacons(string input, int yLine)
    {
        var sensorBeacons = Parse(input);
        var onRow = sensorBeacons.Where(s => s.OnRow(yLine))
            .Select(s => s.WidthOn(yLine));

        var none = new HashSet<int>();

        foreach (var length in onRow)
        {
            for (var x = length.from; x <= length.to; x++)
                none.Add(x);
        }

        foreach (var sensorBeacon in sensorBeacons
                     .Where(s => s.BeaconY == yLine))
        {
            none.Remove(sensorBeacon.BeaconX);
        }

        return none.Count;
    }
    
    public class SensorBeacon
    {
        public int SensorX;
        public int SensorY;
        public int BeaconX;
        public int BeaconY;
        public int Distance;
        public int MinY;
        public int MaxY;

        public SensorBeacon(string input)
        {
            var split = input.Replace("Sensor at ", "")
                .Replace(": closest beacon is at ", ",")
                .Replace("x=", "")
                .Replace("y=", "")
                .Replace(" ", "")
                .Split(",")
                .Select(int.Parse)
                .ToArray();

            SensorX = split[0];
            SensorY = split[1];
            BeaconX = split[2];
            BeaconY = split[3];
            Distance = Math.Abs(BeaconX - SensorX) + Math.Abs(BeaconY - SensorY);
            MinY = SensorY - Distance;
            MaxY = SensorY + Distance;
        }

        public bool OnRow(int yLine) => yLine >= MinY && yLine <= MaxY;

        public (int from, int to) WidthOn(int yLine, int max)
        {
            var p = WidthOn(yLine);
            return (Math.Max(p.from, 0), Math.Min(p.to, max));
        }

        public (int from, int to) WidthOn(int yLine)
        {
            var dy = Math.Abs(yLine - SensorY);
            return (SensorX - (Distance - dy),
                SensorX + (Distance - dy));
        }
    }
}