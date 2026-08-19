using Microsoft.Extensions.Logging;
namespace DataStructures.Heap.PairingHeap;

/// <summary>
/// Node comparer.
/// </summary>
/// <typeparam name="T">Node type.</typeparam>
public class PairingNodeComparer<T>(Sorting sortDirection, IComparer<T> comparer) : IComparer<T> where T : IComparable
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly bool isMax = sortDirection == Sorting.Descending;
    private readonly IComparer<T> nodeComparer = comparer;

    public int Compare(T? x, T? y)
    {
		_logger.LogDebug(">>> Entering Compare(x={X},y={Y})", x, y);
		_logger.LogDebug("<<< Exiting Compare(x={X},y={Y})", x, y);
        return !isMax
            ? CompareNodes(x, y)
            : CompareNodes(y, x);
    }

    private int CompareNodes(T? one, T? second)
    {
		_logger.LogDebug(">>> Entering CompareNodes(one={One},second={Second})", one, second);
		_logger.LogDebug("<<< Exiting CompareNodes(one={One},second={Second})", one, second);
        return nodeComparer.Compare(one, second);
    }
}
