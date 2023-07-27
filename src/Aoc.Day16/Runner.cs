using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day16;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 16;
  }

  public int Puzzle1()
  {
    var valves    = this.BuildValves();
    var simulator = new Simulator(valves, "AA", 1, 30);
    var bests     = simulator.CalculateMaximumRelief(1, 6);

    return bests.Values.Max();
  }

  public int Puzzle2()
  {
    // get best solutions
    var valves    = this.BuildValves();
    var simulator = new Simulator(valves, "AA", 1, 26);
    var bests     = simulator.CalculateMaximumRelief(1, 6);

    // find all distinct pairs
    var pairs = new List<(string, string)>();
    var ids   = new Queue<string>(bests.Keys);
    while (ids.Count > 0)
    {
      var id0 = ids.Dequeue();
      var hs0 = new HashSet<string>(id0.Split(","));
      foreach (string id1 in ids)
      {
        var hs1 = new HashSet<string>(id1.Split(","));
        if (!hs0.Overlaps(hs1)) {
          pairs.Add((id0, id1));
        }
      }
    }

    // get best combined value
    int combined = 0;
    foreach (var tuple in pairs)
    {
      int t1 = bests[tuple.Item1];
      int t2 = bests[tuple.Item2];
      if (t1 + t2 > combined)
      {
        combined = t1 + t2;
      }
    }

    return combined;
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== STRUCTS ===================================

  private Dictionary<string, Valve> BuildValves ()
  {
    var valves = new Dictionary<string, Valve>();
    var re     = new Regex(@"^Valve (\w+) has flow rate=(\d+); tunnels? leads? to valves? (.+)$");
    foreach (var line in this.Data())
    {
      var m = re.Match(line);
      if (m.Success)
      {
        var id         = m.Groups[1].Value;
        var flow       = Int32.Parse(m.Groups[2].Value);
        var connectIds = m.Groups[3].Value.Split(", ").ToList();

        valves[id] = new Valve(id, flow, connectIds);
      }
    }
    return valves;
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day16/input.txt");
  }
}