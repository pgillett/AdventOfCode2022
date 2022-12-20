using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent;

public class Day19
{
    public int QualityLevels(string input)
    {
        var blueprints = input.Split("Blueprint ")
            .Where(l => l.Length > 5)
            .Select(l => new Blueprint(l)).ToArray();

        var total = 0;
        foreach (var blueprint in blueprints)
        {
            var max = MaxGeode(blueprint, 24);
            Console.WriteLine($"Blueprint {blueprint.Number} {max}");
            total += max * blueprint.Number;
        }
        return total;
    }
    
    public int QualityLevels3(string input)
    {
        var blueprints = input.Split("Blueprint ")
            .Where(l => l.Length > 5)
            .Select(l => new Blueprint(l))
            .Where(b => b.Number <= 3).ToArray();

        var total = 1;
        foreach (var blueprint in blueprints)
        {
            var max = MaxGeode(blueprint, 32);
            Console.WriteLine($"Blueprint {blueprint.Number} {max}");
            total *= max;
        }
        return total;
    }

    public int MaxGeode(Blueprint blueprint, int totalMinutes)
    {
        var state = new State(blueprint, totalMinutes);
        return state.Max;
        var startState = (ulong) 1;
        return AtState(blueprint, 0, startState, totalMinutes);
    }

    public class State
    {
        public Blueprint Blueprint;
        public int TotalMinutes;
        public int Max;

        public int GeodeMax;

        public State(Blueprint blueprint, int totalMinutes)
        {
            Blueprint = blueprint;
            TotalMinutes = totalMinutes;

            var startState = (ulong) 1;
            Max = AtState(0, startState);
        }

        public int AtState(int minute, ulong state)
        {
            if (minute == TotalMinutes - 1)
            {
                var resource = state + (state << 32);
                var geode = resource >> (24 + 32);
                return (int) geode;
            }

            var max = 0;
            
            if((state & OreMask) >= (Blueprint.Robots[3].Cost & OreMask)
               && (state & ObsidianMask) >= (Blueprint.Robots[3].Cost & ObsidianMask))
            {
                var next = state - Blueprint.Robots[3].Cost;
                next += next << 32;
                next += 1 << 24;
                max = Math.Max(max, AtState(minute + 1, next));
                return max;
            }

            if (((state & OreMask) < Blueprint.MaxOre) | ((state & ClayMask) < Blueprint.MaxClay)
                | ((state & ObsidianMask) < Blueprint.MaxObsidian))
            {
                var nothing = state + (state << 32);
                max = Math.Max(max, AtState(minute + 1, nothing));
            }

            if (minute < TotalMinutes - 7)
            {
                if ((state & 255) < Blueprint.MaxRobots[0])
                {
                    if ((state & OreMask) >= Blueprint.Robots[0].Cost)
                    {
                        var next = state - Blueprint.Robots[0].Cost;
                        next += next << 32;
                        next += 1;
                        max = Math.Max(max, AtState(minute + 1, next));
                    }
                }
            }

            if (minute < TotalMinutes - 5)
            {
                if ((state & (255 << 8)) < Blueprint.MaxRobots[1])
                {
                    if ((state & OreMask) >= Blueprint.Robots[1].Cost)
                    {
                        var next = state - Blueprint.Robots[1].Cost;
                        next += next << 32;
                        next += 1 << 8;
                        max = Math.Max(max, AtState(minute + 1, next));
                    }
                }
            }

            if (minute < TotalMinutes - 3)
            {
                if ((state & (255 << 16)) < Blueprint.MaxRobots[2])
                {
                    if((state & OreMask) >= (Blueprint.Robots[2].Cost & OreMask)
                       && (state & ClayMask) >= (Blueprint.Robots[2].Cost & ClayMask))
                    {
                        var next = state - Blueprint.Robots[2].Cost;
                        next += next << 32;
                        next += 1 << 16;
                        max = Math.Max(max, AtState(minute + 1, next));
                    }
                }
            }
            
           

            return max;
        }
        
