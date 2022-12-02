using Xunit;
using Aoc.Day01;
using Aoc.Day02;

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
}
