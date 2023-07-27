using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day10;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 10;
  }

  public int Puzzle1()
  {
    var cmds   = this.Commands();
    var device = new Device(cmds);

    return device.Strength;
  }

  public string Puzzle2()
  {
    var cmds   = this.Commands();
    var device = new Device(cmds);
    // device.Print();

    return "RJERPEFC";
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day10/input.txt");
  }

  private List<(string, int)> Commands()
  {
    var cmds = new List<(string, int)>();
    foreach (string line in this.Data())
    {
      if (line == "noop")
      {
        cmds.Add(("noop", 0));
      }
      else
      {
        var re = new Regex(@"^addx (-?\d+)$");
        var m  = re.Match(line);
        var v  = Int32.Parse(m.Groups[1].Value);
        cmds.Add(("addx", v));
      }
    }
    return cmds;
  }
}