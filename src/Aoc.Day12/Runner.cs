using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

using Aoc.Utility;

﻿namespace Aoc.Day12;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 12;
  }

  public int Puzzle1()
  {
    var squares  = this.BuildSquares();
    var graph    = this.BuildGraph(squares);
    var origin   = this.FindSquare(squares, "S");
    var terminus = this.FindSquare(squares, "E");
    var result   = graph.Dijkstra(origin.GraphId, terminus.GraphId);

    return result.Distance;
  }

  public int Puzzle2()
  {
    var squares   = this.BuildSquares();
    var graph     = this.BuildGraph(squares);
    var terminus  = this.FindSquare(squares, "E");
    var originIds = new List<uint>();
    foreach (var id in squares.Keys)
    {
      if (squares[id].Symbol == char.Parse("a"))
      {
        originIds.Add(squares[id].GraphId);
      }
    }

    int best = Int32.MaxValue;
    foreach (var originId in originIds)
    {
      var result = graph.Dijkstra(originId, terminus.GraphId);
      if (result.Distance < best)
      {
        best = result.Distance;
      }
    }

    return best;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== GRAPH =====================================

  private Graph<Square, string> BuildGraph (Dictionary<(int, int), Square> squares)
  {
    // add nodes and map graph ids to squares
    var graph = new Graph<Square, string>();
    foreach (var sq in squares.Values)
    {
      uint graphId = graph.AddNode(sq);
      sq.GraphId = graphId;
    }

    // connect squares by graph ids
    foreach (var sq in squares.Values)
    {
      var nids = sq.GetNeighborIds().ToList();
      foreach (var nid in nids)
      {
        try
        {
          var nsq  = squares[nid];
          var diff = nsq.Elevation - sq.Elevation;

          if (diff <= 1)
          {
            graph.Connect(sq.GraphId, nsq.GraphId, 1, "dummy");
          }
        }
        catch {}  //
      }
    }

    return graph;
  }


  // ========== SQUARES ===================================

  private Dictionary<(int, int), Square> BuildSquares ()
  {
    var squares = new Dictionary<(int, int), Square>();
    foreach (var (y, line) in this.Data().Select((v, i) => (i, v)))
    {
      var row = line.ToCharArray().ToList();
      foreach (var (x, c) in row.Select((v, i) => (i, v)))
      {
        var tmp = new Square(x, y, c);
        squares[tmp.Id] = tmp;
      }
    }
    return squares;
  }

  public Square FindSquare (Dictionary<(int, int), Square> squares, string symbol)
  {
    foreach (KeyValuePair<(int, int), Square> entry in squares)
    {
      if (entry.Value.Symbol == symbol.ToCharArray()[0])
      {
        return entry.Value;
      }
    }
    return null;
  }

  public void PrintSquares (Dictionary<(int, int), Square> squares)
  {
    var xMax = squares.Keys.ToList().Select((item, _) => item.Item1).Max() + 1;
    var yMax = squares.Keys.ToList().Select((item, _) => item.Item2).Max() + 1;

    foreach (int y in Enumerable.Range(0, yMax))
    {
      var row = new List<string>();
      foreach (int x in Enumerable.Range(0, xMax))
      {
        var sq = squares[(x, y)];
        row.Add(sq.Symbol.ToString());
      }
      Console.WriteLine(String.Join("", row));
    }
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day12/input.txt");
  }
}
