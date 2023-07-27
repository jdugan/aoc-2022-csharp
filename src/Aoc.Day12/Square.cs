namespace Aoc.Day12;

public class Square
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Square (int x, int y, char symbol)
  {
    Id        = this.ToId(x, y);
    X         = x;
    Y         = y;
    Symbol    = symbol;
    Elevation = this.ElevationFromSymbol(symbol);
  }
  public (int, int) Id        { get; private set; }
  public int        Elevation { get; private set; }
  public uint       GraphId   { get; set; }
  public char       Symbol    { get; private set; }
  public int        X         { get; private set; }
  public int        Y         { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== IDS =======================================

  public List<(int, int)> GetNeighborIds ()
  {
    var nid = this.ToId(this.X, this.Y - 1);
    var sid = this.ToId(this.X, this.Y + 1);
    var eid = this.ToId(this.X + 1, this.Y);
    var wid = this.ToId(this.X - 1, this.Y);

    return new List<(int, int)> { nid, sid, eid, wid };
  }

  public (int, int) ToId (int x, int y)
  {
    return (x, y);
  }


  // ========== DISPLAY ===================================

  public void AddToPath ()
  {
    if (this.Symbol.ToString() != "S" && this.Symbol.ToString() != "E")
    {
      this.Symbol = ".".ToCharArray()[0];
    }
    // this.Symbol = this.Symbol.ToString().ToUpper().ToCharArray()[0];
  }


  // ========== OVERRIDES =================================

  public override string ToString ()
  {
    return $"{this.GraphId} {this.Id} => {this.Symbol}, {this.Elevation}";
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private int ElevationFromSymbol (char symbol)
  {
    switch (symbol.ToString())
    {
      case "S":
        return 1;
      case "E":
        return 26;
      default:
        int ascii = Convert.ToInt32(symbol);
        return ascii - 96;
    }
  }
}