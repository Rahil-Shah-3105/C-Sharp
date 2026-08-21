using Algorithms.Knapsack;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Knapsack;

public static class BranchAndBoundKnapsackSolverTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void BranchAndBoundTest_Example1_Success()
    {
		_logger.LogDebug(">>> Entering BranchAndBoundTest_Example1_Success()");
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D' };
		_logger.LogDebug("BranchAndBoundTest_Example1_Success(): items → {Items}", items);
        var values = new[] { 18, 20, 14, 18 };
        var weights = new[] { 2, 4, 6, 9 };

        var capacity = 15;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
		_logger.LogDebug("BranchAndBoundTest_Example1_Success(): solver → {Solver}", solver);
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);
		_logger.LogDebug("BranchAndBoundTest_Example1_Success(): actualResult → {ActualResult}", actualResult);

        // Assert
        actualResult.Should().BeEquivalentTo(new[] { 'A', 'B', 'D' });
		_logger.LogDebug("<<< Exiting BranchAndBoundTest_Example1_Success()");
    }

    [Test]
    public static void BranchAndBoundTest_Example2_Success()
    {
		_logger.LogDebug(">>> Entering BranchAndBoundTest_Example2_Success()");
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
		_logger.LogDebug("BranchAndBoundTest_Example2_Success(): items → {Items}", items);
        var values = new[] { 505, 352, 458, 220, 354, 414, 498, 545, 473, 543 };
        var weights = new[] { 23, 26, 20, 18, 32, 27, 29, 26, 30, 27 };

        var capacity = 67;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
		_logger.LogDebug("BranchAndBoundTest_Example2_Success(): solver → {Solver}", solver);
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);
		_logger.LogDebug("BranchAndBoundTest_Example2_Success(): actualResult → {ActualResult}", actualResult);

        // Assert
        actualResult.Should().BeEquivalentTo(new[] { 'H', 'D', 'A' });
		_logger.LogDebug("<<< Exiting BranchAndBoundTest_Example2_Success()");
    }

    [Test]
    public static void BranchAndBoundTest_CapacityIsZero_NothingTaken()
    {
		_logger.LogDebug(">>> Entering BranchAndBoundTest_CapacityIsZero_NothingTaken()");
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D' };
		_logger.LogDebug("BranchAndBoundTest_CapacityIsZero_NothingTaken(): items → {Items}", items);
        var values = new[] { 18, 20, 14, 18 };
        var weights = new[] { 2, 4, 6, 9 };

        var capacity = 0;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
		_logger.LogDebug("BranchAndBoundTest_CapacityIsZero_NothingTaken(): solver → {Solver}", solver);
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);
		_logger.LogDebug("BranchAndBoundTest_CapacityIsZero_NothingTaken(): actualResult → {ActualResult}", actualResult);

        // Assert
        actualResult.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting BranchAndBoundTest_CapacityIsZero_NothingTaken()");
    }

    [Test]
    public static void BranchAndBoundTest_PlentyCapacity_EverythingIsTaken()
    {
		_logger.LogDebug(">>> Entering BranchAndBoundTest_PlentyCapacity_EverythingIsTaken()");
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D' };
		_logger.LogDebug("BranchAndBoundTest_PlentyCapacity_EverythingIsTaken(): items → {Items}", items);
        var values = new[] { 18, 20, 14, 18 };
        var weights = new[] { 2, 4, 6, 9 };

        var capacity = 1000;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
		_logger.LogDebug("BranchAndBoundTest_PlentyCapacity_EverythingIsTaken(): solver → {Solver}", solver);
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);
		_logger.LogDebug("BranchAndBoundTest_PlentyCapacity_EverythingIsTaken(): actualResult → {ActualResult}", actualResult);

        // Assert
        actualResult.Should().BeEquivalentTo(items);
		_logger.LogDebug("<<< Exiting BranchAndBoundTest_PlentyCapacity_EverythingIsTaken()");
    }

    [Test]
    public static void BranchAndBoundTest_NoItems_NothingTaken()
    {
		_logger.LogDebug(">>> Entering BranchAndBoundTest_NoItems_NothingTaken()");
        // Arrange
        var items = Array.Empty<char>();
		_logger.LogDebug("BranchAndBoundTest_NoItems_NothingTaken(): items → {Items}", items);
        var values = Array.Empty<int>();
		_logger.LogDebug("BranchAndBoundTest_NoItems_NothingTaken(): values → {Values}", values);
        var weights = Array.Empty<int>();
		_logger.LogDebug("BranchAndBoundTest_NoItems_NothingTaken(): weights → {Weights}", weights);

        var capacity = 15;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
		_logger.LogDebug("BranchAndBoundTest_NoItems_NothingTaken(): solver → {Solver}", solver);
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);
		_logger.LogDebug("BranchAndBoundTest_NoItems_NothingTaken(): actualResult → {ActualResult}", actualResult);

        // Assert
        actualResult.Should().BeEmpty();
		_logger.LogDebug("<<< Exiting BranchAndBoundTest_NoItems_NothingTaken()");
    }
}
