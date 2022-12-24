using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day23
{
    public int Method(string input) => Method2(input, 10, false);

    public int Method3(string input) => Method2(input, 999999999, true);

    public int Method2(string input, int rounds, bool stop)
    {
        var elves = new List<Elf>();

        {
            var y = 0;
            foreach (var line in input.Split(Environment.NewLine))
            {
                for (var x = 0; x < line.Length; x++)
                    if (line[x] == '#')
                        elves.Add(new Elf(x, y));
                y++;
            }
        }

        for (var round = 0; round < rounds; round++)
        {
            var startPos = elves.Select(e => (e.X, e.Y)).ToHashSet();
            foreach (var elf in elves)
            {
                var clear = IsClear(startPos, elf,
                    new[] {(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)});

                if (clear)
                {

                }
                else
                {
                    for (var d = 0; d < 4; d++)
                    {
                        var dir = (elf.Direction + d) % 4;


                        if (dir == 0 && IsClear(startPos, elf, new[] {(-1, -1), (0, -1), (1, -1)}))
                        {
                            elf.Move(0, -1);
                            break;
                        }

                        if (dir == 1 && IsClear(startPos, elf, new[] {(-1, 1), (0, 1), (1, 1)}))
                        {
                            elf.Move(0, 1);
                            break;
                        }

                        if (dir == 2 && IsClear(startPos, elf, new[] {(-1, -1), (-1, 0), (-1, 1)}))
                        {
                            elf.Move(-1, 0);
                            break;
                        }

                        if (dir == 3 && IsClear(startPos, elf, new[] {(1, -1), (1, 0), (1, 1)}))
                        {
                            elf.Move(1, 0);
                            break;
                        }
                    }
                }
            }

            var map = new Dictionary<(int x, int y), int>();
            foreach (var elf in elves.Where(e => e.IsMoving))
            {
                var p = (elf.IntendX, elf.IntendY);
                if (!map.ContainsKey(p)) map[p] = 0;
                map[p]++;
            }

            foreach (var elf in elves)
            {
                var p = (elf.IntendX, elf.IntendY);
                if (map.ContainsKey(p) && map[p] > 1)
                    elf.Clear();
            }

            if (stop && !elves.Any(e => e.IsMoving)) return round+1;

            foreach (var elf in elves)
            {
                var p = (elf.IntendX, elf.IntendY);
                elf.Update();
            }

            if(false)
            {
                Console.WriteLine($"Round {round}");
                var minX = elves.Min(e => e.X);
                var maxX = elves.Max(e => e.X);
                var minY = elves.Min(e => e.Y);
                var maxY = elves.Max(e => e.Y);
                var hash = elves.Select(e => (e.X, e.Y)).ToHashSet();
                for (var y = minY; y <= maxY; y++)
                {
                    Console.WriteLine();
                    for (var x = minX; x <= maxX; x++)
                {
                    Console.Write(hash.Contains((x, y))? '#': '.');
                }
                    
                }
                Console.WriteLine();
                Console.WriteLine();

            }
    }

        {
            var minX = elves.Min(e => e.X);
            var maxX = elves.Max(e => e.X);
            var minY = elves.Min(e => e.Y);
            var maxY = elves.Max(e => e.Y);

            var hash = elves.Select(e => (e.X, e.Y)).ToHashSet();
            var ground = 0;
            for (var y = minY; y <= maxY; y++)
            for (var x = minX; x <= maxX; x++)
            {
                if (!hash.Contains((x, y))) ground++;
            }

            return ground;
        }
    }

    public class Elf
    {
        public int X;
        public int Y;

        public int IntendX;
        public int IntendY;

        public bool IsMoving;

        public int Direction;

        public Elf(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(int x, int y)
        {
            IntendX = X + x;
            IntendY = Y + y;
            IsMoving = true;
        }

        public void Update()
        {
            if (IsMoving)
            {
                X = IntendX;
                Y = IntendY;
            }

            IsMoving = false;
            Direction = (Direction + 1) % 4;
        }

        public void Clear()
        {
            IsMoving = false;
        }
    }

    public bool IsClear(HashSet<(int x, int y)> map, Elf elf, (int x, int y)[] positions)
    {
        foreach (var pos in positions)
        {
            if (map.Contains((elf.X + pos.x, elf.Y + pos.y))) return false;
        }

        return true;
    }
}