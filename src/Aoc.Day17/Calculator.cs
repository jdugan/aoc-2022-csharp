namespace Aoc.Day17;

public class Calculator
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Calculator (
    long offsetLen,
    long offsetHeight,
    long periodLen,
    long periodHeight,
    Dictionary<long, long> remainderHeights)
  {
    OffsetLen        = offsetLen;
    OffsetHeight     = offsetHeight;
    PeriodLen        = periodLen;
    PeriodHeight     = periodHeight;
    RemainderHeights = remainderHeights;
  }
  public long                   OffsetLen        { get; private set; }
  public long                   OffsetHeight     { get; private set; }
  public long                   PeriodLen        { get; private set; }
  public long                   PeriodHeight     { get; private set; }
  public Dictionary<long, long> RemainderHeights { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public long DetermineHeight (long cycles)
  {
    var numRepeats = (cycles - this.OffsetLen) / this.PeriodLen;
    var remRepeats = (cycles - this.OffsetLen) % this.PeriodLen;
    var remHeight  = this.RemainderHeights[remRepeats];

    return (numRepeats * this.PeriodHeight) + remHeight + this.OffsetHeight;
  }
}
