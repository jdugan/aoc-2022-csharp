using System.IO;
using Aoc.Utility;

﻿namespace Aoc.Day03;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //--------------------------------------------------------

  public int Day()
  {
    return 3;
  }

  public int Puzzle1()
  {
    return this.Data().
              Select(sack => this.DivideCompartments(sack)).
              Select(a => this.FindCommonItem(a)).
              Select(c => this.ToPriority(c)).
              Sum();
  }

  public int Puzzle2()
  {
    return this.DivideGroups(this.Data()).
              Select(a => this.FindCommonItem(a)).
              Select(c => this.ToPriority(c)).
              Sum();
  }


  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  // ========== HELPERS ====================================

  private List<string> DivideCompartments(string line)
  {
    var strs = new List<string>();
    int len  = line.Length;

    strs.Add(line.Substring(0, len/2));
    strs.Add(line.Substring(len/2, len/2));

    return strs;
  }

  private List<List<string>> DivideGroups(List<string> sacks)
  {
    var groups = new List<List<string>>();
    var group  = new List<string>();
    int count  = 0;

    foreach (string sack in sacks)
    {
      group.Add(sack);
      count += 1;

      if (count == 3)
      {
        groups.Add(group);
        group = new List<string>();
        count = 0;
      }
    }

    return groups;
  }

  private char FindCommonItem(List<string> strs)
  {
    char[] dups = strs[0].ToCharArray();
    char[] curr;

    foreach (string s in strs.Skip(1))
    {
      curr = s.ToCharArray();
      dups = dups.Intersect(curr).Cast<char>().ToArray();
    }

    return dups[0];
  }

  private int ToPriority(char c)
  {
    int ascii  = Convert.ToInt32(c);
    int offset = (ascii < 91) ? -38 : -96;  // ascii has cases in opposite order

    return ascii + offset;
  }


  // ========== DATA =======================================

  private List<string> Data()
  {
    List<string> lines = Reader.ToStrings("data/day03/input.txt");
    return lines;
  }
}
