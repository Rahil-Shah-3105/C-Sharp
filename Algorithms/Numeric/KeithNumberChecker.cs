using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///  In number theory, a Keith number or repfigit number is a natural number n in a given number base b with k digits such that
///  when a sequence is created such that the first k terms are the k digits of n and each subsequent term is the sum of the
///  previous k terms, n is part of the sequence.
/// </summary>
public static class KeithNumberChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Checks if a number is a Keith number or not.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if it is a Keith number; False otherwise.</returns>
    public static bool IsKeithNumber(int number)
    {
		_logger.LogDebug(">>> Entering IsKeithNumber(number={Number})", number);
        if (number < 0)
        {
			_logger.LogWarning("Throwing ArgumentException from IsKeithNumber(number={Number})", number);
            throw new ArgumentException($"{nameof(number)} cannot be negative");
        }

        var tempNumber = number;

        var stringNumber = number.ToString();
		_logger.LogDebug("IsKeithNumber(number={Number}): stringNumber → {StringNumber}", number, stringNumber);

        var digitsInNumber = stringNumber.Length;

        /* storing the terms of the series */
        var termsArray = new int[number];

        for (var i = digitsInNumber - 1; i >= 0; i--)
        {
            termsArray[i] = tempNumber % 10;
            tempNumber /= 10;
        }

        var sum = 0;
        var k = digitsInNumber;
        while (sum < number)
        {
            sum = 0;

            for (var j = 1; j <= digitsInNumber; j++)
            {
                sum += termsArray[k - j];
            }

            termsArray[k] = sum;
            k++;
        }

		_logger.LogDebug("<<< Exiting IsKeithNumber(number={Number})", number);
        return sum == number;
    }
}
