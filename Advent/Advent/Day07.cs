using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day07
{
    public long SumMost100000(string input)
    {
        Process(input);
        return AllDirectories.Select(d => d.Size()).Where(s => s <= 100000).Sum();
    }
    
    public long FreeUp(string input)
    {
        Process(input);

        var free = 70000000 - RootDirectory.Size();
        var toFree = 30000000 - free;

        var sizes = AllDirectories.Select(d => d.Size()).OrderBy(s => s);

        return sizes.First(s => s >= toFree);
    }

    public Item CurrentDirectory;

    public Item RootDirectory;

    public List<Item> AllDirectories;

    public void Parse(string line)
    {
        if (line[0] == '$')
        {
            if (line.Substring(2, 2) == "cd")
            {
                var cd = line.Substring(5);
                CurrentDirectory = cd switch
                {
                    ".." => CurrentDirectory.Parent,
                    "/" => RootDirectory,
                    _ => CurrentDirectory.Cd(cd)
                };
            }
        }
        else
        {
            var split = line.Split(' ');
            if (split[0][0] == 'd')
            {
                var newDirectory = new Item(CurrentDirectory);
                CurrentDirectory.Items[split[1]] = newDirectory;
                AllDirectories.Add(newDirectory);
            }
            else
            {
                CurrentDirectory.Items[split[1]] = new Item(long.Parse(split[0]));
            }
        }
    }

    private void Process(string input)
    {
        RootDirectory = new Item(null);
        AllDirectories = new List<Item> {RootDirectory};
        CurrentDirectory = RootDirectory;

        foreach (var line in input.Split(Environment.NewLine))
        {
            Parse(line);
        }
    }
}

public class Item
{
    public Dictionary<string, Item> Items = new();
    public long FileSize;

    public long Size() => FileSize + Items.Values.Sum(d => d.Size());
    
    public Item Parent;

    public Item(Item parent)
    {
        Parent = parent;
    }

    public Item(long fileSize)
    {
        FileSize = fileSize;
    }

    public Item Cd(string name) => Items[name];
}