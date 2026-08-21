using Microsoft.Extensions.Logging;
namespace Algorithms.Numeric;

/// <summary>
///     Find the absolute value of a number.
/// </summary>
public static class Abs
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///    Returns the absolute value of a number.
    /// </summary>
    /// <typeparam name="T">Type of number.</typeparam>
    /// <param name="inputNum">Number to find the absolute value of.</param>
    /// <returns>Absolute value of the number.</returns>
    public static T AbsVal<T>(T inputNum) where T : INumber<T>
    {
		_logger.LogDebug(">>> Entering AbsVal(inputNum={InputNum})", inputNum);
        return T.IsNegative(inputNum) ? -inputNum : inputNum;
    }

    /// <summary>
    ///   Returns the number with the smallest absolute value on the input array.
    /// </summary>
    /// <typeparam name="T">Type of number.</typeparam>
    /// <param name="inputNums">Array of numbers to find the smallest absolute.</param>
    /// <returns>Smallest absolute number.</returns>
    public static T AbsMin<T>(T[] inputNums) where T : INumber<T>
    {
		_logger.LogDebug(">>> Entering AbsMin(inputNums={InputNums})", inputNums);
        if (inputNums.Length == 0)
        {
			_logger.LogWarning("Throwing ArgumentException from AbsMin(inputNums={InputNums})", inputNums);
            throw new ArgumentException("Array is empty.");
        }

        var min = inputNums[0];
		_logger.LogDebug("AbsMin(inputNums={InputNums}): min → {Min}", inputNums, min);
        for (var index = 1; index < inputNums.Length; index++)
        {
            var current = inputNums[index];
            if (AbsVal(current).CompareTo(AbsVal(min)) < 0)
            {
                min = current;
            }
        }

		_logger.LogDebug("<<< Exiting AbsMin(inputNums={InputNums})", inputNums);
        return min;
    }

    /// <summary>
    ///  Returns the number with the largest absolute value on the input array.
    /// </summary>
    /// <typeparam name="T">Type of number.</typeparam>
    /// <param name="inputNums">Array of numbers to find the largest absolute.</param>
    /// <returns>Largest absolute number.</returns>
    public static T AbsMax<T>(T[] inputNums) where T : INumber<T>
    {
		_logger.LogDebug(">>> Entering AbsMax(inputNums={InputNums})", inputNums);
        if (inputNums.Length == 0)
        {
			_logger.LogWarning("Throwing ArgumentException from AbsMax(inputNums={InputNums})", inputNums);
            throw new ArgumentException("Array is empty.");
        }

        var max = inputNums[0];
		_logger.LogDebug("AbsMax(inputNums={InputNums}): max → {Max}", inputNums, max);
        for (var index = 1; index < inputNums.Length; index++)
        {
            var current = inputNums[index];
            if (AbsVal(current).CompareTo(AbsVal(max)) > 0)
            {
                max = current;
            }
        }

		_logger.LogDebug("<<< Exiting AbsMax(inputNums={InputNums})", inputNums);
        return max;
    }
}
