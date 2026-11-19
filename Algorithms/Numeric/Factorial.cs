using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     The factorial of a positive integer n, denoted by n!,
///     is the product of all positive integers less than or equal to n.
/// </summary>
public static class Factorial
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Calculates factorial of a integer number.
    /// </summary>
    /// <param name="inputNum">Integer Input number.</param>
    /// <returns>Factorial of integer input number.</returns>
    public static BigInteger Calculate(int inputNum)
    {
		_logger.LogError(">>> Entering Calculate(inputNum={InputNum})", inputNum);
        // Convert integer input to BigInteger
        BigInteger num = new BigInteger(inputNum);
		_logger.LogError("Calculate(inputNum={InputNum}): num → {Num}", inputNum, num);

        // Don't calculate factorial if input is a negative number.
        if (BigInteger.Compare(num, BigInteger.Zero) < 0)
        {
			_logger.LogError("Throwing ArgumentException from Calculate(inputNum={InputNum})", inputNum);
            throw new ArgumentException("Only for num >= 0");
        }

        // Factorial of numbers greater than 0.
        BigInteger result = BigInteger.One;
		_logger.LogError("Calculate(inputNum={InputNum}): result → {Result}", inputNum, result);

        for (BigInteger i = BigInteger.One; BigInteger.Compare(i, num) <= 0; i = BigInteger.Add(i, BigInteger.One))
        {
            result = BigInteger.Multiply(result, i);
        }

		_logger.LogWarning("<<< Exiting Calculate(inputNum={InputNum})", inputNum);
        return result;
    }
}
