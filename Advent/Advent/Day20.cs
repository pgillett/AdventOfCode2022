using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent;

public class Day20
{
    public long Method2(string input)
    {
        var list = input.Split(Environment.NewLine)
            .Select(l => new Item(int.Parse(l) * 811589153L)).ToList();

        var process = list.ToList();
        for (var pass = 0; pass < 10; pass++)
        {
            foreach (var item in list)
            {
                var i = process.IndexOf(item);
                for (var m = 0; m < Math.Abs(item.Value) % (list.Count - 1); m++)
                {
                    if (item.Value > 0)
                    {
                        var t = i + 1;
                        var x = Mod(t, list.Count);
                        var y = Mod(i, list.Count);
                        (process[x], process[y]) =
                            (process[y], process[x]);
                        i++;
                    }
                    else
                    {
                        var t = i - 1;
                        var x = Mod(t, list.Count);
                        var y = Mod(i, list.Count);
                        (process[x], process[y]) =
                            (process[y], process[x]);
                        i--;
                    }
                }

            }


            Console.WriteLine();
            foreach (var p in process)
            {
                Console.Write(p.Value + " ");
            }
        }

        var find = process.Single(i => i.Value == 0);
        var start = process.IndexOf(find);
        var r1 = process[Mod(start + 1000, list.Count)].Value;
        var r2 = process[Mod(start + 2000, list.Count)].Value;
        var r3 = process[Mod(start + 3000, list.Count)].Value;
        
        return r1 + r2 + r3;
    }
    public long Method(string input)
    {
   //     var list = input.Split(Environment.NewLine)
     //       .Select(v => new Link(int.Parse(v)))
      //      .ToArray();
         var list = input.Split(Environment.NewLine)
             .Select(l => new Item(int.Parse(l))).ToList();

        var process = list.ToList();

         // for (var l = 0; l < list.Length; l++)
         // {
         //     if (l > 1) list[l].Before = list[l - 1];
         //     if (l < list.Length - 1) list[l].After = list[l + 1];
         // }
         //
         // list[0].Before = list[list.Length - 1];
         // list[list.Length - 1].After = list[0];

//        var pos = list[0];

        foreach (var item in list)
        {
            var i = process.IndexOf(item);
            for (var m = 0; m < Math.Abs(item.Value); m++)
            {
                if (item.Value > 0)
                {
                    var t = i + 1;
                    var x = Mod(t, list.Count);
                    var y = Mod(i, list.Count);
                    (process[x], process[y]) = 
                        (process[y], process[x]);
                    i++;
                }
                else
                {
                    var t = i - 1;
                    var x = Mod(t, list.Count);
                    var y = Mod(i, list.Count);
                    (process[x], process[y]) = 
                        (process[y], process[x]);
                    i--;
                }
            }
        
            // Console.WriteLine();
            // foreach(var p in process)
            // {
            //       Console.Write(p.Value+" ");
            // }
        }

        var find = process.Single(i => i.Value == 0);
        var start = process.IndexOf(find);
        var r1 = process[Mod(start + 1000, list.Count)].Value;
        var r2 = process[Mod(start + 2000, list.Count)].Value;
        var r3 = process[Mod(start + 3000, list.Count)].Value;
        
        return r1 + r2 + r3;
        
        

        

        // foreach (var item in list)
        // {
        //     var swap = item;
        //     for(var i = 0; i< Math.Abs(item.Value); i++)
        //     if (item.Value > 0)
        //     {
        //         for (var i = 0; i < item.Value; i++)
        //         {
        //             var a = item.After;
        //             var b = item.Before;
        //             b.After = a;
        //             a.Before = b;
        //             item.Before = a;
        //             item.After = a.After;
        //             a.After.Before = item;
        //             a.After = item;
        //         }
        //     }
        //     
        // }

        return 0;
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

    public class Link
    {
        public Link Before;
        public Link After;
        public int Value;

        public Link(int value)
        {
            Value = value;
        }
    }
}