using System.Collections;

namespace Aoc.Day21;

public class Monkey
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Monkey (string id, List<string> keys, string symbol, List<long> values)
  {
    Id     = id;
    Keys   = keys;
    Symbol = symbol;
    Values = values;
  }
  public string       Id     { get; private set; }
  public List<string> Keys   { get; private set; }
  public string       Symbol { get; set; }
  public List<long>   Values { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== EXPANSION =================================

  public ArrayList ExpressionForKey (string key)
  {
    if (this.HasValue())
    {
      return new ArrayList{ this.Value() };
    }
    else
    {
      var isFirst = (this.Keys[0] == key);
      var term    = this.Id;
      var value   = (isFirst) ? this.Values[1] : this.Values[0];
      var inverse = Int64.Parse("-1");

      switch (this.Symbol)
      {
        case "*":
          return new ArrayList{ term, value, "/" };
        case "/":
          if (isFirst) {
            return new ArrayList{ term, value, "*" };
          }
          else {
            return new ArrayList{ term, inverse, "**", value, "*" };
          }
        case "+":
          return new ArrayList{ term, value, "-" };
        case "-":
          if (isFirst) {
            return new ArrayList{ term, value, "+" };
          }
          else {
            return new ArrayList{ term, inverse, "*", value, "+" };
          }
        default:
          return new ArrayList();
      }
    }
  }


  // ========== REDUCTION =================================

  public void Translation (Dictionary<string, Monkey> monkeys)
  {
    for (int i = 0; i < this.Keys.Count; i++)
    {
      var k = this.Keys[i];
      var v = this.Values[i];

      if (monkeys.ContainsKey(k))
      {
        var m = monkeys[k];
        if (v == Int64.MinValue)
        {
          if (m.HasValue())
          {
            this.Values[i] = m.Value();
          }
        }
      }
    }
  }

  public long Value()
  {
    switch (this.Symbol)
    {
      case "*":
        return this.Values[0] * this.Values[1];
      case "/":
        return this.Values[0] / this.Values[1];
      case "+":
        return this.Values[0] + this.Values[1];
      case "-":
        return this.Values[0] - this.Values[1];
      default:
        return this.Values[0];
    }
  }

  // ========== STATE =====================================

  public bool HasValue ()
  {
    return !this.NeedsTranslation();
  }

  public bool NeedsTranslation ()
  {
    var count = this.Values.Where(v => v == Int64.MinValue).ToList().Count;
    return count > 0;
  }


  // ========== DISPLAY ===================================

  public void Print ()
  {
    if (this.Values.Count == 1) {
      Console.WriteLine(
        "{0} => {1}",
        this.Id,
        this.Value()
      );
    }
    else {
      Console.WriteLine(
        "{0} => {1} ({2}) {3} {4} ({5}) = {6}",
        this.Id,
        this.Keys[0],
        this.Values[0],
        this.Symbol,
        this.Keys[1],
        this.Values[1],
        this.Value()
      );
    }
  }
}