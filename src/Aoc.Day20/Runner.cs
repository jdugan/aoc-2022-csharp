using System.Text.RegularExpressions;

using Aoc.Utility;

﻿namespace Aoc.Day20;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 20;
  }

  // WRONG: 2637 => too high (0 at 1312)
  public int Puzzle1()
  {
    var nodes                  = this.BuildList();
    var linked                 = this.BuildLinkedList(nodes);
    var (idxToNode, nodeToIdx) = this.BuildDictionaries(linked);
    var count                  = nodes.Count;

    Console.WriteLine("");
    foreach (var n in nodes)
    {
      Console.WriteLine(n.Value);
      if (n.Value % count != 0) // only bother if node moves
      {
        // get the nodes and indices
        var node1  = linked.Find(n);
        var index1 = nodeToIdx[n];
        var index2 = (((index1 + n.Value) % count) + count) % count; // adjust and force to positive num
        var node2  = linked.Find(idxToNode[index2]);
        if (n.Value == 10000)
        {
          Console.WriteLine("  {0}, {1}, {2}, {3}", node1.Value.Value, index1, index2, node2.Value.Value);
        }

        // update linked list
        linked.Remove(node1);
        if (n.Value > 0)
        {
          linked.AddAfter(node2, node1);
        }
        else
        {
          linked.AddBefore(node2, node1);
        }

        // update dictionaries
        (idxToNode, nodeToIdx) = this.BuildDictionaries(linked);
      }
      // Console.WriteLine("{0} => {1}", n.Value, String.Join(",", linked.Select(n => n.Value)));
    }
    Console.WriteLine("");

    var n0    = nodes.Where(n => n.Value == 0).ToList()[0];
    var i0    = nodeToIdx[n0];
    var i1    = (i0 + 1000) % count;
    var i2    = (i0 + 2000) % count;
    var i3    = (i0 + 3000) % count;
    Console.WriteLine(n0.Id);
    Console.WriteLine(n0.Value);
    Console.WriteLine(i0);
    Console.WriteLine(i1);
    Console.WriteLine(i2);
    Console.WriteLine(i3);
    Console.WriteLine(idxToNode[i1].Value);
    Console.WriteLine(idxToNode[i2].Value);
    Console.WriteLine(idxToNode[i3].Value);

    var score = idxToNode[i1].Value + idxToNode[i2].Value + idxToNode[i3].Value;

    return score;
  }

  public int Puzzle2()
  {
    return -2;
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== STRUCTURES ================================

  private (Dictionary<int, Node>, Dictionary<Node, int>) BuildDictionaries (LinkedList<Node> linked)
  {
    var idxToNode = new Dictionary<int, Node>();
    var nodeToIdx = new Dictionary<Node, int>();
    foreach (var (idx, node) in linked.Select((v, i) => (i, v)))
    {
      idxToNode[idx]  = node;
      nodeToIdx[node] = idx;
    }
    return (idxToNode, nodeToIdx);
  }

  private LinkedList<Node> BuildLinkedList (List<Node> list)
  {
    return new LinkedList<Node>(list);
  }

  private List<Node> BuildList ()
  {
    return this.Data().
              Select((v, i) => new Node(i, v)).
              ToList();
  }


  // ========== DATA ======================================

  private List<int> Data()
  {
    return Reader.ToIntegers("data/day20/input.txt");
  }
}
