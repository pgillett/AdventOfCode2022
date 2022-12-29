using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day24
{
    public int Steps(string input) => MethodMain(input, false);
    public int StepsBackBack(string input) => MethodMain(input, true);
    public int MethodMain(string input, bool both)
    {
        var lines = input.Split(Environment.NewLine);

        var maxY = lines.Length;
        var maxX = lines[0].Length;

        var blizzards = new List<Blizzard>();
        for(var y = 1; y<maxY -1; y++)
            for(var x = 1; x<maxX -1; x++)
                if("^v<>".Contains(lines[y][x]))
                    blizzards.Add(new Blizzard(x,y,lines[y][x]));

        var shortest = 0;

        var targets = new (int x, int y)[] {(maxX - 2, maxY - 1), (1, 0), (maxX - 2, maxY - 1)};
    
        var posList = new List<(int x, int y, int step)>() {(1, 0, both ? 2 : 0)};
        
        while (posList.Count > 0)
        {
            foreach (var blizzard in blizzards)
            {
                blizzard.Move(maxX, maxY);
            }
            
            shortest++;
                
            var map = blizzards.Select(b => (b.X, b.Y)).ToHashSet();

            var newList = new HashSet<(int x, int y, int step)>();

            foreach (var pos in posList)
            {
                var step = pos.step;
                
                if (pos.x == targets[step].x && pos.y == targets[step].y)
                {
                    if (step == 0)
                        return shortest - 1;
                    step--;
                }
                
                foreach (var move in Moves)
                {
                    var nx = pos.x + move.x;
                    var ny = pos.y + move.y;
                    if (nx >= 0 && ny >= 0 && nx <= maxX - 1 && ny <= maxY - 1)
                    {
                        if (lines[ny][nx] != '#')
                        {
                            if (!map.Contains((nx, ny)))
                                newList.Add((nx, ny, step));
                        }
                    }
                }
            }

            posList = newList.ToList();
        }

        return 0;
    }

    public (int x, int y)[] Moves = {(0, -1), (0, 1), (-1, 0), (1, 0), (0, 0)};
    
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public class Blizzard
    {
        public int X;
        public int Y;
        public Direction Direction;

        public Blizzard(int x, int y, char d)
        {
            X = x;
            Y = y;
            Direction = (Direction) "^v<>".IndexOf(d);
        }

        public void Move(int maxX, int maxY)
        {
            (X, Y) = Direction switch
            {
                Direction.Up => (X, Y == 1 ? maxY - 2 : Y - 1),
                Direction.Down => (X, Y == maxY - 2 ? 1 : Y + 1),
                Direction.Left => (X == 1 ? maxX - 2 : X - 1, Y),
                Direction.Right => (X == maxX - 2 ? 1 : X + 1, Y)
            };
        }
    }
}