using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day17
{
    public long Trillion(string input) => UnitsAfter(input, 1000000000000L);
    
    public long UnitsTall(string input) => UnitsAfter(input, 2022);
    
    public long UnitsAfter(string input, long number)
    {
        var rocks = RockInput.Split(Environment.NewLine + Environment.NewLine)
            .Select(s => new Rock(s)).ToArray();

        var map = new Map();

        var repeat = new Dictionary<string, List<(int height, int count)>>();

        var jetPos = 0;

        var rockCount = 0;
        while (true)
        {
            var rock = rocks[rockCount % rocks.Length];

            var top = map.Top();

            var x = 3;
            var y = top + 4;

            if (top > 5)
            {
                var hash = $"{rockCount % 5} {jetPos} " +
                           $"{map.Lines[top]} " +
                           $"{map.Lines[top - 1]} " +
                           $"{map.Lines[top - 2]} " +
                           $"{map.Lines[top - 3]} ";

                if (!repeat.ContainsKey(hash))
                {
                    repeat[hash] = new List<(int, int)>();
                }
                else
                {
                    if (repeat.Values.Any(r => r.Count > 2))
                        break;
                }

                repeat[hash].Add((top, rockCount));
            }

            map.ExpandTo(y + rock.Lines.Count - 1);

            while (true)
            {
                var dx = input[jetPos] == '>' ? 1 : -1;

                if (++jetPos == input.Length) 
                    jetPos = 0;

                if (!map.Collide(rock, x + dx, y))
                    x += dx;

                if (map.Collide(rock, x, y - 1))
                    break;

                y--;
            }

            map.Add(rock, x, y);
            rockCount++;
        }

        var filter = repeat.Values.Where(v => v.Count > 1).ToArray();

        var deltaHeight = filter.First()[1].height - filter.First()[0].height;
        var deltaCount = filter.First()[1].count - filter.First()[0].count;

        var remainders = filter.ToDictionary(f => f[0].count % deltaCount,
            f => f[0].height % deltaHeight);

        var repeats = number / deltaCount;
        var rem = (int)(number % deltaCount);

        var height = repeats * deltaHeight;
        height += remainders[rem];
        
        return height;
    }

    public class Map
    {
        public List<int> Lines = new();

        public const int Blank = 0b100000001;

        public Map()
        {
            Lines.Add(0b111111111);
        }

        public void ExpandTo(int expand)
        {
            while (Lines.Count <= expand)
                Lines.Add(Blank);
        }

        public int Top()
        {
            for (var y = Lines.Count - 1; y > 0; y--)
                if (Lines[y] != Blank)
                    return y;
            return 0;
        }

        public void Add(Rock rock, int x, int y)
        {
            for (var l = 0; l < rock.Lines.Count; l++)
            {
                Lines[y + l] |= rock.Lines[l] << x;
            }
        }

        public bool Collide(Rock rock, int x, int y)
        {
            for (var l = 0; l < rock.Lines.Count; l++)
            {
                if ((Lines[y + l] & (rock.Lines[l] << x)) != 0)
                    return true;
            }

            return false;
        }
    }

    public class Rock
    {
        public List<int> Lines = new();

        public Rock(string input)
        {
            var split = input.Split(Environment.NewLine);
            foreach (var s in split.Reverse())
            {
                var line = 0;
                for (var c = 0; c < s.Length; c++)
                    if (s[c] == '#')
                        line |= 1 << c;
                Lines.Add(line);
            }
        }
    }

    public string RockInput = @"####

.#.
###
.#.

..#
..#
###

#
#
#
#

##
##";
}