namespace Aoc.Day19;

public class Simulation
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Simulation (Dictionary<string, (int, int, int)> costs, Dictionary<string, int> targets)
  {
    Costs   = costs;
    Targets = targets;
  }
  public Dictionary<string, (int, int, int)> Costs   { get; private set; }
  public Dictionary<string, int>             Targets { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public int CalculateMaxGeodes(int minutes, int best) {
    var perms = this.GetTargetPermutations();
    foreach (var robotTypes in perms)
    {
      // Console.WriteLine("-------------------------------");
      // Console.WriteLine(String.Join(",", robotTypes));
      // Console.WriteLine("");

      // set up case
      var materials      = new Dictionary<string, int>{ {"ore",0}, {"clay",0}, {"obsidian",0}, {"geode",0} };
      var robots         = new Dictionary<string, int>{ {"ore",1}, {"clay",0}, {"obsidian",0}, {"geode",0} };
      var nextRobotType  = robotTypes.Dequeue();
      var nextRobotCosts = this.Costs[nextRobotType];

      // process time loop
      for (int t = 1; t <= minutes; t++)
      {
        // header
        // Console.WriteLine("== Minute {0} ==", t);

        // STEP 1: purchase robot, if possible
        bool purchased = false;
        if (nextRobotCosts.Item1 <= materials["ore"] && nextRobotCosts.Item2 <= materials["clay"] && nextRobotCosts.Item3 <= materials["obsidian"])
        {
          // Console.WriteLine(
          //   "Spend {0} ore, {1} clay, and {2} obsidian to start building a {3}-collecting robot.",
          //   nextRobotCosts.Item1,
          //   nextRobotCosts.Item2,
          //   nextRobotCosts.Item3,
          //   nextRobotType
          // );
          materials["ore"]      -= nextRobotCosts.Item1;
          materials["clay"]     -= nextRobotCosts.Item2;
          materials["obsidian"] -= nextRobotCosts.Item3;
          purchased              = true;
        }

        // STEP 2: collect samples from existing robots
        foreach (var kvp in robots)
        {
          materials[kvp.Key] += kvp.Value;
          // Console.WriteLine("{0} {1}-collecting robots collect {0} {1}; you now have {2} {1}.", kvp.Value, kvp.Key, materials[kvp.Key]);
        }

        // STEP 3: if purchase made, display readiness
        if (purchased)
        {
          robots[nextRobotType] += 1;
          // Console.WriteLine("The new {0}-collecting robot is ready; you now have {1} of them.", nextRobotType, robots[nextRobotType]);
          if (robotTypes.Count > 0) {
            nextRobotType  = robotTypes.Dequeue();
            nextRobotCosts = this.Costs[nextRobotType];
          }
          else {
            nextRobotType  = "geode";
            nextRobotCosts = this.Costs[nextRobotType];
          }
        }

        // footer
        // Console.WriteLine("");
      }

      if (materials["geode"] > best)
      {
        best = materials["geode"];
      }
    }
    return best;
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== TARGETS ===================================

  private List<Queue<string>> GetTargetPermutations ()
  {
    var targets = new List<string>();
    foreach (var kvp in this.Targets)
    {
      for (int i = 0; i < kvp.Value; i++) {
        targets.Add(kvp.Key);
      }
    }

    var temp  = this.Permutate(targets);
    var hash  = new HashSet<string>();
    var perms = new List<Queue<string>>();
    foreach (var t in temp)
    {
      var q = new Queue<string>(t);
      while (q.Peek() == "obsidian")
      {
        q.Dequeue();
      }
      if (hash.Add(String.Join(",", q)))
      {
        perms.Add(q);
      }
    }
    return perms;
  }

  private List<List<string>> Permutate (List<string> targets)
  {
    var results = new List<List<string>>();
    if (targets.Count == 1) {
      results.Add(targets.GetRange(0, 1));
      return results;
    }
    foreach (var (i, v) in targets.Select((v, i) => (i, v)))
    {
      var others = new List<string>(targets);
      others.Remove(v);
      var perms  = this.Permutate(others);
      if (perms.Count == 0)
      {
        var p1 = new List<string>();
        p1.Add(v);
        results.Add(p1);
      }
      else
      {
        foreach (var p in perms)
        {
          var p1 = new List<string>();
          p1.Add(v);
          p1.AddRange(p);
          results.Add(p1);
        }
      }
    }
    return results;
  }
}
