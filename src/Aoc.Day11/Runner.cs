using System.Numerics;
using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day11;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 11;
  }

  public BigInteger Puzzle1()
  {
    var monkeys = this.PlaySimpleGame(20);
    var score   = this.CalculateMonkeyBusiness(monkeys);

    return score;
  }

  public BigInteger Puzzle2()
  {
    var monkeys = this.PlayAdvancedGame(10000);
    var score   = this.CalculateMonkeyBusiness(monkeys);

    return score;
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== GAMES =====================================

  private Dictionary<int, Monkey> PlayAdvancedGame(int rounds)
  {
    var monkeys   = this.Monkeys();
    var divisors  = monkeys.Select(entry => entry.Value.ThrowDivisor).ToList();
    var inventory = new Dictionary<int, Stack<Item>>();
    foreach (var m in monkeys.Values)
    {
      var items = new Stack<Item>();
      foreach (var value in m.StartingItems)
      {
        items.Push(new Item(divisors, value));
      }
      inventory[m.Id] = items;
    }

    foreach (int round in Enumerable.Range(0, rounds))
    {
      foreach (var m in monkeys.Values)
      {
        var items = inventory[m.Id];
        while (items.Count > 0)
        {
          var item   = items.Pop();
          var nextId = item.InspectBy(m);
          inventory[nextId].Push(item);
          m.Inspections += 1;
        }
      }
    }

    return monkeys;
  }

  private Dictionary<int, Monkey> PlaySimpleGame(int rounds)
  {
    var monkeys   = this.Monkeys();
    var inventory = new Dictionary<int, Stack<ItemWithReduction>>();
    foreach (var m in monkeys.Values)
    {
      var items = new Stack<ItemWithReduction>();
      foreach (var value in m.StartingItems)
      {
        items.Push(new ItemWithReduction(value));
      }
      inventory[m.Id] = items;
    }

    foreach (int round in Enumerable.Range(0, rounds))
    {
      foreach (var m in monkeys.Values)
      {
        var items = inventory[m.Id];
        while (items.Count > 0)
        {
          var item   = items.Pop();
          var nextId = item.InspectBy(m);
          inventory[nextId].Push(item);
          m.Inspections += 1;
        }
      }
    }

    return monkeys;
  }


  // ========== SCORES ====================================

  private BigInteger CalculateMonkeyBusiness (Dictionary<int, Monkey> monkeys)
  {
    //  print results
    // Console.WriteLine("");
    // Console.WriteLine("-----------------------");
    // foreach (var m in monkeys.Values)
    // {
    //   Console.WriteLine("Monkey {0} inspected items {1} times", m.Id, m.Inspections);
    // }
    // Console.WriteLine("-----------------------");
    // Console.WriteLine("");

    // calculate score
    var inspections = monkeys.Values.Select(m => m.Inspections).ToList();
    inspections.Sort();
    inspections.Reverse();
    var factors = inspections.GetRange(0,2);
    var score   = BigInteger.Multiply(factors[0], factors[1]);

    return score;
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day11/input.txt");
  }

  private Dictionary<int, Monkey> Monkeys ()
  {
    var monkeys = new Dictionary<int, Monkey>();
    var groups  = new List<List<string>>();
    var group   = new List<string>();

    // group lines
    foreach (var (index, line) in this.Data().Select((v, i) => (i, v)))
    {
      group.Add(line);
      if ((index + 1) % 6 == 0) {
        groups.Add(group);
        group = new List<string>();
      }
    }

    // add monkeys to dictionary
    foreach (var g in groups)
    {
      var id            = this.ParseId(g[0]);
      var startingItems = this.ParseStartingItems(g[1]);
      var operation     = this.ParseOperation(g[2]);
      var throwDivisor  = this.ParseDivisor(g[3]);
      var throwTrueTo   = this.ParseThrowTrueTo(g[4]);
      var throwFalseTo  = this.ParseThrowFalseTo(g[5]);

      monkeys[id] = new Monkey(id, operation, startingItems, throwDivisor, throwTrueTo, throwFalseTo);
    }

    return monkeys;
  }

  // ========== PARSERS ===================================

  private int ParseId (string line)
  {
    var re  = new Regex(@"^Monkey (\d+):$");
    var m   = re.Match(line);
    int val = Int32.Parse(m.Groups[1].Value);

    return val;
  }

  private List<int> ParseStartingItems (string line)
  {
    var re  = new Regex(@"^Starting items: (.+)$");
    var m   = re.Match(line);
    var val = new List<int>();
    foreach (string s in m.Groups[1].Value.Split(", "))
    {
      val.Add(Int32.Parse(s));
    }
    return val;
  }

  private string ParseOperation (string line)
  {
    var    re  = new Regex(@"^Operation: new = old (.+)$");
    var    m   = re.Match(line);
    string val = m.Groups[1].Value;

    return val;
  }

  private int ParseDivisor (string line)
  {
    var re  = new Regex(@"^Test: divisible by (\d+)$");
    var m   = re.Match(line);
    var val = Int32.Parse(m.Groups[1].Value);

    return val;
  }

  private int ParseThrowTrueTo (string line)
  {
    var re  = new Regex(@"^If true: throw to monkey (\d+)$");
    var m   = re.Match(line);
    int val = Int32.Parse(m.Groups[1].Value);

    return val;
  }

  private int ParseThrowFalseTo (string line)
  {
    var re  = new Regex(@"^If false: throw to monkey (\d+)$");
    var m   = re.Match(line);
    int val = Int32.Parse(m.Groups[1].Value);

    return val;
  }
}
