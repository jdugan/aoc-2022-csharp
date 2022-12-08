using System.Text.RegularExpressions;

namespace Aoc.Day08;

public class Point
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Point(int x, int y)
  {
    X = x;
    Y = y;
  }
  public int X { get; private set; }
  public int Y { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== IDS =======================================

  public Point FromId (string id)
  {
    var re = new Regex(@"^(\d+),(\d+)$");
    var m  = re.Match(id);
    var x  = Int32.Parse(m.Groups[1].Value);
    var y  = Int32.Parse(m.Groups[2].Value);

    return new Point(x, y);
  }

  public string Id ()
  {
    return $"{X},{Y}";
  }

  // ========== NEIGHBORS =================================

  public string EastId ()
  {
    return $"{X+1},{Y}";
  }

  public string NorthId ()
  {
    return $"{X},{Y-1}";
  }

  public string SouthId ()
  {
    return $"{X},{Y+1}";
  }

  public string WestId ()
  {
    return $"{X-1},{Y}";
  }
}
