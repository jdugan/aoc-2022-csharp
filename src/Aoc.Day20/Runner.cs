using Aoc.Utility;

﻿namespace Aoc.Day20;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 20;
  }

  public long Puzzle1()
  {
    var tuples = this.PerformMix(1, 1);
    var coords = this.GetCoordinates(tuples);

    return coords.Sum();
  }

  public long Puzzle2()
  {
    var tuples = this.PerformMix(10, 811589153);
    var coords = this.GetCoordinates(tuples);

    return coords.Sum();
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== MIXING ====================================

  private List<(long, long)> PerformMix (int times, long factor)
  {
    // set working variables
    var tuples = this.BuildList(factor);
    var count  = tuples.Count;

    // mix n times
    foreach (int _ in Enumerable.Range(0, times))
    {
      // process in original id order
      foreach (int id in Enumerable.Range(0, count))
      {
        // find the id in the current list
        foreach (var (i, t) in tuples.Select((v, i) => (i, v)))
        {
          if (t.Item1 == id)
          {
            // determine new index
            var idx = (i + t.Item2) % (count - 1);
            if (idx < 0) {
              idx = idx + count - 1;
            }

            // remove the tuple and add it back in the new spot
            tuples.Remove(t);
            tuples.Insert((int)idx, t);

            // stop searching
            break;
          }
        }
      }
    }

    return tuples;
  }

  // ========== SCORING ===================================

  private List<long> GetCoordinates (List<(long, long)> tuples)
  {
    // find origin index
    int idx0 = 0;
    foreach (var t in tuples) {
      if (t.Item2 == 0) {
        break;
      }
      idx0 += 1;
    }

    // find grove coord indices
    int idx1 = (idx0 + 1000) % (tuples.Count);
    int idx2 = (idx0 + 2000) % (tuples.Count);
    int idx3 = (idx0 + 3000) % (tuples.Count);

    // add coord values to list
    var coords = new List<long>();
    coords.Add(tuples[idx1].Item2);
    coords.Add(tuples[idx2].Item2);
    coords.Add(tuples[idx3].Item2);
    return coords;
  }


  // ========== STRUCTURES ================================

  private List<(long, long)> BuildList(long factor)
  {
    return this.Data().
              Select((v, i) => ((long)i, (long)v*factor)).
              ToList();
  }


  // ========== DATA ======================================

  private List<int> Data()
  {
    return Reader.ToIntegers("data/day20/input.txt");
  }
}