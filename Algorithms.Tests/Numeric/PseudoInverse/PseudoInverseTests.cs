using Microsoft.Extensions.Logging;
namespace Algorithms.Tests.Numeric.PseudoInverse;

public static class PseudoInverseTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void SquaredMatrixInverseWorks()
    {
		_logger.LogError(">>> Entering SquaredMatrixInverseWorks()");
        // Arrange
        var inMat = new double[,] { { 2, 4, 6 }, { 2, 0, 2 }, { 6, 8, 14 } };
        var inMatCopy = new double[,] { { 2, 4, 6 }, { 2, 0, 2 }, { 6, 8, 14 } };

        // Act
        // using AA+A = A
        var result = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(inMat);
		_logger.LogError("SquaredMatrixInverseWorks(): result → {Result}", result);
        var aainva = inMatCopy.Multiply(result).Multiply(inMatCopy);
		_logger.LogError("SquaredMatrixInverseWorks(): aainva → {Aainva}", aainva);

        var rounded = aainva.RoundToNextInt();
		_logger.LogError("SquaredMatrixInverseWorks(): rounded → {Rounded}", rounded);
        var isequal = rounded.IsEqual(inMatCopy);
		_logger.LogError("SquaredMatrixInverseWorks(): isequal → {Isequal}", isequal);
        // Assert
        Assert.That(isequal, Is.True);
		_logger.LogWarning("<<< Exiting SquaredMatrixInverseWorks()");
    }

    [Test]
    public static void NonSquaredMatrixPseudoInverseMatrixWorks()
    {
		_logger.LogError(">>> Entering NonSquaredMatrixPseudoInverseMatrixWorks()");
        // Arrange
        var inMat = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };
        var inMatCopy = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };

        // Act
        // using (A+)+ = A
        var result = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(inMat);
		_logger.LogError("NonSquaredMatrixPseudoInverseMatrixWorks(): result → {Result}", result);
        var result2 = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(result);
		_logger.LogError("NonSquaredMatrixPseudoInverseMatrixWorks(): result2 → {Result2}", result2);

        var rounded = result2.RoundToNextInt();
		_logger.LogError("NonSquaredMatrixPseudoInverseMatrixWorks(): rounded → {Rounded}", rounded);

        var isequal = rounded.IsEqual(inMatCopy);
		_logger.LogError("NonSquaredMatrixPseudoInverseMatrixWorks(): isequal → {Isequal}", isequal);
        // Assert
        Assert.That(isequal, Is.True);
		_logger.LogWarning("<<< Exiting NonSquaredMatrixPseudoInverseMatrixWorks()");
    }
}