        public const ulong OreMask = ((ulong)255) << 32;
        public const ulong ClayMask = ((ulong)255) << (32 + 8);
        public const ulong ObsidianMask = ((ulong)255) << (32 + 16);
    }

    public int AtState(Blueprint blueprint, int minute, ulong state, int totalMinutes)
    {
        if (minute == totalMinutes - 1)
        {
            var resource = state + (state << 32);
            var geode = resource >> (24 + 32);
            return (int) geode;
        }

        var max = 0;
        
        if (CanMake(blueprint.Robots[3], state))
        {
            var next = state - blueprint.Robots[3].Cost;
            next += (next << 32);
            next += 1 << 24;
            max = Math.Max(max, AtState(blueprint, minute + 1, next, totalMinutes));
            return max;
        }

        var nothing = state + (state << 32);
        max = Math.Max(max, AtState(blueprint, minute + 1, nothing, totalMinutes));

        if (minute < totalMinutes - 7)
        {
            if ((state & 255) < blueprint.MaxRobots[0])
            {
                if (CanMake(blueprint.Robots[0], state))
                {
                    var next = state - blueprint.Robots[0].Cost;
                    next += (next << 32);
                    next += 1;
                    max = Math.Max(max, AtState(blueprint, minute + 1, next, totalMinutes));
                }
            }
        }

        if (minute < totalMinutes - 5)
        {
            if ((state & (255 << 8)) < blueprint.MaxRobots[1])
            {
                if (CanMake(blueprint.Robots[1], state))
                {
                    var next = state - blueprint.Robots[1].Cost;
                    next += (next << 32);
                    next += 1 << 8;
                    max = Math.Max(max, AtState(blueprint, minute + 1, next, totalMinutes));
                }
            }
        }

        if (minute < totalMinutes - 3)
        {
            if ((state & (255 << 16)) < blueprint.MaxRobots[2])
            {
                if (CanMake(blueprint.Robots[2], state))
                {
                    var next = state - blueprint.Robots[2].Cost;
                    next += (next << 32);
                    next += 1 << 16;
                    max = Math.Max(max, AtState(blueprint, minute + 1, next, totalMinutes));
                }
            }
        }

        return max;
    }

    public int MaxGeode2(Blueprint blueprint, int totalMinutes)
    {
        var startState = (ulong)1;

        var toCheck = new List<ulong>();
        toCheck.Add(startState);

        var biggest = 0;

        var maxGeode = 0;
        var maxObsidian = 0;
        
        for(var minute = 0; minute < totalMinutes; minute++)
        {
            var newCheck = new List<ulong>();

            foreach (var state in toCheck)
            {
                if (minute == totalMinutes - 1)
                {
                    var resource = state + (state << 32);
                    var geode = resource >> (24+32);
                    if ((int) geode < 0)
                        throw new Exception(geode.ToString());
                    if ((int)geode > biggest)
                        biggest = (int)geode;
                }
                else
                {
                    var nothing = state + (state << 32);
                    newCheck.Add(nothing);

                    if (minute < totalMinutes - 7)
                    {
                        if ((state & 255) < blueprint.MaxRobots[0])
                        {
                            if (CanMake(blueprint.Robots[0], state))
                            {
                                var next = state - blueprint.Robots[0].Cost;
                                next += (next << 32);
                                next += 1;
                                newCheck.Add(next);
                            }
                        }
                    }

                    if (minute < totalMinutes - 5)
                    {
                        if ((state & (255 << 8)) < blueprint.MaxRobots[1])
                        {
                            if (CanMake(blueprint.Robots[1], state))
                            {
                                var next = state - blueprint.Robots[1].Cost;
                                next += (next << 32);
                                next += 1 << 8;
                                newCheck.Add(next);
                            }
                        }
                    }

                    if (minute < totalMinutes - 3)
                    {
                        if ((state & (255 << 16)) < blueprint.MaxRobots[2])
                        {
                            if (CanMake(blueprint.Robots[2], state))
                            {
                                var next = state - blueprint.Robots[2].Cost;
                                next += (next << 32);
                                next += 1 << 16;
                                newCheck.Add(next);
                            }
                        }
                    }

                    if (CanMake(blueprint.Robots[3], state))
                    {
                        var next = state - blueprint.Robots[3].Cost;
                        next += (next << 32);
                        next += 1 << 24;
                        newCheck.Add(next);
                    }
               }
            }

            toCheck = newCheck;
        }
        
        return biggest;
    }

