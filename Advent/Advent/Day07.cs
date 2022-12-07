using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day07
{
    public int SumMost100000(string input)
    {
        Process(input);
        return AllDirectories.Select(d => d.Size()).Where(s => s <= 100000).Sum();
    }
    
    public int FreeUp(string input)
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
        var split = line.Split(' ');
        switch ((split[0], split[1]))
        {
            case ("$", "cd"):
                CurrentDirectory = split[2] switch
                {
                    ".." => CurrentDirectory.Parent,
                    "/" => RootDirectory,
                    _ => CurrentDirectory.Cd(split[2])
                };
                break;
            case ("$", "ls"):
                break;
            case ("dir", _):
                var newDirectory = new Item(CurrentDirectory);
                CurrentDirectory.Items[split[1]] = newDirectory;
                AllDirectories.Add(newDirectory);
                break;
            default:
                CurrentDirectory.Items[split[1]] = new Item(int.Parse(split[0]));
                break;
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
    public int FileSize;

    public int Size() => FileSize + Items.Values.Sum(d => d.Size());
    
    public Item Parent;

    public Item(Item parent)
    {
        Parent = parent;
    }

    public Item(int fileSize)
    {
        FileSize = fileSize;
    }

    public Item Cd(string name) => Items[name];
}