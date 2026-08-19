using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class AmicableNumbersTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(220, 284)]
    [TestCase(1184, 1210)]
    [TestCase(2620, 2924)]
    [TestCase(5020, 5564)]
    public static void AmicableNumbersChecker_Test(int x, int y)
    {
		_logger.LogDebug(">>> Entering AmicableNumbersChecker_Test(x={X},y={Y})", x, y);
        // Arrange

        // Act
        var result = AmicableNumbersChecker.AreAmicableNumbers(x, y);
		_logger.LogDebug("AmicableNumbersChecker_Test(x={X},y={Y}): result → {Result}", x, y, result);

        // Assert
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting AmicableNumbersChecker_Test(x={X},y={Y})", x, y);
    }
}
