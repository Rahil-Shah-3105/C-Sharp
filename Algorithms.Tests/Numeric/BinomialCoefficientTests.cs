using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class BinomialCoefficientTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(4, 2, 6)]
    [TestCase(7, 3, 35)]
    public static void CalculateFromPairs(int n, int k, int expected)
    {
		_logger.LogDebug(">>> Entering CalculateFromPairs(n={N},k={K},expected={Expected})", n, k, expected);
        // Arrange

        // Act
        var result = BinomialCoefficient.Calculate(new BigInteger(n), new BigInteger(k));
		_logger.LogDebug("CalculateFromPairs(n={N},k={K},expected={Expected}): result → {Result}", n, k, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(new BigInteger(expected)));
		_logger.LogDebug("<<< Exiting CalculateFromPairs(n={N},k={K},expected={Expected})", n, k, expected);
    }

    [TestCase(3, 7)]
    public static void TeoremCalculateThrowsException(int n, int k)
    {
		_logger.LogDebug(">>> Entering TeoremCalculateThrowsException(n={N},k={K})", n, k);
        // Arrange

        // Act

        // Assert
		_logger.LogDebug("<<< Exiting TeoremCalculateThrowsException(n={N},k={K})", n, k);
        _ = Assert.Throws<ArgumentException>(() => BinomialCoefficient.Calculate(new BigInteger(n), new BigInteger(k)));
    }
}