    public bool CanMake(Robot robot, ulong resource)
    {
        for (var i = 0; i < 3; i++)
        {
            var m = ((ulong) 255) << (i * 8 + 32);
            var a = resource & m;
            var b = robot.Cost & m;
            if (a < b)
                return false;
        }

        return true;
    }

    public class Blueprint
    {
        public Robot[] Robots;
        public int Number;
        public uint[] MaxRobots;
        public ulong MaxOre;
        public ulong MaxClay;
        public ulong MaxObsidian;

        public Blueprint(string input)
        {
            Number = int.Parse(Word(input, ':'));
            var split = input.Split("Each ");
            Robots = split.Skip(1).Select(r => new Robot(r)).ToArray();

            MaxRobots = new uint[4];

            MaxRobots[0] = ((uint) Math.Max(Robots[1].Costs[0], Math.Max(Robots[2].Costs[0], Robots[3].Costs[0])));
            MaxRobots[1] = ((uint) Robots[2].Costs[1]) << 8;
            MaxRobots[2] = ((uint) Robots[3].Costs[2]) << 16;

            MaxOre = ((ulong) Math.Max(Math.Max(Robots[0].Costs[0], Robots[1].Costs[0]),
                Math.Max(Robots[2].Costs[0], Robots[3].Costs[0]))) +99;
            MaxOre <<= 32;

            MaxClay = ((ulong) Robots[2].Costs[1]);
            MaxClay <<= (32 + 8);
            
            MaxObsidian = ((ulong) Robots[3].Costs[2]);
            MaxObsidian <<= (32 + 16);
            // for (var i = 0; i < 3; i++)
            // {
            //     Max[i] = ((uint) Robots.Max(r => r.Costs[i])) << (i * 8);
            // }
        }
    }

    public class Robot
    {
        public int[] Costs = new int[4];
        public ulong Cost = 0;
        public Mineral Type;

        public Robot(string input)
        {
            var type = Word(input);
            Type = StringToMineral(type);
            var rest = input.Substring(type.Length).Replace(" robot costs ", "");
            rest = Word(rest, '.');
            var costs = rest.Split(" and ");
            foreach (var cost in costs)
            {
                var part = cost.Split(' ');
                var mineral = StringToMineral(part[1]);
                Costs[(int)mineral] = int.Parse(part[0]);
            }

            for (var i = 0; i < 3; i++)
            {
                Cost += ((ulong) Costs[i]) << (i * 8 + 32);
            }
        }

        public int CanMake(uint resource)
        {
  //          var min = int.MaxValue;
            for (var i = 0; i < 4; i++)
            {
                var a = (int)(resource >> (i * 8)) & 255;
                if (a < Costs[i])
                    return 0;
            }

            return 1;
                // if (Costs[i] > 0)
                // {
                //     var a = (int)(resource >> (i * 8)) & 255;
                //     min = Math.Min(min, a / Costs[i]);
                // }
                //
//            return min;
        }
    }

    public static string Word(string input, char delimiter = ' ') =>
        input.Substring(0, input.IndexOf(delimiter));
    
    public enum Mineral { ore, clay, obsidian, geode }

    public static Mineral StringToMineral(string input) =>
        input switch
        {
            "ore" => Mineral.ore,
            "clay" => Mineral.clay,
            "obsidian" => Mineral.obsidian,
            "geode" => Mineral.geode
        };
}