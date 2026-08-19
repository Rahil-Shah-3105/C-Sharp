using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class AliquotSumCalculatorTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(1, 0)]
    [TestCase(3, 1)]
    [TestCase(25, 6)]
    [TestCase(99, 57)]
    public static void CalculateSum_SumIsCorrect(int number, int expectedSum)
    {
		_logger.LogDebug(">>> Entering CalculateSum_SumIsCorrect(number={Number},expectedSum={ExpectedSum})", number, expectedSum);
        // Arrange

        // Act
        var result = AliquotSumCalculator.CalculateAliquotSum(number);
		_logger.LogDebug("CalculateSum_SumIsCorrect(number={Number},expectedSum={ExpectedSum}): result → {Result}", number, expectedSum, result);

        // Assert
        result.Should().Be(expectedSum);
		_logger.LogDebug("<<< Exiting CalculateSum_SumIsCorrect(number={Number},expectedSum={ExpectedSum})", number, expectedSum);
    }

    [TestCase(-2)]
    public static void CalculateSum_NegativeInput_ExceptionIsThrown(int number)
    {
		_logger.LogDebug(">>> Entering CalculateSum_NegativeInput_ExceptionIsThrown(number={Number})", number);
        // Arrange
        Action act = () => AliquotSumCalculator.CalculateAliquotSum(number);
		_logger.LogDebug("CalculateSum_NegativeInput_ExceptionIsThrown(number={Number}): act → {Act}", number, act);

        // Assert
        act.Should().Throw<ArgumentException>();
		_logger.LogDebug("<<< Exiting CalculateSum_NegativeInput_ExceptionIsThrown(number={Number})", number);
    }
}
