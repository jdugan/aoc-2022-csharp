using System.Numerics;

namespace Aoc.Day11;

public class ItemWithReduction
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public ItemWithReduction (int value)
  {
    Value = (BigInteger)value;
  }
  public BigInteger Value { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public int InspectBy (Monkey monkey)
  {
    // parse operation
    string[]   parts   = monkey.Operation.Replace("old", this.Value.ToString()).Split(" ");
    string     action  = parts[0];
    BigInteger operand = BigInteger.Parse(parts[1]);

    // set new value
    BigInteger newItem;
    if (action == "*")
    {
      newItem = BigInteger.Multiply(this.Value, operand);
    }
    else
    {
      newItem = BigInteger.Add(this.Value, operand);
    }
    newItem = BigInteger.Divide(newItem, 3);
    this.Value = newItem;

    // determine next monkey
    if (this.Value % monkey.ThrowDivisor == 0)
    {
      return monkey.ThrowTrueTo;
    }
    else {
      return monkey.ThrowFalseTo;
    }
  }
}