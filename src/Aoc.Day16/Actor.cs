namespace Aoc.Day16;

public class Actor
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Actor (string position)
  {
    Path     = new Queue<string>();
    Position = position;
    Target   = "";
  }
  public Queue<string> Path     { get; set; }
  public string        Position { get; set; }
  public string        Target   { get; set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  public Actor Copy ()
  {
    var copy    = new Actor(this.Position);
    copy.Path   = this.CopyPath(this.Path);
    copy.Target = this.Target;

    return copy;
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  public Queue<string> CopyPath (Queue<string> other)
  {
    var copy    = new Queue<string>();
    foreach (var i in Enumerable.Range(0, other.Count))
    {
      var item = this.Path.Dequeue();
      copy.Enqueue(item);
    }
    return copy;
  }
}
