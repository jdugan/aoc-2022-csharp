using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day14;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 14;
  }

  public int Puzzle1()
  {
    var cave = this.CaveWithoutFloor(500, 0);
    var count = cave.SandVolume();

    return count;
  }

  public int Puzzle2()
  {
    var cave  = this.CaveWithFloor();
    var count = cave.SandVolume();

    return count;
    // return -2;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day14/input.txt");
  }

  private Dictionary<(int, int), Tile> Tiles ()
  {
    // add origin
    var tiles = new Dictionary<(int, int), Tile>();

    // add rocks
    foreach (var line in this.Data())
    {
      var pairs      = line.Split(" -> ").ToList();
      var prevId     = this.ParseCoordString(pairs[0]);
      var prev       = new Tile(prevId.Item1, prevId.Item2, "#");
      tiles[prev.Id] = prev;

      foreach (var pair in pairs.GetRange(1, pairs.Count - 1))
      {
        var currId = this.ParseCoordString(pair);
        var curr   = new Tile(currId.Item1, currId.Item2, "#");
        var xs     = new List<int> { prev.X, curr.X };
        var ys     = new List<int> { prev.Y, curr.Y };
        xs.Sort();
        ys.Sort();

        for (int y = ys[0]; y <= ys[1]; y++)
        {
          for (int x = xs[0]; x <= xs[1]; x++)
          {
            tiles[(x, y)] = new Tile(x, y, "#");
          }
        }

        prev = curr;
      }
    }

    // return dict
    return tiles;
  }

  private Cave CaveWithFloor ()
  {
    var cave = this.CaveWithoutFloor(500, 0);
    var y    = cave.GetMaxY() + 1;

    foreach (int i in Enumerable.Range(0, 300))
    {
      int x0 = cave.Origin.Item1 - i;
      int x1 = cave.Origin.Item1 + i;

      cave.Tiles[(x0, y)] = new Tile(x0, y, "#");
      cave.Tiles[(x1, y)] = new Tile(x1, y, "#");
    }

    return cave;
  }

  private Cave CaveWithoutFloor (int x, int y)
  {
    var tiles = this.Tiles();
    foreach (int i in Enumerable.Range(1,10))
    {
      int x0 = x - i;
      int x1 = x + i;

      tiles[(x0, y)] = new Tile(x0, y, "#");
      tiles[(x1, y)] = new Tile(x1, y, "#");
    }
    var cave = new Cave((x, y), tiles);

    return cave;
  }

  // ========== HELPERS ===================================

  private (int, int) ParseCoordString (string input)
  {
    var coords = input.Split(",");
    int x      = Int32.Parse(coords[0]);
    int y      = Int32.Parse(coords[1]);

    return (x, y);
  }
}