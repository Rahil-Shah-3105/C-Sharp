using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     A perfect square is an element of algebraic structure that is equal to the square of another element.
/// </summary>
public static class PerfectSquareChecker
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Checks if a number is a perfect square or not.
    /// </summary>
    /// <param name="number">Number too check.</param>
    /// <returns>True if is a perfect square; False otherwise.</returns>
    public static bool IsPerfectSquare(int number)
    {
		_logger.LogDebug(">>> Entering IsPerfectSquare(number={Number})", number);
        if (number < 0)
        {
            return false;
        }

        var sqrt = (int)Math.Sqrt(number);
		_logger.LogDebug("IsPerfectSquare(number={Number}): sqrt → {Sqrt}", number, sqrt);
		_logger.LogDebug("<<< Exiting IsPerfectSquare(number={Number})", number);
        return sqrt * sqrt == number;
    }
}
