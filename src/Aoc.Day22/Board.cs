using System.Numerics;

namespace Aoc.Day22;

public class Board
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Board (Dictionary<(int, int), Tile> tiles, string type)
  {
    Tiles = tiles;
    Type  = type;

    ColumnLimits = this.BuildColumnLimits();
    RowLimits    = this.BuildRowLimits();
  }
  public Dictionary<int, (int, int)>  ColumnLimits { get; private set; }
  public Dictionary<int, (int, int)>  RowLimits    { get; private set; }
  public Dictionary<(int, int), Tile> Tiles        { get; private set; }
  public string                       Type         { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ATTRIBUTES ================================

  public (int, int) OriginId ()
  {
    int y = 1;
    int x = this.RowLimits[y].Item1;

    return (x, y);
  }

  public ((int, int), string) WrapAroundState (int x, int y, string direction)
  {
    // get relative info
    var relLength = this.SideLength();
    var relSide   = this.GetRelativeSide(x, y);
    var relEdge   = this.GetRelativeEdge(direction);
    var relCoord  = this.GetRelativeCoord(x, y, relEdge);

    // get next info
    var nextTuple     = (this.Type == "2D") ?
                          this.GetWrapAround2dTuple((relLength, relSide, relEdge)) :
                          this.GetWrapAround3dTuple((relLength, relSide, relEdge));
    var nextSide      = nextTuple.Item1;
    var nextEdge      = nextTuple.Item2;
    var nextDirection = nextTuple.Item3;
    var nextInverted  = nextTuple.Item4;
    var nextCoords    = this.GetWrapAroundCoords(relCoord, nextSide, nextEdge, nextInverted);

    // return info
    return (nextCoords, nextDirection);
  }


  // ========== DISPLAY ===================================

  public void Print (Sprite sprite)
  {
    var xMax = this.ColumnLimits.Keys.ToList().Max();
    var yMax = this.RowLimits.Keys.ToList().Max();

    Console.WriteLine("");
    for (int y = 1; y <= yMax; y++)
    {
      var row = new List<string>();
      for (int x = 1; x <= xMax; x++)
      {
        var id = (x, y);
        if (sprite.History.ContainsKey(id))
        {
          row.Add(sprite.History[id]);
        }
        else if (this.Tiles.ContainsKey(id))
        {
          row.Add(this.Tiles[id].Display);
        }
        else {
          row.Add(" ");
        }
      }
      Console.WriteLine(String.Join("", row));
    }
    Console.WriteLine("");
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== ATTRIBUTES ================================

  private int SideLength ()
  {
    var xMax = this.ColumnLimits.Keys.ToList().Max();
    var yMax = this.RowLimits.Keys.ToList().Max();

    return (int)BigInteger.GreatestCommonDivisor((BigInteger)xMax, (BigInteger)yMax);
  }


  // ========== STRUCTS ===================================

  private Dictionary<int, (int, int)> BuildColumnLimits ()
  {
    var dict = new Dictionary<int, (int, int)>();
    foreach (var t in this.Tiles.Values)
    {
      if (dict.ContainsKey(t.X))
      {
        var tuple = dict[t.X];
        int min = (t.Y < tuple.Item1) ? t.Y : tuple.Item1;
        int max = (t.Y > tuple.Item2) ? t.Y : tuple.Item2;
        dict[t.X] = (min, max);
      }
      else {
        dict[t.X] = (t.Y, t.Y);
      }
    }
    return dict;
  }

  private Dictionary<int, (int, int)> BuildRowLimits ()
  {
    var dict = new Dictionary<int, (int, int)>();
    foreach (var t in this.Tiles.Values)
    {
      if (dict.ContainsKey(t.Y))
      {
        var tuple = dict[t.Y];
        int min = (t.X < tuple.Item1) ? t.X : tuple.Item1;
        int max = (t.X > tuple.Item2) ? t.X : tuple.Item2;
        dict[t.Y] = (min, max);
      }
      else {
        dict[t.Y] = (t.X, t.X);
      }
    }
    return dict;
  }


  // ========== WRAP AROUND ===============================

  private int GetRelativeCoord (int x, int y, string edge)
  {
    var len = this.SideLength();

    if (edge == "T" || edge == "B") {
      return ((x - 1) % len) + 1;
    }
    else {
      return ((y - 1) % len) + 1;
    }
  }

  private string GetRelativeEdge (string direction) => direction switch
  {
    ">" => "R",
    "v" => "B",
    "<" => "L",
    _   => "T"
  };

  private int GetRelativeSide(int x, int y)
  {
    var len = this.SideLength();
    return (((y-1)/len)*4) + ((x-1)/len) + 1;
  }

  private (int, string, string, bool) GetWrapAround2dTuple ((int, int, string) tuple) => tuple switch
  {
    (50, 2,  "T") => (10, "B", "^", false),
    (50, 2,  "L") => (3,  "R", "<", false),
    (50, 3,  "T") => (3,  "B", "^", false),
    (50, 3,  "R") => (2,  "L", ">", false),
    (50, 3,  "B") => (3,  "T", "v", false),
    (50, 6,  "L") => (6,  "R", "<", false),
    (50, 6,  "R") => (6,  "L", ">", false),
    (50, 9,  "T") => (13, "B", "^", false),
    (50, 9,  "L") => (10, "R", "<", false),
    (50, 10, "R") => (9,  "L", ">", false),
    (50, 10, "B") => (2,  "T", "v", false),
    (50, 13, "L") => (13, "R", "<", false),
    (50, 13, "R") => (13, "L", ">", false),
    (50, 13, "B") => (9,  "T", "v", false)
  };

  private (int, string, string, bool) GetWrapAround3dTuple ((int, int, string) tuple) => tuple switch
  {
    (50, 2,  "T") => (13, "L", ">", false),
    (50, 2,  "L") => (9,  "L", ">", true),
    (50, 3,  "T") => (13, "B", "^", false),
    (50, 3,  "R") => (10, "R", "<", true),
    (50, 3,  "B") => (6,  "R", "<", false),
    (50, 6,  "L") => (9,  "T", "v", false),
    (50, 6,  "R") => (3,  "B", "^", false),
    (50, 9,  "T") => (6,  "L", ">", false),
    (50, 9,  "L") => (2,  "L", ">", true),
    (50, 10, "R") => (3,  "R", "<", true),
    (50, 10, "B") => (13, "R", "<", false),
    (50, 13, "L") => (2,  "T", "v", false),
    (50, 13, "R") => (10, "B", "^", false),
    (50, 13, "B") => (3,  "T", "v", false)
  };

  private (int, int) GetWrapAroundCoords (int relCoord, int side, string edge, bool inverted)
  {
    int xNext;
    int yNext;
    int len = this.SideLength();

    int x0  = ((((side - 1) % 4)) * len) + 1;
    int y0  = (((side - 1)/4) * len) + 1;

    if (edge == "T" || edge == "B") {
      xNext = (inverted)    ? x0 + len - relCoord : x0 + relCoord - 1;
      yNext = (edge == "B") ? y0 + len - 1        : y0;
    }
    else {
      xNext = (edge == "R") ? x0 + len - 1        : x0;
      yNext = (inverted)    ? y0 + len - relCoord : y0 + relCoord - 1;
    }

    return (xNext, yNext);
  }
}
