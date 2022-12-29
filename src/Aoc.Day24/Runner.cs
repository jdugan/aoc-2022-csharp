using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day24;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 24;
  }

  public int Puzzle1()
  {
    var snapshot = this.BuildSnapshot();
    var site     = new Site(snapshot);
    var dist     = site.ShortestDistance();

    return dist;
  }

  public int Puzzle2()
  {
    // first leg
    var snapshot = this.BuildSnapshot();
    var site     = new Site(snapshot);
    var dist1    = site.ShortestDistance();

    // second leg
    snapshot  = site.LastSnapshot().Invert(0);
    site      = new Site(snapshot);
    var dist2 = site.ShortestDistance() + 1;

    // third leg
    snapshot  = site.LastSnapshot().Invert(0);
    site      = new Site(snapshot);
    var dist3 = site.ShortestDistance() + 1;

    // combined distance
    return dist1 + dist2 + dist3;
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== STRUCTS ===================================

  private Snapshot BuildSnapshot ()
  {
    var lines  = this.Data();
    var points = new Dictionary<(int, int, int), Point>();
    for (int y = 0; y < lines.Count; y++)
    {
      var row = lines[y].ToCharArray().Select(c => c.ToString()).ToList();
      for (int x = 0; x < row.Count; x++)
      {
        var c = row[x];
        if (c != "#")
        {
          var p = new Point(x, y, 0);
          if (c != ".")
          {
            p.Winds.Add(c);
          }
          points[p.Id] = p;
        }
      }
    }

    var snapshot          = new Snapshot(0, points);
    snapshot.ColumnLimits = snapshot.BuildColumnLimits();
    snapshot.RowLimits    = snapshot.BuildRowLimits();

    return snapshot;
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day24/input.txt");
  }
}
