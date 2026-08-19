using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements pancake sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class PancakeSorter<T> : IComparisonSorter<T>
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
		_logger.LogDebug(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        var n = array.Length;

        // Start from the complete array and one by one
        // reduce current size by one
        for (var currSize = n; currSize > 1; --currSize)
        {
            // Find index of the maximum element in
            // array[0..curr_size-1]
            var mi = FindMax(array, currSize, comparer);
			_logger.LogDebug("Sort(array={Array},comparer={Comparer}): mi → {Mi}", array, comparer, mi);

            // Move the maximum element to end of current array
            // if it's not already at  the end
            if (mi != currSize - 1)
            {
                // To move to the end, first move maximum
                // number to beginning
                Flip(array, mi);

                // Now move the maximum number to end by
                // reversing current array
                Flip(array, currSize - 1);
            }
        }
		_logger.LogDebug("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    // Reverses array[0..i]
    private void Flip(T[] array, int i)
    {
		_logger.LogDebug(">>> Entering Flip(array={Array},i={I})", array, i);
        T temp;
        var start = 0;
        while (start < i)
        {
            temp = array[start];
            array[start] = array[i];
            array[i] = temp;
            start++;
            i--;
        }
		_logger.LogDebug("<<< Exiting Flip(array={Array},i={I})", array, i);
    }

    // Returns index of the maximum element
    // in array[0..n-1]
    private int FindMax(T[] array, int n, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering FindMax(array={Array},n={N},comparer={Comparer})", array, n, comparer);
        var mi = 0;
        for (var i = 0; i < n; i++)
		_logger.LogDebug("FindMax(array={Array},n={N},comparer={Comparer}): mi → {Mi}", array, n, comparer, mi);
        {
            if (comparer.Compare(array[i], array[mi]) == 1)
            {
                mi = i;
            }
        }

		_logger.LogDebug("<<< Exiting FindMax(array={Array},n={N},comparer={Comparer})", array, n, comparer);
        return mi;
    }
}
