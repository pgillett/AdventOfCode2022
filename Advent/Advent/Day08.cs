using System;
using System.Collections.Generic;

namespace Advent;

public class Day08
{
    private string[] _trees;
    private HashSet<(int, int)> _visible;
    private int _size;
    
    public int VisibleTrees(string input)
    {
        Parse(input);

        for (var i = 0; i < _size; i++)
        {
            Scan(0, i, 1, 0);
            Scan(_size - 1, i, -1, 0);
            Scan(i, 0, 0, 1);
            Scan(i, _size - 1, 0, -1);
        }
        
        return _visible.Count;
    }
    
    public int HighestScenic(string input)
    {
        Parse(input);

        var scenic = 0;

        for (var x = 1; x < _size - 1; x++)
        {
            for (var y = 1; y < _size - 1; y++)
            {
                scenic = Math.Max(scenic, ScenicAt(x, y));
            }
        }

        return scenic;
    }

    private void Parse(string input)
    {
        _trees = input.Split(Environment.NewLine);
        _size = _trees.Length;
        _visible = new HashSet<(int, int)>();
    }
    
    private void Scan(int xPos, int yPos, int dx, int dy)
    {
        var max = 0;
        while (yPos >= 0 && yPos < _size && xPos >= 0 && xPos < _size)
        {
            if (_trees[yPos][xPos] > max)
            {
                _visible.Add((xPos, yPos));
                max = _trees[yPos][xPos];
            }
            xPos += dx;
            yPos += dy;
        }
    }

    private int ScenicAt(int xPos, int yPos) =>
        Count(xPos, yPos, -1, 0) *
        Count(xPos, yPos, 1, 0) *
        Count(xPos, yPos, 0, -1) *
        Count(xPos, yPos, 0, 1);

    private int Count(int xPos, int yPos, int dx, int dy)
    {
        var max = _trees[yPos][xPos];
        var visible = 0;
        xPos += dx;
        yPos += dy;
        while (yPos >= 0 && yPos < _size && xPos >= 0 && xPos < _size)
        {
            visible++;
            if (_trees[yPos][xPos] >= max)
                break;
            xPos += dx;
            yPos += dy;
        }

        return visible;
    }
}