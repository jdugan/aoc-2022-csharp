namespace Aoc.Day19;

public class Simulation : IComparable
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Simulation (List<Robot> costs, Dictionary<string, int> materials, Dictionary<string, int> workers)
  {
    Costs     = costs;
    Materials = materials;
    Workers   = workers;
  }
  public List<Robot>             Costs     { get; }
  public Dictionary<string, int> Materials { get; }
  public Dictionary<string, int> Workers   { get; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== CALCULATIONS ==============================

  public List<Simulation> Iterate() {
    var simulations = new List<Simulation>();

    // make one where we buy nothing
    var materials = this.Materials.ToDictionary(entry => entry.Key, entry => entry.Value);
    var workers   = this.Workers.ToDictionary(entry => entry.Key, entry => entry.Value);
    materials["ore"]      += workers["ore"];
    materials["clay"]     += workers["clay"];
    materials["obsidian"] += workers["obsidian"];
    materials["geode"]    += workers["geode"];
    simulations.Add(new Simulation(this.Costs, materials, workers));

    // and, make one for each robot we can buy
    foreach (var pr in this.PurchasableRobots())
    {
      var _materials = this.Materials.ToDictionary(entry => entry.Key, entry => entry.Value);
      var _workers   = this.Workers.ToDictionary(entry => entry.Key, entry => entry.Value);
      _materials["ore"]      += _workers["ore"];
      _materials["clay"]     += _workers["clay"];
      _materials["obsidian"] += _workers["obsidian"];
      _materials["geode"]    += _workers["geode"];
      _materials["ore"]      -= pr.OreCost;
      _materials["clay"]     -= pr.ClayCost;
      _materials["obsidian"] -= pr.ObsidianCost;
      _workers[pr.Type]      += 1;
      simulations.Add(new Simulation(this.Costs, _materials, _workers));
    }

    return simulations;
  }


  // ========== SORTING ===================================

  public int CompareTo (Object obj)
  {
    Simulation other = (Simulation)obj;
    var leftIndex  = this.SortIndex();
    var rightIndex = other.SortIndex();

    for (int i=0; i < leftIndex.Count; i++)
    {
      if (leftIndex[i] < rightIndex[i])
      {
        return 1;
      }
      else if (leftIndex[i] > rightIndex[i])
      {
        return -1;
      }
    }
    return 0;
  }

  public List<int> SortIndex () {
    var mge = this.Materials["geode"];
    var mob = this.Materials["obsidian"];
    var mcl = this.Materials["clay"];
    var mor = this.Materials["ore"];
    var wge = this.Workers["geode"];
    var wob = this.Workers["obsidian"];
    var wcl = this.Workers["clay"];
    var wor = this.Workers["ore"];

    var factors = new List<int>();
    factors.Add(mge + wge);
    factors.Add(mob + wob);
    factors.Add(mcl + wcl);
    factors.Add(mor + wor);
    factors.Add(wge);
    factors.Add(wob);
    factors.Add(wcl);
    factors.Add(wor);
    return factors;
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== COSTS =====================================

  public List<Robot> PurchasableRobots() {
    var robots = new List<Robot>();
    foreach (var c in this.Costs)
    {
      if (c.OreCost <= this.Materials["ore"] && c.ClayCost <= this.Materials["clay"] && c.ObsidianCost <= this.Materials["obsidian"])
      {
        robots.Add(c);
      }
    }
    return robots;
  }
}