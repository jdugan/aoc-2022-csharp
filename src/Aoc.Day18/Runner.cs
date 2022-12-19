using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

using Aoc.Utility;

﻿namespace Aoc.Day18;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 18;
  }

  public int Puzzle1()
  {
    var cubes = this.CubeQueue();
    var sides  = this.UncoveredSides(cubes);

    return sides;
  }

  public int Puzzle2()
  {
    var cubes = this.CubeDictionary();
    var area  = this.SurfaceArea(cubes);

    return area;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== CALCULATIONS ==============================

  private int SurfaceArea (Dictionary<string, Cube> cubes)
  {
    var ranges    = this.GetSearchRanges(cubes);
    var airs      = this.GetAirCubes(ranges, cubes);
    var adjacents = this.GetAdjacentAirCubes(airs, cubes);
    var graph     = this.GetAirGraph(airs);
    var origin    = this.GetOrigin(ranges, airs);

    var shared   = 0;
    foreach (var ac in adjacents)
    {
      var result = graph.Dijkstra(origin.GraphId, ac.GraphId);
      if (result.Distance != Int32.MaxValue)
      {
        foreach (var nid in ac.NeighborIds())
        {
          if (cubes.ContainsKey(nid))
          {
            shared += 1;
          }
        }
      }
    }

    return shared;
  }

  private int UncoveredSides (Queue<Cube> cubes)
  {
    var possible = cubes.Count * 6;
    var shared   = 0;

    while (cubes.Count > 0)
    {
      var cube = cubes.Dequeue();
      foreach (var c in cubes)
      {
        if (cube.IsAdjacent(c))
        {
          shared += 1;
        }
      }
    }

    return possible - (shared * 2);
  }


  // ========== GEOMETRY ==================================

  private HashSet<Cube> GetAdjacentAirCubes (
    Dictionary<string, Cube> airs,
    Dictionary<string, Cube> cubes
  )
  {
    var adjacents = new HashSet<Cube>();
    foreach (var ac in airs.Values)
    {
      var nids = ac.NeighborIds();
      foreach (var nid in nids)
      {
        if (cubes.ContainsKey(nid))
        {
          adjacents.Add(ac);
          break;
        }
      }
    }
    return adjacents;
  }

  private Dictionary<string, Cube> GetAirCubes (
    Dictionary<string, (int, int)> ranges,
    Dictionary<string, Cube> cubes
  )
  {
    // determine ranges for an enclosing volume
    // big enough to hold all possible adjacent
    // cubes and an additional walking path around
    // them.
    var rangeX   = ranges["x"];
    var rangeY   = ranges["y"];
    var rangeZ   = ranges["z"];

    // loop all cubes in our search area:
    //   - add air cubes to a Dijkstra graph
    //   - create a dictionary of air cubes
    var airs = new Dictionary<string, Cube>();
    for (var x = rangeX.Item1; x <= rangeX.Item2; x++)
    {
      for (var y = rangeY.Item1; y <= rangeY.Item2; y++)
      {
        for (var z = rangeZ.Item1; z <= rangeZ.Item2; z++)
        {
          var id = $"{x},{y},{z}";
          if (!cubes.ContainsKey(id))
          {
            var cube = new Cube(id);
            airs[id] = cube;
          }
        }
      }
    }
    return airs;
  }

  private Graph<Cube, string> GetAirGraph (Dictionary<string, Cube> airs)
  {
    // add nodes
    var graph = new Graph<Cube, string>();
    foreach (var ac in airs.Values)
    {
      uint gid   = graph.AddNode(ac);
      ac.GraphId = gid;
    }

    // connect nodes
    foreach (var ac in airs.Values)
    {
      var nids = ac.NeighborIds();
      foreach (var nid in nids)
      {
        if (airs.ContainsKey(nid))
        {
          graph.Connect(ac.GraphId, airs[nid].GraphId, 1, "dummy");
        }
      }
    }

    return graph;
  }

  private Cube GetOrigin (Dictionary<string, (int, int)> ranges, Dictionary<string, Cube> airs)
  {
    var x  = ranges["x"].Item1;
    var y  = ranges["y"].Item1;
    var z  = ranges["z"].Item1;
    var id = $"{x},{y},{z}";

    return airs[id];
  }

  private Dictionary<string, (int, int)> GetSearchRanges (Dictionary<string, Cube> cubes)
  {
    // get range of droplet
    var maxX = Int32.MinValue;
    var minX = Int32.MaxValue;
    var maxY = Int32.MinValue;
    var minY = Int32.MaxValue;
    var minZ = Int32.MaxValue;
    var maxZ = Int32.MinValue;
    foreach (var c in cubes.Values)
    {
      if (c.X > maxX)
      {
        maxX = c.X;
      }
      if (c.X < minX)
      {
        minX = c.X;
      }

      if (c.Y > maxY)
      {
        maxY = c.Y;
      }
      if (c.Y < minY)
      {
        minY = c.Y;
      }

      if (c.Z > maxZ)
      {
        maxZ = c.Z;
      }
      if (c.Z < minZ)
      {
        minZ = c.Z;
      }
    }

    // condense into dict
    var ranges = new Dictionary<string, (int, int)>();
    ranges["x"] = (minX - 2, maxX + 2);
    ranges["y"] = (minY - 2, maxY + 2);
    ranges["z"] = (minZ - 2, maxZ + 2);
    return ranges;
  }

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day18/input.txt");
  }

  private Queue<Cube> CubeQueue ()
  {
    var cubes = new Queue<Cube>();
    foreach (var id in this.Data())
    {
      cubes.Enqueue(new Cube(id));
    }
    return cubes;
  }

  private Dictionary<string, Cube> CubeDictionary ()
  {
    var cubes = new Dictionary<string, Cube>();
    foreach (var id in this.Data())
    {
      cubes[id] = new Cube(id);
    }
    return cubes;
  }
}
