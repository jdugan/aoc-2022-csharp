using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day07;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //--------------------------------------------------------

  public int Day()
  {
    return 7;
  }

  public int Puzzle1()
  {
    var root = this.FileSystem();

    return root.AggregateSmallFolderSize(0, 100000);
  }

  public int Puzzle2()
  {
    var root  = this.FileSystem();
    var total = 70000000;
    var free  = total - root.Size;
    var goal  = 30000000 - free;

    return root.FindBestCandidateSize(total, goal);
  }

  //--------------------------------------------------------
  // Private Methods
  //--------------------------------------------------------

  // ========== DATA =======================================

  private List<string> Data()
  {
    List<string> lines = Reader.ToStrings("data/day07/input.txt");
    return lines;
  }

  private Folder FileSystem()
  {
    // prep input
    var lines = this.Data();
    lines.RemoveAt(0);

    // build root directory
    Folder? dummy = null;
    var root      = new Folder(dummy, "/", 0);
    Folder parent = root;

    // set parsing regex
    var re_cd   = new Regex(@"^\$ cd (.+)$");
    var re_dir  = new Regex(@"^dir (.+)$");
    var re_file = new Regex(@"^(\d+) (.+)$");

    // parse instructions
    foreach (string line in lines)
    {
      Match m;

      // directory
      m = re_dir.Match(line);
      if (m.Success)
      {
        var name   = m.Groups[1].Value;
        var folder = new Folder(parent, name, 0);
        parent.AddFolder(folder);
        continue;
      }

      // file
      m = re_file.Match(line);
      if (m.Success)
      {
        var name = m.Groups[2].Value;
        var size = Int32.Parse(m.Groups[1].Value);
        var item = new Item(parent, name, size);
        parent.AddItem(item);
        continue;
      }

      // move
      m = re_cd.Match(line);
      if (m.Success)
      {
        var name = m.Groups[1].Value;
        if (name == "..")
        {
          parent = parent.Parent;
        }
        else
        {
          parent = parent.GetFolderByName(name);
        }
        continue;
      }
    }

    // set folder sizes
    root.SetSize();

    return root;
  }
}
