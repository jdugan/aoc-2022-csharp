using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day09;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //--------------------------------------------------------

  public int Day()
  {
    return 9;
  }

  public int Puzzle1()
  {
    return this.CountVisits(1);
  }

  public int Puzzle2()
  {
    return this.CountVisits(9);
  }

  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  // ========== ROPE HELPERS ===============================

  private int CountVisits(int tailSize)
  {
    Knot prev;
    var  head = new Knot();
    var  tail = new List<Knot>();
    for (int i = 0; i < tailSize; i++)
    {
      tail.Add(new Knot());
    }

    foreach ((string direction, int steps) cmd in this.Commands())
    {
      for (int i = 0; i < cmd.steps; i++)
      {
        prev = head.Move(cmd.direction);
        foreach (Knot knot in tail)
        {
          prev = knot.Follow(prev);
        }
      }
    }

    return tail[tail.Count - 1].Visits.Count;
  }


  // ========== DATA =======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day09/input.txt");
  }

  private List<(string, int)> Commands()
  {
    var cmds = new List<(string, int)>();
    var re   = new Regex(@"^(\w) (\d+)$");
    foreach (string line in this.Data())
    {
      var m = re.Match(line);
      if (m.Success)
      {
        var direction = m.Groups[1].Value;
        var steps     = Int32.Parse(m.Groups[2].Value);

        cmds.Add((direction, steps));
      }
    }
    return cmds;
  }
}
