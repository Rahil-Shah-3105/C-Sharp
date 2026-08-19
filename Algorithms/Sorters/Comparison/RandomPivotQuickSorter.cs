using Microsoft.Extensions.Logging;
namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Sorts arrays using quicksort (selecting random point as a pivot).
/// </summary>
/// <typeparam name="T">Type of array element.</typeparam>
public sealed class RandomPivotQuickSorter<T> : QuickSorter<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Random random = new();

    protected override T SelectPivot(T[] array, IComparer<T> comparer, int left, int right) =>
        array[random.Next(left, right + 1)];
		_logger.LogDebug(">>> Entering SelectPivot(array={Array},comparer={Comparer},left={Left},right={Right})", array, comparer, left, right);
}
