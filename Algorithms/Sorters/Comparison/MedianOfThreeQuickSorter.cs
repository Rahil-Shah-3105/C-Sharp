using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Sorts arrays using quicksort (selecting median of three as a pivot).
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public sealed class MedianOfThreeQuickSorter<T> : QuickSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    protected override T SelectPivot(T[] array, IComparer<T> comparer, int left, int right)
    {
		_logger.LogDebug(">>> Entering SelectPivot(array={Array},comparer={Comparer},left={Left},right={Right})", array, comparer, left, right);
        var leftPoint = array[left];
        var middlePoint = array[left + (right - left) / 2];
		_logger.LogDebug("SelectPivot(array={Array},comparer={Comparer},left={Left},right={Right}): middlePoint → {MiddlePoint}", array, comparer, left, right, middlePoint);
        var rightPoint = array[right];

		_logger.LogDebug("<<< Exiting SelectPivot(array={Array},comparer={Comparer},left={Left},right={Right})", array, comparer, left, right);
        return FindMedian(comparer, leftPoint, middlePoint, rightPoint);
    }

    private static T FindMedian(IComparer<T> comparer, T a, T b, T c)
    {
		_logger.LogDebug(">>> Entering FindMedian(comparer={Comparer},a={A},b={B},c={C})", comparer, a, b, c);
        if (comparer.Compare(a, b) <= 0)
        {
            // a <= b <= c
            if (comparer.Compare(b, c) <= 0)
            {
                return b;
            }

            // a <= c < b
            if (comparer.Compare(a, c) <= 0)
            {
                return c;
            }

            // c < a <= b
            return a;
        }

        // a > b >= c
        if (comparer.Compare(b, c) >= 0)
        {
            return b;
        }

        // a >= c > b
        if (comparer.Compare(a, c) >= 0)
        {
		_logger.LogDebug("<<< Exiting FindMedian(comparer={Comparer},a={A},b={B},c={C})", comparer, a, b, c);
            return c;
        }

        // c > a > b
        return a;
    }
}
