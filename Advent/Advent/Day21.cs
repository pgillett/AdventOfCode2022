using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day21
{
    public long Root(string input)
    {
        var monkeys = Parse(input);

        return monkeys["root"].Value(monkeys).Value;
    }

    public long Me(string input)
    {
        var monkeys = Parse(input);

        monkeys["humn"].LiteralValue = null;
        var root = monkeys["root"];

        var left = monkeys[root.Left].Value(monkeys);
        if (left != null)
        {
            return monkeys[root.Right].Solve(left.Value, monkeys).Value;
        }

        var right = monkeys[root.Right].Value(monkeys);
        return monkeys[root.Left].Solve(right.Value, monkeys).Value;
    }

    public Dictionary<string, Monkey> Parse(string input) => 
        input.Split(Environment.NewLine).Select(l => new Monkey(l))
        .ToDictionary(m => m.Name, m => m);

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

        public long? Value(Dictionary<string, Monkey> monkeys)
        {
            if (LiteralValue.HasValue) return LiteralValue.Value;

            if (Left == null || Right == null) return null;
            var left = monkeys[Left].Value(monkeys);
            var right = monkeys[Right].Value(monkeys);

            var value = Op switch
            {
                '+' => left + right,
                '-' => left -right,
                '*' => left * right,
                '/' => left / right,
                _ => null
            };

            LiteralValue = value;
            return value;
        }
        
        public long? Solve(long answer, Dictionary<string, Monkey> monkeys)
        {
            if (Name == "humn") return answer;

            if (LiteralValue.HasValue) return LiteralValue.Value;

            if (Left == null || Right == null) return null;

            var left = monkeys[Left].Value(monkeys);
            if (left != null)
            {
                var target = Op switch
                {
                    '+' => answer - left.Value,
                    '-' => left.Value - answer,
                    '*' => answer / left.Value,
                    '/' => left.Value / answer,
                    _ => throw new Exception()
                };
                return monkeys[Right].Solve(target, monkeys);
            }
            else
            {
                var right = monkeys[Right].Value(monkeys).Value;
                var target = Op switch
                {
                    '+' => answer - right,
                    '-' => answer + right,
                    '*' => answer / right,
                    '/' => answer * right,
                    _ => throw new Exception()
                };
                return monkeys[Left].Solve(target, monkeys);
            }
        }
    }
}