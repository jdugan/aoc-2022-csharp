namespace Aoc.Day18;

public class Cube
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Cube (string id)
  {
    var parts = id.Split(",").Select(s => Int32.Parse(s)).ToList();

    Id      = id;
    X       = parts[0];
    Y       = parts[1];
    Z       = parts[2];
    GraphId = 0;
  }
  public string Id      { get; private set; }
  public int    X       { get; private set; }
  public int    Y       { get; private set; }
  public int    Z       { get; private set; }
  public uint   GraphId { get; set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ATTRIBUTES ================================

  public List<string> NeighborIds ()
  {
    var x   = this.X;
    var y   = this.Y;
    var z   = this.Z;

    var ids = new List<string>();
    ids.Add($"{x+1},{y},{z}");
    ids.Add($"{x-1},{y},{z}");
    ids.Add($"{x},{y+1},{z}");
    ids.Add($"{x},{y-1},{z}");
    ids.Add($"{x},{y},{z+1}");
    ids.Add($"{x},{y},{z-1}");

    return ids;
  }


  // ========== STATE =====================================

  public bool IsAdjacent (Cube other)
  {
    int dx = Math.Abs(this.X - other.X);
    int dy = Math.Abs(this.Y - other.Y);
    int dz = Math.Abs(this.Z - other.Z);

    return ((dx == 1 && dy == 0 && dz == 0) ||
            (dx == 0 && dy == 1 && dz == 0) ||
            (dx == 0 && dy == 0 && dz == 1));
  }
}