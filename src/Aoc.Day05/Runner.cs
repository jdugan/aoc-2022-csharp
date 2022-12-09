using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day05;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 5;
  }

  public string Puzzle1()
  {
    var stacks = this.Stacks();
    var instrs = this.Instructions();
    foreach (var instruction in this.Instructions())
    {
      stacks = this.Move9000(stacks, instruction);
    }

    return this.PrintTopCrates(stacks);
  }

  public string Puzzle2()
  {
    var stacks = this.Stacks();
    var instrs = this.Instructions();
    foreach (var instruction in this.Instructions())
    {
      stacks = this.Move9001(stacks, instruction);
    }

    return this.PrintTopCrates(stacks);
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== CRANE =====================================

  private Dictionary<int, Stack<string>> Move9000 (Dictionary<int, Stack<string>> stacks, (int moves, int fromId, int toId) instruction)
  {
    foreach (var _ in Enumerable.Range(0, instruction.moves))
    {
      string crate = stacks[instruction.fromId].Pop();
      stacks[instruction.toId].Push(crate);
    }
    return stacks;
  }

  private Dictionary<int, Stack<string>> Move9001 (Dictionary<int, Stack<string>> stacks, (int moves, int fromId, int toId) instruction)
  {
    var tmpStack = new Stack<string>();
    foreach (var _ in Enumerable.Range(0, instruction.moves))
    {
      string crate = stacks[instruction.fromId].Pop();
      tmpStack.Push(crate);
    }
    foreach (var _ in Enumerable.Range(0, instruction.moves))
    {
      string crate = tmpStack.Pop();
      stacks[instruction.toId].Push(crate);
    }
    return stacks;
  }

  private string PrintTopCrates (Dictionary<int, Stack<string>> stacks)
  {
    var crates = new List<string>();
    foreach (var kvp in stacks)
    {
      crates.Add(kvp.Value.Peek());
    }
    return String.Join("", crates.ToArray());
  }

  // ========== DATA ======================================

  private List<string> Data()
  {
    List<string> lines = Reader.GetLines("data/day05/input.txt");
    return lines;
  }

  private List<(int, int, int)> Instructions()
  {
    var instrs = new List<(int, int, int)>();
    var re     = new Regex(@"^move (\d+) from (\d+) to (\d+)$");
    foreach (string line in this.Data())
    {
      var m  = re.Match(line);
      if (m.Success)
      {
        var i1 = Int32.Parse(m.Groups[1].Value);
        var i2 = Int32.Parse(m.Groups[2].Value);
        var i3 = Int32.Parse(m.Groups[3].Value);
        instrs.Add((i1, i2, i3));
      }
    }
    return instrs;
  }

  private Dictionary<int, Stack<string>> Stacks()
  {
    // memoise lines
    var lines = new List<string>();
    foreach (string line in this.Data())
    {
      if (line.Length > 0)
      {
        lines.Add(line);
      }
      else
      {
        break;
      }
    }
    lines.Reverse();

    // map indices to keys
    var keyDict  = new Dictionary<int, int>();
    var keyStr   = $" {lines[0]}";
    var keyRegex = new Regex(@"\d");
    foreach (Match match in keyRegex.Matches(keyStr))
    {
      var key = match.Groups[0].Index;
      var val = Int32.Parse(match.Groups[0].Value);
      keyDict[key] = val;
    }

    // initialise stack dictionary
    var stacks = new Dictionary<int, Stack<string>>();
    foreach (int id in keyDict.Values)
    {
      stacks[id] = new Stack<string>();
    }

    // map crates into stack dictionary
    var createRegex = new Regex(@"\w");
    foreach (string line in lines.GetRange(1, 8))
    {
      foreach (Match match in createRegex.Matches(line))
      {
        var index = match.Groups[0].Index;
        var crate = match.Groups[0].Value;
        var id    = keyDict[index];

        stacks[id].Push(crate);
      }
    }

    return stacks;
  }
}
