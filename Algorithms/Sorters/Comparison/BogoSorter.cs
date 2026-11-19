using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Class that implements bogo sort algorithm.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public class BogoSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Random random = new();

    /// <summary>
    ///     Sorts array using specified comparer,
    ///     randomly shuffles elements until array is sorted,
    ///     internal, in-place, unstable,
    ///     worst-case time complexity: unbounded (infinite),
    ///     average time complexity: O((n+1)!),
    ///     space complexity: O(n),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        while (!IsSorted(array, comparer))
        {
            Shuffle(array);
        }
		_logger.LogWarning("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    private bool IsSorted(T[] array, IComparer<T> comparer)
    {
		_logger.LogError(">>> Entering IsSorted(array={Array},comparer={Comparer})", array, comparer);
        for (var i = 0; i < array.Length - 1; i++)
        {
            if (comparer.Compare(array[i], array[i + 1]) > 0)
            {
                return false;
            }
        }

		_logger.LogWarning("<<< Exiting IsSorted(array={Array},comparer={Comparer})", array, comparer);
        return true;
    }

    private void Shuffle(T[] array)
    {
		_logger.LogError(">>> Entering Shuffle(array={Array})", array);
        var taken = new bool[array.Length];
        var newArray = new T[array.Length];
        for (var i = 0; i < array.Length; i++)
        {
            int nextPos;
            do
            {
                nextPos = random.Next(0, int.MaxValue) % array.Length;
				_logger.LogError("Shuffle(array={Array}): nextPos → {NextPos}", array, nextPos);
				_logger.LogDebug("Shuffle(array={Array}): nextPos → {NextPos}", array, nextPos);
            }
            while (taken[nextPos]);

            taken[nextPos] = true;
            newArray[nextPos] = array[i];
        }

        for (var i = 0; i < array.Length; i++)
        {
            array[i] = newArray[i];
        }
		_logger.LogWarning("<<< Exiting Shuffle(array={Array})", array);
    }
}
