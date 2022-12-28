using System;
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
            total *= max;
        }
        return total;
    }

    public int MaxGeode(Blueprint blueprint, int totalMinutes)
    {
        var state = new State(blueprint, totalMinutes);
        return state.Max;
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
            var resource = state + (state << 32);
            var geode = resource >> (24 + 32);

            GeodeMax = Math.Max(GeodeMax, (int) geode);

            if (minute == TotalMinutes - 1)
            {
                return (int) geode;
            }

            if (!Estimate(minute, state, GeodeMax))
            {
                return 0;
            }

            var max = 0;

            if (CanGeode(state))
            {
                var next = state - Blueprint.Robots[3].Cost;
                next += next << 32;
                next += 1 << 24;
                max = Math.Max(max, AtState(minute + 1, next));
                return max;
            }

            if (minute < TotalMinutes - 7)
            {
                if ((state & 255) < Blueprint.MaxRobots[0])
                {
                    if (CanOre(state))
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
                    if (CanClay(state))
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
                    if (CanObsidian(state))
                    {
                        var next = state - Blueprint.Robots[2].Cost;
                        next += next << 32;
                        next += 1 << 16;
                        max = Math.Max(max, AtState(minute + 1, next));
                    }
                }
            }

            if (((state & OreMask) < Blueprint.MaxOre) | ((state & ClayMask) < Blueprint.MaxClay)
                                                       | ((state & ObsidianMask) < Blueprint.MaxObsidian))
            {
                var nothing = state + (state << 32);
                max = Math.Max(max, AtState(minute + 1, nothing));
            }

            return max;
        }

        public bool CanOre(ulong state)
        {
            return (state & OreMask) >= Blueprint.Robots[0].Cost;
        }

        public bool CanClay(ulong state)
        {
            return (state & OreMask) >= Blueprint.Robots[1].Cost;
        }

        public bool CanObsidian(ulong state)
        {
            return (state & OreMask) >= (Blueprint.Robots[2].Cost & OreMask)
                   && (state & ClayMask) >= (Blueprint.Robots[2].Cost & ClayMask);
        }

        public bool CanGeode(ulong state)
        {
            return (state & OreMask) >= (Blueprint.Robots[3].Cost & OreMask)
                   && (state & ObsidianMask) >= (Blueprint.Robots[3].Cost & ObsidianMask);
        }
        
        public const ulong OreMask = ((ulong)255) << 32;
        public const ulong ClayMask = ((ulong)255) << (32 + 8);
        public const ulong ObsidianMask = ((ulong)255) << (32 + 16);
        
        public bool Estimate(int minute, ulong state, int maxGeode)
        {
            var forOre = (ulong)0;
            var forClay = (ulong)0;
            var forObsidian = (ulong)0;
            var forGeode = (ulong)0;
            
            while (minute < TotalMinutes)
            {
                minute++;

                state += (state << 32);

                if (CanOre(state - forOre))
                {
                    forOre += Blueprint.Robots[0].Cost;
                    state += 1;
                }

                if (CanClay(state - forClay))
                {
                    forClay += Blueprint.Robots[1].Cost;
                    state += 1 << 8;
                }

                if (CanObsidian(state - forObsidian))
                {
                    forObsidian += Blueprint.Robots[2].Cost;
                    state += 1 << 16;
                }

                if (CanGeode(state - forGeode))
                {
                    forGeode += Blueprint.Robots[3].Cost;
                    state += 1 << 24;
                }

                var geode = (int) (state >> (32 + 24));
                if (geode > maxGeode) return true;
            }

            return false;
        }
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
        }
    }

    public class Robot
    {
        public int[] Costs = new int[4];
        public ulong Cost = 0;

        public Robot(string input)
        {
            var type = Word(input);
            var rest = input.Substring(type.Length).Replace(" robot costs ", "");
            rest = Word(rest, '.');
            var costs = rest.Split(" and ");
            foreach (var cost in costs)
            {
                var part = cost.Split(' ');
                var mineral = StringToMineral(part[1]);
                Costs[mineral] = int.Parse(part[0]);
            }

            for (var i = 0; i < 3; i++)
            {
                Cost += ((ulong) Costs[i]) << (i * 8 + 32);
            }
        }
    }

    public static string Word(string input, char delimiter = ' ') =>
        input.Substring(0, input.IndexOf(delimiter));
    
    public static int StringToMineral(string input) =>
        input switch
        {
            "ore" => 0,
            "clay" => 1,
            "obsidian" => 2,
            "geode" => 3
        };
}