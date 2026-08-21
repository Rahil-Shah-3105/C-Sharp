using Algorithms.Numeric;
using NUnit.Framework;
using System;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

/// <summary>
/// Tests for the SumOfDigits class.
/// </summary>
public static class SumOfDigitsTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Tests the calculation of the sum of digits for various non-negative integers.
    /// </summary>
    /// <param name="input">The input number.</param>
    /// <param name="expectedSum">The expected sum of its digits.</param>
    [TestCase(0, 0)]
    [TestCase(7, 7)]
    [TestCase(10, 1)]
    [TestCase(42, 6)]
    [TestCase(12345, 15)]
    [TestCase(9999, 36)]
    [TestCase(8675309, 38)]
    [TestCase(2147483647, 46)] // Max value for int
    public static void GetsCorrectSumOfDigits(int input, int expectedSum)
    {
		_logger.LogDebug(">>> Entering GetsCorrectSumOfDigits(input={Input},expectedSum={ExpectedSum})", input, expectedSum);
        // Act
        var result = SumOfDigits.Calculate(input);
		_logger.LogDebug("GetsCorrectSumOfDigits(input={Input},expectedSum={ExpectedSum}): result → {Result}", input, expectedSum, result);

        // Assert
        Assert.That(result, Is.EqualTo(expectedSum));
		_logger.LogDebug("<<< Exiting GetsCorrectSumOfDigits(input={Input},expectedSum={ExpectedSum})", input, expectedSum);
    }

    /// <summary>
    /// Tests that the method throws an ArgumentException when a negative number is provided.
    /// </summary>
    /// <param name="num">The negative input number.</param>
    [TestCase(-1)]
    [TestCase(-100)]
    [TestCase(-54321)]
    public static void ThrowsExceptionForNegativeNumbers(int num)
    {
		_logger.LogDebug(">>> Entering ThrowsExceptionForNegativeNumbers(num={Num})", num);
        // Act
        void Act() => SumOfDigits.Calculate(num);

        // Assert
		_logger.LogDebug("<<< Exiting ThrowsExceptionForNegativeNumbers(num={Num})", num);
        _ = Assert.Throws<ArgumentException>(Act);
		_logger.LogDebug("ThrowsExceptionForNegativeNumbers(num={Num}): _ → {_}", num, _);
    }
}
