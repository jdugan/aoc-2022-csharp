namespace Aoc.Day16;

public class Scenario
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Scenario (List<Actor> actors)
  {
    Actors       = this.CopyActors(actors);
    Flow         = 0;
    TotalRelief  = 0;
    Visited      = new List<string>();
  }
  public List<Actor>   Actors      { get; set; }
  public int           Flow        { get; set; }
  public int           TotalRelief { get; set; }
  public List<string>  Visited     { get; set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  public void Accumulate ()
  {
    this.TotalRelief += this.Flow;
  }

  public Scenario Copy ()
  {
    var copy         = new Scenario(this.Actors);
    copy.Flow        = this.Flow;
    copy.TotalRelief = this.TotalRelief;
    copy.Visited     = new List<string>(this.Visited);

    return copy;
  }


  // ========== ATTRIBUTES ================================

  public string Key ()
  {
    var visited = this.Visited;
    visited.Sort();

    return String.Join(",", visited);
  }

  // What if I'm targetless but about to turn something on?
  public int PotentialFlow(Dictionary<string, Valve> valves, int ticksRemaining)
  {
    int flow = 0;
    foreach (string s in this.Targets())
    {
      flow += valves[s].Flow * ticksRemaining;
    }
    return flow;
  }

  public List<string> Targets ()
  {
    return this.Actors.
              Select(a => a.Target).
              Where(t => t != "").
              ToList();
  }

  public int TargetedSteps ()
  {
    return this.Actors.
              Select(a => a.Path.Count).
              Sum();
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  private List<Actor> CopyActors (List<Actor> otherActors)
  {
    var actors = new List<Actor>();
    foreach (var a in otherActors)
    {
      actors.Add(a.Copy());
    }
    return actors;
  }
}
