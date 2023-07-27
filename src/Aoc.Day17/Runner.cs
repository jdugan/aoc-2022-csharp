using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day17;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 17;
  }

  public long Puzzle1()
  {
    var calculator = this.BuildCalculator(7);

    return calculator.DetermineHeight(2022);
  }

  public long Puzzle2()
  {
    var calculator = this.BuildCalculator(7);

    return calculator.DetermineHeight(1000000000000);
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== CALCULATOR ================================

  private Calculator BuildCalculator (int width)
  {
    // set conditions
    var heights   = new Dictionary<long, long>();
    var results   = new Dictionary<(string, int, int), (int, int)>();
    var tower     = new Tower();
    var rockDefs  = this.RockDefinitions();
    var winds     = this.Winds();
    int rockIndex = 0;
    int windIndex = 0;

    // figure out when exact state repeats
    long periodLen    = 0;
    long periodHeight = 0;
    long offsetLen    = 0;
    long offsetHeight = 0;
    int i             = 1;
    int found         = 0;
    while (found < 10)
    {
      (tower, rockIndex, windIndex) = this.AccumulateRocks(tower, rockDefs, winds, rockIndex, windIndex, width, 1);
      var result = (tower.CaptureRow(tower.Height, width), rockIndex, windIndex);

      if (results.ContainsKey(result)) {
        var dup      = results[result];
        periodLen    = i - dup.Item1;
        periodHeight = tower.Height - dup.Item2;
        offsetLen    = dup.Item1;
        offsetHeight = dup.Item2;
        found       += 1;

        foreach (var t in results.Values)
        {
          if (t.Item1 > offsetLen)
          {
            heights[t.Item1 - offsetLen] = t.Item2 - offsetHeight;
          }
        }
      }
      else {
        results[result] = (i, tower.Height);
        found = 0;
      }
      i += 1;
    }

    return new Calculator(offsetLen, offsetHeight, periodLen, periodHeight, heights);
  }

  // ========== PHYSICS ===================================

  private (Tower, int, int) AccumulateRocks(
    Tower tower,
    List<Rock> rockDefs,
    List<string> winds,
    int rockIndex,
    int windIndex,
    int width,
    int cycles
  )
  {
    // get first value
    var rockDef  = rockDefs[rockIndex];
    var wind     = winds[windIndex];

    foreach (var i in Enumerable.Range(0, cycles))
    {
      // set new rock in absolute space
      var rock = rockDef.SetAbsolutePosition(tower.Height);

      // while not at rest, move sideways and down
      var falling = true;
      while (falling) {
        if (wind == "<") {
          rock.TryMoveLeft(tower);
        }
        else {
          rock.TryMoveRight(tower, width);
        }
        falling   = rock.TryMoveDown(tower);
        windIndex = (windIndex + 1) % winds.Count;
        wind      = winds[windIndex];
      }

      // add rock to tower
      tower.AddRock(rock);

      // get next rock
      rockIndex = (rockIndex + 1) % rockDefs.Count;
      rockDef   = rockDefs[rockIndex];
    }

    return (tower, rockIndex, windIndex);
  }

  // ========== STRUCTS ===================================

  private List<Rock> RockDefinitions ()
  {
    var rocks = new List<Rock>();

    // horizontal
    var ps1 = new List<RockPoint>();
    ps1.Add(new RockPoint(0, 0, true,  true, false));
    ps1.Add(new RockPoint(1, 0, false, true, false));
    ps1.Add(new RockPoint(2, 0, false, true, false));
    ps1.Add(new RockPoint(3, 0, false, true, true));
    rocks.Add(new Rock(ps1));

    // plus
    var ps2 = new List<RockPoint>();
    ps2.Add(new RockPoint(0, 0,  true,  true,  false));
    ps2.Add(new RockPoint(1, 0,  false, false, false));
    ps2.Add(new RockPoint(2, 0,  false, true,  true));
    ps2.Add(new RockPoint(1, 1,  true,  false, true));
    ps2.Add(new RockPoint(1, -1, true,  true,  true));
    rocks.Add(new Rock(ps2));

    // backward L
    var ps3 = new List<RockPoint>();
    ps3.Add(new RockPoint(0, 0,  true,  true,  false));
    ps3.Add(new RockPoint(1, 0,  false, true,  false));
    ps3.Add(new RockPoint(2, 0,  false, true,  true));
    ps3.Add(new RockPoint(2, 1,  true,  false, true));
    ps3.Add(new RockPoint(2, 2,  true,  false, true));
    rocks.Add(new Rock(ps3));

    // vertical
    var ps4 = new List<RockPoint>();
    ps4.Add(new RockPoint(0, 0,  true, true,  true));
    ps4.Add(new RockPoint(0, 1,  true, false, true));
    ps4.Add(new RockPoint(0, 2,  true, false, true));
    ps4.Add(new RockPoint(0, 3,  true, false, true));
    rocks.Add(new Rock(ps4));

    // square
    var ps5 = new List<RockPoint>();
    ps5.Add(new RockPoint(0, 0,  true,  true,  false));
    ps5.Add(new RockPoint(1, 0,  false, true,  true));
    ps5.Add(new RockPoint(0, 1,  true,  false, false));
    ps5.Add(new RockPoint(1, 1,  false, false, true));
    rocks.Add(new Rock(ps5));

    return rocks;
  }

  private List<string> Winds ()
  {
    return this.Data()[0].
              ToCharArray().
              ToList().
              Select(c => c.ToString()).
              ToList();
  }

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day17/input.txt");
  }
}