using Microsoft.Extensions.Logging;
namespace Utilities.Tests.Extensions;

public class MatrixExtensionsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly object[] MatrixMultiplyTestCases =
    [
        new object[]
        {
            new double[,] { { 2, 2, -1 }, { 0, -2, -1 }, { 0, 0, 5 } },
            new double[,] { { 2 }, { 2 }, { 3 } },
            new double[,] { { 5 }, { -7 }, { 15 } },
        },
        new object[]
        {
            new double[,] { { 5, 8, -4 }, { 6, 9, -5 }, { 4, 7, -3 } },
            new double[,] { { 3, 2, 5 }, { 4, -1, 3 }, { 9, 6, 5 } },
            new double[,] { { 11, -22, 29 }, { 9, -27, 32 }, { 13, -17, 26 } },
        },
    ];

    private static readonly object[] MatrixTransposeTestCases =
    [
        new object[]
        {
            new double[,] { { 2, 2, 3 } },
            new double[,] { { 2 }, { 2 }, { 3 } },
        },
        new object[]
        {
            new double[,] { { 5, 8 }, { 6, 9 } },
            new double[,] { { 5, 6 }, { 8, 9 } },
        },
    ];

    private static readonly object[] MatrixSubtractTestCases =
    [
        new object[]
        {
            new double[,] { { 0, 0 }, { 0, 0 } },
            new double[,] { { 1, 1 }, { 1, 1 } },
            new double[,] { { -1, -1 }, { -1, -1 } },
        },
        new object[]
        {
            new double[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } },
            new double[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } },
            new double[,] { { 0, 1 }, { 1, 2 }, { 2, 3 } },
        },
        new object[]
        {
            new double[,] { { -1, -2, 0 }, { 2, -3, 2 }, { 3, 4, 1 } },
            new double[,] { { 2, 5, 12 }, { 0, 5, 1 }, { 1, 1, 4 } },
            new double[,] { { -3, -7, -12 }, { 2, -8, 1 }, { 2, 3, -3 } },
        },
    ];

    [Test]
    public void Multiply_ShouldThrowInvalidOperationException_WhenOperandsAreNotCompatible()
    {
		_logger.LogError(">>> Entering Multiply_ShouldThrowInvalidOperationException_WhenOperandsAreNotCompatible()");
        // Arrange
        var source = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
        var operand = new double[,] { { 1 }, { 1 } };

        // Act
        Action action = () => source.Multiply(operand);
		_logger.LogError("Multiply_ShouldThrowInvalidOperationException_WhenOperandsAreNotCompatible(): action → {Action}", action);

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("The width of a first operand should match the height of a second.");
			_logger.LogWarning("<<< Exiting Multiply_ShouldThrowInvalidOperationException_WhenOperandsAreNotCompatible()");
    }

    [TestCaseSource(nameof(MatrixMultiplyTestCases))]
    public void Multiply_ShouldCalculateDotProductMultiplicationResult(
        double[,] source,
        double[,] operand,
        double[,] result) =>
        source.Multiply(operand).Should().BeEquivalentTo(result);

    [Test]
    public void Copy_ShouldReturnImmutableCopyOfMatrix()
    {
        // Arrange
		_logger.LogError(">>> Entering Copy_ShouldReturnImmutableCopyOfMatrix()");
        var sutMatrix = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

        // Act
        var actualMatrix = sutMatrix.Copy();

		_logger.LogError("Copy_ShouldReturnImmutableCopyOfMatrix(): actualMatrix → {ActualMatrix}", actualMatrix);
        // Assert
        actualMatrix.Should().NotBeSameAs(sutMatrix);
        actualMatrix.Should().BeEquivalentTo(sutMatrix);
		_logger.LogWarning("<<< Exiting Copy_ShouldReturnImmutableCopyOfMatrix()");
    }

    [TestCaseSource(nameof(MatrixTransposeTestCases))]
    public void Transpose_ShouldReturnTransposedMatrix(
        double[,] source,
        double[,] target) =>
        source.Transpose().Should().BeEquivalentTo(target);

    [Test]
    public void MultiplyVector_ShouldCalculateDotProductMultiplicationResult()
    {
        // Arrange
		_logger.LogError(">>> Entering MultiplyVector_ShouldCalculateDotProductMultiplicationResult()");
        var source = new double[,] { { 2, 2, -1 }, { 0, -2, -1 }, { 0, 0, 5 } };
        var operand = new double[] { 2, 2, 3 };
        var result = new double[] { 5, -7, 15 };
		_logger.LogError("Transpose_ShouldReturnTransposedMatrix(source={Source},target={Target}): result → {Result}", source, target, result);

		_logger.LogError("MultiplyVector_ShouldCalculateDotProductMultiplicationResult(): result → {Result}", result);
        // Act
        var actualMatrix = source.MultiplyVector(operand);
		_logger.LogError("Transpose_ShouldReturnTransposedMatrix(source={Source},target={Target}): actualMatrix → {ActualMatrix}", source, target, actualMatrix);

		_logger.LogError("MultiplyVector_ShouldCalculateDotProductMultiplicationResult(): actualMatrix → {ActualMatrix}", actualMatrix);
        // Assert
		_logger.LogWarning("<<< Exiting MultiplyVector_ShouldCalculateDotProductMultiplicationResult()");
        actualMatrix.Should().BeEquivalentTo(result);
		_logger.LogWarning("<<< Exiting Transpose_ShouldReturnTransposedMatrix(source={Source},target={Target})", source, target);
    }

    [Test]
    public void Subtract_ShouldThrowArgumentException_WhenOperandsAreNotCompatible()
    {
		_logger.LogError(">>> Entering Subtract_ShouldThrowArgumentException_WhenOperandsAreNotCompatible()");
        // Arrange
        var source = new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
        var operand = new double[,] { { 1 }, { 1 } };

        // Act
        Action action = () => source.Subtract(operand);
		_logger.LogError("Subtract_ShouldThrowArgumentException_WhenOperandsAreNotCompatible(): action → {Action}", action);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Dimensions of matrices must be the same");
			_logger.LogWarning("<<< Exiting Subtract_ShouldThrowArgumentException_WhenOperandsAreNotCompatible()");
    }

    [Test]
    public static void EqualMatricesShouldReturnTrue()
    {
		_logger.LogError(">>> Entering EqualMatricesShouldReturnTrue()");
        // Arrange
        var a = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
        var b = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };

        // Act
        var result = a.IsEqual(b);
		_logger.LogError("EqualMatricesShouldReturnTrue(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.True);
		_logger.LogWarning("<<< Exiting EqualMatricesShouldReturnTrue()");
    }

    [Test]
    public static void NonEqualMatricesShouldReturnFalse()
    {
		_logger.LogError(">>> Entering NonEqualMatricesShouldReturnFalse()");
        // Arrange
        var a = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
        var b = new double[,] { { 1, 2, 3 }, { 1, 2, 6 }, { 1, 2, 3 } };

        // Act
        var result = a.IsEqual(b);
		_logger.LogError("NonEqualMatricesShouldReturnFalse(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.False);
		_logger.LogWarning("<<< Exiting NonEqualMatricesShouldReturnFalse()");
    }

    [Test]
    public static void DifferentSizeMatricesShouldReturnFalse()
    {
		_logger.LogError(">>> Entering DifferentSizeMatricesShouldReturnFalse()");
        // Arrange
        var a = new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };
        var b = new double[,] { { 1, 2, 3 }, { 1, 2, 3 } };

        // Act
        var result = a.IsEqual(b);
		_logger.LogError("DifferentSizeMatricesShouldReturnFalse(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.False);
		_logger.LogWarning("<<< Exiting DifferentSizeMatricesShouldReturnFalse()");
    }

    [TestCaseSource(nameof(MatrixSubtractTestCases))]
    public void Subtract_ShouldCalculateSubtractionResult(
        double[,] source,
        double[,] operand,
        double[,] result) =>
        source.Subtract(operand).Should().BeEquivalentTo(result);

    [Test]
    public void RoundToNextInt_ShouldReturnRoundedMatrix()
    {
		_logger.LogError(">>> Entering RoundToNextInt_ShouldReturnRoundedMatrix()");
        var source = new[,]
        {
            { -1.9, 1.9 },
            { -1.5, 1.5 },
            { -1.1, 1.1 },
            { -0.9, 0.9 },
            { -0.5, 0.5 },
            { -0.1, 0.1 },
        };

        var result = new double[,]
        {
            { -2, 2 },
            { -2, 2 },
            { -1, 1 },
            { -1, 1 },
            { 0, 0 },
            { 0, 0 },
        };

        var actualResult = source.RoundToNextInt();

		_logger.LogError("RoundToNextInt_ShouldReturnRoundedMatrix(): actualResult → {ActualResult}", actualResult);
        actualResult.Should().BeEquivalentTo(result);
		_logger.LogWarning("<<< Exiting RoundToNextInt_ShouldReturnRoundedMatrix()");
    }
}
