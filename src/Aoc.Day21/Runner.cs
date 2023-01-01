using System.Collections;
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
    return this.CalculateRoot();
  }

  public long Puzzle2()
  {
    return this.CalculateHuman();
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== CALCULATIONS ==============================

  private long CalculateHuman ()
  {
    // get monkeys and remove humn
    var monkeys = this.Monkeys();
    monkeys.Remove("humn");

    // get root keys
    var rootKeys = monkeys["root"].Keys.ToList();
    var rkey1    = rootKeys[0];
    var rkey2    = rootKeys[1];

    // translate as far as we can
    while (monkeys[rkey1].NeedsTranslation() && monkeys[rkey2].NeedsTranslation())
    {
      (monkeys, _) = this.PerformTranslations(monkeys);
    }

    // the way my loop works, this is the same as equality
    if (monkeys["root"].Values[0] == Int64.MinValue) {
      monkeys["root"].Values[0] = 0;
    }
    else {
      monkeys["root"].Values[1] = 0;
    }

    // build calculation
    var terms = this.ExpandTerms(new ArrayList{"humn"}, monkeys);
    while (terms[0].GetType() == typeof(string))
    {
      terms = this.ExpandTerms(terms, monkeys);
    }

    return this.ComputeTerms(terms);
  }

  private long CalculateRoot ()
  {
    var  monkeys = this.Monkeys();
    bool done    = false;
    while (!done) {
      (monkeys, done) = this.PerformTranslations(monkeys);
    }

    return monkeys["root"].Value();
  }

  // ========== EXPANSIONS ================================

  private ArrayList ExpandTerms (ArrayList current, Dictionary<string, Monkey> monkeys)
  {
    string key = (string)current[0];
    var list   = current.GetRange(1, current.Count - 1);
    var monkey = monkeys.Values.ToList().Find(m => m.Keys.Contains(key));
    var expr   = monkey.ExpressionForKey(key);
    expr.AddRange(list);

    return expr;
  }

  private long ComputeTerms (ArrayList terms)
  {
    long term1 = Int64.MaxValue;
    long term2 = Int64.MaxValue;

    for (int n = 0; n < terms.Count; n++)
    {
      if (term1 == Int64.MaxValue) {
        term1 = (long)terms[n];
      }
      else if (term2 == Int64.MaxValue) {
        term2 = (long)terms[n];
      }
      else {
        switch (terms[n])
        {
          case "**":
            term1 = (long)Math.Pow(term1, term2);
            break;
          case "*":
            term1 = term1 * term2;
            break;
          case "/":
            term1 = term1 / term2;
            break;
          case "+":
            term1 = term1 + term2;
            break;
          case "-":
            term1 = term1 - term2;
            break;
        }
        term2 = Int64.MaxValue;
      }
    }

    return term1;
  }


  // ========== REDUCTIONS ================================

  private int NeedsTranslationCount (Dictionary<string, Monkey> monkeys)
  {
    return monkeys.Values.Where(m => m.NeedsTranslation()).ToList().Count;
  }

  private (Dictionary<string, Monkey>, bool) PerformTranslations (Dictionary<string, Monkey> monkeys)
  {
    foreach (var m in monkeys.Values)
    {
      if (m.NeedsTranslation())
      {
        m.Translation(monkeys);
      }
    }
    bool done = this.TranslationsComplete(monkeys);

    return (monkeys, done);
  }

  private bool TranslationsComplete (Dictionary<string, Monkey> monkeys)
  {
    return this.NeedsTranslationCount(monkeys) == 0;
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
