namespace Aoc.Day14;

public class Cave
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Cave ((int, int) origin, Dictionary<(int, int), Tile> tiles)
  {
    Origin = origin;
    Tiles  = tiles;
  }
  public (int, int) Origin                  { get; private set; }
  public Dictionary<(int, int), Tile> Tiles { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== CALCULATE =================================

  public int SandVolume ()
  {
    bool overflowing = false;
    while (!overflowing)
    {
      overflowing = this.Drop();
    }

    return this.Tiles.
              Where(kvp => kvp.Value.State == "o").
              ToList().
              Count;
  }


  // ========== DIMENSIONS ================================

  public int GetMaxX ()
  {
    int best = Int32.MinValue;
    foreach (var (x, y) in this.Tiles.Keys)
    {
      if (x > best)
      {
        best = x;
      }
    }
    return best + 1;
  }

  public int GetMinX ()
  {
    int best = Int32.MaxValue;
    foreach (var (x, y) in this.Tiles.Keys)
    {
      if (x < best)
      {
        best = x;
      }
    }
    return best - 1;
  }

  public int GetMaxY ()
  {
    int best = Int32.MinValue;
    foreach (var (x, y) in this.Tiles.Keys)
    {
      if (y > best)
      {
        best = y;
      }
    }
    return best + 1;
  }

  public int GetMinY ()
  {
    int best = Int32.MaxValue;
    foreach (var (x, y) in this.Tiles.Keys)
    {
      if (y < best)
      {
        best = y;
      }
    }
    return best - 1;
  }


  // ========== DISPLAY ===================================

  public void Print ()
  {
    var xMin = this.GetMinX();
    var xMax = this.GetMaxX();
    var yMin = this.GetMinY();
    var yMax = this.GetMaxY();

    Console.WriteLine("");
    for (int y = yMin; y <= yMax; y++)
    {
      var row = new List<string>();
      for (int x = xMin; x <= xMax; x++)
      {
        try
        {
          row.Add(this.Tiles[(x, y)].State);
        }
        catch {
          row.Add(".");
        }
      }
      row.Add($" {y}");
      Console.WriteLine(String.Join("", row));
    }
    Console.WriteLine("");
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private bool Drop ()
  {
    bool moving = true;
    int  count  = this.Tiles.Count;
    int  yMax   = this.GetMaxY();
    int  x      = this.Origin.Item1;
    int  y      = this.Origin.Item2;

    while (moving && y < yMax)
    {
      bool moved = false;
      var  moves = new List<(int, int)> { (x, y+1), (x-1, y+1), (x+1, y+1) };
      foreach (var move in moves)
      {
        if (!this.Tiles.ContainsKey(move)) {
          x = move.Item1;
          y = move.Item2;
          moved = true;
          break;
        }
      }
      if (!moved)
      {
        this.Tiles[(x, y)] = new Tile(x, y, "o");
        moving = false;
      }
    }

    return y == yMax || (x, y) == this.Origin;
  }
}
