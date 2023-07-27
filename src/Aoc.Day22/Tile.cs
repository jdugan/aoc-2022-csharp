namespace Aoc.Day22;

public class Tile
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Tile (int x, int y, string display)
  {
    Id      = (x, y);
    X       = x;
    Y       = y;
    Display = display;
  }
  public (int, int) Id      { get; private set; }
  public int        X       { get; private set; }
  public int        Y       { get; private set; }
  public string     Display { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public bool IsOpen ()
  {
    return this.Display == ".";
  }
}