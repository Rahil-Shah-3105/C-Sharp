using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Cycle sort is an in-place, unstable sorting algorithm,
///     a comparison sort that is theoretically optimal in terms of the total
///     number of writes to the original array.
///     It is based on the idea that the permutation to be sorted can be factored
///     into cycles, which can individually be rotated to give a sorted result.
/// </summary>
/// <typeparam name="T">Type array input.</typeparam>
public class CycleSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts input array using Cycle sort.
    /// </summary>
    /// <param name="array">Input array.</param>
    /// <param name="comparer">Integer comparer.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        for (var i = 0; i < array.Length - 1; i++)
        {
            MoveCycle(array, i, comparer);
        }
		_logger.LogDebug("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
    }

    private static void MoveCycle(T[] array, int startingIndex, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering MoveCycle(array={Array},startingIndex={StartingIndex},comparer={Comparer})", array, startingIndex, comparer);
        var item = array[startingIndex];
        var pos = startingIndex + CountSmallerElements(array, startingIndex + 1, item, comparer);
		_logger.LogDebug("MoveCycle(array={Array},startingIndex={StartingIndex},comparer={Comparer}): pos → {Pos}", array, startingIndex, comparer, pos);
		_logger.LogDebug("MoveCycle(array={Array},startingIndex={StartingIndex},comparer={Comparer}): item → {Item}", array, startingIndex, comparer, item);

        if (pos == startingIndex)
        {
            return;
        }

        pos = SkipSameElements(array, pos, item, comparer);

        var temp = array[pos];
        array[pos] = item;
        item = temp;

        while (pos != startingIndex)
        {
            pos = startingIndex + CountSmallerElements(array, startingIndex + 1, item, comparer);
            pos = SkipSameElements(array, pos, item, comparer);

            temp = array[pos];
            array[pos] = item;
            item = temp;
        }
		_logger.LogDebug("<<< Exiting MoveCycle(array={Array},startingIndex={StartingIndex},comparer={Comparer})", array, startingIndex, comparer);
    }

    private static int SkipSameElements(T[] array, int nextIndex, T item, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering SkipSameElements(array={Array},nextIndex={NextIndex},item={Item},comparer={Comparer})", array, nextIndex, item, comparer);
        while (comparer.Compare(array[nextIndex], item) == 0)
        {
            nextIndex++;
        }

		_logger.LogDebug("<<< Exiting SkipSameElements(array={Array},nextIndex={NextIndex},item={Item},comparer={Comparer})", array, nextIndex, item, comparer);
        return nextIndex;
    }

    private static int CountSmallerElements(T[] array, int startingIndex, T element, IComparer<T> comparer)
    {
		_logger.LogDebug(">>> Entering CountSmallerElements(array={Array},startingIndex={StartingIndex},element={Element},comparer={Comparer})", array, startingIndex, element, comparer);
        var smallerElements = 0;
        for (var i = startingIndex; i < array.Length; i++)
        {
            if (comparer.Compare(array[i], element) < 0)
            {
                smallerElements++;
            }
        }

		_logger.LogDebug("<<< Exiting CountSmallerElements(array={Array},startingIndex={StartingIndex},element={Element},comparer={Comparer})", array, startingIndex, element, comparer);
        return smallerElements;
    }
}
