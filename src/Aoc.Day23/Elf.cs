namespace Aoc.Day23;

public class Elf
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Elf (int x, int y)
  {
    Id      = (x, y);
    X       = x;
    Y       = y;
  }
  public (int, int) Id { get; private set; }
  public int        X  { get; private set; }
  public int        Y  { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ATTRIBUTES (DIRS) =========================

  public List<(int, int)> AdjacentIds ()
  {
    var eids = this.EastIds();
    var nids = this.NorthIds();
    var sids = this.SouthIds();
    var wids = this.WestIds();
    return eids.Concat(nids).Concat(sids).Concat(wids).Distinct().ToList();
  }

  public List<(int, int)> IdsForDirection (string direction) => direction switch
  {
    "E" => this.EastIds(),
    "N" => this.NorthIds(),
    "S" => this.SouthIds(),
    "W" => this.WestIds(),
    _   => new List<(int, int)>()
  };

  public (int, int) NextIdForDirection (string direction)
  {
    return this.IdsForDirection(direction)[1];
  }


  // ========== ATTRIBUTES (DIRECTIONS) ===================

  public List<(int, int)> EastIds ()
  {
    var ids = new List<(int, int)>();
    ids.Add((this.X+1, this.Y-1));
    ids.Add((this.X+1, this.Y));
    ids.Add((this.X+1, this.Y+1));
    return ids;
  }

  public List<(int, int)> NorthIds ()
  {
    var ids = new List<(int, int)>();
    ids.Add((this.X-1, this.Y-1));
    ids.Add((this.X, this.Y-1));
    ids.Add((this.X+1, this.Y-1));
    return ids;
  }

  public List<(int, int)> SouthIds ()
  {
    var ids = new List<(int, int)>();
    ids.Add((this.X-1, this.Y+1));
    ids.Add((this.X, this.Y+1));
    ids.Add((this.X+1, this.Y+1));
    return ids;
  }

  public List<(int, int)> WestIds ()
  {
    var ids = new List<(int, int)>();
    ids.Add((this.X-1, this.Y-1));
    ids.Add((this.X-1, this.Y));
    ids.Add((this.X-1, this.Y+1));
    return ids;
  }

  // ========== ACTIONS ===================================

  public Elf Move ((int, int) coord)
  {
    this.Id = coord;
    this.X  = coord.Item1;
    this.Y  = coord.Item2;

    return this;
  }
}
