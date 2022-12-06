using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day05
{
    public string FinalTop9000(string input) => FinalTop(input, false);
    public string FinalTop9001(string input) => FinalTop(input, true);
    
    public string FinalTop(string input, bool inOrder)
    {
        var parts = input.Split(Environment.NewLine + Environment.NewLine);
        var stacks = new Stacks(parts[0]);
        var instructions = parts[1].Split(Environment.NewLine);

        foreach (var instruction in instructions)
        {
            stacks.Move(instruction, inOrder);
        }

        return stacks.Top();
    }
    
    public class Stacks
    {
        public Stack<char>[] Columns;

        public Stacks(string input)
        {
            var split = input.Split(Environment.NewLine);
            var number = (split[0].Length + 1) / 4;

            Columns = new Stack<char>[number];
            for (var i = 0; i < number; i++)
                Columns[i] = new Stack<char>();

            for (var i = split.Length - 2; i >= 0; i--)
            {
                for (var n = 1; n <= split[i].Length; n+=4)
                {
                    var c = split[i][n];
                    if (c != ' ')
                        Columns[(n - 1) / 4].Push(c);
                }
            }
        }

        public void Move(string instruction, bool inOrder)
        {
            var split = instruction.Split(' ');
            var number = int.Parse(split[1]);
            var from = int.Parse(split[3]);
            var to = int.Parse(split[5]);
            
            if (inOrder)
            {
                var move = new Stack<char>();
                for (var i = 0; i < number; i++)
                {
                    move.Push(Columns[from - 1].Pop());
                }

                for (var i = 0; i < number; i++)
                {
                    Columns[to - 1].Push(move.Pop());
                }
            }
            else
            {
                for (var i = 0; i < number; i++)
                {
                    var crate = Columns[from - 1].Pop();
                    Columns[to - 1].Push(crate);
                }
            }
        }

        public string Top() =>
            new string(Columns.Where(c => c.Count > 0).Select(c => c.Pop()).ToArray());
    }
}