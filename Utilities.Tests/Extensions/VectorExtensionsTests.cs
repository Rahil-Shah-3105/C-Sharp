using Microsoft.Extensions.Logging;
namespace Utilities.Tests.Extensions;

public class VectorExtensionsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Copy_ShouldReturnCopyOfVector()
    {
		_logger.LogDebug(">>> Entering Copy_ShouldReturnCopyOfVector()");
        var vector = new double[] { 0, 1, 2, 3 };

        var vectorCopy = vector.Copy();
		_logger.LogDebug("Copy_ShouldReturnCopyOfVector(): vectorCopy → {VectorCopy}", vectorCopy);

        vectorCopy.Should().BeEquivalentTo(vector);
        vectorCopy.Should().NotBeSameAs(vector);
		_logger.LogDebug("<<< Exiting Copy_ShouldReturnCopyOfVector()");
    }

    [Test]
    public void OuterProduct_ShouldCalculateOuterProduct()
    {
		_logger.LogDebug(">>> Entering OuterProduct_ShouldCalculateOuterProduct()");
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
		_logger.LogDebug("OuterProduct_ShouldCalculateOuterProduct(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogDebug("<<< Exiting OuterProduct_ShouldCalculateOuterProduct()");
    }

    [Test]
    public void Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch()
    {
		_logger.LogDebug(">>> Entering Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch()");
        var lhs = new double[] { 1, 2, 3 };
        var rhs = new double[] { 1, 2, 3, 4 };

        var func = () => lhs.Dot(rhs);
		_logger.LogDebug("Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch(): func → {Func}", func);

        func.Should().Throw<ArgumentException>()
            .WithMessage("Dot product arguments must have same dimension");
			_logger.LogDebug("<<< Exiting Dot_ShouldThrowArgumentException_WhenDimensionsDoNotMatch()");
    }

    [Test]
    public void Dot_ShouldCalculateDotProduct()
    {
		_logger.LogDebug(">>> Entering Dot_ShouldCalculateDotProduct()");
        var lhs = new double[] { 1, 2, 3 };
        var rhs = new double[] { 4, 5, 6 };

        var actualResult = lhs.Dot(rhs);
		_logger.LogDebug("Dot_ShouldCalculateDotProduct(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().Be(32);
		_logger.LogDebug("<<< Exiting Dot_ShouldCalculateDotProduct()");
    }

    [Test]
    public void Magnitude_ShouldCalculateMagnitude()
    {
		_logger.LogDebug(">>> Entering Magnitude_ShouldCalculateMagnitude()");
        var vector = new double[] { -3, 4 };

        var actualResult = vector.Magnitude();
		_logger.LogDebug("Magnitude_ShouldCalculateMagnitude(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeApproximately(5.0, 0.0001);
		_logger.LogDebug("<<< Exiting Magnitude_ShouldCalculateMagnitude()");
    }

    [Test]
    public void Scale_ShouldCalculateScale()
    {
		_logger.LogDebug(">>> Entering Scale_ShouldCalculateScale()");
        var vector = new double[] { -1, 0, 1 };
        var factor = 2;

        var result = new double[] { -2, 0, 2 };
		_logger.LogDebug("Scale_ShouldCalculateScale(): result → {Result}", result);

        var actualResult = vector.Scale(factor);
		_logger.LogDebug("Scale_ShouldCalculateScale(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogDebug("<<< Exiting Scale_ShouldCalculateScale()");
    }

    [Test]
    public void ToColumnVector_ShouldReturnColumnVector()
    {
		_logger.LogDebug(">>> Entering ToColumnVector_ShouldReturnColumnVector()");
        var vector = new double[] { 1, 2, 3, 4 };
        var result = new double[,] { { 1 }, { 2 }, { 3 }, { 4 } };
		_logger.LogDebug("ToColumnVector_ShouldReturnColumnVector(): result → {Result}", result);

        var actualResult = vector.ToColumnVector();
		_logger.LogDebug("ToColumnVector_ShouldReturnColumnVector(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogDebug("<<< Exiting ToColumnVector_ShouldReturnColumnVector()");
    }

    [Test]
    public void ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector()
    {
		_logger.LogDebug(">>> Entering ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector()");
        var source = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

        var func = () => source.ToRowVector();
		_logger.LogDebug("ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector(): func → {Func}", func);

        func.Should().Throw<InvalidOperationException>()
            .WithMessage("The column vector should have only 1 element in width.");
			_logger.LogDebug("<<< Exiting ToRowVector_ShouldThrowInvalidOperationException_WhenSourceIsNotAColumnVector()");
    }

    [Test]
    public void ToRowVector_ShouldReturnRowVector()
    {
		_logger.LogDebug(">>> Entering ToRowVector_ShouldReturnRowVector()");
        var source = new double[,] { { 1 }, { 2 }, { 3 }, { 4 } };
        var result = new double[] { 1, 2, 3, 4 };
		_logger.LogDebug("ToRowVector_ShouldReturnRowVector(): result → {Result}", result);

        var actualResult = source.ToRowVector();
		_logger.LogDebug("ToRowVector_ShouldReturnRowVector(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogDebug("<<< Exiting ToRowVector_ShouldReturnRowVector()");
    }

    [Test]
    public void ToDiagonalMatrix_ShouldReturnDiagonalMatrix()
    {
		_logger.LogDebug(">>> Entering ToDiagonalMatrix_ShouldReturnDiagonalMatrix()");
        var source = new double[] { 1, 2, 3, 4 };
        var result = new double[,]
        {
            { 1, 0, 0, 0 },
            { 0, 2, 0, 0 },
            { 0, 0, 3, 0 },
            { 0, 0, 0, 4 },
        };

        var actualResult = source.ToDiagonalMatrix();
		_logger.LogDebug("ToDiagonalMatrix_ShouldReturnDiagonalMatrix(): actualResult → {ActualResult}", actualResult);

        actualResult.Should().BeEquivalentTo(result);
		_logger.LogDebug("<<< Exiting ToDiagonalMatrix_ShouldReturnDiagonalMatrix()");
    }
}
