using Algorithms.Problems.NQueens;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Problems.NQueens;

public static class BacktrackingNQueensSolverTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 0)]
    [TestCase(3, 0)]
    [TestCase(4, 2)]
    [TestCase(5, 10)]
    [TestCase(6, 4)]
    [TestCase(7, 40)]
    [TestCase(8, 92)]
    [TestCase(8, 92)]
    [TestCase(9, 352)]
    [TestCase(10, 724)]
    [TestCase(11, 2680)]
    public static void SolvesCorrectly(int n, int expectedNumberOfSolutions)
    {
		_logger.LogDebug(">>> Entering SolvesCorrectly(n={N},expectedNumberOfSolutions={ExpectedNumberOfSolutions})", n, expectedNumberOfSolutions);
        // Arrange
        // Act
        var result = new BacktrackingNQueensSolver().BacktrackSolve(n).ToList();
		_logger.LogDebug("SolvesCorrectly(n={N},expectedNumberOfSolutions={ExpectedNumberOfSolutions}): result → {Result}", n, expectedNumberOfSolutions, result);

        // Assert
        result.Should().HaveCount(expectedNumberOfSolutions);
        foreach (var solution in result)
        {
            ValidateOneQueenPerRow(solution);
            ValidateOneQueenPerColumn(solution);
            ValidateOneQueenPerTopLeftBottomRightDiagonalLine(solution);
            ValidateOneQueenPerBottomLeftTopRightDiagonalLine(solution);
        }
		_logger.LogDebug("<<< Exiting SolvesCorrectly(n={N},expectedNumberOfSolutions={ExpectedNumberOfSolutions})", n, expectedNumberOfSolutions);
    }

    [Test]
    public static void NCannotBeNegative()
    {
		_logger.LogDebug(">>> Entering NCannotBeNegative()");
        var n = -1;

        Action act = () => new BacktrackingNQueensSolver().BacktrackSolve(n);
		_logger.LogDebug("NCannotBeNegative(): act → {Act}", act);

        act.Should().Throw<ArgumentException>();
		_logger.LogDebug("<<< Exiting NCannotBeNegative()");
    }

    private static void ValidateOneQueenPerRow(bool[,] solution)
    {
		_logger.LogDebug(">>> Entering ValidateOneQueenPerRow(solution={Solution})", solution);
        for (var i = 0; i < solution.GetLength(1); i++)
        {
            var foundQueen = false;
			_logger.LogDebug("ValidateOneQueenPerRow(solution={Solution}): foundQueen → {FoundQueen}", solution, foundQueen);
            for (var j = 0; j < solution.GetLength(0); j++)
            {
                foundQueen = ValidateCell(foundQueen, solution[j, i]);
            }
        }
		_logger.LogDebug("<<< Exiting ValidateOneQueenPerRow(solution={Solution})", solution);
    }

    private static void ValidateOneQueenPerColumn(bool[,] solution)
    {
		_logger.LogDebug(">>> Entering ValidateOneQueenPerColumn(solution={Solution})", solution);
        for (var i = 0; i < solution.GetLength(0); i++)
        {
            var foundQueen = false;
			_logger.LogDebug("ValidateOneQueenPerColumn(solution={Solution}): foundQueen → {FoundQueen}", solution, foundQueen);
            for (var j = 0; j < solution.GetLength(1); j++)
            {
                foundQueen = ValidateCell(foundQueen, solution[i, j]);
            }
        }
		_logger.LogDebug("<<< Exiting ValidateOneQueenPerColumn(solution={Solution})", solution);
    }

    private static void ValidateOneQueenPerTopLeftBottomRightDiagonalLine(bool[,] solution)
    {
		_logger.LogDebug(">>> Entering ValidateOneQueenPerTopLeftBottomRightDiagonalLine(solution={Solution})", solution);
        for (var i = 0; i < solution.GetLength(0); i++)
        {
            var foundQueen = false;
			_logger.LogDebug("ValidateOneQueenPerTopLeftBottomRightDiagonalLine(solution={Solution}): foundQueen → {FoundQueen}", solution, foundQueen);
            for (var j = 0; i + j < solution.GetLength(1); j++)
            {
                foundQueen = ValidateCell(foundQueen, solution[i + j, i]);
            }
        }

        for (var i = 0; i < solution.GetLength(1); i++)
        {
            var foundQueen = false;
            for (var j = 0; i + j < solution.GetLength(0); j++)
            {
                foundQueen = ValidateCell(foundQueen, solution[j, i + j]);
            }
        }
		_logger.LogDebug("<<< Exiting ValidateOneQueenPerTopLeftBottomRightDiagonalLine(solution={Solution})", solution);
    }

    private static void ValidateOneQueenPerBottomLeftTopRightDiagonalLine(bool[,] solution)
    {
		_logger.LogDebug(">>> Entering ValidateOneQueenPerBottomLeftTopRightDiagonalLine(solution={Solution})", solution);
        for (var i = 0; i < solution.GetLength(0); i++)
        {
            var foundQueen = false;
			_logger.LogDebug("ValidateOneQueenPerBottomLeftTopRightDiagonalLine(solution={Solution}): foundQueen → {FoundQueen}", solution, foundQueen);
            for (var j = 0; i - j >= 0; j++)
            {
                foundQueen = ValidateCell(foundQueen, solution[i - j, i]);
            }
        }

        for (var i = 0; i < solution.GetLength(1); i++)
        {
            var foundQueen = false;
            for (var j = 0; i - j >= 0 && solution.GetLength(0) - j > 0; j++)
            {
                foundQueen = ValidateCell(foundQueen, solution[solution.GetLength(0) - j - 1, i - j]);
            }
        }
		_logger.LogDebug("<<< Exiting ValidateOneQueenPerBottomLeftTopRightDiagonalLine(solution={Solution})", solution);
    }

    private static bool ValidateCell(bool foundQueen, bool currentCell)
    {
		_logger.LogDebug(">>> Entering ValidateCell(foundQueen={FoundQueen},currentCell={CurrentCell})", foundQueen, currentCell);
        if (foundQueen)
        {
            currentCell.Should().BeFalse();
        }

		_logger.LogDebug("<<< Exiting ValidateCell(foundQueen={FoundQueen},currentCell={CurrentCell})", foundQueen, currentCell);
        return foundQueen || currentCell;
    }
}
