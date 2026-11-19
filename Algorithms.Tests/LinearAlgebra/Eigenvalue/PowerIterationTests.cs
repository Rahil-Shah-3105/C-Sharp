using Algorithms.LinearAlgebra.Eigenvalue;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.LinearAlgebra.Eigenvalue;

public class PowerIterationTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly object[] DominantVectorTestCases =
    [
        new object[]
        {
            3.0,
            new[] { 0.7071039, 0.70710966 },
            new[,] { { 2.0, 1.0 }, { 1.0, 2.0 } },
        },
        new object[]
        {
            4.235889,
            new[] { 0.91287093, 0.40824829 },
            new[,] { { 2.0, 5.0 }, { 1.0, 2.0 } },
        },
    ];

    private readonly double epsilon = Math.Pow(10, -5);

    [Test]
    public void Dominant_ShouldThrowArgumentException_WhenSourceMatrixIsNotSquareShaped()
    {
		_logger.LogError(">>> Entering Dominant_ShouldThrowArgumentException_WhenSourceMatrixIsNotSquareShaped()");
        // Arrange
        var source = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 }, { 0, 0, 0 } };

        // Act
        Action action = () => PowerIteration.Dominant(source, StartVector(source.GetLength(0)), epsilon);
		_logger.LogError("Dominant_ShouldThrowArgumentException_WhenSourceMatrixIsNotSquareShaped(): action → {Action}", action);

        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("The source matrix is not square-shaped.");
		_logger.LogWarning("<<< Exiting Dominant_ShouldThrowArgumentException_WhenSourceMatrixIsNotSquareShaped()");
    }

    [Test]
    public void Dominant_ShouldThrowArgumentException_WhenStartVectorIsNotSameSizeAsMatrix()
    {
		_logger.LogError(">>> Entering Dominant_ShouldThrowArgumentException_WhenStartVectorIsNotSameSizeAsMatrix()");
        // Arrange
        var source = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        var startVector = new double[] { 1, 0, 0, 0 };

        // Act
        Action action = () => PowerIteration.Dominant(source, startVector, epsilon);
		_logger.LogError("Dominant_ShouldThrowArgumentException_WhenStartVectorIsNotSameSizeAsMatrix(): action → {Action}", action);

        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("The length of the start vector doesn't equal the size of the source matrix.");
			_logger.LogWarning("<<< Exiting Dominant_ShouldThrowArgumentException_WhenStartVectorIsNotSameSizeAsMatrix()");
    }

    [TestCaseSource(nameof(DominantVectorTestCases))]
    public void Dominant_ShouldCalculateDominantEigenvalueAndEigenvector(
        double eigenvalue,
        double[] eigenvector,
        double[,] source)
    {
		_logger.LogError(">>> Entering Dominant_ShouldCalculateDominantEigenvalueAndEigenvector(eigenvalue={Eigenvalue},eigenvector={Eigenvector},source={Source})", eigenvalue, eigenvector, source);
        // Act
        var (actualEigVal, actualEigVec) =
            PowerIteration.Dominant(source, StartVector(source.GetLength(0)), epsilon);

        // Assert
        actualEigVal.Should().BeApproximately(eigenvalue, epsilon);
        actualEigVec.Magnitude().Should().BeApproximately(eigenvector.Magnitude(), epsilon);
		_logger.LogWarning("<<< Exiting Dominant_ShouldCalculateDominantEigenvalueAndEigenvector(eigenvalue={Eigenvalue},eigenvector={Eigenvector},source={Source})", eigenvalue, eigenvector, source);
    }

    private double[] StartVector(int length)
    {
        _logger.LogError(">>> Entering StartVector(length={Length})", length);
        _logger.LogWarning("<<< Exiting StartVector(length={Length})", length);
        return new Random(111111).NextVector(length);
    }
}
