using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class PerfectSquareTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(-4, ExpectedResult = false)]
    [TestCase(4, ExpectedResult = true)]
    [TestCase(9, ExpectedResult = true)]
    [TestCase(10, ExpectedResult = false)]
    [TestCase(16, ExpectedResult = true)]
    [TestCase(70, ExpectedResult = false)]
    [TestCase(81, ExpectedResult = true)]
    public static bool IsPerfectSquare_ResultIsCorrect(int number)
    {
		_logger.LogDebug(">>> Entering IsPerfectSquare_ResultIsCorrect(number={Number})", number);
        // Arrange

        // Act
        var result = PerfectSquareChecker.IsPerfectSquare(number);
		_logger.LogDebug("IsPerfectSquare_ResultIsCorrect(number={Number}): result → {Result}", number, result);

        // Assert
		_logger.LogDebug("<<< Exiting IsPerfectSquare_ResultIsCorrect(number={Number})", number);
        return result;
    }
}
