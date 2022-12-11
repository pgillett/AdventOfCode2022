using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day11
{
    public long MonkeyBusiness(string input) => After(input, 20, true);

    public long NoWorryFactor(string input) => After(input, 10000, false);

    private long After(string input, int rounds, bool divide3)
    {
        var monkeys = input.Split(Environment.NewLine + Environment.NewLine)
            .Select(i => new Monkey(i)).ToArray();

        var factor = 1L;
        foreach (var t in monkeys)
            factor *= t.Divisible;

        for (var r = 0; r < rounds; r++)
        {
            foreach(var monkey in monkeys)
            {
                monkey.Inspected += monkey.Items.Count();
                foreach(var item in monkey.NewItems(divide3, factor))
                {
                    monkeys[monkey.ToMonkey(item)].Items.Add(item);
                }
                monkey.Items = new List<long>();
            }
        }

        var order = monkeys.OrderByDescending(m => m.Inspected).ToArray();
        return order[0].Inspected * order[1].Inspected;
    }
}

public class Monkey
{
    public List<long> Items;
    public bool Multiply;
    public int By;
    public int Divisible;
    public int TrueTo;
    public int FalseTo;
    public long Inspected;

    public Monkey(string input)
    {
        var split = input.Split(Environment.NewLine).ToArray();
        Items = split[1].Split(':')[1].Split(", ").Select(long.Parse).ToList();
        Multiply = split[2].Contains('*');
        By = int.Parse(Last(split[2].Replace("old", "0")));
        Divisible = int.Parse(Last(split[3]));
        TrueTo = int.Parse(Last(split[4]));
        FalseTo = int.Parse(Last(split[5]));
    }

    public int ToMonkey(long item) => item % Divisible == 0 ? TrueTo : FalseTo;
    
    public IEnumerable<long> NewItems(bool divide3, long factor) =>
        Items.Select(item =>
        {
            var by = By == 0 ? item : By;
            var newItem = Multiply ? item * by : item + by;
            if (divide3)
                newItem /= 3;
            newItem %= factor;
            return newItem;
        });

    public string Last(string input) => input.Split(' ')[^1];
}