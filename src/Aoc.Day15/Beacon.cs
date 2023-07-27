using System.Numerics;

namespace Aoc.Day15;

public class Beacon
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Beacon (int x, int y)
  {
    Id        = (x, y);
    X         = x;
    Y         = y;
    Frequency = this.SetFrequency(x, y);
  }
  public (int, int) Id        { get; private set; }
  public int        X         { get; private set; }
  public int        Y         { get; private set; }
  public BigInteger Frequency { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  private BigInteger SetFrequency (int x, int y)
  {
    BigInteger v1 = BigInteger.Multiply(4000000, x);
    BigInteger v2 = BigInteger.Add(v1, y);

    return v2;
  }
}