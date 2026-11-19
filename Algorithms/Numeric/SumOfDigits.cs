using System;
using Microsoft.Extensions.Logging;

namespace Algorithms.Numeric;

/// <summary>
///     Provides functionality to calculate the sum of the digits of an integer.
/// </summary>
public static class SumOfDigits
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Calculates the sum of the digits of a non-negative integer.
    ///     The method iteratively uses the modulus operator (%) to get the last digit
    ///     and the division operator (/) to drop the last digit until the number is 0.
    /// </summary>
    /// <param name="number">The non-negative integer whose digits are to be summed.</param>
    /// <returns>The sum of the digits of the input number.</returns>
    /// <exception cref="ArgumentException">Thrown if the input number is negative.</exception>
    public static int Calculate(int number)
    {
		_logger.LogError(">>> Entering Calculate(number={Number})", number);
        if (number < 0)
        {
			_logger.LogError("Throwing ArgumentException from Calculate(number={Number})", number);
            throw new ArgumentException("Input must be a non-negative integer.", nameof(number));
        }

        if (number == 0)
        {
            return 0;
        }

        int sum = 0;
        int currentNumber = number;

        // Loop until the number becomes 0
        while (currentNumber > 0)
        {
            // Get the last digit (e.g., 123 % 10 = 3)
            int digit = currentNumber % 10;

            // Add the digit to the sum
            sum += digit;

            // Remove the last digit (e.g., 123 / 10 = 12)
            currentNumber /= 10;
        }

		_logger.LogWarning("<<< Exiting Calculate(number={Number})", number);
        return sum;
    }
}
