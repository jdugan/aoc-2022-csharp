using Xunit;

namespace Aoc.RegressionTests;

public class SanityChecks
{
    [Fact]
    public void Day01Tests()
    {
      var runner = new Aoc.Day01.Runner();

      Assert.True(runner.Puzzle1() == 69177);
      Assert.True(runner.Puzzle2() == 207456);
    }

    [Fact]
    public void Day02Tests()
    {
      var runner = new Aoc.Day02.Runner();

      Assert.True(runner.Puzzle1() == 13526);
      Assert.True(runner.Puzzle2() == 14204);
    }

    [Fact]
    public void Day03Tests()
    {
      var runner = new Aoc.Day03.Runner();

      Assert.True(runner.Puzzle1() == 7831);
      Assert.True(runner.Puzzle2() == 2683);
    }

    [Fact]
    public void Day04Tests()
    {
      var runner = new Aoc.Day04.Runner();

      Assert.True(runner.Puzzle1() == 464);
      Assert.True(runner.Puzzle2() == 770);
    }

    [Fact]
    public void Day05Tests()
    {
      var runner = new Aoc.Day05.Runner();

      Assert.True(runner.Puzzle1() == "ZWHVFWQWW");
      Assert.True(runner.Puzzle2() == "HZFZCCWWV");
    }

    [Fact]
    public void Day06Tests()
    {
      var runner = new Aoc.Day06.Runner();

      Assert.True(runner.Puzzle1() == 1702);
      Assert.True(runner.Puzzle2() == 3559);
    }

    [Fact]
    public void Day07Tests()
    {
      var runner = new Aoc.Day07.Runner();

      Assert.True(runner.Puzzle1() == 1908462);
      Assert.True(runner.Puzzle2() == 3979145);
    }

    [Fact]
    public void Day08Tests()
    {
      var runner = new Aoc.Day08.Runner();

      Assert.True(runner.Puzzle1() == 1789);
      Assert.True(runner.Puzzle2() == 314820);
    }

    [Fact]
    public void Day09Tests()
    {
      var runner = new Aoc.Day09.Runner();

      Assert.True(runner.Puzzle1() == 6494);
      Assert.True(runner.Puzzle2() == 2691);
    }

    [Fact]
    public void Day10Tests()
    {
      var runner = new Aoc.Day10.Runner();

      Assert.True(runner.Puzzle1() == 14160);
      Assert.True(runner.Puzzle2() == "RJERPEFC");
    }

    [Fact]
    public void Day11Tests()
    {
      var runner = new Aoc.Day11.Runner();

      Assert.True(runner.Puzzle1() == 69918);
      Assert.True(runner.Puzzle2() == 19573408701);
    }

    [Fact]
    public void Day12Tests()
    {
      var runner = new Aoc.Day12.Runner();

      Assert.True(runner.Puzzle1() == 462);
      Assert.True(runner.Puzzle2() == 451);
    }

    [Fact]
    public void Day13Tests()
    {
      var runner = new Aoc.Day13.Runner();

      Assert.True(runner.Puzzle1() == 4643);
      Assert.True(runner.Puzzle2() == 21614);
    }

    [Fact]
    public void Day14Tests()
    {
      var runner = new Aoc.Day14.Runner();

      Assert.True(runner.Puzzle1() == 1001);
      Assert.True(runner.Puzzle2() == 27976);
    }

    [Fact]
    public void Day15Tests()
    {
      var runner = new Aoc.Day15.Runner();

      Assert.True(runner.Puzzle1() == 5147333);
      Assert.True(runner.Puzzle2() == 13734006908372);    // pretty slow
    }

    [Fact]
    public void Day16Tests()
    {
      var runner = new Aoc.Day16.Runner();

      Assert.True(runner.Puzzle1() == 1559);
      // Assert.True(runner.Puzzle2() == -2);
    }

    [Fact]
    public void Day17Tests()
    {
      var runner = new Aoc.Day17.Runner();

      Assert.True(runner.Puzzle1() == 3085);
      Assert.True(runner.Puzzle2() == 1535483870924);
    }

    [Fact]
    public void Day18Tests()
    {
      var runner = new Aoc.Day18.Runner();

      Assert.True(runner.Puzzle1() == 4192);
      Assert.True(runner.Puzzle2() == 2520);      // very slow :(
    }

    [Fact]
    public void Day19Tests()
    {
      var runner = new Aoc.Day19.Runner();

      Assert.True(runner.Puzzle1() == -1);
      Assert.True(runner.Puzzle2() == -2);
    }

    [Fact]
    public void Day20Tests()
    {
      var runner = new Aoc.Day20.Runner();

      Assert.True(runner.Puzzle1() == 872);
      Assert.True(runner.Puzzle2() == 5382459262696);
    }

    [Fact]
    public void Day21Tests()
    {
      var runner = new Aoc.Day21.Runner();

      Assert.True(runner.Puzzle1() == 155708040358220);
      Assert.True(runner.Puzzle2() == -2);
    }

    [Fact]
    public void Day22Tests()
    {
      var runner = new Aoc.Day22.Runner();

      Assert.True(runner.Puzzle1() == 88268);
      Assert.True(runner.Puzzle2() == 124302);
    }

    [Fact]
    public void Day23Tests()
    {
      var runner = new Aoc.Day23.Runner();

      Assert.True(runner.Puzzle1() == -1);
      Assert.True(runner.Puzzle2() == -2);
    }

    [Fact]
    public void Day24Tests()
    {
      var runner = new Aoc.Day24.Runner();

      Assert.True(runner.Puzzle1() == -1);
      Assert.True(runner.Puzzle2() == -2);
    }

    [Fact]
    public void Day25Tests()
    {
      var runner = new Aoc.Day25.Runner();

      Assert.True(runner.Puzzle1() == -1);
      Assert.True(runner.Puzzle2() == -2);
    }
}
