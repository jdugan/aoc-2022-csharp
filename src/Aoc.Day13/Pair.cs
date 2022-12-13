using System.Text.RegularExpressions;

namespace Aoc.Day13;

public class Pair
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Pair (int id, Packet left, Packet right)
  {
    Id        = id;
    Left      = left;
    Right     = right;
  }
  public int    Id    { get; private set; }
  public Packet Left  { get; private set; }
  public Packet Right { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== COMPARISON ================================

  public bool InOrder ()
  {
    return this.Left.CompareTo(this.Right) == -1;
  }


  // ========== DISPLAY ===================================

  public void Print ()
  {
    Console.WriteLine(this.Left);
    Console.WriteLine(this.Right);
    Console.WriteLine(" => {0}", this.InOrder());
  }

  public override string ToString ()
  {
    return $"{this.Left} <=> {this.Right}";
  }
}
