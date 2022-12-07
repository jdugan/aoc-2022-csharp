using Aoc.Utility;

﻿namespace Aoc.Day08;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //--------------------------------------------------------

  public int Day()
  {
    return 8;
  }

  public int Puzzle1()
  {
    return -1;
  }

  public int Puzzle2()
  {
    return -2;
  }

  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  // ========== DATA =======================================

  private List<string> Data()
  {
    List<string> lines = Reader.ToStrings("data/day08/input.txt");
    return lines;
  }
}
