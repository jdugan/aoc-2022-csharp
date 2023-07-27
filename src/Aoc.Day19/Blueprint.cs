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

  public int MaxGeodes (int minutes, int take)
  {
    var costs       = this.Robots;
    var materials   = new Dictionary<string, int>{ {"ore",0}, {"clay",0}, {"obsidian",0}, {"geode",0} };
    var workers     = new Dictionary<string, int>{ {"ore",1}, {"clay",0}, {"obsidian",0}, {"geode",0} };
    var simulations = new List<Simulation>();

    simulations.Add(new Simulation(costs, materials, workers));
    for (int t = 1; t <= minutes; t++)
    {
      var _simulations = new List<Simulation>();
      foreach (var s in simulations)
      {
        _simulations.AddRange(s.Iterate());
      }
      _simulations.Sort();
      simulations = _simulations.Take(take).ToList();
    }

    return simulations[0].Materials["geode"];
  }

  public int QualityLevel (int minutes, int take)
  {
    return this.Id * this.MaxGeodes(minutes, take);
  }
}