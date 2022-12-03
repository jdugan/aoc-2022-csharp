using System.Reflection;
using System.Text.RegularExpressions;

﻿namespace Aoc.Utility;

public class Reader
{
  public static List<int> ToIntegers(string path)
  {
    List<string> lines = Reader.ToStrings(path);
    return lines.Select(s => Int32.Parse(s)).ToList();
  }

  public static List<string> ToStrings(string path)
  {
    List<string> lines = Reader.GetLines(path);
    return lines.Where(s => s != "").ToList();
  }

  public static List<string> GetLines(string path)
  {
    // convert relative path to full path
    Regex  re    = new Regex(@"app|test");
    string dir   = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    int    index = re.Matches(dir)[0].Groups[0].Captures[0].Index;
    string fpath = Path.Join(dir.Substring(0, index), path);

    // read file and read lines
    string[] lines = System.IO.File.ReadAllLines(fpath);
    return lines.Select(s => s.Trim()).ToList();
  }
}
