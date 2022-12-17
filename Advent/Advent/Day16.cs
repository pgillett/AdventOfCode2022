using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent;

public class Day16
{
    public int MostPressure(string input) => MostPressure(input, false);

    public int MostPressureWithElephant(string input) => MostPressure(input, true);
    
    public int MostPressure(string input, bool withElephant)
    {
        var valves = ParseAll(input);

        var states = new List<State>();
        states.Add(new State(valves["AA"]));

        var allCount = valves.Values.Where(v => v.Rate > 0).Sum(v => v.Hash);

        var valveHash = valves.Values
            .Where(v => v.Rate > 0)
            .ToDictionary(v => v.Hash, v => v.Rate);

        var left = new int[allCount + 1];
        for (var i = 0; i < allCount; i++)
        {
            var count = 0;
            for (var j = 1; j < allCount; j = j << 1)
            {
                if ((i & j) == 0)
                    count += valveHash[j];
            }

            left[i] = count;
        }
        
        var biggest = 0;

        var seen = new Dictionary<int, int>();

        var maxPass = withElephant ? 26 : 30;

        for (var pass = 0; pass < maxPass; pass++)
        {
            var newList = new List<State>();
            var steps = (maxPass - pass) - 1;

            foreach (var state in states)
            {
                biggest = Math.Max(biggest, state.Pressure);

                if (state.OpenBits == allCount)
                {
                    continue;
                }

                var maxLeft = left[state.OpenBits];
                var maybe = state.Pressure + maxLeft * steps;

                if (maybe <= biggest)
                    continue;

                maybe -= maxLeft;

                var hash = state.Hash();
                if (seen.ContainsKey(hash))
                {
                    if (seen[hash] >= state.Pressure)
                        continue;
                }

                seen[hash] = state.Pressure;

                for (var m = state.CanOpen ? -1 : 0; m < state.At.LeadsTo.Length; m++)
                {
                    var to = m == -1 ? state.At : state.At.LeadsTo[m];
                    
                    if (withElephant)
                    {
                        var start = state.SamePlace ? Math.Max(m, 0) : (state.CanElephantOpen ? -1 : 0);
                        for (var e = start; e < state.ElephantAt.LeadsTo.Length; e++)
                        {
                            var elephantTo = e == -1 ? state.ElephantAt : state.ElephantAt.LeadsTo[e];
                            
                            if (m >= 0 && e >= 0)
                            {
                                if (maybe <= biggest)
                                    continue;
                                if (to == state.ElephantAt && elephantTo == state.At)
                                    continue;
                                if (to == state.CameFrom || state.ElephantAt == state.ElephantCameFrom)
                                    continue;
                            }

                            if (to != elephantTo || m != -1 || e != -1)
                            {
                                var newState = state.MoveTo(to, elephantTo, m == -1, e == -1, steps);
                                newList.Add(newState);
                            }
                        }
                    }
                    else
                    {
                        if (to != state.CameFrom && state.ElephantAt != state.ElephantCameFrom)
                        {
                            var newState = state.MoveTo(to, state.ElephantAt, m == -1, false, steps);
                            newList.Add(newState);
                        }
                    }
                }
            }

            states = newList.OrderByDescending(i => i.Pressure).ToList();
        }

        return biggest;
    }

    public Dictionary<string, Valve> ParseAll(string input)
    {
        var valves = input.Split(Environment.NewLine).Select(Parse).ToDictionary(v => v.Name, v => v);
        var bit = 1;
        foreach (var valve in valves.Values)
        {
            valve.AddLeads(valves);
            if (valve.Rate > 0)
            {
                valve.Hash = bit;
                bit <<= 1;
            }
        }

        return valves;
    }
    
    [DebuggerDisplay("At {At.Name} Elephant {ElephantAt.Name}")]
    public class State
    {
        public Valve At;
        public int Time;
        public int Pressure;
        public Valve CameFrom;
        public Valve ElephantAt;
        public Valve ElephantCameFrom;
        public int OpenBits;

        public int Hash()
        {
            if(At.Number < ElephantAt.Number)
                return OpenBits | (At.Number << 16) | (ElephantAt.Number << 24);
            return OpenBits | (ElephantAt.Number << 16) | (At.Number << 24);
        }
        
        public bool CanOpen => At.Rate > 0 && (OpenBits & At.Hash) == 0;

        public bool CanElephantOpen => ElephantAt.Rate > 0 
                                       && (OpenBits & ElephantAt.Hash) == 0;

        public bool SamePlace => At == ElephantAt;
        
        public State(Valve valve)
        {
            At = valve;
            ElephantAt = valve;
        }

        public State(State state)
        {
            OpenBits = state.OpenBits;
            Time = state.Time + 1;
            Pressure = state.Pressure;
        }
        
        public State MoveTo(Valve to, Valve elephant, bool open, bool openElephant, int steps)
        {
            var copy = new State(this)
            {
                At = to,
                ElephantAt = elephant,
                CameFrom = to != At ? At : null,
                ElephantCameFrom = elephant != ElephantAt ? ElephantAt : null
            };
            if (open)
            {
                copy.OpenBits += to.Hash;
                copy.Pressure += to.Rate * steps;
            }
            if (openElephant)
            {
                copy.OpenBits += elephant.Hash;
                copy.Pressure += elephant.Rate * steps;
            }
            return copy;
        }
    }

    public Valve Parse(string input, int number)
    {
        var split = input.Replace("Valve ", "")
            .Replace(" has flow rate", "")
            .Replace("; tunnel leads to valve ", "=")
            .Replace("; tunnels lead to valves ", "=")
            .Split('=');

        return new Valve(int.Parse(split[1]), split[0], split[2], number);
    }

    public class Valve
    {
        public string Name;
        public int Rate;
        public Valve[] LeadsTo;
        public string[] LeadStrings;
        public int Hash;
        public int Number;

        public Valve(int rate, string name, string leadsTo, int number)
        {
            Name = name;
            Rate = rate;
            LeadStrings = leadsTo.Split(", ");
            Number = number;
        }

        public void AddLeads(Dictionary<string, Valve> valves)
        {
            LeadsTo = LeadStrings.Select(s => valves[s]).ToArray();
        }
    }
}