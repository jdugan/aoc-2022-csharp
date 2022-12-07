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

      Assert.True(runner.Puzzle1() == -1);
      Assert.True(runner.Puzzle2() == -2);
    }
}
