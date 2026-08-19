using Microsoft.Extensions.Logging;
namespace DataStructures.Hashing.NumberTheory;

/// <summary>
/// Class for prime number operations.
/// </summary>
/// <remarks>
/// A prime number is a natural number greater than 1 that is not a product of two smaller natural numbers.
/// </remarks>
public static class PrimeNumber
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    /// Checks if a number is prime or not.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if number is prime, false otherwise.</returns>
    public static bool IsPrime(int number)
    {
		_logger.LogDebug(">>> Entering IsPrime(number={Number})", number);
        if (number <= 1)
        {
            return false;
        }

        if (number <= 3)
        {
            return true;
        }

        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }

        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
            {
                return false;
            }
        }

		_logger.LogDebug("<<< Exiting IsPrime(number={Number})", number);
        return true;
    }

    /// <summary>
    /// Gets the next prime number.
    /// </summary>
    /// <param name="number">Number to start from.</param>
    /// <param name="factor">Factor to multiply the number by.</param>
    /// <param name="desc">True to get the previous prime number, false otherwise.</param>
    /// <returns>The next prime number.</returns>
    public static int NextPrime(int number, int factor = 1, bool desc = false)
    {
		_logger.LogDebug(">>> Entering NextPrime(number={Number},factor={Factor},desc={Desc})", number, factor, desc);
        number = factor * number;
        int firstValue = number;

        while (!IsPrime(number))
        {
            number += desc ? -1 : 1;
        }

        if (number == firstValue)
        {
		_logger.LogDebug("<<< Exiting NextPrime(number={Number},factor={Factor},desc={Desc})", number, factor, desc);
            return NextPrime(
                number + (desc ? -1 : 1),
                factor,
                desc);
        }

        return number;
    }
}
