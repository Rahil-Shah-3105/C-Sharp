using Microsoft.Extensions.Logging;
namespace Algorithms.Search;

/// <summary>
///     Binary Searcher checks an array for element specified by checking
///     if element is greater or less than the half being checked.
///     time complexity: O(log(n)),
///     space complexity: O(1).
///     Note: Array must be sorted beforehand.
/// </summary>
/// <typeparam name="T">Type of element stored inside array. 2.</typeparam>
public class BinarySearcher<T> where T : IComparable<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Finds index of an array by using binary search.
    /// </summary>
    /// <param name="sortedData">Sorted array to search in.</param>
    /// <param name="item">Item to search for.</param>
    /// <returns>Index of item that equals to item searched for or -1 if none found.</returns>
    public int FindIndex(T[] sortedData, T item)
    {
		_logger.LogDebug(">>> Entering FindIndex(sortedData={SortedData},item={Item})", sortedData, item);
        var leftIndex = 0;
        var rightIndex = sortedData.Length - 1;
		_logger.LogDebug("FindIndex(sortedData={SortedData},item={Item}): rightIndex → {RightIndex}", sortedData, item, rightIndex);
		_logger.LogDebug("FindIndex(sortedData={SortedData},item={Item}): leftIndex → {LeftIndex}", sortedData, item, leftIndex);

        while (leftIndex <= rightIndex)
        {
            var middleIndex = leftIndex + (rightIndex - leftIndex) / 2;
			_logger.LogDebug("FindIndex(sortedData={SortedData},item={Item}): middleIndex → {MiddleIndex}", sortedData, item, middleIndex);

            if (item.CompareTo(sortedData[middleIndex]) > 0)
            {
                leftIndex = middleIndex + 1;
                continue;
            }

            if (item.CompareTo(sortedData[middleIndex]) < 0)
            {
                rightIndex = middleIndex - 1;
                continue;
            }

		_logger.LogDebug("<<< Exiting FindIndex(sortedData={SortedData},item={Item})", sortedData, item);
            return middleIndex;
        }

        return -1;
    }
}
