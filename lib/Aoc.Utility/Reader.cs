using System.Reflection;
using System.Text.RegularExpressions;

﻿namespace Aoc.Utility;

public class Reader
{
  public static List<int> ToIntegers(string path)
  {
    return Reader.ToStrings(path).
              Select(s => Int32.Parse(s)).
              ToList();
  }

  public static List<string> ToStrings(string path)
  {
    return Reader.GetLines(path).
              Where(s => s != "").
              ToList();
  }

  public static List<string> GetLines(string path)
  {
    return Reader.GetRawLines(path).
              Select(s => s.Trim()).
              ToList();
  }

  public static List<string> GetRawLines(string path)
  {
    // convert relative path to full path
    Regex  re    = new Regex(@"app|test");
    string dir   = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    int    index = re.Matches(dir)[0].Groups[0].Captures[0].Index;
    string fpath = Path.Join(dir.Substring(0, index), path);

    // read file and read lines
    return System.IO.File.
              ReadAllLines(fpath).
              ToList();
  }
}