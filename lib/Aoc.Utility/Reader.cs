using System.IO;

﻿namespace Aoc.Utility;

public class Reader
{
  public static int[] ToIntegers(string path)
  {
    string[] lines = Reader.ToLines(path);
    return lines.Select(s => Convert.ToInt32(s)).ToArray();
  }

  public static string[] ToLines(string path)
  {
    string[] lines = System.IO.File.ReadAllLines(path);
    return lines.Select(s => s.Trim()).Where(s => s != "").ToArray();
  }
}
