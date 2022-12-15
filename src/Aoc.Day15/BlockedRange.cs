namespace Aoc.Day15;

public class BlockedRange : IComparable
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public BlockedRange (int from, int to)
  {
    From = from;
    To   = to;
    Size = to - from + 1;
  }
  public int From { get; private set; }
  public int Size { get; private set; }
  public int To   { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public int CompareTo (object obj)
  {
    BlockedRange other = (BlockedRange)obj;

    int dFrom = Math.Abs(this.From - other.From)/(this.From - other.From);
    int dTo   = Math.Abs(this.To - other.To)/(this.To - other.To);

    if (dFrom != 0) {
      return dFrom;
    }
    else {
      return dTo;
    }
  }

  public bool Contains (int x)
  {
    return (x >= this.From) && (x <= this.To);
  }

  public bool Encompasses (BlockedRange other)
  {
    return (this.From <= other.From) && (this.To >= other.To);
  }

  public bool Overlaps (BlockedRange other)
  {
    return other.Contains(this.From) || other.Contains(this.To);
  }
}
