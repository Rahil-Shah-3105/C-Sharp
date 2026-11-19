using Algorithms.Knapsack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Knapsack;

public static class NaiveKnapsackSolverTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TakesHalf(
        [Random(0, 1000, 100, Distinct = true)]
        int length)
    {
		_logger.LogError(">>> Entering TakesHalf(Distinct={Distinct})", Distinct);
        //Arrange
        var solver = new NaiveKnapsackSolver<int>();
		_logger.LogError("TakesHalf(Distinct={Distinct}): solver → {Solver}", Distinct, solver);
        var items = Enumerable.Repeat(42, 2 * length).ToArray();
		_logger.LogError("TakesHalf(Distinct={Distinct}): items → {Items}", Distinct, items);
        var expectedResult = Enumerable.Repeat(42, length);
		_logger.LogError("TakesHalf(Distinct={Distinct}): expectedResult → {ExpectedResult}", Distinct, expectedResult);

        //Act
        var result = solver.Solve(items, length, _ => 1, _ => 1);
		_logger.LogError("TakesHalf(Distinct={Distinct}): result → {Result}", Distinct, result);

        //Assert
        Assert.That(result, Is.EqualTo(expectedResult));
		_logger.LogWarning("<<< Exiting TakesHalf(Distinct={Distinct})", Distinct);
    }
}
