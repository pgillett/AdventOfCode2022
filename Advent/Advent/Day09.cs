using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day09
{
    public int CoversShort(string input) => Covers(input, 2);
    
    public int CoversLong(string input) => Covers(input, 10);

    private int Covers(string input, int number)
    {
        var covers = new HashSet<Knot>();

        var knots = Enumerable.Repeat(new Knot(0, 0), number).ToArray();

        foreach (var line in input.Split(Environment.NewLine))
        {
            var split = line.Split(' ');
            
            for (var i = 0; i < int.Parse(split[1]); i++)
            {
                knots[0] = split[0] switch
                {
                    "R" => knots[0].Move(1, 0),
                    "L" => knots[0].Move(-1, 0),
                    "U" => knots[0].Move(0, 1),
                    "D" => knots[0].Move(0, -1),
                    _ => throw new Exception("invalid")
                };

                for (var k = 1; k < number; k++)
                {
                    knots[k] = knots[k].MoveToward(knots[k - 1]);
                }

                covers.Add(knots[number - 1]);
            }
        }

        return covers.Count;
    }
}

public record Knot(int X, int Y)
{
    public Knot Move(int dx, int dy) => new(X + dx, Y + dy);
    
    public Knot MoveToward(Knot follow) {
        var dx = follow.X - X;
        var dy = follow.Y - Y;
        return (Math.Abs(dx), Math.Abs(dy)) switch
        {
            (> 1, > 1) => Move(dx - Math.Sign(dx), dy - Math.Sign(dy)),
            (> 1, _) => Move(dx - Math.Sign(dx), dy),
            (_, > 1) => Move(dx, dy - Math.Sign(dy)),
            _ => this
        };
    }
}