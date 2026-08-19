using Microsoft.Extensions.Logging;
namespace Algorithms.Search;

/// <summary>
///     Class that implements interpolation search algorithm.
/// </summary>
public static class InterpolationSearch
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds the index of the item searched for in the array.
    ///     Algorithm performance:
    ///     worst-case: O(n),
    ///     average-case: O(log(log(n))),
    ///     best-case: O(1).
    /// </summary>
    /// <param name="sortedArray">Array with sorted elements to be searched in. Cannot be null.</param>
    /// <param name="val">Value to be searched for. Cannot be null.</param>
    /// <returns>If an item is found, return index, else return -1.</returns>
    public static int FindIndex(int[] sortedArray, int val)
    {
		_logger.LogDebug(">>> Entering FindIndex(sortedArray={SortedArray},val={Val})", sortedArray, val);
        var start = 0;
        var end = sortedArray.Length - 1;
		_logger.LogDebug("FindIndex(sortedArray={SortedArray},val={Val}): end → {End}", sortedArray, val, end);

        while (start <= end && val >= sortedArray[start] && val <= sortedArray[end])
        {
            var denominator = (sortedArray[end] - sortedArray[start]) * (val - sortedArray[start]);
			_logger.LogDebug("FindIndex(sortedArray={SortedArray},val={Val}): denominator → {Denominator}", sortedArray, val, denominator);

            if (denominator == 0)
            {
                denominator = 1;
            }

            var pos = start + (end - start) / denominator;
			_logger.LogDebug("FindIndex(sortedArray={SortedArray},val={Val}): pos → {Pos}", sortedArray, val, pos);

            if (sortedArray[pos] == val)
            {
                return pos;
            }

            if (sortedArray[pos] < val)
            {
                start = pos + 1;
            }
            else
            {
                end = pos - 1;
            }
        }

		_logger.LogDebug("<<< Exiting FindIndex(sortedArray={SortedArray},val={Val})", sortedArray, val);
        return -1;
    }
}
