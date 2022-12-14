namespace Aoc.Day14;

public class Tile
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Tile (int x, int y, string state)
  {
    Id    = (x, y);
    X     = x;
    Y     = y;
    State = state;
  }
  public (int, int) Id     { get; private set; }
  public int        X      { get; private set; }
  public int        Y      { get; private set; }
  public string     State  { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

}
