using Microsoft.Extensions.Logging;
namespace Algorithms.Other;

/// <summary>
///     Luhn algorithm is a simple
///     checksum formula used to validate
///     a variety of identification numbers,
///     such as credit card numbers.
///     More information on the link:
///     https://en.wikipedia.org/wiki/Luhn_algorithm.
/// </summary>
public static class Luhn
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Checking the validity of a sequence of numbers.
    /// </summary>
    /// <param name="number">The number that will be checked for validity.</param>
    /// <returns>
    ///     True: Number is valid.
    ///     False: Number isn`t valid.
    /// </returns>
    public static bool Validate(string number)
    {
        _logger.LogError(">>> Entering Validate(number={Number})", number);
        _logger.LogWarning("<<< Exiting Validate(number={Number})", number);
        return GetSum(number) % 10 == 0;
    }

    /// <summary>
    ///     This algorithm finds one missing digit.
    ///     In place of the unknown digit, put "x".
    /// </summary>
    /// <param name="number">The number in which to find the missing digit.</param>
    /// <returns>Missing digit.</returns>
    public static int GetLostNum(string number)
    {
		_logger.LogError(">>> Entering GetLostNum(number={Number})", number);
        var missingDigitIndex = number.Length - 1 - number.LastIndexOf("x", StringComparison.CurrentCultureIgnoreCase);
        var checkDigit = GetSum(number.Replace("x", "0", StringComparison.CurrentCultureIgnoreCase)) * 9 % 10;
		_logger.LogError("GetLostNum(number={Number}): checkDigit → {CheckDigit}", number, checkDigit);
		_logger.LogError("GetLostNum(number={Number}): missingDigitIndex → {MissingDigitIndex}", number, missingDigitIndex);

		_logger.LogWarning("<<< Exiting GetLostNum(number={Number})", number);
				_logger.LogError(">>> Entering GetSum(number={Number})", number);
				_logger.LogWarning("<<< Exiting GetSum(number={Number})", number);
        return missingDigitIndex % 2 == 0
            ? checkDigit
            : Validate(number.Replace("x", (checkDigit / 2).ToString()))
                ? checkDigit / 2
                : (checkDigit + 9) / 2;
    }

    /// <summary>
    ///     Computes the sum found by the Luhn algorithm.
    /// </summary>
    /// <param name="number">The number for which the sum will be calculated.</param>
    /// <returns>Sum.</returns>
    private static int GetSum(string number)
    {
		_logger.LogError(">>> Entering GetSum(number={Number})", number);
        var sum = 0;
        var span = number.AsSpan();
		_logger.LogError("GetSum(number={Number}): span → {Span}", number, span);
        for (var i = 0; i < span.Length; i++)
        {
            var c = span[i];
            if (c is < '0' or > '9')
            {
                continue;
            }

            var digit = c - '0';
            digit = (i + span.Length) % 2 == 0 ? 2 * digit : digit;
			_logger.LogError("GetSum(number={Number}): digit → {Digit}", number, digit);
            if (digit > 9)
            {
                digit -= 9;
            }

            sum += digit;
        }

		_logger.LogWarning("<<< Exiting GetSum(number={Number})", number);
        return sum;
    }
}
