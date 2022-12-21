using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent;

public class Day21
{
    public long Answer(string input)
    {
        var monkeys = input.Split(Environment.NewLine).Select(l => new Monkey(l))
            .ToDictionary(m => m.Name, m => m);

        return monkeys["root"].Value(monkeys);
    }

    public long Me(string input)
    {
        var monkeys = input.Split(Environment.NewLine).Select(l => new Monkey(l))
            .ToDictionary(m => m.Name, m => m);
        
        var humn = monkeys["humn"];
        humn.LiteralValue = null;
        var root = monkeys["root"];

        long answer;
        
        try
        {
            var left = monkeys[root.Left].Value(monkeys);
            answer = Solve(monkeys[root.Right], left, monkeys);
        }
        catch
        {
            var right = monkeys[root.Right].Value(monkeys);
            answer = Solve(monkeys[root.Left], right, monkeys);
        }

        return answer;
    }

    public long Solve(Monkey monkey, long answer, Dictionary<string, Monkey> monkeys)
    {
        if (monkey.Name == "humn") return answer;

        if (monkey.LiteralValue.HasValue) return monkey.LiteralValue.Value;

        try
        {
            var left = monkeys[monkey.Left].Value(monkeys);
            var target = monkey.Op switch
            {
                '+' => answer - left,
                '-' => left - answer,
                '*' => answer / left,
                '/' => left / answer,
                _ => throw new Exception()
            };
            return Solve(monkeys[monkey.Right], target, monkeys);
        }
        catch
        {
            var right = monkeys[monkey.Right].Value(monkeys);
            var target = monkey.Op switch
            {
                '+' => answer - right,
                '-' => answer + right,
                '*' => answer / right,
                '/' => answer * right,
                _ => throw new Exception()
            };
            return Solve(monkeys[monkey.Left], target, monkeys);
        }
    }

    public class Monkey
    {
        public string Name;
        public long? LiteralValue;

        public char Op;
        public string Left;
        public string Right;

        public Monkey(string input)
        {
            Name = input.Substring(0, 4);

            if (char.IsDigit(input[6]))
            {
                LiteralValue = int.Parse(input.Substring(6));
            }
            else
            {
                var split = input.Substring(6).Split(' ');
                Left = split[0];
                Op = split[1][0];
                Right = split[2];
            }
        }

        public long Value(Dictionary<string, Monkey> monkeys)
        {
            if (LiteralValue.HasValue) return LiteralValue.Value;

            var left = monkeys[Left].Value(monkeys);
            var right = monkeys[Right].Value(monkeys);

            var value = Op switch
            {
                '+' => left + right,
                '-' => left -right,
                '*' => left * right,
                '/' => left / right,
                _ => throw new Exception()
            };

            LiteralValue = value;
            return value;
        }
    }
}