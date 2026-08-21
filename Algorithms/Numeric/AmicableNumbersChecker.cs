using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
/// Amicable numbers are two different natural numbers related in such a way that the sum of the proper divisors of
/// each is equal to the other number. That is, σ(a)=b+a and σ(b)=a+b, where σ(n) is equal to the sum of positive divisors of n (see also divisor function).
/// See <a href="https://en.wikipedia.org/wiki/Amicable_numbers">here</a> for more info.
/// </summary>
public static class AmicableNumbersChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Checks if two numbers are amicable or not.
    /// </summary>
    /// <param name="x">First number to check.</param>
    /// <param name="y">Second number to check.</param>
    /// <returns>True if they are amicable numbers. False if not.</returns>
    public static bool AreAmicableNumbers(int x, int y)
    {
		_logger.LogDebug(">>> Entering AreAmicableNumbers(x={X},y={Y})", x, y);
        return SumOfDivisors(x) == y && SumOfDivisors(y) == x;
    }

    private static int SumOfDivisors(int number)
    {
		_logger.LogDebug(">>> Entering SumOfDivisors(number={Number})", number);
        var sum = 0; /* sum of its positive divisors */
        for (var i = 1; i < number; ++i)
        {
            if (number % i == 0)
            {
                sum += i;
            }
        }

		_logger.LogDebug("<<< Exiting SumOfDivisors(number={Number})", number);
        return sum;
    }
}
