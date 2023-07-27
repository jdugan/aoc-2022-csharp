namespace Aoc.Day09;

public class Knot
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Knot()
  {
    X      = 0;
    Y      = 0;
    Visits = new HashSet<(int, int)> { (0, 0) };
  }
  public int X  { get; private set; }
  public int Y  { get; private set; }
  public HashSet<(int, int)> Visits { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public Knot Move(string direction) {
    switch (direction)
    {
      case "D":
        this.Y += -1;
        break;
      case "L":
        this.X += -1;
        break;
      case "R":
        this.X += 1;
        break;
      case "U":
        this.Y += 1;
        break;
    }
    this.Record();

    return this;
  }

  public Knot Follow(Knot leader) {
    int dx = leader.X - this.X;
    int dy = leader.Y - this.Y;
    if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
    {
      dx = (dx == 0) ? 0 : dx/Math.Abs(dx);
      dy = (dy == 0) ? 0 : dy/Math.Abs(dy);

      this.X += dx;
      this.Y += dy;
    }
    this.Record();

    return this;
  }

  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private void Record()
  {
    this.Visits.Add((this.X, this.Y));
  }
}