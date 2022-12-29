namespace Aoc.Day24;

public class Snapshot
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Snapshot (int id, Dictionary<(int, int, int), Point> points)
  {
    Id     = id;
    Points = points;

    ColumnLimits = new Dictionary<int, (int, int)>();
    RowLimits    = new Dictionary<int, (int, int)>();
  }
  public int                                Id           { get; private set; }
  public Dictionary<(int, int, int), Point> Points       { get; private set; }
  public Dictionary<int, (int, int)>        ColumnLimits { get; set; }
  public Dictionary<int, (int, int)>        RowLimits    { get; set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  public Snapshot GenerateNext ()
  {
    // copy all points to a new dict (without winds)
    var points = new Dictionary<(int, int, int), Point>();
    foreach (var p in this.Points.Values)
    {
      var c = p.Copy();
      points[c.Id] = c;
    }

    // make a snapshot with a new id and the new points
    var snapshot          = new Snapshot(this.Id + 1, points);
    snapshot.ColumnLimits = this.ColumnLimits;
    snapshot.RowLimits    = this.RowLimits;

    // add winds in new locations
    foreach (var p in this.Points.Values)
    {
      foreach (var w in p.Winds)
      {
        var nextId = p.NextIdForWind(w);
        if (!snapshot.Points.ContainsKey(nextId)) {
          switch (w)
          {
            case ">":
              nextId = (this.RowLimits[p.Y].Item1, p.Y, nextId.Item3);
              break;
            case "^":
              nextId = (p.X, this.ColumnLimits[p.X].Item2, nextId.Item3);
              break;
            case "v":
              nextId = (p.X, this.ColumnLimits[p.X].Item1, nextId.Item3);
              break;
            default:
              nextId = (this.RowLimits[p.Y].Item2, p.Y, nextId.Item3);
              break;
          }
        }
        snapshot.Points[nextId].Winds.Add(w);
      }
    }

    // return the new version
    return snapshot;
  }

  public Snapshot Invert (int z)
  {
    // get max dimensions
    int xMax = this.GetMaxX();
    int yMax = this.GetMaxY();

    // copy all points to a new dict with inverted
    // coords and winds
    var points = new Dictionary<(int, int, int), Point>();
    foreach (var p in this.Points.Values)
    {
      var c = new Point(xMax - p.X, yMax - p.Y, z);
      foreach (var w in p.Winds)
      {
        c.Winds.Add(p.InvertWind(w));
      }
      points[c.Id] = c;
    }

    // build new object and set limits
    var snapshot          = new Snapshot(z, points);
    snapshot.ColumnLimits = snapshot.BuildColumnLimits();
    snapshot.RowLimits    = snapshot.BuildRowLimits();

    return snapshot;
  }


  // ========== ATTRIBUTES ================================

  public Dictionary<int, (int, int)> BuildColumnLimits ()
  {
    var dict = new Dictionary<int, (int, int)>();
    foreach (var p in this.Points.Values)
    {
      if (dict.ContainsKey(p.X))
      {
        var tuple = dict[p.X];
        int min = (p.Y < tuple.Item1) ? p.Y : tuple.Item1;
        int max = (p.Y > tuple.Item2) ? p.Y : tuple.Item2;
        dict[p.X] = (min, max);
      }
      else {
        dict[p.X] = (p.Y, p.Y);
      }
    }
    return dict;
  }

  public Dictionary<int, (int, int)> BuildRowLimits ()
  {
    var dict = new Dictionary<int, (int, int)>();
    foreach (var p in this.Points.Values)
    {
      if (dict.ContainsKey(p.Y))
      {
        var tuple = dict[p.Y];
        int min = (p.X < tuple.Item1) ? p.X : tuple.Item1;
        int max = (p.X > tuple.Item2) ? p.X : tuple.Item2;
        dict[p.Y] = (min, max);
      }
      else {
        dict[p.Y] = (p.X, p.X);
      }
    }
    return dict;
  }

  public Point GetDestination ()
  {
    return this.Points.Values.ToList().Find(p => p.Y == this.GetMaxY());
  }

  public Point GetOrigin ()
  {
    return this.Points.Values.ToList().Find(p => p.Y == this.GetMinY());
  }




  // ========== DISPLAY ===================================

  public void Print ()
  {
    Console.WriteLine("{0}:", this.Id);
    for (int y = this.GetMinY(); y <= this.GetMaxY(); y++)
    {
      var row = new List<string>();
      for (int x = this.GetMinX() - 1; x <= this.GetMaxX() + 1; x++)
      {
        var key = (x, y, this.Id);
        if (this.Points.ContainsKey(key))
        {
          row.Add(this.Points[key].Print());
        }
        else
        {
          row.Add("#");
        }
      }
      Console.WriteLine(String.Join("", row));
    }
    Console.WriteLine("");
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private int GetMaxX ()
  {
    return this.Points.Keys.Select(k => k.Item1).Max();
  }

  private int GetMinX ()
  {
    return this.Points.Keys.Select(k => k.Item1).Min();
  }

  private int GetMaxY ()
  {
    return this.Points.Keys.Select(k => k.Item2).Max();
  }

  private int GetMinY ()
  {
    return this.Points.Keys.Select(k => k.Item2).Min();
  }
}
