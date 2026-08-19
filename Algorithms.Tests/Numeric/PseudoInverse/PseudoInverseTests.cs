using Microsoft.Extensions.Logging;
namespace Algorithms.Tests.Numeric.PseudoInverse;

public static class PseudoInverseTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void SquaredMatrixInverseWorks()
    {
		_logger.LogDebug(">>> Entering SquaredMatrixInverseWorks()");
        // Arrange
        var inMat = new double[,] { { 2, 4, 6 }, { 2, 0, 2 }, { 6, 8, 14 } };
        var inMatCopy = new double[,] { { 2, 4, 6 }, { 2, 0, 2 }, { 6, 8, 14 } };

        // Act
        // using AA+A = A
        var result = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(inMat);
		_logger.LogDebug("SquaredMatrixInverseWorks(): result → {Result}", result);
        var aainva = inMatCopy.Multiply(result).Multiply(inMatCopy);
		_logger.LogDebug("SquaredMatrixInverseWorks(): aainva → {Aainva}", aainva);

        var rounded = aainva.RoundToNextInt();
		_logger.LogDebug("SquaredMatrixInverseWorks(): rounded → {Rounded}", rounded);
        var isequal = rounded.IsEqual(inMatCopy);
		_logger.LogDebug("SquaredMatrixInverseWorks(): isequal → {Isequal}", isequal);
        // Assert
        Assert.That(isequal, Is.True);
		_logger.LogDebug("<<< Exiting SquaredMatrixInverseWorks()");
    }

    [Test]
    public static void NonSquaredMatrixPseudoInverseMatrixWorks()
    {
		_logger.LogDebug(">>> Entering NonSquaredMatrixPseudoInverseMatrixWorks()");
        // Arrange
        var inMat = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };
        var inMatCopy = new double[,] { { 1, 2, 3, 4 }, { 0, 1, 4, 7 }, { 5, 6, 0, 1 } };

        // Act
        // using (A+)+ = A
        var result = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(inMat);
		_logger.LogDebug("NonSquaredMatrixPseudoInverseMatrixWorks(): result → {Result}", result);
        var result2 = Algorithms.Numeric.Pseudoinverse.PseudoInverse.PInv(result);
		_logger.LogDebug("NonSquaredMatrixPseudoInverseMatrixWorks(): result2 → {Result2}", result2);

        var rounded = result2.RoundToNextInt();
		_logger.LogDebug("NonSquaredMatrixPseudoInverseMatrixWorks(): rounded → {Rounded}", rounded);

        var isequal = rounded.IsEqual(inMatCopy);
		_logger.LogDebug("NonSquaredMatrixPseudoInverseMatrixWorks(): isequal → {Isequal}", isequal);
        // Assert
        Assert.That(isequal, Is.True);
		_logger.LogDebug("<<< Exiting NonSquaredMatrixPseudoInverseMatrixWorks()");
    }
}
