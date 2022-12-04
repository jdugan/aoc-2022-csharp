using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day04;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //--------------------------------------------------------

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


  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  // ========== AGGREGATORS ================================

  private int ContainsAggregator (int count, List<Assignment> group)
  {
    if (group[0].Contains(group[1]))
    {
      count += 1;
    }
    return count;
  }

  private int OverlapsAggregator (int count, List<Assignment> group)
  {
    if (group[0].Overlaps(group[1]))
    {
      count += 1;
    }
    return count;
  }


  // ========== DATA =======================================

  private List<List<Assignment>> AssignmentGroups()
  {
    var groups = new List<List<Assignment>>();
    var re     = new Regex(@"^(\d+)-(\d+),(\d+)-(\d+)$");
    foreach (string line in this.Data())
    {
      var m     = re.Match(line);
      var a1    = new Assignment(Int32.Parse(m.Groups[1].Value), Int32.Parse(m.Groups[2].Value));
      var a2    = new Assignment(Int32.Parse(m.Groups[3].Value), Int32.Parse(m.Groups[4].Value));
      var group = new List<Assignment> { a1, a2 };
      groups.Add(group);
    }
    return groups;
  }

  private List<string> Data()
  {
    List<string> lines = Reader.ToStrings("data/day04/input.txt");
    return lines;
  }
}
