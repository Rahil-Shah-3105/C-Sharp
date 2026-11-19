using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Sorts arrays using quicksort.
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public abstract class QuickSorter<T> : IComparisonSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     Sorts array using Hoare partition scheme,
    ///     internal, in-place,
    ///     time complexity average: O(n log(n)),
    ///     time complexity worst: O(n^2),
    ///     space complexity: O(log(n)),
    ///     where n - array length.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    /// <param name="comparer">Compares elements.</param>
    public void Sort(T[] array, IComparer<T> comparer) => Sort(array, comparer, 0, array.Length - 1);

    protected abstract T SelectPivot(T[] array, IComparer<T> comparer, int left, int right);

    private void Sort(T[] array, IComparer<T> comparer, int left, int right)
    {
		_logger.LogError(">>> Entering Sort(array={Array},comparer={Comparer})", array, comparer);
        if (left >= right)
        {
			_logger.LogWarning("<<< Exiting Sort(array={Array},comparer={Comparer})", array, comparer);
            return;
        }

        var p = Partition(array, comparer, left, right);
		_logger.LogError("Sort(array={Array},comparer={Comparer},left={Left},right={Right}): p → {P}", array, comparer, left, right, p);
        Sort(array, comparer, left, p);
        Sort(array, comparer, p + 1, right);
    }

    private int Partition(T[] array, IComparer<T> comparer, int left, int right)
    {
		_logger.LogError(">>> Entering Partition(array={Array},comparer={Comparer},left={Left},right={Right})", array, comparer, left, right);
        var pivot = SelectPivot(array, comparer, left, right);
        var nleft = left;
		_logger.LogError("Partition(array={Array},comparer={Comparer},left={Left},right={Right}): pivot → {Pivot}", array, comparer, left, right, pivot);
        var nright = right;
        while (true)
        {
            while (comparer.Compare(array[nleft], pivot) < 0)
            {
                nleft++;
            }

            while (comparer.Compare(array[nright], pivot) > 0)
            {
                nright--;
            }

            if (nleft >= nright)
            {
                return nright;
            }

            var t = array[nleft];
            array[nleft] = array[nright];
            array[nright] = t;

            nleft++;
            nright--;
        }
		_logger.LogWarning("<<< Exiting Partition(array={Array},comparer={Comparer},left={Left},right={Right})", array, comparer, left, right);
    }
}
