using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day19;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 19;
  }

  public int Puzzle1()
  {
    return -1;
  }

  public int Puzzle2()
  {
    return -2;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day19/input.txt");
  }
}
