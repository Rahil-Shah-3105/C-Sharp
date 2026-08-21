using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Comb sort is a relatively simple sorting algorithm that improves on bubble sort.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class CombSorter<T>(double shrinkFactor = 1.3) : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private double ShrinkFactor { get; } = shrinkFactor;

    /// <summary>
    ///     Sorts array using specified comparer,
    ///     internal, in-place, unstable,
    ///     worst case performance: O(n^2),
    ///     best case performance: O(n log(n)),
    ///     average performance: O(n^2 / 2^p),
    ///     space complexity: O(1),
    ///     where n - array length and p - number of increments.
    ///     See <a href="https://en.wikipedia.org/wiki/Comb_sort">here</a> for more info.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        var gap = array.Length;
        var sorted = false;
		_logger.LogDebug("Sort(array={Array},comparer={Comparer}): sorted → {Sorted}", array, comparer, sorted);
		_logger.LogDebug("Sort(array={Array},comparer={Comparer}): gap → {Gap}", array, comparer, gap);
        while (!sorted)
        {
            gap = (int)Math.Floor(gap / ShrinkFactor);
            if (gap <= 1)
            {
                gap = 1;
                sorted = true;
            }

            for (var i = 0; i < array.Length - gap; i++)
            {
                if (comparer.Compare(array[i], array[i + gap]) > 0)
                {
                    (array[i], array[i + gap]) = (array[i + gap], array[i]);
                    sorted = false;
                }
            }
        }
		_logger.LogDebug("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }
}
