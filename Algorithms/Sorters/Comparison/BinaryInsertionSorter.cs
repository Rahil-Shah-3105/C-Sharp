using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements binary insertion sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class BinaryInsertionSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts array using specified comparer,
    ///     variant of insertion sort where binary search is used to find place for next element
    ///     internal, in-place, unstable,
    ///     time complexity: O(n^2),
    ///     space complexity: O(1),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        for (var i = 1; i < array.Length; i++)
        {
            var target = array[i];
            var moveIndex = i - 1;
            var targetInsertLocation = BinarySearch(array, 0, moveIndex, target, comparer);
			_logger.LogDebug("Sort(array={Array},comparer={Comparer}): targetInsertLocation → {TargetInsertLocation}", array, comparer, targetInsertLocation);
            Array.Copy(array, targetInsertLocation, array, targetInsertLocation + 1, i - targetInsertLocation);

            array[targetInsertLocation] = target;
        }
		_logger.LogDebug("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    /// <summary>Implementation of Binary Search using an iterative approach.</summary>
    /// <param name="array">
    ///     An array of values sorted in ascending order between the index values left and right to search
    ///     through.
    /// </param>
    /// <param name="from">Left index to search from (inclusive).</param>
    /// <param name="to">Right index to search to (inclusive).</param>
    /// <param name="target">The value to find placefor in the provided array.</param>
    /// <param name="comparer">Compares elements.</param>
    /// <returns>The index where to insert target value.</returns>
    private static int BinarySearch(T[] array, int from, int to, T target, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering BinarySearch(array={Array},from={From},to={To},target={Target},comparer={Comparer})", array, from, to, target, comparer);
        var left = from;
        var right = to;
		_logger.LogDebug("BinarySearch(array={Array},from={From},to={To},target={Target},comparer={Comparer}): right → {Right}", array, from, to, target, comparer, right);
		_logger.LogDebug("BinarySearch(array={Array},from={From},to={To},target={Target},comparer={Comparer}): left → {Left}", array, from, to, target, comparer, left);
        while (right > left)
        {
            var middle = (left + right) / 2;
			_logger.LogDebug("BinarySearch(array={Array},from={From},to={To},target={Target},comparer={Comparer}): middle → {Middle}", array, from, to, target, comparer, middle);
            var comparisonResult = comparer.Compare(target, array[middle]);
			_logger.LogDebug("BinarySearch(array={Array},from={From},to={To},target={Target},comparer={Comparer}): comparisonResult → {ComparisonResult}", array, from, to, target, comparer, comparisonResult);

            if (comparisonResult == 0)
            {
                return middle + 1;
            }

            if (comparisonResult > 0)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

		_logger.LogDebug("<<< Exiting BinarySearch(array={Array},from={From},to={To},target={Target},comparer={Comparer})", array, from, to, target, comparer);
        return comparer.Compare(target, array[left]) < 0 ? left : left + 1;
    }
}
