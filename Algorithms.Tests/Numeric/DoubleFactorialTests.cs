using System.Numerics;
using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

/// <summary>
/// Tests for the DoubleFactorial class methods.
/// </summary>
public static class DoubleFactorialTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Tests the calculation of double factorial for non-negative integers.
    /// Includes base cases (0, 1), odd numbers (5, 11), even numbers (6, 12), 
    /// and a large number (20) that benefits from BigInteger.
    /// </summary>
    /// <param name="input">The number N to calculate N!!.</param>
    /// <param name="expected">The expected result as a string (for BigInteger parsing).</param>
    [TestCase(0, "1")]     // Base Case: 0!! = 1
    [TestCase(1, "1")]     // Base Case: 1!! = 1
    [TestCase(5, "15")]    // Odd: 5 * 3 * 1 = 15
    [TestCase(6, "48")]    // Even: 6 * 4 * 2 = 48
    [TestCase(11, "10395")]// Larger Odd: 11 * 9 * 7 * 5 * 3 * 1 = 10395
    [TestCase(12, "46080")] // Larger Even: 12 * 10 * 8 * 6 * 4 * 2 = 46080
    [TestCase(20, "3715891200")] // Large Even
    public static void GetsDoubleFactorial(int input, string expected)
    {
		_logger.LogDebug(">>> Entering GetsDoubleFactorial(input={Input},expected={Expected})", input, expected);
        // Arrange
        BigInteger expectedBigInt = BigInteger.Parse(expected);
		_logger.LogDebug("GetsDoubleFactorial(input={Input},expected={Expected}): expectedBigInt → {ExpectedBigInt}", input, expected, expectedBigInt);

        // Act
        var result = DoubleFactorial.Calculate(input);
		_logger.LogDebug("GetsDoubleFactorial(input={Input},expected={Expected}): result → {Result}", input, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expectedBigInt));
		_logger.LogDebug("<<< Exiting GetsDoubleFactorial(input={Input},expected={Expected})", input, expected);
    }

    /// <summary>
    /// Tests that calculating double factorial for negative numbers throws an ArgumentException.
    /// </summary>
    /// <param name="num">A negative integer input.</param>
    [TestCase(-1)]
    [TestCase(-5)]
    [TestCase(-10)]
    public static void GetsDoubleFactorialExceptionForNegativeNumbers(int num)
    {
		_logger.LogDebug(">>> Entering GetsDoubleFactorialExceptionForNegativeNumbers(num={Num})", num);
        // Arrange

        // Act
        void Act() => DoubleFactorial.Calculate(num);

        // Assert
		_logger.LogDebug("<<< Exiting GetsDoubleFactorialExceptionForNegativeNumbers(num={Num})", num);
        _ = Assert.Throws<ArgumentException>(Act);
		_logger.LogDebug("GetsDoubleFactorialExceptionForNegativeNumbers(num={Num}): _ → {_}", num, _);
    }
}