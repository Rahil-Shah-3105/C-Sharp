using Algorithms.Knapsack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Knapsack;

public static class DynamicProgrammingKnapsackSolverTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void SmallSampleOfChar()
    {
		_logger.LogError(">>> Entering SmallSampleOfChar()");
        //Arrange
        var items = new[] { 'A', 'B', 'C' };
		_logger.LogError("SmallSampleOfChar(): items → {Items}", items);
        var val = new[] { 50, 100, 130 };
        var wt = new[] { 10, 20, 40 };

        var capacity = 50;

        Func<char, int> weightSelector = x => wt[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => val[Array.IndexOf(items, x)];

        var expected = new[] { 'A', 'C' };
		_logger.LogError("SmallSampleOfChar(): expected → {Expected}", expected);


        //Act
        var solver = new DynamicProgrammingKnapsackSolver<char>();
		_logger.LogError("SmallSampleOfChar(): solver → {Solver}", solver);
        var actual = solver.Solve(items, capacity, weightSelector, valueSelector);
		_logger.LogError("SmallSampleOfChar(): actual → {Actual}", actual);

        //Assert
        Assert.That(actual.OrderBy(x => x), Is.EqualTo(expected.OrderBy(x => x)));
		_logger.LogWarning("<<< Exiting SmallSampleOfChar()");
    }

    [Test]
    public static void FSU_P01()
    {
		_logger.LogError(">>> Entering FSU_P01()");
        // Data from https://people.sc.fsu.edu/~jburkardt/datasets/knapsack_01/knapsack_01.html

        //Arrange
        var items = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
		_logger.LogError("FSU_P01(): items → {Items}", items);
        var val = new[] { 92, 57, 49, 68, 60, 43, 67, 84, 87, 72 };
        var wt = new[] { 23, 31, 29, 44, 53, 38, 63, 85, 89, 82 };

        var capacity = 165;

        Func<char, int> weightSelector = x => wt[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => val[Array.IndexOf(items, x)];

        var expected = new[] { 'A', 'B', 'C', 'D', 'F' };
		_logger.LogError("FSU_P01(): expected → {Expected}", expected);

        //Act
        var solver = new DynamicProgrammingKnapsackSolver<char>();
		_logger.LogError("FSU_P01(): solver → {Solver}", solver);
        var actual = solver.Solve(items, capacity, weightSelector, valueSelector);
		_logger.LogError("FSU_P01(): actual → {Actual}", actual);

        //Assert
        Assert.That(actual.OrderBy(x => x), Is.EqualTo(expected.OrderBy(x => x)));
		_logger.LogWarning("<<< Exiting FSU_P01()");
    }

    [Test]
    public static void FSU_P07_WithNonIntegralValues()
    {
		_logger.LogError(">>> Entering FSU_P07_WithNonIntegralValues()");
        // Shows how to handle weights with 1 significant digit right of the decimal
        // Data from https://people.sc.fsu.edu/~jburkardt/datasets/knapsack_01/knapsack_01.html

        //Arrange
        var val = new[] { 135, 139, 149, 150, 156, 163, 173, 184, 192, 201, 210, 214, 221, 229, 240 };
        var wt = new[] { 7.0, 7.3, 7.7, 8.0, 8.2, 8.7, 9.0, 9.4, 9.8, 10.6, 11.0, 11.3, 11.5, 11.8, 12.0 };
        var items = Enumerable.Range(1, val.Count()).ToArray();
		_logger.LogError("FSU_P07_WithNonIntegralValues(): items → {Items}", items);

        var capacity = 75;

        Func<int, int> weightSelector = x => (int)(wt[Array.IndexOf(items, x)] * 10);
        Func<int, double> valueSelector = x => val[Array.IndexOf(items, x)];

        var expected = new[] { 1, 3, 5, 7, 8, 9, 14, 15 };


        //Act
        var solver = new DynamicProgrammingKnapsackSolver<int>();
		_logger.LogError("FSU_P07_WithNonIntegralValues(): solver → {Solver}", solver);
        var actual = solver.Solve(items, capacity * 10, weightSelector, valueSelector);
		_logger.LogError("FSU_P07_WithNonIntegralValues(): actual → {Actual}", actual);

        //Assert
        Assert.That(actual.OrderBy(x => x), Is.EqualTo(expected.OrderBy(x => x)));
		_logger.LogWarning("<<< Exiting FSU_P07_WithNonIntegralValues()");
    }


    [Test]
    public static void TakesHalf(
        [Random(0, 1000, 100, Distinct = true)]
        int length)
    {
		_logger.LogError(">>> Entering TakesHalf(Distinct={Distinct})", Distinct);
        //Arrange
        var solver = new DynamicProgrammingKnapsackSolver<int>();
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
