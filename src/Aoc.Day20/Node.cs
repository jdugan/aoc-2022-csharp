namespace Aoc.Day20;

public class Node
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Node (int id, int value)
  {
    Id    = id;
    Value = value;
  }
  public int Id    { get; private set; }
  public int Value { get; private set; }
}
