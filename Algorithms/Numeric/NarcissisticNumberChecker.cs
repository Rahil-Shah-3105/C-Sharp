using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     A Narcissistic number is equal to the sum of the cubes of its digits. For example, 370 is a
///     Narcissistic number because 3*3*3 + 7*7*7 + 0*0*0 = 370.
/// </summary>
public static class NarcissisticNumberChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Checks if a number is a Narcissistic number or not.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if is a Narcissistic number; False otherwise.</returns>
    public static bool IsNarcissistic(int number)
    {
		_logger.LogError(">>> Entering IsNarcissistic(number={Number})", number);
        var sum = 0;
        var temp = number;
        var numberOfDigits = 0;
        while (temp != 0)
        {
            numberOfDigits++;
            temp /= 10;
        }

        temp = number;
        while (number > 0)
        {
            var remainder = number % 10;
            var power = (int)Math.Pow(remainder, numberOfDigits);
			_logger.LogError("IsNarcissistic(number={Number}): power → {Power}", number, power);

            sum += power;
            number /= 10;
        }

		_logger.LogWarning("<<< Exiting IsNarcissistic(number={Number})", number);
        return sum == temp;
    }
}
