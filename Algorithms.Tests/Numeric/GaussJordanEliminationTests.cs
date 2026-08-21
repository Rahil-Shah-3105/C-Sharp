using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

/// <summary>
///     Class for testing Gauss-Jordan Elimination Algorithm.
/// </summary>
public static class GaussJordanEliminationTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void NonSquaredMatrixThrowsException()
    {
		_logger.LogDebug(">>> Entering NonSquaredMatrixThrowsException()");
        // Arrange
        var solver = new GaussJordanElimination();
		_logger.LogDebug("NonSquaredMatrixThrowsException(): solver → {Solver}", solver);
        var input = new double[,] { { 2, -1, 5 }, { 0, 2, 1 }, { 3, 17, 7 } };

        // Act
        void Act() => solver.Solve(input);

        // Assert
		_logger.LogDebug("<<< Exiting NonSquaredMatrixThrowsException()");
        _ = Assert.Throws<ArgumentException>(Act);
		_logger.LogDebug("NonSquaredMatrixThrowsException(): _ → {_}", _);
    }

    [Test]
    public static void UnableToSolveSingularMatrix()
    {
		_logger.LogDebug(">>> Entering UnableToSolveSingularMatrix()");
        // Arrange
        var solver = new GaussJordanElimination();
		_logger.LogDebug("UnableToSolveSingularMatrix(): solver → {Solver}", solver);
        var input = new double[,] { { 0, 0, 0 }, { 0, 0, 0 } };

        // Act
        var result = solver.Solve(input);
		_logger.LogDebug("UnableToSolveSingularMatrix(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.False);
		_logger.LogDebug("<<< Exiting UnableToSolveSingularMatrix()");
    }
}
