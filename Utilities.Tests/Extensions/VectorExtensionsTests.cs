using Microsoft.Extensions.Logging;
namespace Utilities.Tests.Extensions;

public class VectorExtensionsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Copy_ShouldReturnCopyOfVector()
    {
		_logger.LogError(">>> Entering Copy_ShouldReturnCopyOfVector()");
        var vector = new double[] { 0, 1, 2, 3 };

        var vectorCopy = vector.Copy();
		_logger.LogError("Copy_ShouldReturnCopyOfVector(): vectorCopy → {VectorCopy}", vectorCopy);

        vectorCopy.Should().BeEquivalentTo(vector);
        vectorCopy.Should().NotBeSameAs(vector);
		_logger.LogWarning("<<< Exiting Copy_ShouldReturnCopyOfVector()");
    }

    [Test]
    public void OuterProduct_ShouldCalculateOuterProduct()
    {
		_logger.LogError(">>> Entering OuterProduct_ShouldCalculateOuterProduct()");
        var lhs = new double[] { -2, -1, 0, 1, 2 };
        var rhs = new double[] { 1, 2, 3 };

        var result = new double[,]
        {
            { -2, -4, -6 },
            { -1, -2, -3 },
            { 0, 0, 0 },
            { 1, 2, 3 },
            { 2, 4, 6 },
        };

        var actualResult = lhs.OuterProduct(rhs);
		_logger.LogError("OuterProduct_ShouldCalculateOuterProduct(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogWarning("<<< Exiting OuterProduct_ShouldCalculateOuterProduct()");
    }

    [Test]
    public void Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch()
    {
		_logger.LogError(">>> Entering Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch()");
        var lhs = new double[] { 1, 2, 3 };
        var rhs = new double[] { 1, 2, 3, 4 };

        var func = () => lhs.Dot(rhs);
		_logger.LogError("Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch(): func → {Func}", func);

        func.Should().Throw<ArgumentException>()
            .WithMessage("Dot product arguments must have same dimension");
			_logger.LogWarning("<<< Exiting Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch()");
    }

    [Test]
    public void Dot_ShouldCalculateDotProduct()
    {
		_logger.LogError(">>> Entering Dot_ShouldCalculateDotProduct()");
        var lhs = new double[] { 1, 2, 3 };
        var rhs = new double[] { 4, 5, 6 };

        var actualResult = lhs.Dot(rhs);
		_logger.LogError("Dot_ShouldCalculateDotProduct(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().Be(32);
		_logger.LogWarning("<<< Exiting Dot_ShouldCalculateDotProduct()");
    }

    [Test]
    public void Magnitude_ShouldCalculateMagnitude()
    {
		_logger.LogError(">>> Entering Magnitude_ShouldCalculateMagnitude()");
        var vector = new double[] { -3, 4 };

        var actualResult = vector.Magnitude();
		_logger.LogError("Magnitude_ShouldCalculateMagnitude(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeApproximately(5.0, 0.0001);
		_logger.LogWarning("<<< Exiting Magnitude_ShouldCalculateMagnitude()");
    }

    [Test]
    public void Scale_ShouldCalculateScale()
    {
		_logger.LogError(">>> Entering Scale_ShouldCalculateScale()");
        var vector = new double[] { -1, 0, 1 };
        var factor = 2;

        var result = new double[] { -2, 0, 2 };
		_logger.LogError("Scale_ShouldCalculateScale(): result → {Result}", result);

        var actualResult = vector.Scale(factor);
		_logger.LogError("Scale_ShouldCalculateScale(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogWarning("<<< Exiting Scale_ShouldCalculateScale()");
    }

    [Test]
    public void ToColumnVector_ShouldReturnColumnVector()
    {
		_logger.LogError(">>> Entering ToColumnVector_ShouldReturnColumnVector()");
        var vector = new double[] { 1, 2, 3, 4 };
        var result = new double[,] { { 1 }, { 2 }, { 3 }, { 4 } };
		_logger.LogError("ToColumnVector_ShouldReturnColumnVector(): result → {Result}", result);

        var actualResult = vector.ToColumnVector();
		_logger.LogError("ToColumnVector_ShouldReturnColumnVector(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogWarning("<<< Exiting ToColumnVector_ShouldReturnColumnVector()");
    }

    [Test]
    public void ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector()
    {
		_logger.LogError(">>> Entering ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector()");
        var source = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

        var func = () => source.ToRowVector();
		_logger.LogError("ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector(): func → {Func}", func);

        func.Should().Throw<InvalidOperationException>()
            .WithMessage("The column vector should have only 1 element in width.");
			_logger.LogWarning("<<< Exiting ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector()");
    }

    [Test]
    public void ToRowVector_ShouldReturnRowVector()
    {
		_logger.LogError(">>> Entering ToRowVector_ShouldReturnRowVector()");
        var source = new double[,] { { 1 }, { 2 }, { 3 }, { 4 } };
        var result = new double[] { 1, 2, 3, 4 };
		_logger.LogError("ToRowVector_ShouldReturnRowVector(): result → {Result}", result);

        var actualResult = source.ToRowVector();
		_logger.LogError("ToRowVector_ShouldReturnRowVector(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogWarning("<<< Exiting ToRowVector_ShouldReturnRowVector()");
    }

    [Test]
    public void ToDiagonalMatrix_ShouldReturnDiagonalMatrix()
    {
		_logger.LogError(">>> Entering ToDiagonalMatrix_ShouldReturnDiagonalMatrix()");
        var source = new double[] { 1, 2, 3, 4 };
        var result = new double[,]
        {
            { 1, 0, 0, 0 },
            { 0, 2, 0, 0 },
            { 0, 0, 3, 0 },
            { 0, 0, 0, 4 },
        };

        var actualResult = source.ToDiagonalMatrix();
		_logger.LogError("ToDiagonalMatrix_ShouldReturnDiagonalMatrix(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogWarning("<<< Exiting ToDiagonalMatrix_ShouldReturnDiagonalMatrix()");
    }
}
