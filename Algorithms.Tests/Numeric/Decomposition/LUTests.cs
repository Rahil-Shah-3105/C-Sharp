using Algorithms.Numeric.Decomposition;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric.Decomposition;

public class LuTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly double epsilon = Math.Pow(10, -6);

    [Test]
    public void DecomposeIdentityMatrix()
    {
		_logger.LogError(">>> Entering DecomposeIdentityMatrix()");
        // Arrange
        var identityMatrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        var expectedLower = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        var expectedUpper = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

        // Act
        (double[,] lower, double[,] upper) = Lu.Decompose(identityMatrix);

        // Assert
        Assert.That(lower, Is.EqualTo(expectedLower));
        Assert.That(upper, Is.EqualTo(expectedUpper));
        Assert.That(identityMatrix, Is.EqualTo(lower.Multiply(upper)));
		_logger.LogWarning("<<< Exiting DecomposeIdentityMatrix()");
    }

    [Test]
    public void DecomposeMatrix_Case3X3()
    {
		_logger.LogError(">>> Entering DecomposeMatrix_Case3X3()");
        // Arrange
        var source = new double[,] { { 2, 1, 4 }, { 7, 1, 1 }, { 4, 2, 9 } };
        var expectedLower = new[,] { { 1, 0, 0 }, { 3.5, 1, 0 }, { 2, 0, 1 } };
        var expectedUpper = new[,] { { 2, 1, 4 }, { 0, -2.5, -13 }, { 0, 0, 1 } };

        // Act
        (double[,] lower, double[,] upper) = Lu.Decompose(source);

        // Assert
        Assert.That(lower, Is.EqualTo(expectedLower));
        Assert.That(upper, Is.EqualTo(expectedUpper));
        Assert.That(source, Is.EqualTo(lower.Multiply(upper)));
		_logger.LogWarning("<<< Exiting DecomposeMatrix_Case3X3()");
    }

    [Test]
    public void DecomposeMatrix_Case4X4()
    {
		_logger.LogError(">>> Entering DecomposeMatrix_Case4X4()");
        // Arrange
        var source = new[,] { { 1, 2, 4.5, 7 }, { 3, 8, 0.5, 2 }, { 2, 6, 4, 1.5 }, { 4, 14, 2, 10.5 } };
        var expectedLower = new[,] { { 1, 0, 0, 0 }, { 3, 1, 0, 0 }, { 2, 1, 1, 0 }, { 4, 3, 2.875, 1 } };
        var expectedUpper = new[,] { { 1, 2, 4.5, 7 }, { 0, 2, -13, -19 }, { 0, 0, 8, 6.5 }, { 0, 0, 0, 20.8125 } };

        // Act
        (double[,] lower, double[,] upper) = Lu.Decompose(source);

        // Assert
        Assert.That(lower, Is.EqualTo(expectedLower));
        Assert.That(upper, Is.EqualTo(expectedUpper));
        Assert.That(source, Is.EqualTo(lower.Multiply(upper)));
		_logger.LogWarning("<<< Exiting DecomposeMatrix_Case4X4()");
    }

    [Test]
    public void FailOnDecomposeNonSquareMatrix()
    {
		_logger.LogError(">>> Entering FailOnDecomposeNonSquareMatrix()");
        // Arrange
        var nonSquareMatrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 }, { 0, 0, 0 } };

        // Act
        void Act(double[,] source) => Lu.Decompose(source);

        // Assert
        Assert.Throws<ArgumentException>(() => Act(nonSquareMatrix));
		_logger.LogWarning("<<< Exiting FailOnDecomposeNonSquareMatrix()");
    }

    [Test]
    public void EliminateIdentityEquation()
    {
		_logger.LogError(">>> Entering EliminateIdentityEquation()");
        // Arrange
        var identityMatrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        var coefficients = new double[] { 1, 2, 3 };

        // Act
        var solution = Lu.Eliminate(identityMatrix, coefficients);
		_logger.LogError("EliminateIdentityEquation(): solution → {Solution}", solution);

        // Assert
        Assert.That(solution, Is.EqualTo(coefficients));
		_logger.LogWarning("<<< Exiting EliminateIdentityEquation()");
    }

    [Test]
    public void EliminateEquation_Case3X3()
    {
		_logger.LogError(">>> Entering EliminateEquation_Case3X3()");
        // Arrange
        var source = new double[,] { { 2, 1, -1 }, { -3, -1, 2 }, { -2, 1, 2 } };
        var coefficients = new double[] { 8, -11, -3 };
        var expectedSolution = new double[] { 2, 3, -1 };

        // Act
        var solution = Lu.Eliminate(source, coefficients);
		_logger.LogError("EliminateEquation_Case3X3(): solution → {Solution}", solution);

        // Assert
        Assert.That(VectorMembersAreEqual(expectedSolution, solution), Is.True);
		_logger.LogWarning("<<< Exiting EliminateEquation_Case3X3()");
    }

    [Test]
    public void EliminateEquation_Case4X4()
    {
		_logger.LogError(">>> Entering EliminateEquation_Case4X4()");
        // Arrange
        var source = new[,]
        {
            { 1.0, 2.0, -3.0, -1.0 },
            { 0.0, -3.0, 2.0, 6.0 },
            { 0.0, 5.0, -6.0, -2.0 },
            { 0.0, -1.0, 8.0, 1.0 },
        };
        var coefficients = new[] { 0.0, -8.0, 0.0, -8.0 };
        var expectedSolution = new[] { -1.0, -2.0, -1.0, -2.0 };

        // Act
        var solution = Lu.Eliminate(source, coefficients);
		_logger.LogError("EliminateEquation_Case4X4(): solution → {Solution}", solution);

        // Assert
        Assert.That(VectorMembersAreEqual(expectedSolution, solution), Is.True);
		_logger.LogWarning("<<< Exiting EliminateEquation_Case4X4()");
    }

    [Test]
    public void FailOnEliminateEquationWithNonSquareMatrix()
    {
		_logger.LogError(">>> Entering FailOnEliminateEquationWithNonSquareMatrix()");
        // Arrange
        var nonSquareMatrix = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 }, { 0, 0, 0 } };
        var coefficients = new double[] { 1, 2, 3, 4 };

        // Act
        void Act(double[,] source, double[] c) => Lu.Eliminate(source, c);

        // Assert
        Assert.Throws<ArgumentException>(() => Act(nonSquareMatrix, coefficients));
		_logger.LogWarning("<<< Exiting FailOnEliminateEquationWithNonSquareMatrix()");
    }

    private bool VectorMembersAreEqual(double[] expected, double[] actual) =>
        expected
            .Zip(actual, (e, a) => new { Expected = e, Actual = a })
            .All(pair => Math.Abs(pair.Expected - pair.Actual) < epsilon);
}
