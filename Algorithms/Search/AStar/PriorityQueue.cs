using Microsoft.Extensions.Logging;
// todo: extract to data structures
namespace Algorithms.Search.AStar;

/// <summary>
///     Generic Priority Queue.
///     List based.
/// </summary>
/// <typeparam name="T">
///     The type that will be stored.
///     Has to be IComparable of T.
/// </typeparam>
public class PriorityQueue<T>
    where T : IComparable<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly bool isDescending;

    // The underlying structure.
    private readonly List<T> list;

    public PriorityQueue(bool isDescending = false)
    {
		_logger.LogError(">>> Entering PriorityQueue(isDescending={IsDescending})", isDescending);
        this.isDescending = isDescending;
		_logger.LogDebug("PriorityQueue(isDescending={IsDescending}): this.isDescending → {Arg1}", isDescending, this.isDescending);
        list = [];
		_logger.LogDebug("PriorityQueue(isDescending={IsDescending}): list → {List}", isDescending, list);
		_logger.LogWarning("<<< Exiting PriorityQueue(capacity={Capacity},isDescending={IsDescending})", capacity, isDescending);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PriorityQueue{T}" /> class.
    /// </summary>
    /// <param name="capacity">Initial capacity.</param>
    /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
    public PriorityQueue(int capacity, bool isDescending = false)
    {
		_logger.LogError(">>> Entering PriorityQueue(capacity={Capacity},isDescending={IsDescending})", capacity, isDescending);
        list = new List<T>(capacity);
		_logger.LogDebug("PriorityQueue(capacity={Capacity},isDescending={IsDescending}): list → {List}", capacity, isDescending, list);
        this.isDescending = isDescending;
		_logger.LogDebug("PriorityQueue(collection={Collection},isDescending={IsDescending}): this.isDescending → {Arg2}", collection, isDescending, this.isDescending);
		_logger.LogWarning("<<< Exiting PriorityQueue(collection={Collection},isDescending={IsDescending})", collection, isDescending);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PriorityQueue{T}" /> class.
    /// </summary>
    /// <param name="collection">Internal data.</param>
    /// <param name="isDescending">Should Reverse Sort order? Default: false.</param>
    public PriorityQueue(IEnumerable<T> collection, bool isDescending = false)
        : this()
    {
        this.isDescending = isDescending;
        foreach (var item in collection)
        {
            Enqueue(item);
        }
    }

    /// <summary>
    ///     Gets Number of enqueued items.
    /// </summary>
    public int Count => list.Count;

    /// <summary>
    ///     Enqueues an item into the Queue.
    /// </summary>
    /// <param name="x">The item to Enqueue.</param>
    public void Enqueue(T x)
    {
		_logger.LogError(">>> Entering Enqueue(x={X})", x);
        list.Add(x);
        var i = Count - 1; // Position of x

        while (i > 0)
        {
            var p = (i - 1) / 2; // Start at half of i
			_logger.LogError("Enqueue(x={X}): p → {P}", x, p);
            if ((isDescending ? -1 : 1) * list[p].CompareTo(x) <= 0)
            {
                break;
            }

            list[i] = list[p]; // Put P to position of i
            i = p; // I = (I-1)/2
        }

        if (Count > 0)
        {
            list[i] = x; // If while loop way executed at least once(X got replaced by some p), add it to the list
        }
		_logger.LogWarning("<<< Exiting Enqueue(x={X})", x);
    }

    /// <summary>
    ///     Dequeues the item at the end of the queue.
    /// </summary>
    /// <returns>The dequeued item.</returns>
    public T Dequeue()
    {
		_logger.LogError(">>> Entering Dequeue()");
        var target = Peek(); // Get first in list
        var root = list[Count - 1]; // Hold last of the list
		_logger.LogError("Dequeue(): target → {Target}", target);
        list.RemoveAt(Count - 1); // But remove it from the list

        var i = 0;
        while (i * 2 + 1 < Count)
        {
            var a = i * 2 + 1; // Every second entry starting by 1
            var b = i * 2 + 2; // Every second entries neighbour
            var c = b < Count && (isDescending ? -1 : 1) * list[b].CompareTo(list[a]) < 0
                ? b
                : a; // Whether B(B is in range && B is smaller than A) or A

            if ((isDescending ? -1 : 1) * list[c].CompareTo(root) >= 0)
            {
                break;
            }

            list[i] = list[c];
            i = c;
        }

        if (Count > 0)
        {
            list[i] = root;
        }

		_logger.LogWarning("<<< Exiting Dequeue()");
        return target;
    }

    /// <summary>
    ///     Returns the next element in the queue without dequeuing.
    /// </summary>
    /// <returns>The next element of the queue.</returns>
    public T Peek()
    {
		_logger.LogError(">>> Entering Peek()");
        if (Count == 0)
        {
			_logger.LogError("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("Queue is empty.");
        }

		_logger.LogWarning("<<< Exiting Peek()");
        return list[0];
    }

    /// <summary>
    ///     Clears the Queue.
    /// </summary>
    public void Clear()
    {
        _logger.LogError(">>> Entering Clear()");
        _logger.LogWarning("<<< Exiting Clear()");
        list.Clear();
    }

    /// <summary>
    ///     Returns the Internal Data.
    /// </summary>
    /// <returns>The internal data structure.</returns>
    public List<T> GetData()
    {
        _logger.LogError(">>> Entering GetData()");
        _logger.LogWarning("<<< Exiting GetData()");
        return list;
    }
}
