using System.Numerics;

namespace Aoc.Day11;

public class Monkey
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Monkey(int id, string operation, List<int> startingItems, int throwDivisor, int throwTrueTo, int throwFalseTo)
  {
    Id            = id;
    Operation     = operation;
    StartingItems = startingItems;
    ThrowDivisor  = throwDivisor;
    ThrowFalseTo  = throwFalseTo;
    ThrowTrueTo   = throwTrueTo;
    Inspections   = 0;
  }
  public int       Id            { get; private set; }
  public int       Inspections   { get; set; }
  public string    Operation     { get; private set; }
  public List<int> StartingItems { get; private set; }
  public int       ThrowDivisor  { get; private set; }
  public int       ThrowFalseTo  { get; private set; }
  public int       ThrowTrueTo   { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

}
