using Xunit;
using Aoc.Day01;
using Aoc.Day02;
using Aoc.Day03;
using Aoc.Day04;
using Aoc.Day05;

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
}
