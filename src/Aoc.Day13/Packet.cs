using System.Text.RegularExpressions;

namespace Aoc.Day13;

public class Packet : IComparable
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Packet (int id, string raw)
  {
    Id     = id;
    Raw    = raw;
    Parsed = this.Parse(raw);
  }
  public int          Id     { get; private set; }
  public List<object> Parsed { get; set; }
  public string       Raw    { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== COMPARISON ================================

  public int CompareTo (object obj)
  {
    // assign working values
    Packet other     = (Packet)obj;
    var    leftList  = this.Parsed;
    var    rightList = other.Parsed;

    // loop left items to minimum range
    int minRange = new List<int> { leftList.Count, rightList.Count }.Min();
    foreach (var i in Enumerable.Range(0, minRange))
    {
      // grab items and check types
      var  left       = leftList[i];
      var  right      = rightList[i];
      bool isLeftInt  = left.GetType()  == typeof(Int32);
      bool isRightInt = right.GetType() == typeof(Int32);

      // VERIFY => two integers
      if (isLeftInt && isRightInt)
      {
        if ((int)left < (int)right) {
          return -1;
        }
        else if ((int)left > (int)right)
        {
          return 1;
        }
      }
      else
      {
        // VERIFY => corece to two lists, then packets
        List<object> c1;
        List<object> c2;
        if (isLeftInt)
        {
          c1 = new List<object> { left };
          c2 = (List<object>)right;
        }
        else if (isRightInt)
        {
          c2 = new List<object> { right };
          c1 = (List<object>)left;
        }
        else {
          c1 = (List<object>)left;
          c2 = (List<object>)right;
        }

        var p1  = new Packet(1, "[1]");
        p1.Parsed = c1;
        var p2  = new Packet(2, "[2]");
        p2.Parsed = c2;

        var val = p1.CompareTo(p2);
        if (val != 0)
        {
          return val;
        }
      }
    }
    if (leftList.Count > rightList.Count)
    {
      return 1;
    }
    else if (leftList.Count < rightList.Count)
    {
      return -1;
    }
    else
    {
      return 0;
    }
  }


  // ========== DISPLAY ===================================

  public override string ToString ()
  {
    return $"{this.Raw}";
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== PARSING ===================================

  private (string, string) ExtractInt (string input)
  {
    var re     = new Regex(@"^(\d+)");
    var m      = re.Match(input);
    var length = m.Groups[0].Captures[0].Length;

    var strItem = input.Substring(0, length);
    var strRem  = input.Substring(length);

    return (strItem, strRem);
  }

  private (string, string) ExtractList (string input)
  {
    var openCount  = 0;
    var closeCount = 0;
    var length     = 0;
    foreach (var c in input.ToCharArray())
    {
      length += 1;
      switch (c.ToString())
      {
        case "[":
          openCount += 1;
          break;
        case "]":
          closeCount += 1;
          break;
      }
      if (openCount > 0 && openCount == closeCount)
      {
        break;
      }
    }

    var strItem = input.Substring(0, length);
    var strRem  = input.Substring(length);

    return (strItem, strRem);
  }

  private List<object> Parse (string input)
  {
    var ignore = new List<string> { ",", "]" };
    var list = new List<object>();
    var str  = input.Substring(1);
    while (str.Length > 0)
    {
      if (str[0].ToString() == "[")
      {
        var (strItem, strRemainder) = this.ExtractList(str);
        var item  = this.Parse(strItem);
        list.Add(item);
        str = strRemainder;
      }
      else if (ignore.Contains(str[0].ToString()))
      {
        str = str.Substring(1);
      }
      else
      {
        var (strItem, strRemainder) = this.ExtractInt(str);
        var item = Int32.Parse(strItem);
        list.Add(item);
        str = strRemainder;
      }
    }
    return list;
  }
}