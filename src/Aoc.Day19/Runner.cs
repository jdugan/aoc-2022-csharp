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
    // return this.BuildBlueprints().
    //           Select(b => b.QualityLevel(24)).
    //           Sum();
    return -1;
  }

  public int Puzzle2()
  {
    return -2;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== STRUCTS ===================================

  private List<Blueprint> BuildBlueprints ()
  {
    var blueprints = new List<Blueprint>();
    foreach (var line in this.Data())
    {
      var re  = new Regex(@"^Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.$");
      var m   = re.Match(line);
      var id  = Int32.Parse(m.Groups[1].Value);
      var r1a = Int32.Parse(m.Groups[2].Value);
      var r2a = Int32.Parse(m.Groups[3].Value);
      var r3a = Int32.Parse(m.Groups[4].Value);
      var r3b = Int32.Parse(m.Groups[5].Value);
      var r4a = Int32.Parse(m.Groups[6].Value);
      var r4b = Int32.Parse(m.Groups[7].Value);

      var robots = new List<Robot>();
      robots.Add(new Robot("ore",      r1a, 0,   0));
      robots.Add(new Robot("clay",     r2a, 0,   0));
      robots.Add(new Robot("obsidian", r3a, r3b, 0));
      robots.Add(new Robot("geode",    r4a, 0,   r4b));

      blueprints.Add(new Blueprint(id, robots));
    }
    return blueprints;
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day19/input-test.txt");
  }
}
