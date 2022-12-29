namespace Aoc.Day19;

public class Robot
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Robot (string type, int ore, int clay, int obsidian)
  {
    Type         = type;
    OreCost      = ore;
    ClayCost     = clay;
    ObsidianCost = obsidian;
  }
  public string Type         { get; private set; }
  public int    OreCost      { get; private set; }
  public int    ClayCost     { get; private set; }
  public int    ObsidianCost { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------


}
