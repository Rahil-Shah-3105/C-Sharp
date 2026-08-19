using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements Shell sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class ShellSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts array using specified comparer,
    ///     based on bubble sort,
    ///     internal, in-place, unstable,
    ///     worst-case time complexity: O(n^2),
    ///     space complexity: O(1),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        for (var step = array.Length / 2; step > 0; step /= 2)
        {
            for (var i = 0; i < step; i++)
            {
                GappedBubbleSort(array, comparer, i, step);
            }
        }
		_logger.LogDebug("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    private static void GappedBubbleSort(T[] array, IComparer<T> comparer, int start, int step)
    {
		_logger.LogDebug(">>> Entering GappedBubbleSort(array={Array},comparer={Comparer},start={Start},step={Step})", array, comparer, start, step);
        for (var j = start; j < array.Length - step; j += step)
        {
            var wasChanged = false;
			_logger.LogDebug("GappedBubbleSort(array={Array},comparer={Comparer},start={Start},step={Step}): wasChanged → {WasChanged}", array, comparer, start, step, wasChanged);
            for (var k = start; k < array.Length - j - step; k += step)
            {
                if (comparer.Compare(array[k], array[k + step]) > 0)
                {
                    var temp = array[k];
                    array[k] = array[k + step];
                    array[k + step] = temp;
                    wasChanged = true;
                }
            }

            if (!wasChanged)
            {
                break;
            }
        }
		_logger.LogDebug("<<< Exiting GappedBubbleSort(array={Array},comparer={Comparer},start={Start},step={Step})", array, comparer, start, step);
    }
}
