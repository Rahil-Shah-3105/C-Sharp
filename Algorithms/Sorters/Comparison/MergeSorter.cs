using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Divide and Conquer algorithm, which splits
///     array in two halves, calls itself for the two
///     halves and then merges the two sorted halves.
/// </summary>
/// <typeparam name="T">Type of array elements.</typeparam>
public class MergeSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts array using merge sort algorithm,
    ///     originally designed as external sorting algorithm,
    ///     internal, stable,
    ///     time complexity: O(n log(n)),
    ///     space complexity: O(n),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Comparer to compare elements of <paramref name="array" />.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        if (array.Length <= 1)
        {
            return;
        }

        var (left, right) = Split(array);
        Sort(left, comparer);
        Sort(right, comparer);
        Merge(array, left, right, comparer);
		_logger.LogDebug("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    private static void Merge(T[] array, T[] left, T[] right, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Merge(array={Array},left={Left},right={Right},comparer={Comparer})", array, left, right, comparer);
        var mainIndex = 0;
        var leftIndex = 0;
        var rightIndex = 0;

        while (leftIndex < left.Length && rightIndex < right.Length)
        {
            var compResult = comparer.Compare(left[leftIndex], right[rightIndex]);
			_logger.LogDebug("Merge(array={Array},left={Left},right={Right},comparer={Comparer}): compResult → {CompResult}", array, left, right, comparer, compResult);
            array[mainIndex++] = compResult <= 0 ? left[leftIndex++] : right[rightIndex++];
        }

        while (leftIndex < left.Length)
        {
            array[mainIndex++] = left[leftIndex++];
        }

        while (rightIndex < right.Length)
        {
            array[mainIndex++] = right[rightIndex++];
        }
		_logger.LogDebug("<<< Exiting Merge(array={Array},left={Left},right={Right},comparer={Comparer})", array, left, right, comparer);
    }

    private static (T[] Left, T[] Right) Split(T[] array)
    {
		_logger.LogDebug(">>> Entering Split(Left={Left},Right={Right})", Left, Right);
        var mid = array.Length / 2;
		_logger.LogDebug("<<< Exiting Split(Left={Left},Right={Right})", Left, Right);
        return (array.Take(mid).ToArray(), array.Skip(mid).ToArray());
    }
}
