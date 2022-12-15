namespace Aoc.Day15;

public class Cave
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Cave (List<Beacon> beacons, List<Sensor> sensors)
  {
    Beacons   = beacons;
    Sensors   = sensors;
  }
  public List<Beacon> Beacons { get; private set; }
  public List<Sensor> Sensors { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public List<Beacon> GetBeaconsInRangeForRow (BlockedRange range, int row)
  {
    return this.Beacons.
              Where(b => b.Y == row).
              Where(b => range.Contains(b.X)).
              ToList();
  }

  public List<BlockedRange> GetBlockedRangesForRow (int row)
  {
    var ranges = new List<BlockedRange>();
    foreach (var s in this.Sensors)
    {
      var br = s.GetBlockedRangeForRow(row);
      if (br.From != Int32.MinValue)
      {
        ranges.Add(br);
      }
    }

    return this.CompressBlockedRanges(ranges);
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private List<BlockedRange> CompressBlockedRanges (List<BlockedRange> ranges)
  {
    var compressed = new List<BlockedRange>();
    var baseline   = ranges[0];
    var changed    = false;

    foreach (var i in Enumerable.Range(1, ranges.Count - 1))
    {
      var r = ranges[i];
      if (changed)
      {
        compressed.Add(r);
      }
      else
      {
        if (baseline.Encompasses(r))
        {
          // Console.WriteLine("baseline encompasses item: {0}, {1}, {2}, {3}", baseline.From, baseline.To, r.From, r.To);
          compressed.Add(baseline);
          changed = true;
        }
        else if (r.Encompasses(baseline))
        {
          // Console.WriteLine("item encompasses baseline: {0}, {1}, {2}, {3}", baseline.From, baseline.To, r.From, r.To);
          compressed.Add(r);
          changed = true;
        }
        else if (r.Overlaps(baseline))
        {
          // Console.WriteLine("item and baseline overlap: {0}, {1}, {2}, {3}", baseline.From, baseline.To, r.From, r.To);
          if (baseline.Contains(r.From))
          {
            compressed.Add(new BlockedRange(baseline.From, r.To));
          }
          else {
            compressed.Add(new BlockedRange(r.From, baseline.To));
          }
          changed = true;
        }
        else
        {
          compressed.Add(r);
        }
      }
    }
    // Console.WriteLine("----------------------------");
    // foreach (var br in compressed)
    // {
    //   Console.WriteLine("{0}, {1}", br.From, br.To);
    // }

    if (changed) {
      compressed = this.CompressBlockedRanges(compressed);
    }
    else
    {
      compressed.Add(baseline);
    }
    return compressed;
  }
}
