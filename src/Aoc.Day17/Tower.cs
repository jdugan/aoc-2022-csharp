namespace Aoc.Day17;

public class Tower
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Tower ()
  {
    Height = 0;
    Points = new Dictionary<(int, int), RockPoint>();
  }
  public int Height { get; set; }
  public Dictionary<(int, int), RockPoint> Points { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  public void AddRock (Rock rock)
  {
    foreach (var p in rock.Points)
    {
      this.Points[p.Id()] = p;
      this.AdjustHeight(p.Y);
    }
  }

  public void TruncatePoints ()
  {
    var keys = new List<(int, int)>();
    foreach (var id in this.Points.Keys)
    {
      if (id.Item2 < this.Height - 100)
      {
        keys.Add(id);
      }
    }
    foreach (var k in keys)
    {
      this.Points.Remove(k);
    }
  }

  // ========== DISPLAY ===================================

  public string CaptureRow (int y, int width)
  {
    var row = new List<string>();
    row.Add("|");
    for (int x = 0; x < width; x++) {
      if (this.Points.ContainsKey((x, y))) {
        row.Add("#");
      }
      else {
        row.Add(".");
      }
    }
    row.Add("|");
    return String.Join("", row);
  }

  public void Print (int width)
  {
    // header
    Console.WriteLine("");
    Console.WriteLine("Tower: ({0}, {1})", this.Height, this.Points.Count);
    Console.WriteLine("");

    // rows
    for (int y = (int)this.Height; y > 0; y--)
    {
      this.PrintRow(y, width);
    }

    // floor
    var floor = new List<string>();
    floor.Add("+");
    for (int i = 0; i < width; i++) {
      floor.Add("-");
    }
    floor.Add("+");
    Console.WriteLine(String.Join("", floor));
    Console.WriteLine("");
  }

  public void PrintRow (int y, int width)
  {
    var row = this.CaptureRow(y, width);
    Console.WriteLine(row);
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private void AdjustHeight (int y)
  {
    if (y > this.Height)
    {
      this.Height = y;
    }
  }
}
