namespace Aoc.Day10;

public class Device
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Device(List<(string, int)> cmds)
  {
    Clock    = 1;
    Signal   = 1;
    Strength = 0;
    Signals  = new Dictionary<int, int>();

    this.ProcessCommands(cmds);
  }
  public int Clock    { get; private set; }
  public int Signal   { get; private set; }
  public int Strength { get; private set; }
  public Dictionary<int, int> Signals  { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public void Print() {
    var screen  = new List<List<string>>();
    var numRows = 6;
    var numCols = 40;

    foreach (var y in Enumerable.Range(0, numRows))
    {
      var row = new List<string>();
      foreach (var x in Enumerable.Range(0, numCols))
      {
        int clock  = (y * numCols) + x + 1;
        int signal = this.Signals[clock];
        var range  = new List<int> { signal - 1, signal, signal + 1 };
        if (range.Contains(x))
        {
          row.Add("#");
        }
        else
        {
          row.Add(".");
        }
      }
      screen.Add(row);
    }

    foreach (var row in screen)
    {
      Console.WriteLine(string.Join("", row));
    }
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== ATTRIBUTES ================================

  private void SetStrength()
  {
    var keys = new List<int> { 20, 60, 100, 140, 180, 220 };
    int sum  = 0;
    foreach (int k in keys)
    {
      sum += k * this.Signals[k];
    }
    this.Strength = sum;
  }

  // ========== COMMANDS ==================================

  private void PerformAddX(int x)
  {
    this.PerformNoop();
    this.Signals[this.Clock] = this.Signal;
    this.Signal += x;
    this.Clock  += 1;
  }

  private void PerformNoop()
  {
    this.Signals[this.Clock] = this.Signal;
    this.Clock += 1;
  }

  private void ProcessCommands(List<(string, int)> cmds)
  {
    foreach (var cmd in cmds)
    {
      if (cmd.Item1 == "noop")
      {
        this.PerformNoop();
      }
      else
      {
        this.PerformAddX(cmd.Item2);
      }
    }
    this.SetStrength();
  }
}
