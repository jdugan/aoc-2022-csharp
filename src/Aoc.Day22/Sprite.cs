namespace Aoc.Day22;

public class Sprite
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Sprite ((int, int) coords, string direction)
  {
    X         = coords.Item1;
    Y         = coords.Item2;
    Direction = direction;

    History   = this.BuildHistory();
    // Turns     = this.BuildTurns();
  }
  public int    X         { get; private set; }
  public int    Y         { get; private set; }
  public string Direction { get; private set; }
  public Dictionary<(int, int), string> History { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  public void Move (Board board, int steps)
  {
    var nextId = this.NextId();

    if (board.Tiles.ContainsKey(nextId))
    {
      this.MoveOnBoard(board, nextId, this.Direction, steps);
    }
    else
    {
      this.MoveOffBoard(board, steps);
    }
  }

  public void Turn (string direction)
  {
    switch (direction)
    {
      case "L":
        this.TurnLeft();
        break;
      case "R":
        this.TurnRight();
        break;
      default:
        // noop
        break;
    }
    this.Record();
  }


  // ========== ATTRIBUTES ================================

  public (int, int) NextId ()
  {
    switch (this.Direction)
    {
      case ">":
        return (this.X + 1, this.Y);
      case "v":
        return (this.X, this.Y + 1);
      case "<":
        return (this.X - 1, this.Y);
      default:
        return (this.X, this.Y - 1);
    }
  }


  // ========== SCORING ===================================

  public int CalculateScore ()
  {
    return this.CoordScore() + this.DirectionScore();
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  private void MoveOffBoard (Board board, int steps)
  {
    var (nextId, direction) = board.WrapAroundState(this.X, this.Y, this.Direction);

    this.MoveOnBoard(board, nextId, direction, steps);
  }

  private void MoveOnBoard (Board board, (int, int) nextId, string direction, int steps)
  {
    if (board.Tiles[nextId].IsOpen())
    {
      this.X         = nextId.Item1;
      this.Y         = nextId.Item2;
      this.Direction = direction;
      this.Record();

      if (steps > 1) {
        this.Move(board, steps - 1);
      }
    }
  }

  private void TurnLeft ()
  {
    switch (this.Direction) {
      case ">":
        this.Direction = "^";
        break;
      case "v":
        this.Direction = ">";
        break;
      case "<":
        this.Direction = "v";
        break;
      default:
        this.Direction = "<";
        break;
    }
  }

  private void TurnRight ()
  {
    switch (this.Direction) {
      case ">":
        this.Direction = "v";
        break;
      case "v":
        this.Direction = "<";
        break;
      case "<":
        this.Direction = "^";
        break;
      default:
        this.Direction = ">";
        break;
    }
  }

  // ========== HISTORY ===================================

  private void Record ()
  {
    this.History[(this.X, this.Y)] = this.Direction;
  }


  // ========== SCORING ===================================

  private int CoordScore ()
  {
    return (1000 * this.Y) + (4 * this.X);
  }

  private int DirectionScore ()
  {
    switch (this.Direction)
    {
      case ">":
        return 0;
      case "v":
        return 1;
      case "<":
        return 2;
      default:
        return 3;
    }
  }

  // ========== STRUCTS ===================================

  private Dictionary<(int, int), string> BuildHistory()
  {
    return new Dictionary<(int, int), string>();
  }
}
