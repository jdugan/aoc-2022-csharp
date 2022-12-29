using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace Aoc.Day24;

public class Site
{
  // ------------------------------------------------------
  // Configuration
  // ------------------------------------------------------

  public Site (Snapshot snapshot)
  {
    Snapshots = new List<Snapshot>{ snapshot };

    Graph       = new Graph<Point, string>();
    Origin      = this.GetOrigin();
    Destination = this.GetDestination();

    this.AddNewGraphNodes(snapshot);
  }
  public Point                Destination { get; private set; }
  public Graph<Point, string> Graph       { get; private set; }
  public Point                Origin      { get; private set; }
  public List<Snapshot>       Snapshots   { get; private set; }


  // ------------------------------------------------------
  // Public Methods
  // ------------------------------------------------------

  public int ShortestDistance ()
  {
    // build graph with snapshots for minimum
    // possible steps
    var current   = this.LastSnapshot();
    var manhattan = Math.Abs(this.Destination.X - this.Origin.X) + Math.Abs(this.Destination.Y - this.Origin.Y);
    foreach (var _ in Enumerable.Range(0, manhattan))
    {
      var next = current.GenerateNext();
      this.AddNewGraphNodes(next);
      this.AddNewConnections(current, next);
      this.Destination = this.GetDestination();

      this.Snapshots.Add(next);
      current = next;
    }
    var result = this.Graph.Dijkstra(this.Origin.GraphId, this.Destination.GraphId);
    var best   = result.Distance;

    // keep adding a new step and checking for a shortest
    // path until we find one.
    while (best == Int32.MaxValue)
    {
      var next = current.GenerateNext();
      this.AddNewGraphNodes(next);
      this.AddNewConnections(current, next);
      this.Destination = this.GetDestination();

      this.Snapshots.Add(next);
      current = next;

      result = this.Graph.Dijkstra(this.Origin.GraphId, this.Destination.GraphId);
      best   = result.Distance;
    }

    return best;
  }


  // ========== ATTRIBUTES ================================

  public Snapshot LastSnapshot ()
  {
    return this.Snapshots[this.Snapshots.Count - 1];
  }


  // ------------------------------------------------------
  // Private Methods
  // ------------------------------------------------------

  // ========== ACTIONS ===================================

  private void AddNewConnections (Snapshot current, Snapshot next)
  {
    foreach (var p in current.Points.Values)
    {
      if (p.IsOpen())
      {
        foreach (var nid in p.NeighborIds())
        {
          if (next.Points.ContainsKey(nid))
          {
            var np = next.Points[nid];
            if (np.IsOpen())
            {
              // Console.WriteLine("{0} ({1}) => {2} ({3})", p.GraphId, p.Id, np.GraphId, np.Id);
              this.Graph.Connect(p.GraphId, np.GraphId, 1, "dummy");
            }
          }
        }
      }
    }
  }

  private void AddNewGraphNodes (Snapshot snapshot)
  {
    foreach (var p in snapshot.Points.Values)
    {
      uint graphId = this.Graph.AddNode(p);
      snapshot.Points[p.Id].GraphId = graphId;
    }
  }


  // ========== ATTRIBUTES ================================

  private Point GetDestination ()
  {
    return this.LastSnapshot().GetDestination();
  }

  private Point GetOrigin ()
  {
    return this.Snapshots[0].GetOrigin();
  }
}
