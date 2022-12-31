namespace Aoc.Day16;

public class Valve : IComparable
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Valve (string id, int flow, List<string> neighborIds)
  {
    Id          = id;
    GraphId     = 0;
    Flow        = flow;
    NeighborIds = neighborIds;
  }
  public string       Id          { get; private set; }
  public int          Flow        { get; private set; }
  public uint         GraphId     { get; set; }
  public List<string> NeighborIds { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== SORTING ===================================
  
  public int CompareTo (object? obj)
  {
    Valve other = (Valve)obj;

    return this.Id.CompareTo(other.Id);
  }
}
