namespace Aoc.Day17;

public class RockPoint
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public RockPoint (int x, int y, bool isLeftEdge, bool isBottomEdge, bool isRightEdge)
  {
    X            = x;
    Y            = y;
    IsLeftEdge   = isLeftEdge;
    IsBottomEdge = isBottomEdge;
    IsRightEdge  = isRightEdge;
    IsOrigin     = (x == 0 && y == 0);
  }
  public int   X            { get; set; }
  public int   Y            { get; set; }
  public bool  IsLeftEdge   { get; private set; }
  public bool  IsBottomEdge { get; private set; }
  public bool  IsRightEdge  { get; private set; }
  public bool  IsOrigin     { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public RockPoint Copy ()
  {
    return new RockPoint(
                  this.X,
                  this.Y,
                  this.IsLeftEdge,
                  this.IsBottomEdge,
                  this.IsRightEdge
               );
  }

  public (int, int) Id ()
  {
    return (this.X, this.Y);
  }
}
