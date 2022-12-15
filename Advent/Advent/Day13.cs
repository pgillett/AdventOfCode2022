using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day13
{
    public int SumIndices(string input)
    {
        var inputs = ParsePairs(input);

        var results = inputs.Where(i => i.left.CompareTo(i.right) == -1).ToArray();

        return results.Select(r => inputs.IndexOf(r) + 1).Sum();
    }

    private List<(Item left, Item right)> ParsePairs(string input)
    {
        return input.Split(Environment.NewLine + Environment.NewLine)
            .Select(ParsePair).ToList();
    }

    private (Item left, Item right) ParsePair(string input)
    {
        var split = input.Split(Environment.NewLine);
        return (Item.Parse(split[0]), Item.Parse(split[1]));
    }
    
    
    public int Decoder(string input)
    {
        var inputs = ParsePairs(input);

        var pairs = inputs.SelectMany(i => new [] {i.left, i.right}).ToList();

        var pair2 = Item.Parse("[[2]]");
        var pair6 = Item.Parse("[[6]]");
        
        pairs.AddRange(new [] {pair2,pair6});

        pairs.Sort();

        return (pairs.IndexOf(pair2) + 1) * (pairs.IndexOf(pair6) + 1);
    }

    public class Item : IComparable<Item>
    {
        public bool IsIndividual;
        public int Value;
        public List<Item> Items = new();
       
        public static Item Parse(string input)
        {
            if (input.Contains('[') || input.Contains(','))
            {
                input = input.Substring(1, input.Length - 2);
                return ItemList(input);
            }

            var item = new Item {IsIndividual = true, Value = int.Parse(input)};
            item.Items.Add(item);
            return item;
        }

        private static Item ItemList(string input)
        {
            if (input.Length == 0) return new Item {Items = new List<Item>()};
            
            var indent = 0;
            var items = new List<string> {""};
            foreach (var c in input)
            {
                if (c == ',' && indent == 0)
                {
                    items.Add("");
                }
                else
                {
                    if (c == '[') indent++;
                    if (c == ']') indent--;
                    items[^1] += c;
                }
            }

            return new Item {Items = items.Select(Parse).ToList()};
        }

        public int CompareTo(Item right)
        {
            if (IsIndividual && right.IsIndividual)
            {
                return Value.CompareTo(right.Value);
            }

            for (var i = 0; i < Math.Max(Items.Count, right.Items.Count); i++)
            {
                if (i >= Items.Count) return -1;
                if (i >= right.Items.Count) return 1;

                var result = Items[i].CompareTo(right.Items[i]);

                if (result != 0) return result;
            }

            return 0;
        }
    }
}