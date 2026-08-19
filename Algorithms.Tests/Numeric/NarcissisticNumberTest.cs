using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class NarcissisticNumberTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(2, ExpectedResult = true)]
    [TestCase(3, ExpectedResult = true)]
    [TestCase(28, ExpectedResult = false)]
    [TestCase(153, ExpectedResult = true)]
    [TestCase(170, ExpectedResult = false)]
    [TestCase(371, ExpectedResult = true)]
    public static bool NarcissisticNumberWork(int number)
    {
		_logger.LogDebug(">>> Entering NarcissisticNumberWork(number={Number})", number);
        // Arrange

        // Act
        var result = NarcissisticNumberChecker.IsNarcissistic(number);
		_logger.LogDebug("NarcissisticNumberWork(number={Number}): result → {Result}", number, result);

        // Assert
		_logger.LogDebug("<<< Exiting NarcissisticNumberWork(number={Number})", number);
        return result;
    }
}
