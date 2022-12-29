namespace Aoc.Day24;

public class Point
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Point (int x, int y, int z)
  {
    Id      = (x, y, z);
    X       = x;
    Y       = y;
    Z       = z;
    GraphId = 0;
    Winds   = new List<string>();
  }
  public (int, int, int)  Id      { get; private set; }
  public uint             GraphId { get; set; }
  public List<string>     Winds   { get; private set; }
  public int              X       { get; private set; }
  public int              Y       { get; private set; }
  public int              Z       { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  public Point Copy ()
  {
    return new Point(this.X, this.Y, this.Z + 1);
  }

  public string Print ()
  {
    if (this.Winds.Count == 0) {
      return ".";
    }
    else if (this.Winds.Count == 1) {
      return this.Winds[0];
    }
    else {
      return this.Winds.Count.ToString();
    }
  }

  public string InvertWind(string wind) => wind switch
  {
    ">" => "<",
    "^" => "v",
    "v" => "^",
    _   => ">"
  };


  // ========== ATTRIBUTES (CALCULATED IDS) ===============

  public List<(int, int, int)> NeighborIds ()
  {
    var cid = this.CenterId();
    var eid = this.EastId();
    var nid = this.NorthId();
    var sid = this.SouthId();
    var wid = this.WestId();
    return new List<(int, int, int)>{ cid, eid, nid, sid, wid };
  }

  public (int, int, int) NextIdForWind(string wind) => wind switch
  {
    ">" => this.EastId(),
    "^" => this.NorthId(),
    "v" => this.SouthId(),
    _   => this.WestId()
  };


  // ========== ATTRIBUTES (CARDINAL IDS) =================

  public (int, int, int) CenterId ()
  {
    return (this.X, this.Y, this.Z + 1);
  }

  public (int, int, int) EastId ()
  {
    return (this.X+1, this.Y, this.Z + 1);
  }

  public (int, int, int) NorthId ()
  {
    return (this.X, this.Y-1, this.Z + 1);
  }

  public (int, int, int) SouthId ()
  {
    return (this.X, this.Y+1, this.Z + 1);
  }

  public (int, int, int) WestId ()
  {
    return (this.X-1, this.Y, this.Z + 1);
  }

  // ========== STATE =====================================

  public bool IsOpen ()
  {
    return this.Winds.Count == 0;
  }
}
