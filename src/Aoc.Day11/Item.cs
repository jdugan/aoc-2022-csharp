using System.Numerics;

namespace Aoc.Day11;

public class Item
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Item (List<int> divisors, int value)
  {
    Moduli = this.InitializeModuli(divisors, value);
  }
  public Dictionary<int, int> Moduli { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public int InspectBy (Monkey monkey)
  {
    string operation = monkey.Operation;
    int    modulus;
    int    divisor;

    // update modulus for all divisors as an
    // optimisation against crazy long operation
    // lists.
    foreach (KeyValuePair<int, int> entry in Moduli)
    {
      divisor = entry.Key;
      modulus = entry.Value;

      if (monkey.Operation == "* old")
      {
        modulus = ((modulus % divisor) * (modulus % divisor)) % divisor;
      }
      else
      {
        string[] parts   = operation.Split(" ");
        string   action  = parts[0];
        int      operand = Int32.Parse(parts[1]);

        if (action == "*")
        {
          modulus = ((modulus % divisor) * (operand % divisor)) % divisor;
        }
        else
        {
          modulus = ((modulus % divisor) + (operand % divisor)) % divisor;
        }
      }
      this.Moduli[divisor] = modulus;
    }

    // determine next monkey
    if (this.Moduli[monkey.ThrowDivisor] == 0)
    {
      return monkey.ThrowTrueTo;
    }
    else {
      return monkey.ThrowFalseTo;
    }
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private Dictionary<int, int> InitializeModuli (List<int> divisors, int value)
  {
    var dict = new Dictionary<int, int>();
    foreach (int d in divisors)
    {
      dict[d] = value % d;
    }
    return dict;
  }
}