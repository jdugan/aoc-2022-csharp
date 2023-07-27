namespace Aoc.Day07;

public class Item
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Item(Folder parent, string name, int size)
  {
    Parent = parent;
    Name   = name;
    Size   = size;
  }
  public Folder Parent { get; private set; }
  public string Name   { get; private set; }
  public int    Size   { get; private set; }
}