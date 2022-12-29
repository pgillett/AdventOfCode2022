using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day20
{
    public long GroveDecryption(string input) => Mix(input, 811589153L, 10);

    public long GroveCoordinate(string input) => Mix(input, 1, 1);

    public long Mix(string input, long encryption, int number)
    {
        var list = input.Split(Environment.NewLine)
            .Select(l => new Item(int.Parse(l) * encryption)).ToList();
        
        var process = list.ToList();

        for (var pass = 0; pass < number; pass++)
        {
            foreach (var item in list)
            {
                var pos = process.IndexOf(item);
                for (var move = 0; move < Math.Abs(item.Value) % (list.Count - 1); move++)
                {
                    var first = Mod(pos, list.Count);
                    var direction = item.Value > 0 ? 1 : -1;
                    var second = Mod(pos + direction, list.Count);
                    pos += direction;
                    (process[second], process[first]) = (process[first], process[second]);
                }
            }
        }

        return Calc(process, list);
    }
    
    private long Calc(List<Item> process, List<Item> list)
    {
        var find = process.Single(i => i.Value == 0);
        var start = process.IndexOf(find);
        var r1 = process[(start + 1000) % list.Count].Value;
        var r2 = process[(start + 2000) % list.Count].Value;
        var r3 = process[(start + 3000) % list.Count].Value;

        return r1 + r2 + r3;
    }

    public class Item
    {
        public long Value;
        public Item(long value)
        {
            Value = value;
        }
    }
    
    public int Mod(int i, int m) => (i % m + m) % m;
}