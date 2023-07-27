using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day22;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 22;
  }

  public int Puzzle1()
  {
    var cmds   = this.BuildCommands();
    var board  = this.BuildBoard("2D");
    var sprite = new Sprite(board.OriginId(), ">");

    foreach ((string direction, int steps) in cmds)
    {
      sprite.Turn(direction);
      sprite.Move(board, steps);
    }

    return sprite.CalculateScore();
  }

  public int Puzzle2()
  {
    var cmds   = this.BuildCommands();
    var board  = this.BuildBoard("3D");
    var sprite = new Sprite(board.OriginId(), ">");

    foreach ((string direction, int steps) in cmds)
    {
      sprite.Turn(direction);
      sprite.Move(board, steps);
    }

    return sprite.CalculateScore();
  }


  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== STRUCTS ===================================

  private Board BuildBoard (string type)
  {
    var lines = this.Data();
    var tiles = new Dictionary<(int, int), Tile>();
    for (int y = 0; y < lines.Count - 1; y++)
    {
      var strs = lines[y].ToCharArray().ToList().Select(c => c.ToString()).ToList();
      for (int x = 0; x < strs.Count; x++)
      {
        if (strs[x] == "." || strs[x] == "#")
        {
          var t = new Tile(x + 1, y + 1, strs[x]);
          tiles[t.Id] = t;
        }
      }
    }
    return new Board(tiles, type);
  }

  private List<(string, int)> BuildCommands ()
  {
    // find matches
    var lines = this.Data();
    var line  = lines[lines.Count - 1].Trim();
    var re    = new Regex(@"([FLR]\d+)");
    var mc    = re.Matches($"F{line}");

    // build string list
    var strs  = new List<string>();
    foreach (Match m in mc)
    {
      strs.Add(m.Groups[0].Value);
    }

    // build tuple list
    return strs.
              Select(s => (s.Substring(0, 1), Int32.Parse(s.Substring(1)))).
              ToList();
  }


  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.GetRawLines("data/day22/input.txt");
  }
}