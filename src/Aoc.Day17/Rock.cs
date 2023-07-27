namespace Aoc.Day17;

public class Rock
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Rock (List<RockPoint> points)
  {
    Points = points;
  }
  public List<RockPoint> Points { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public bool AtRest (Tower tower)
  {
    return !this.OpenBelow(tower);
  }

  public Rock SetAbsolutePosition (int height)
  {
    var offsetX = this.GetLeftOffset();
    var offsetY = (int)height + 4 - this.GetBottomOffset();
    var points  = new List<RockPoint>();
    foreach (var p in this.Points)
    {
      var rp = p.Copy();
      rp.X += offsetX;
      rp.Y += offsetY;
      points.Add(rp);
    }
    return new Rock(points);
  }


  // ========== MOVEMENT ==================================

  public bool TryMoveDown (Tower tower)
  {
    if (this.OpenBelow(tower))
    {
      // Console.WriteLine("Moving down");
      foreach (var p in this.Points)
      {
        p.Y -= 1;
      }
      return true;
    }
    else{
      return false;
    }
  }

  public void TryMoveLeft (Tower tower)
  {
    if (this.OpenLeft(tower))
    {
      // Console.WriteLine("Moving left");
      foreach (var p in this.Points)
      {
        p.X -= 1;
      }
    }
  }

  public void TryMoveRight (Tower tower, int width)
  {
    if (this.OpenRight(tower, width))
    {
      // Console.WriteLine("Moving right");
      foreach (var p in this.Points)
      {
        p.X += 1;
      }
    }
  }


  // ========== DISPLAY ===================================

  public void Print ()
  {
    foreach (var p in this.Points) {
      Console.WriteLine("  {0}", p.Id());
    }
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== AVAILABILITY ==============================

  private bool OpenBelow (Tower tower)
  {
    var open = true;
    foreach (var p in this.GetBottomEdgePoints())
    {
      if (p.Y - 1 == 0 || tower.Points.ContainsKey((p.X, p.Y - 1)))
      {
        open = false;
        break;
      }
    }
    return open;
  }

  private bool OpenLeft (Tower tower)
  {
    var open = true;
    foreach (var p in this.GetLeftEdgePoints())
    {
      if (p.X - 1 == -1 || tower.Points.ContainsKey((p.X - 1, p.Y)))
      {
        open = false;
        break;
      }
    }
    return open;
  }

  private bool OpenRight (Tower tower, int width)
  {
    var open = true;
    foreach (var p in this.GetRightEdgePoints())
    {
      if (p.X + 1 == width || tower.Points.ContainsKey((p.X + 1, p.Y)))
      {
        open = false;
        break;
      }
    }
    return open;
  }


  // ========== EDGES =====================================

  private List<RockPoint> GetBottomEdgePoints ()
  {
    return this.Points.Where(p => p.IsBottomEdge).ToList();
  }

  private List<RockPoint> GetLeftEdgePoints ()
  {
    return this.Points.Where(p => p.IsLeftEdge).ToList();
  }

  private List<RockPoint> GetRightEdgePoints ()
  {
    return this.Points.Where(p => p.IsRightEdge).ToList();
  }


  // ========== OFFSETS ===================================

  private int GetBottomOffset ()
  {
    return this.GetBottomEdgePoints().
              Select(p => p.Y).
              Min();

  }

  private int GetLeftOffset ()
  {
    return 2;
  }
}