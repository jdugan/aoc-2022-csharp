using Aoc.Utility;

﻿namespace Aoc.Day06;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //--------------------------------------------------------

  public int Day()
  {
    return 6;
  }

  public int Puzzle1()
  {
    return this.FindMarker(4);
  }

  public int Puzzle2()
  {
    return this.FindMarker(14);
  }

  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  // ========== SIGNAL =====================================

  private int FindMarker(int size)
  {
    var chars = this.Data();
    int max   = chars.Count - size;
    int pos   = -1;
    foreach (int i in Enumerable.Range(0, max))
    {
      var cs = chars.GetRange(i, size);
      if (cs.Distinct().Count() == size)
      {
        pos = i + size;
        break;
      }
    }
    return pos;
  }


  // ========== DATA =======================================

  private List<char> Data()
  {
    List<string> lines = Reader.ToStrings("data/day06/input.txt");
    return lines[0].ToCharArray().ToList();
  }
}
