namespace Aoc.Day19;

public class Blueprint
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Blueprint (int id, List<Robot> robots)
  {
    Id     = id;
    Robots = robots;
  }
  public int         Id     { get; private set; }
  public List<Robot> Robots { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ATTRIBUTES ================================

  public int QualityLevel (int minutes)
  {
    return this.Id * this.CalculateMaxGeodes(minutes);
  }

  public Dictionary<string, (int, int, int)> RobotCosts ()
  {
    var costs = new Dictionary<string, (int, int, int)>();
    Console.WriteLine("");
    Console.WriteLine("=========================");
    foreach (var r in this.Robots)
    {
      costs[r.Type] = (r.OreCost, r.ClayCost, r.ObsidianCost);
      Console.WriteLine((r.Type, r.OreCost, r.ClayCost, r.ObsidianCost));
    }
    Console.WriteLine("=========================");
    Console.WriteLine("");
    return costs;
  }


  // ========== CALCULATIONS ==============================

  public int CalculateMaxGeodes (int minutes)
  {
    var best        = 0;
    var costs       = this.RobotCosts();
    var targetPerms = this.BuildTargetPermutations();

    foreach (var targets in targetPerms)
    {
      var simulation = new Simulation(costs, targets);
      var result     = simulation.CalculateMaxGeodes(minutes, best);

      if (result > best)
      {
        best = result;
      }
    }

    return best;
  }


  // ========== PERMUTATIONS ==============================

  private List<Dictionary<string, int>> BuildTargetPermutations ()
  {
    var perms = new List<Dictionary<string, int>>();
    for (int ore = 0; ore <= 0; ore++)
    {
      for (int clay = 1; clay <= 3; clay++)
      {
        for (int obsidian = 1; obsidian <= 2; obsidian++)
        {
          var d         = new Dictionary<string, int>();
          d["ore"]      = ore;
          d["clay"]     = clay;
          d["obsidian"] = obsidian;
          perms.Add(d);
        }
      }
    }
    return perms;
  }
}
