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
                        if ((state & 255) < blueprint.Max[0])
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
                        if ((state & (255 << 8)) < blueprint.Max[1])
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
                        if ((state & (255 << 16)) < blueprint.Max[2])
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

    [DebuggerDisplay(
        "Robots: {Robots[0]} {Robots[1]} {Robots[2]} {Robots[3]} Resources: {Resources[0]} {Resources[1]} {Resources[2]} {Resources[3]} Making: {Making[0]} {Making[1]} {Making[2]} {Making[3]}")]
    public class State
    {
//        public int Minutes = 0;
        public uint Resource;

        public uint Robot;
//        public uint Making;

        // public int[] Resources = new int[4];
        // public int[] Robots = new int[4];
        // public int[] Making = new int[4];

        public State()
        {
        }

        // public void Process()
        // {
        //     Minutes++;
        //     Resource += Robot;
        //     Robot += Making;
        //     Making = 0;
        //     // for (var i = 0; i < 4; i++)
        //     // {
        //     //     Resources[i] += Robots[i];
        //     //     Robots[i] += Making[i];
        //     //     Making[i] = 0;
        //     // }
        // }

        public void AddRobot(int r0, int r1, int r2, int r3)
        {
            Robot += ((uint) r0) + (((uint) r1) << 8) + (((uint) r2) << 16) + (((uint) r3) << 24);
        }

        public State Copy()
        {
//            var copy2 = this;
            var copy = new State();
//            copy.Minutes = Minutes;
            copy.Robot = Robot;
//            copy.Making = Making;
            copy.Resource = Resource;
            // for (var i = 0; i < 4; i++)
            // {
            //     copy.Robots[i] = Robots[i];
            //     copy.Making[i] = Making[i];
            //     copy.Resources[i] = Resources[i];
            // }

            return copy;
        }

//         public void Adjust(Robot robot, int number)
//         {
//             if (number == 0) return;
//             Making += (uint)number << ((int)robot.Type * 8);
//             Resource -= robot.Cost;
// //            Resources[0] -= robot.Costs[0] * number;
//  //           Resources[1] -= robot.Costs[1] * number;
//   //          Resources[2] -= robot.Costs[2] * number;
//         }
//     }
    }

    public class Blueprint
    {
        public Robot[] Robots;
        public int Number;
        public uint[] Max;

        public Blueprint(string input)
        {
            Number = int.Parse(Word(input, ':'));
            var split = input.Split("Each ");
            Robots = split.Skip(1).Select(r => new Robot(r)).ToArray();

            Max = new uint[4];

            for (var i = 0; i < 3; i++)
            {
                Max[i] = ((uint) Robots.Max(r => r.Costs[i])) << (i * 8);
            }
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