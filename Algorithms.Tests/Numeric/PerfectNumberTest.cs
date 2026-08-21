using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class PerfectNumberTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(6)]
    [TestCase(28)]
    [TestCase(496)]
    [TestCase(8128)]
    public static void PerfectNumberWork(int number)
    {
		_logger.LogDebug(">>> Entering PerfectNumberWork(number={Number})", number);
        // Arrange

        // Act
        var result = PerfectNumberChecker.IsPerfectNumber(number);
		_logger.LogDebug("PerfectNumberWork(number={Number}): result → {Result}", number, result);

        // Assert
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting PerfectNumberWork(number={Number})", number);
    }

    [TestCase(-2)]
    public static void PerfectNumberShouldThrowEx(int number)
    {
		_logger.LogDebug(">>> Entering PerfectNumberShouldThrowEx(number={Number})", number);
        // Arrange

        // Assert
        Assert.Throws<ArgumentException>(() => PerfectNumberChecker.IsPerfectNumber(number));
		_logger.LogDebug("<<< Exiting PerfectNumberShouldThrowEx(number={Number})", number);
    }
}
