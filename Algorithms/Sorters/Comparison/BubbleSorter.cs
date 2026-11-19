using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements bubble sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class BubbleSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts array using specified comparer,
    ///     internal, in-place, stable,
    ///     time complexity: O(n^2),
    ///     space complexity: O(1),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        for (var i = 0; i < array.Length - 1; i++)
        {
            var wasChanged = false;
            for (var j = 0; j < array.Length - i - 1; j++)
            {
                if (comparer.Compare(array[j], array[j + 1]) > 0)
                {
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                    wasChanged = true;
					_logger.LogError("Sort(array={Array},comparer={Comparer}): wasChanged → {WasChanged}", array, comparer, wasChanged);
                }
            }

            if (!wasChanged)
            {
                break;
            }
        }
		_logger.LogWarning("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }
}
