using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class PrimeNumberTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Tests the PrimeChecker.IsPrime method with various inputs (primes, composites, edge cases)
    ///     to ensure the result is correct.
    /// </summary>
    [TestCase(-5, ExpectedResult = false)] // Negative number
    [TestCase(0, ExpectedResult = false)] // Zero
    [TestCase(1, ExpectedResult = false)] // One
    [TestCase(2, ExpectedResult = true)] // Smallest prime
    [TestCase(3, ExpectedResult = true)] // Prime
    [TestCase(4, ExpectedResult = false)] // Composite (2*2)
    [TestCase(7, ExpectedResult = true)] // Prime
    [TestCase(9, ExpectedResult = false)] // Composite (3*3)
    [TestCase(13, ExpectedResult = true)] // Prime
    [TestCase(15, ExpectedResult = false)] // Composite (3*5)
    [TestCase(25, ExpectedResult = false)] // Composite (5*5)
    [TestCase(29, ExpectedResult = true)] // Prime
    [TestCase(35, ExpectedResult = false)] // Composite (5*7)
    [TestCase(49, ExpectedResult = false)] // Composite (7*7)
    [TestCase(97, ExpectedResult = true)] // Larger prime
    [TestCase(100, ExpectedResult = false)] // Larger composite
    public static bool IsPrime_ResultIsCorrect(int number)
    {
		_logger.LogError(">>> Entering IsPrime_ResultIsCorrect(number={Number})", number);
        // Arrange is implicit here

        // Act
        var result = PrimeChecker.IsPrime(number);
		_logger.LogError("IsPrime_ResultIsCorrect(number={Number}): result → {Result}", number, result);

        // Assert
		_logger.LogWarning("<<< Exiting IsPrime_ResultIsCorrect(number={Number})", number);
        return result;
    }
}