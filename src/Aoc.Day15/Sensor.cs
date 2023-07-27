namespace Aoc.Day15;

public class Sensor
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Sensor (int x, int y, Beacon beacon)
  {
    Id     = (x, y);
    X      = x;
    Y      = y;
    Beacon = beacon;
    Radius = Math.Abs(x - Beacon.X) + Math.Abs(y - Beacon.Y);
  }
  public Beacon     Beacon { get; private set; }
  public (int, int) Id     { get; private set; }
  public int        Radius { get; private set; }
  public int        X      { get; private set; }
  public int        Y      { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public BlockedRange GetBlockedRangeForRow (int y)
  {
    int dx = this.Radius - Math.Abs(y - this.Y);
    if (dx > 0)
    {
      return new BlockedRange(this.X - dx, this.X + dx);
    }

    return new BlockedRange(Int32.MinValue, Int32.MinValue);
  }
}