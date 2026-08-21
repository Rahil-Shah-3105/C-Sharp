using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class FactorialTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(0, "1")]
    [TestCase(1, "1")]
    [TestCase(4, "24")]
    [TestCase(10, "3628800")]
    [TestCase(18, "6402373705728000")]
    public static void GetsFactorial(int input, string expected)
    {
		_logger.LogDebug(">>> Entering GetsFactorial(input={Input},expected={Expected})", input, expected);
        // Arrange
        BigInteger expectedBigInt = BigInteger.Parse(expected);
		_logger.LogDebug("GetsFactorial(input={Input},expected={Expected}): expectedBigInt → {ExpectedBigInt}", input, expected, expectedBigInt);

        // Act
        var result = Factorial.Calculate(input);
		_logger.LogDebug("GetsFactorial(input={Input},expected={Expected}): result → {Result}", input, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expectedBigInt));
		_logger.LogDebug("<<< Exiting GetsFactorial(input={Input},expected={Expected})", input, expected);
    }

    [TestCase(-5)]
    [TestCase(-10)]
    public static void GetsFactorialExceptionForNegativeNumbers(int num)
    {
		_logger.LogDebug(">>> Entering GetsFactorialExceptionForNegativeNumbers(num={Num})", num);
        // Arrange

        // Act
        void Act() => Factorial.Calculate(num);

        // Assert
		_logger.LogDebug("<<< Exiting GetsFactorialExceptionForNegativeNumbers(num={Num})", num);
        _ = Assert.Throws<ArgumentException>(Act);
		_logger.LogDebug("GetsFactorialExceptionForNegativeNumbers(num={Num}): _ → {_}", num, _);
    }
}
