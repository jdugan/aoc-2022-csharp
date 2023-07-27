using System.Numerics;
using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day15;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 15;
  }

  public int Puzzle1()
  {
    var row       = 2000000;
    var cave      = this.Cave();
    var range     = cave.GetBlockedRangesForRow(row)[0];
    var beacons   = cave.GetBeaconsInRangeForRow(range, row);

    return range.Size - beacons.Count;
  }

  public BigInteger Puzzle2()
  {
    var cave = this.Cave();
    int x    = 0;
    int y    = 0;

    foreach (int i in Enumerable.Range(2500000, 4000000))
    {
      var ranges = cave.GetBlockedRangesForRow(i);
      if (ranges.Count > 1)
      {
        x = ranges[0].To + 1;
        y = i;
        break;
      }
    }
    var beacon = new Beacon(x, y);

    return beacon.Frequency;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  private List<(int, int)> CompressRanges(List<(int, int)> compressed, (int, int) current)
  {
    return compressed;
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day15/input.txt");
  }

  private Cave Cave ()
  {
    var beaconIds = new HashSet<(int, int)>();
    var beacons   = new List<Beacon>();
    var sensors   = new List<Sensor>();

    foreach (string line in this.Data())
    {
      var re = new Regex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");
      var m  = re.Match(line);

      int sx = Int32.Parse(m.Groups[1].Value);
      int sy = Int32.Parse(m.Groups[2].Value);
      int bx = Int32.Parse(m.Groups[3].Value);
      int by = Int32.Parse(m.Groups[4].Value);

      var b = new Beacon(bx, by);
      var s = new Sensor(sx, sy, b);

      if (!beaconIds.Contains(b.Id))
      {
        beaconIds.Add((bx, by));
        beacons.Add(b);
      }
      sensors.Add(s);
    }

    return new Cave(beacons.ToList(), sensors);
  }
}