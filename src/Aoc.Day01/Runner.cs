using Aoc.Utility;

﻿namespace Aoc.Day01;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //--------------------------------------------------------

  public int Day()
  {
    return 1;
  }

  public int Puzzle1()
  {
    return this.Rations()[0];
  }

  public int Puzzle2()
  {
    return this.Rations().GetRange(0, 3).Sum();
  }


  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  // ========== DATA =======================================

  private List<string> Data()
  {
    List<string> lines = Reader.GetLines("data/day01/input.txt");
    return lines;
  }

  private List<int> Rations()
  {
    var rations = new List<int>();
    var curr    = 0;

    foreach (string line in this.Data())
    {
      if (line == "")
      {
        rations.Add(curr);
        curr = 0;
      }
      else
      {
        curr += Int32.Parse(line);
      }
    }
    rations.Sort();
    rations.Reverse();

    return rations;
  }
}
