using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace Aoc.Day16;

public class Simulator
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Simulator (Dictionary<string, Valve> valves, string position, int concurrency, int ticks)
  {
    Concurrency  = concurrency;
    Position     = position;
    Ticks        = ticks;
    Valves       = valves;

    Graph        = this.BuildGraph();
    Destinations = this.BuildDestinations();
  }
  public int                                         Concurrency  { get; private set; }
  public Dictionary<string, Dictionary<string, int>> Destinations { get; private set; }
  public Graph<Valve, string>                        Graph        { get; private set; }
  public string                                      Position     { get; private set; }
  public int                                         Ticks        { get; private set; }
  public Dictionary<string, Valve>                   Valves       { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public Dictionary<string, int> CalculateMaximumRelief (int minSize, int maxSize)
  {
    var bests = new Dictionary<string, int>();
    var ids   = this.Destinations[this.Position].Keys.ToList();
    var perms = this.BuildPowerPermutations(ids, minSize, maxSize);

    foreach (var vids in perms)
    {
      var visited  = new List<string>();
      var position = this.Position;
      var ticks    = this.Ticks;
      var total    = 0;
      foreach (string vid in vids)
      {
        var steps = this.Destinations[position][vid];
        var flow  = this.Valves[vid].Flow;

        position  = vid;
        ticks     = ticks - steps - 1;
        visited.Add(vid);

        if (ticks >= 0) {
          total += flow * ticks;
        }
        else {
          break;
        }
      }

      visited.Sort();
      var key = String.Join(",", visited);

      if (bests.ContainsKey(key))
      {
        if (total > bests[key])
        {
          bests[key] = total;
        }
      }
      else {
        bests[key] = total;
      }
    }

    return bests;
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== STRUCTS ===================================

  private Dictionary<string, Dictionary<string, int>> BuildDestinations ()
  {
    // create a lookup dictionary to make paths
    // more intuitive to debug
    var lookup = new Dictionary<uint, string>();
    foreach (var v in this.Valves.Values)
    {
      lookup[v.GraphId] = v.Id;
    }

    // get ids for valves we want to visit
    var ids = this.Valves.
                  Values.
                  Where(v => v.Id == "AA" || v.Flow > 0).
                  Select(v => v.Id).
                  ToList();
    ids.Sort();

    // for each id, memoise paths to all other destinations
    var outer = new Dictionary<string, Dictionary<string, int>>();
    foreach (var id in ids)
    {
      var inner    = new Dictionary<string, int>();
      var otherIds = new List<string>(ids).Where(item => item != id).ToList();
      foreach (var oid in otherIds) {
        var origin   = this.Valves[id].GraphId;
        var terminus = this.Valves[oid].GraphId;
        var result   = this.Graph.Dijkstra(origin, terminus);
        if (result.Distance != Int32.MaxValue)
        {
          inner[oid] = result.Distance;
        }
      }
      outer[id] = inner;
    }
    return outer;
  }

  private Graph<Valve, string> BuildGraph ()
  {
    // initialise graph
    var graph = new Graph<Valve, string>();

    // add nodes and map graph ids to valves
    foreach (var v in this.Valves.Values)
    {
      uint graphId              = graph.AddNode(v);
      this.Valves[v.Id].GraphId = graphId;
    }

    // connect valves by graph ids
    foreach (var v in this.Valves.Values)
    {
      var nids = v.NeighborIds;
      foreach (var nid in nids)
      {
        graph.Connect(v.GraphId, this.Valves[nid].GraphId, 1, "dummy");
      }
    }

    // return graph
    return graph;
  }

  private List<List<string>> BuildPowerPermutations (List<string> ids, int minSize, int maxSize)
  {
    var perms = new List<List<string>>();
    for (int n = minSize; n <= maxSize; n++)
    {
      perms.AddRange(this.BuildPermutations(ids, n));
    }
    return perms;
  }

  private List<List<string>> BuildPermutations (List<string> ids, int size)
  {
    var results = new List<List<string>>();
    if (size == 0) {
      return results;
    }
    if (ids.Count == 1) {
      results.Add(ids.GetRange(0, 1));
      return results;
    }
    foreach (var (i, v) in ids.Select((v, i) => (i, v)))
    {
      var others = new List<string>(ids);
      others.Remove(v);
      var perms  = this.BuildPermutations(others, size - 1);
      if (perms.Count == 0)
      {
        results.Add(new List<string>{ v });
      }
      else
      {
        foreach (var p in perms)
        {
          var p1 = new List<string>();
          p1.Add(v);
          p1.AddRange(p);
          results.Add(p1);
        }
      }
    }
    return results;
  }
}