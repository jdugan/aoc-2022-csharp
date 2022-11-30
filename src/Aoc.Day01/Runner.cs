using System.IO;
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
    return -1;
  }

  public int Puzzle2()
  {
    return -2;
  }

  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  private string[] Data()
  {
    string[] lines = Reader.ToLines("data/day01/input.txt");
    return lines;
  }
}
