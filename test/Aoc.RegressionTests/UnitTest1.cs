using Xunit;
using Aoc.Day01;

namespace Aoc.RegressionTests;

public class SanityChecks
{
    [Fact]
    public void Day01Tests()
    {
      var runner = new Aoc.Day01.Runner();

      Assert.True(runner.Puzzle1() == -1);
      Assert.True(runner.Puzzle2() == -2);
    }
}
