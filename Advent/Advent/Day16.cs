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

        var queue = new Queue<State>();
        queue.Enqueue(new State(valves["AA"]));

        var allCount = valves.Values.Count(v => v.Rate > 0);
        
        var biggest = 0;

        var seen = new Dictionary<int, int>();

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.Time == (withElephant ? 25 : 29))
            {
                if (state.Pressure + state.OpenPressure > biggest)
                    biggest = state.Pressure + state.OpenPressure;
            }
            else
            {
                if (state.OpenBits == allCount)
                {
                    queue.Enqueue(state.MoveTo(state.At, state.ElephantAt, false, false));
                }
                else
                {
                    for (var m = state.CanOpen ? -1 : 0; m < state.At.LeadsTo.Length; m++)
                    {
                        var to = m == -1 ? state.At : state.At.LeadsTo[m];
                        if (withElephant)
                        {
                            var start = state.SamePlace ? Math.Max(m, 0) : (state.CanElephantOpen ? -1 : 0);
                            for (var e = start; e < state.ElephantAt.LeadsTo.Length; e++)
                            {
                                var elephantTo = e == -1 ? state.ElephantAt : state.ElephantAt.LeadsTo[e];

                                if (to != elephantTo || m != e)
                                {
                                    if (to != state.CameFrom && state.ElephantAt != state.ElephantCameFrom)
                                    {
                                        State newState;
                                        newState = state.MoveTo(to, elephantTo, m == -1, e == -1);
                                        var newHash = newState.Hash();
                                        if (!seen.ContainsKey(newHash) || seen[newHash] < newState.Pressure)
                                        {
                                            queue.Enqueue(newState);
                                            seen[newHash] = newState.Pressure;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (to != state.CameFrom && state.ElephantAt != state.ElephantCameFrom)
                            {
                                var newState = state.MoveTo(to, state.ElephantAt, m == -1, false);
                                var newHash = newState.Hash();
                                if (!seen.ContainsKey(newHash) || seen[newHash] < newState.Pressure)
                                {
                                    queue.Enqueue(newState);
                                    seen[newHash] = newState.Pressure;
                                }
                            }
                        }
                    }
                }
            }
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
        public int OpenPressure;
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
            Pressure = state.Pressure + state.OpenPressure;
            OpenPressure = state.OpenPressure;
        }
        
        public State MoveTo(Valve to, Valve elephant, bool open, bool openElephant)
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
                copy.OpenPressure += to.Rate;
            }
            if (openElephant)
            {
                copy.OpenBits += elephant.Hash;
                copy.OpenPressure += elephant.Rate;
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