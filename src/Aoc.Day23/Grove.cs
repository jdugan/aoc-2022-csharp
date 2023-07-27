namespace Aoc.Day23;

public class Grove
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Grove (Dictionary<(int, int), Elf> elves, List<string> directions)
  {
    Elves      = elves;
    Directions = directions;
  }
  public List<string>                Directions { get; private set; }
  public Dictionary<(int, int), Elf> Elves      { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ATTRIBUTES ================================

  public int OpenSpaces ()
  {
    var area  = (this.GetMaxX() - this.GetMinX() + 1) * (this.GetMaxY() - this.GetMinY() + 1);
    var elves = this.Elves.Count;

    return area - elves;
  }


  // ========== ACTIONS ===================================

  public int PerformTurn ()
  {
    // moves
    var moves = new Dictionary<(int, int), List<(int, int)>>();

    // identify movers and moves
    foreach (var elf in this.Elves.Values)
    {
      var allOpen = elf.AdjacentIds().TrueForAll(aid => !this.Elves.ContainsKey(aid));
      if (!allOpen)
      {
        foreach (var d in this.Directions)
        {
          bool canMove = elf.IdsForDirection(d).
                              TrueForAll(id => !this.Elves.ContainsKey(id));
          if (canMove)
          {
            var nextId = elf.NextIdForDirection(d);
            if (!moves.ContainsKey(nextId))
            {
              moves[nextId] = new List<(int, int)>();
            }
            moves[nextId].Add(elf.Id);
            break;
          }
        }
      }
    }

    // move unique movers
    int movers = 0;
    foreach (var kvp in moves)
    {
      if (kvp.Value.Count == 1)
      {
        var oldLoc = kvp.Value[0];
        var newLoc = kvp.Key;

        var elf = this.Elves[oldLoc];
        elf.Move(newLoc);

        this.Elves.Remove(oldLoc);
        this.Elves[newLoc] = elf;

        movers += 1;
      }
    }

    // rotate directions
    var dir = this.Directions[0];
    this.Directions.Remove(dir);
    this.Directions.Add(dir);

    // return num movers
    return movers;
  }

  public void Print ()
  {
    Console.WriteLine("");
    for (int y = this.GetMinY(); y <= this.GetMaxY(); y++)
    {
      var row = new List<string>();
      for (int x = this.GetMinX(); x <= this.GetMaxX(); x++)
      {
        if (this.Elves.ContainsKey((x, y)))
        {
          row.Add("#");
        }
        else
        {
          row.Add(".");
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

  private int GetMaxX ()
  {
    return this.Elves.Keys.Select(t => t.Item1).Max();
  }

  private int GetMinX ()
  {
    return this.Elves.Keys.Select(t => t.Item1).Min();
  }

  private int GetMaxY ()
  {
    return this.Elves.Keys.Select(t => t.Item2).Max();
  }

  private int GetMinY ()
  {
    return this.Elves.Keys.Select(t => t.Item2).Min();
  }
}