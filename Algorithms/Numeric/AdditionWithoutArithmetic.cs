using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     Add the integers without arithmetic operation.
/// </summary>
public static class AdditionWithoutArithmetic
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///    Returns the sum of two integers.
    /// </summary>
    /// <param name="first">First number to add.</param>
    /// <param name="second">Second number to add.</param>
    /// <returns>Sum of the two numbers.</returns>
    public static int CalculateAdditionWithoutArithmetic(int first, int second)
    {
		_logger.LogDebug(">>> Entering CalculateAdditionWithoutArithmetic(first={First},second={Second})", first, second);
        while (second != 0)
        {
            int c = first & second;      // Carry
            first ^= second;             // Sum without carry
            second = c << 1;            // Carry shifted left
        }

		_logger.LogDebug("<<< Exiting CalculateAdditionWithoutArithmetic(first={First},second={Second})", first, second);
        return first;
    }
}
