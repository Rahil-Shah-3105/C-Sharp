using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements gnome sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class GnomeSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Moves forward through the array until it founds two elements out of order,
    ///     then swaps them and move back one position,
    ///     internal, in-place, stable,
    ///     time complexity: O(n2),
    ///     space complexity: O(1).
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        int index = 0;

        while (index < array.Length)
        {
            if (index == 0 || comparer.Compare(array[index], array[index - 1]) >= 0)
            {
                index++;
            }
            else
            {
                Swap(array, index, index - 1);
                index--;
            }
        }
		_logger.LogDebug("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    public void Swap(T[] array, int index1, int index2)
    {
		_logger.LogDebug(">>> Entering Swap(array={Array},index1={Index1},index2={Index2})", array, index1, index2);
        (array[index1], array[index2]) = (array[index2], array[index1]);
		_logger.LogDebug("<<< Exiting Swap(array={Array},index1={Index1},index2={Index2})", array, index1, index2);
    }
}
