using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
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
    return this.CalculateMaxRelief(this.Valves(), 1, "AA", 30, false);
    // return -1;
  }

  // WRONG - 2160 (too low / ~ 15mins?)
  // WRONG - 2165 (too low / ~ 15mins?)
  public int Puzzle2()
  {
    return this.CalculateMaxRelief(this.Valves(), 2, "AA", 26, true);
    // return -2;
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== CALCULATIONS ==============================

  private int CalculateMaxRelief (Dictionary<string, Valve> valves, int concurrency, string position, int ticks, bool debug)
  {
    // build base structures
    Graph<Valve, string> graph;
    (graph, valves)   = this.BuildGraph(valves);
    var graphDict     = this.BuildGraphDictionary(valves);
    var destinations  = this.BuildDestinations(valves);
    var actors        = new List<Actor>();
    foreach (var i in Enumerable.Range(0, concurrency))
    {
      actors.Add(new Actor(position));
    }

    // set up universe
    var initial          = new Scenario(actors);
    var scenarios        = new List<Scenario>{ initial };
    var bests            = new Dictionary<string, Scenario>();
    bests[initial.Key()] = initial;

    // play out all possibilities
    for (int t = 0; t < ticks; t++)
    {
      var ticksRemaining = ticks - t - 1;

      // evaluate possibles
      var newScenarios = new List<Scenario>();
      foreach (var s in scenarios)
      {
        var sc = s.Copy();  // copy everything!

        sc.Accumulate();

        var targetless = new List<Actor>();
        foreach (var a in sc.Actors)
        {
          // if walking somewhere, just take the
          // next step
          if (a.Path.Count > 0)
          {
            a.Position = a.Path.Dequeue();
          }
          else
          {
            // if I made it to my destination and
            // haven't opened the valve yet, do
            // that now. this works because we
            // never send two actors to the same
            // place.
            var cv = valves[a.Position];
            if (!sc.Visited.Contains(a.Position) && cv.Flow > 0)
            {
              a.Target  = "";
              sc.Flow  += cv.Flow;
              sc.Visited.Add(cv.Id);
            }
            // otherwise we need to figure out where
            // to go next. add the actor to a list.
            // but only if there's time.
            else
            {
              targetless.Add(a);
            }
          }
        }

        // if everyone was busy with their current
        // agenda, just add the scenario to the list
        if (targetless.Count == 0)
        {
          newScenarios.Add(sc);
        }
        // else, we found actors needing a new home,
        // and we need to send them somewhere.
        else
        {
          var visited  = sc.Visited;
          var targeted = sc.Targets();
          var targets  = destinations.Values.
                            Where(v => !targeted.Contains(v.Id)).
                            Where(v => !visited.Contains(v.Id)).
                            ToList();

          if (targets.Count == 0)
          {
            newScenarios.Add(sc);
          }
          else
          {
            // match num targets to num actors by adding
            // dummy targets.
            if (targetless.Count > targets.Count)
            {
              for (int i = 0; i < targetless.Count - targets.Count; i++)
              {
                targets.Add(new Valve("DUMMY", 0, new List<string>()));
              }
            }

            var perms = this.BuildPermutationsForValves(targets, targetless.Count);
            foreach (var perm in perms)
            {
              var permIdx   = 0;
              var newActors = new List<Actor>();
              foreach (var a in sc.Actors)
              {
                if (!targetless.Contains(a))
                {
                  newActors.Add(a.Copy());
                }
                else {
                  var v = perm[permIdx];

                  if (v.Id == "DUMMY")
                  {
                    newActors.Add(a.Copy());
                  }
                  else
                  {
                    var result = graph.Dijkstra(valves[a.Position].GraphId, v.GraphId);
                    var ids    = result.GetPath().Select(gid => graphDict[gid].Id).ToList();
                    ids        = ids.GetRange(1, ids.Count - 1);

                    var path = new Queue<string>();
                    foreach (var id in ids)
                    {
                      path.Enqueue(id);
                    }

                    var a1      = new Actor(a.Position);
                    a1.Path     = path;
                    a1.Target   = v.Id;
                    a1.Position = a1.Path.Dequeue();
                    newActors.Add(a1);
                  }

                  permIdx += 1;
                }
              }

              var ns    = sc.Copy();
              ns.Actors = newActors;
              newScenarios.Add(ns);
            }
          }
        }
      }

      // optimise scenario list
      bests     = this.UpdateBestResults(newScenarios, bests);
      scenarios = this.EliminateSuboptimalScenarios(newScenarios, bests);
      scenarios = this.EliminateDuplicateScenarios(scenarios, valves, ticksRemaining);

      // emit measure of entropy
      if (debug)
      {
        // Console.WriteLine("----------------------------------");
        Console.WriteLine("{0} => {1} ({2})", t + 1, scenarios.Count, bests.Values.Select(v => v.TotalRelief).Max());
        // Console.WriteLine("SCENARIOS");
        // foreach (var s in scenarios)
        // {
        //   var ts = s.Actors.Select(a => a.Target).ToList();
        //   Console.WriteLine(" [{0}] => [{1}], {2}, {3}", s.Key(), String.Join(",", ts), s.Actors.Count, s.TotalRelief);
        // }
        // Console.WriteLine("BESTS");
        // var keys = bests.Keys.ToList();
        // keys.Sort();
        // foreach (var k in keys)
        // {
        //   Console.WriteLine(" [{0}] => {1}", k, bests[k]);
        // }
      }
    }

    return bests.Values.Select(v => v.TotalRelief).Max();
  }


  // ========== CALC HELPERS ==============================

  private List<Scenario> EliminateDuplicateScenarios (
    List<Scenario> newScenarios,
    Dictionary<string, Valve> valves,
    int ticksRemaining
  )
  {
    var scenarios = new List<Scenario>();
    var dict      = new Dictionary<string, Scenario>();
    foreach (var ns in newScenarios)
    {
      var targets = String.Join(",", ns.Targets());
      if (targets == "")
      {
        scenarios.Add(ns);
      }
      else
      {
        var steps = ns.TargetedSteps();
        var key   = $"{ns.Key()}/{targets}";

        if (dict.ContainsKey(key))
        {
          int pf1 = ns.PotentialFlow(valves, ticksRemaining);
          int pf2 = dict[key].PotentialFlow(valves, ticksRemaining);
          if (pf1 > pf2) {
            dict[key] = ns;
          }
        }
        else
        {
          dict[key] = ns;
        }
      }
    }
    scenarios.AddRange(dict.Values.ToList());

    return scenarios;
  }

  private List<Scenario> EliminateSuboptimalScenarios (List<Scenario> newScenarios, Dictionary<string, Scenario> bests)
  {
    var scenarios = new List<Scenario>();
    foreach (var ns in newScenarios)
    {
      var key = ns.Key();
      if (ns.TotalRelief == bests[key].TotalRelief)
      {
        scenarios.Add(ns);
      }
    }
    return scenarios;
  }

  private Dictionary<string, Scenario> UpdateBestResults (List<Scenario> newScenarios, Dictionary<string, Scenario> bests)
  {
    foreach (var ns in newScenarios)
    {
      var key = ns.Key();
      if (bests.ContainsKey(key))
      {
        if (ns.TotalRelief > bests[key].TotalRelief)
        {
          bests[key] = ns;
        }
      }
      else
      {
        bests[key] = ns;
      }
    }
    return bests;
  }


  // ========== STRUCTURES ================================

  private Dictionary<string, Valve> BuildDestinations (Dictionary<string, Valve> valves)
  {
    var dests = new Dictionary<string, Valve>();
    foreach (var v in valves.Values) {
      if (v.Flow > 0)
      {
        dests[v.Id] = v;
      }
    }
    return dests;
  }

  private (Graph<Valve, string>, Dictionary<string, Valve>) BuildGraph (Dictionary<string, Valve> valves)
  {
    // add nodes and map graph ids to squares
    var graph = new Graph<Valve, string>();
    foreach (var v in valves.Values)
    {
      uint graphId = graph.AddNode(v);
      v.GraphId = graphId;
    }

    // connect valves by graph ids
    foreach (var v in valves.Values)
    {
      var nids = v.NeighborIds;
      foreach (var nid in nids)
      {
        graph.Connect(v.GraphId, valves[nid].GraphId, 1, "dummy");
      }
    }

    return (graph, valves);
  }

  private Dictionary<uint, Valve> BuildGraphDictionary (Dictionary<string, Valve> valves)
  {
    var dict = new Dictionary<uint, Valve>();
    foreach (var v in valves.Values)
    {
      dict[v.GraphId] = v;
    }
    return dict;
  }

  private List<List<Valve>> BuildPermutationsForValves (List<Valve> valves, int size)
  {
    var results = new List<List<Valve>>();
    if (size == 0) {
      return results;
    }
    if (valves.Count == 1) {
      results.Add(valves.GetRange(0, 1));
      return results;
    }
    foreach (var (i, v) in valves.Select((v, i) => (i, v)))
    {
      var others = new List<Valve>(valves);
      others.Remove(v);
      var perms  = this.BuildPermutationsForValves(others, size - 1);
      if (perms.Count == 0)
      {
        var p1 = new List<Valve>();
        p1.Add(v);
        results.Add(p1);
      }
      else
      {
        foreach (var p in perms)
        {
          var p1 = new List<Valve>();
          p1.Add(v);
          p1.AddRange(p);
          results.Add(p1);
        }
      }
    }
    return results;
  }

  private Dictionary<string, Valve> Valves ()
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
