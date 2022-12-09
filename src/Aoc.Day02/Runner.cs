using Aoc.Utility;

﻿namespace Aoc.Day02;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 2;
  }

  public int Puzzle1()
  {
    return this.Data().
              Select(cmd => this.ToScore(cmd)).
              Sum();
  }

  public int Puzzle2()
  {
    return this.Data().
              Select(cmd => this.ToTurn(cmd)).
              Select(cmd => this.ToScore(cmd)).
              Sum();
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== GAME ======================================

  private int ToScore(string turn) => turn switch
  {
    "A X" => 4,
    "B X" => 1,
    "C X" => 7,
    "A Y" => 8,
    "B Y" => 5,
    "C Y" => 2,
    "A Z" => 3,
    "B Z" => 9,
    "C Z" => 6,
    _     => 0
  };

  private string ToTurn(string turn) => turn switch
  {
    "A X" => "A Z",
    "B X" => "B X",
    "C X" => "C Y",
    "A Y" => "A X",
    "B Y" => "B Y",
    "C Y" => "C Z",
    "A Z" => "A Y",
    "B Z" => "B Z",
    "C Z" => "C X",
    _     => ""
  };

  // ========== DATA ======================================

  private List<string> Data()
  {
    List<string> lines = Reader.ToStrings("data/day02/input.txt");
    return lines;
  }

}
