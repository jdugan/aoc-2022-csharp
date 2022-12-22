using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day21;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 21;
  }

  public long Puzzle1()
  {
    var  monkeys = this.Monkeys();
    bool done    = false;
    while (!done) {
      (monkeys, done) = this.PerformForwardTranslations(monkeys);
    }
    
    return monkeys["root"].ForwardValue();
  }

  public int Puzzle2()
  {
    return -2;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== FORWARD ===================================

  private bool ForwardTranslationsComplete (Dictionary<string, Monkey> monkeys)
  {
    int count = monkeys.Values.Where(m => m.NeedsForwardTranslation()).ToList().Count;
    return count == 0;
  }

  private (Dictionary<string, Monkey>, bool) PerformForwardTranslations (Dictionary<string, Monkey> monkeys)
  {
    foreach (var m in monkeys.Values)
    {
      if (m.NeedsForwardTranslation())
      {
        m.ForwardTranslation(monkeys);
      }
    }
    bool done = this.ForwardTranslationsComplete(monkeys);

    return (monkeys, done);
  }


  // ========== STRUCTS ===================================

  private Dictionary<string, Monkey> Monkeys ()
  {
    var monkeys = new Dictionary<string, Monkey>();
    foreach (var line in this.Data())
    {
      var re1 = new Regex(@"^(\w+): (-?\d+)$");
      var m1  = re1.Match(line);
      if (m1.Success)
      {
        var id     = m1.Groups[1].Value;
        var keys   = new List<string>();
        var symbol = "=>";
        var values = new List<long>{ Int64.Parse(m1.Groups[2].Value) };

        monkeys[id] = new Monkey(id, keys, symbol, values);
      }
      else
      {
        var re2 = new Regex(@"^(\w+): (\w+) (.) (\w+)$");
        var m2  = re2.Match(line);
        if (m2.Success)
        {
          var id     = m2.Groups[1].Value;
          var keys   = new List<string>{ m2.Groups[2].Value, m2.Groups[4].Value };
          var symbol = m2.Groups[3].Value;
          var values = new List<long>{ Int64.MinValue, Int64.MinValue };

          monkeys[id] = new Monkey(id, keys, symbol, values);
        }
      }
    }
    return monkeys;
  }

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day21/input.txt");
  }
}
