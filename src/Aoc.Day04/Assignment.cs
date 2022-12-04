namespace Aoc.Day04;

public class Assignment
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Assignment(int s1, int s2)
  {
    Section1 = s1;
    Section2 = s2;
  }
  public int Section1 { get; private set; }
  public int Section2 { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== COMPARISONS ===============================

  public bool Contains (Assignment other)
  {
    bool b1 = Section1 >= other.Section1 && Section1 <= other.Section2;
    bool b2 = Section2 >= other.Section1 && Section2 <= other.Section2;
    bool b3 = other.Section1 >= Section1 && other.Section1 <= Section2;
    bool b4 = other.Section2 >= Section1 && other.Section2 <= Section2;

    return (b1 && b2) || (b3 && b4);
  }

  public bool Overlaps (Assignment other)
  {
    bool b1 = Section1 >= other.Section1 && Section1 <= other.Section2;
    bool b2 = Section2 >= other.Section1 && Section2 <= other.Section2;
    bool b3 = other.Section1 >= Section1 && other.Section1 <= Section2;
    bool b4 = other.Section2 >= Section1 && other.Section2 <= Section2;

    return b1 || b2 || b3 || b4;
  }
}
