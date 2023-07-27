using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day04;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 4;
  }

  public int Puzzle1()
  {
    return this.AssignmentGroups().
              Aggregate(0, this.ContainsAggregator);
  }

  public int Puzzle2()
  {
    return this.AssignmentGroups().
              Aggregate(0, this.OverlapsAggregator);
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== AGGREGATORS ===============================

  private int ContainsAggregator (int count, (Assignment, Assignment) t)
  {
    if (t.Item1.Contains(t.Item2))
    {
      count += 1;
    }
    return count;
  }

  private int OverlapsAggregator (int count, (Assignment, Assignment) t)
  {
    if (t.Item1.Overlaps(t.Item2))
    {
      count += 1;
    }
    return count;
  }


  // ========== DATA ======================================

  private List<(Assignment, Assignment)> AssignmentGroups()
  {
    var groups = new List<(Assignment, Assignment)>();
    var re     = new Regex(@"^(\d+)-(\d+),(\d+)-(\d+)$");
    foreach (string line in this.Data())
    {
      var m  = re.Match(line);
      var a1 = new Assignment(Int32.Parse(m.Groups[1].Value), Int32.Parse(m.Groups[2].Value));
      var a2 = new Assignment(Int32.Parse(m.Groups[3].Value), Int32.Parse(m.Groups[4].Value));
      groups.Add((a1, a2));
    }
    return groups;
  }

  private List<string> Data()
  {
    return Reader.ToStrings("data/day04/input.txt");
  }
}