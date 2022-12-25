using Aoc.Utility;

﻿namespace Aoc.Day25;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 25;
  }

  public string Puzzle1()
  {
    var sum = this.Data().
                  Select(s => this.ConvertToDecimal(s)).
                  Sum();

    return this.ConvertToSnafu(sum);
  }

  public int Puzzle2()
  {
    return -2;
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== CONVERSIONS ===============================

  private long ConvertToDecimal (string snafu)
  {
    // break snafu into digits and reverse
    var digits = snafu.
                    ToCharArray().ToList().
                    Select(c => c.ToString()).ToList();
    digits.Reverse();

    // collect parts
    var    parts = new List<long>();
    double pow   = 0;
    foreach (var d in digits)
    {
      parts.Add( this.TranslateToDecimal(d) * (long)Math.Pow(5, pow) );
      pow += 1;
    }

    // return sum
    return parts.Sum();
  }

  private string ConvertToSnafu (long num)
  {
    // convert to base5
    string base5 = "";
    while (num != 0)
    {
      base5 = (num % 5).ToString() + base5;
      num    = num / 5;
    }

    // break base5 into digits and reverse
    var digits = base5.
                    ToCharArray().ToList().
                    Select(c => c.ToString()).ToList();
    digits.Reverse();

    // adjust parts
    var parts = new List<string>();
    for (int i = 0; i < digits.Count; i++)
    {
      (string curr, string next) = this.TranslateToSnafu(digits[i]);
      if (next == "1")
      {
        if (i + 1 < digits.Count)
        {
          digits[i + 1] = (Int64.Parse(digits[i + 1]) + 1).ToString();
        }
        else {
          curr = $"1{curr}";
        }
      }
      parts.Add(curr);
    }

    // return reassembled number
    parts.Reverse();
    return String.Join("", parts);
  }


  // ========== TRANSLATIONS ==============================

  private long TranslateToDecimal (string digit) => digit switch
  {
    "2" => 2,
    "1" => 1,
    "0" => 0,
    "-" => -1,
    _   => -2
  };

  private (string, string) TranslateToSnafu (string digit) => digit switch
  {
    "1" => ("1", "0"),
    "2" => ("2", "0"),
    "3" => ("=", "1"),
    "4" => ("-", "1"),
    "5" => ("0", "1"),
    _   => ("0", "0")
  };


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day25/input.txt");
  }
}
