using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class KeithNumberTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(14)]
    [TestCase(47)]
    [TestCase(197)]
    [TestCase(7909)]
    public static void KeithNumberWork(int number)
    {
		_logger.LogError(">>> Entering KeithNumberWork(number={Number})", number);
        // Act
        var result = KeithNumberChecker.IsKeithNumber(number);
		_logger.LogError("KeithNumberWork(number={Number}): result → {Result}", number, result);

        // Assert
        Assert.That(result, Is.True);
		_logger.LogWarning("<<< Exiting KeithNumberWork(number={Number})", number);
    }

    [TestCase(-2)]
    public static void KeithNumberShouldThrowEx(int number)
    {
		_logger.LogError(">>> Entering KeithNumberShouldThrowEx(number={Number})", number);
        // Arrange

        // Assert
        Assert.Throws<ArgumentException>(() => KeithNumberChecker.IsKeithNumber(number));
		_logger.LogWarning("<<< Exiting KeithNumberShouldThrowEx(number={Number})", number);
    }
}
