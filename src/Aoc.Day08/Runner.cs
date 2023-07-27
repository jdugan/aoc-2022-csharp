using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day08;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 8;
  }

  public int Puzzle1()
  {
    var trees  = this.Trees();
    int xCount = trees[0].Count;
    int yCount = trees.Count;

    int count = 0;
    foreach (int y in Enumerable.Range(0, yCount))
    {
      foreach (int x in Enumerable.Range(0, xCount))
      {
        int h = trees[y][x];
        if (
          x == 0 || x == xCount - 1 ||                                      // x edges
          y == 0 || y == yCount - 1 ||                                      // y edges
          this.CanBeSeen(h, this.GetXRange(trees, y, 0, x))          ||     // west
          this.CanBeSeen(h, this.GetXRange(trees, y, x + 1, xCount)) ||     // east
          this.CanBeSeen(h, this.GetYRange(trees, x, 0, y))          ||     // north
          this.CanBeSeen(h, this.GetYRange(trees, x, y + 1, yCount))        // south
        )
        {
          count += 1;
        }
      }
    }
    return count;
  }

  public int Puzzle2()
  {
    var trees  = this.Trees();
    int xCount = trees[0].Count;
    int yCount = trees.Count;

    int best = 0;
    foreach (int y in Enumerable.Range(0, yCount))
    {
      foreach (int x in Enumerable.Range(0, xCount))
      {
        // skip edges
        if (x == 0 || x == xCount - 1 || y == 0 || y == yCount - 1)
        {
          continue;
        }

        // prepare ranges
        var wRange = this.GetXRange(trees, y, 0, x);
        var eRange = this.GetXRange(trees, y, x + 1, xCount);
        var nRange = this.GetYRange(trees, x, 0, y);
        var sRange = this.GetYRange(trees, x, y + 1, yCount);
        wRange.Reverse();
        nRange.Reverse();

        // score ranges
        int h      = trees[y][x];
        int fw     = this.ViewDistance(h, wRange);
        int fe     = this.ViewDistance(h, eRange);
        int fn     = this.ViewDistance(h, nRange);
        int fs     = this.ViewDistance(h, sRange);
        int score  = fw * fe * fn * fs;

        // evaluate
        if (score > best)
        {
          best = score;
        }
      }
    }
    return best;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== RANGE HELPERS =============================

  private List<int> GetXRange(List<List<int>> trees, int y, int x1, int x2)
  {
    return Enumerable.
              Range(x1, x2 - x1).
              Select(x => trees[y][x]).
              ToList();
  }

  private List<int> GetYRange(List<List<int>> trees, int x, int y1, int y2)
  {
    return Enumerable.
              Range(y1, y2 - y1).
              Select(y => trees[y][x]).
              ToList();
  }


  // ========== STATE HELPERS =============================

  private bool CanBeSeen(int height, List<int> neighborHeights)
  {
    return neighborHeights.
              Select(nh => nh >= height).
              Where(b => !!b).
              ToList().
              Count == 0;
  }

  private int ViewDistance(int height, List<int> neighborHeights)
  {
    int count = 0;
    foreach (int nh in neighborHeights)
    {
      count += 1;
      if (nh >= height)
      {
        break;
      }
    }
    return count;
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day08/input.txt");
  }

  private List<List<int>> Trees()
  {
    var trees = new List<List<int>>();
    foreach (string line in this.Data())
    {
      var chars = line.ToCharArray().ToList();
      var nums  = chars.Select(c => Int32.Parse(c.ToString())).ToList();
      trees.Add(nums);
    }
    return trees;
  }
}