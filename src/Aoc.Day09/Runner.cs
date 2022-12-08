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
    List<string> lines = Reader.ToStrings("data/day09/input.txt");
    return lines;
  }
}
