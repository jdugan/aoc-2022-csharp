using Aoc.Utility;

﻿namespace Aoc.Day13;

public class Runner
{
  //-------------------------------------------------------
  // Public Methods
  //-------------------------------------------------------

  public int Day()
  {
    return 13;
  }

  public int Puzzle1()
  {
    return this.Pairs().
              Where(p => p.InOrder()).
              Select(p => p.Id).
              Sum();
  }

  public int Puzzle2()
  {
    var packets = this.Packets();
    packets.Add(new Packet(9998, "[[2]]"));
    packets.Add(new Packet(9999, "[[6]]"));
    packets.Sort();

    var indices = new List<int>();
    foreach (var (i, p) in packets.Select((v, i) => (i, v)))
    {
      if (p.Id == 9998 || p.Id == 9999)
      {
        indices.Add(i + 1);
      }
    }

    return indices[0] * indices[1];
  }

  //-------------------------------------------------------
  // Private Methods
  //-------------------------------------------------------

  // ========== DATA ======================================

  private List<string> Data()
  {
    return Reader.ToStrings("data/day13/input.txt");
  }

  private List<Packet> Packets ()
  {
    var packets = new List<Packet>();
    foreach (var (id, line) in this.Data().Select((v, i) => (i, v)))
    {
      packets.Add(new Packet(id, line));
    }
    return packets;
  }

  private List<Pair> Pairs ()
  {
    var  pairs = new List<Pair>();
    var  chunk = new List<Packet>();
    var  packetId  = 0;
    var  pairId    = 0;

    foreach (string line in this.Data())
    {
      packetId += 1;
      chunk.Add(new Packet(packetId, line));

      if (packetId % 2 == 0)
      {
        pairId += 1;
        pairs.Add(new Pair(pairId, chunk[0], chunk[1]));

        chunk.Clear();
      }
    }

    return pairs;
  }
}
