using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day23;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 23;
  }

  public int Puzzle1()
  {
    var directions = this.Directions();
    var elves      = this.Elves();
    var grove      = new Grove(elves, directions);

    foreach (var _ in Enumerable.Range(0, 10))
    {
      grove.PerformTurn();
    }

    return grove.OpenSpaces();
  }

  public int Puzzle2()
  {
    var directions = this.Directions();
    var elves      = this.Elves();
    var grove      = new Grove(elves, directions);
    var round      = 0;
    var movers     = Int32.MaxValue;

    while (movers > 0)
    {
      movers = grove.PerformTurn();
      round += 1;
    }

    return round;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== TURNS =====================================

  private List<string> RotateDirections (List<string> directions)
  {
    return directions;
  }


  // ========== STRUCTS ===================================

  private List<string> Directions ()
  {
    return new List<string>{ "N", "S", "W", "E" };
  }

  private Dictionary<(int, int), Elf> Elves ()
  {
    var rows  = this.Data();
    var elves = new Dictionary<(int, int), Elf>();
    int i     = 0;
    for (int y = 0; y < rows.Count; y++)
    {
      var columns = rows[y].ToCharArray().ToList().Select(c => c.ToString()).ToList();
      for (int x = 0; x < columns.Count; x++)
      {
        if (columns[x] == "#")
        {
          i += 1;
          elves[(x, y)] = new Elf(x, y);
        }
      }
    }
    return elves;
  }

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day23/input.txt");
  }
}