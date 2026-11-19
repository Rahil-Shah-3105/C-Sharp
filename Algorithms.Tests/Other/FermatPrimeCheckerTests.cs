using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public static class FermatPrimeCheckerTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(5, true)]
    [TestCase(2633, true)]
    [TestCase(9439, true)]
    [TestCase(1, false)]
    [TestCase(8, false)]
    public static void IsProbablePrime(int inputNum, bool expected)
    {
		_logger.LogError(">>> Entering IsProbablePrime(inputNum={InputNum},expected={Expected})", inputNum, expected);
        // Arrange
        var random = new Randomizer();
		_logger.LogError("IsProbablePrime(inputNum={InputNum},expected={Expected}): random → {Random}", inputNum, expected, random);
        var times = random.Next(1, 1000);
		_logger.LogError("IsProbablePrime(inputNum={InputNum},expected={Expected}): times → {Times}", inputNum, expected, times);

        // Act
        var result = FermatPrimeChecker.IsPrime(inputNum, times);
		_logger.LogError("IsProbablePrime(inputNum={InputNum},expected={Expected}): result → {Result}", inputNum, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting IsProbablePrime(inputNum={InputNum},expected={Expected})", inputNum, expected);
    }
}
