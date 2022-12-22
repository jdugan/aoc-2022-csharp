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
  public string       Symbol { get; private set; }
  public List<long>   Values { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== FORWARD ===================================

  public void ForwardTranslation (Dictionary<string, Monkey> monkeys)
  {
    for (int i = 0; i < this.Keys.Count; i++)
    {
      var k = this.Keys[i];
      var v = this.Values[i];
      var m = monkeys[k];

      if (v == Int64.MinValue)
      {
        if (m.HasForwardValue())
        {
          this.Values[i] = m.ForwardValue();
        }
      }
    }
  }

  public long ForwardValue()
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

  public bool NeedsForwardTranslation ()
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
        this.ForwardValue()
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
        this.ForwardValue()
      );
    }
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== FORWARD ===================================

  private bool HasForwardValue ()
  {
    return !this.NeedsForwardTranslation();
  }
}
