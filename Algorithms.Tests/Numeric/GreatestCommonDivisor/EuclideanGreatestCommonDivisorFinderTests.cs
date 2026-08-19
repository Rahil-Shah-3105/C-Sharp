using Algorithms.Numeric.GreatestCommonDivisor;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric.GreatestCommonDivisor;

public static class EuclideanGreatestCommonDivisorFinderTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(2, 3, 1)]
    [TestCase(1, 1, 1)]
    [TestCase(13, 17, 1)]
    [TestCase(0, 17, 17)]
    [TestCase(17, 0, 17)]
    [TestCase(17, 17, 17)]
    [TestCase(2 * 17, 17, 17)]
    [TestCase(0, 0, int.MaxValue)]
    [TestCase(2 * 13 * 17, 4 * 9 * 13, 2 * 13)]
    public static void GreatestCommonDivisorCorrect(int a, int b, int expectedGcd)
    {
		_logger.LogDebug(">>> Entering GreatestCommonDivisorCorrect(a={A},b={B},expectedGcd={ExpectedGcd})", a, b, expectedGcd);
        // Arrange
        var gcdFinder = new EuclideanGreatestCommonDivisorFinder();
		_logger.LogDebug("GreatestCommonDivisorCorrect(a={A},b={B},expectedGcd={ExpectedGcd}): gcdFinder → {GcdFinder}", a, b, expectedGcd, gcdFinder);

        // Act
        var actualGcd = gcdFinder.FindGcd(a, b);
		_logger.LogDebug("GreatestCommonDivisorCorrect(a={A},b={B},expectedGcd={ExpectedGcd}): actualGcd → {ActualGcd}", a, b, expectedGcd, actualGcd);

        // Assert
        Assert.That(actualGcd, Is.EqualTo(expectedGcd));
		_logger.LogDebug("<<< Exiting GreatestCommonDivisorCorrect(a={A},b={B},expectedGcd={ExpectedGcd})", a, b, expectedGcd);
    }
}
