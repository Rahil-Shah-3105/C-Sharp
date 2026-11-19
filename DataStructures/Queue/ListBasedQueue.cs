using Microsoft.Extensions.Logging;
namespace DataStructures.Queue;

/// <summary>
///     Implementation of a list based queue. FIFO style.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class ListBasedQueue<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly LinkedList<T> queue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListBasedQueue{T}" /> class.
    /// </summary>
    public ListBasedQueue()
    {
        _logger.LogError(">>> Entering ListBasedQueue()");
        return queue = new LinkedList<T>();
    }

    /// <summary>
    ///     Clears the queue.
    /// </summary>
    public void Clear()
    {
		_logger.LogError(">>> Entering Clear()");
        queue.Clear();
		_logger.LogWarning("<<< Exiting Clear()");
    }

    /// <summary>
    ///     Returns the first item in the queue and removes it from the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Dequeue()
    {
		_logger.LogError(">>> Entering Dequeue()");
        if (queue.First is null)
        {
			_logger.LogError("Throwing InvalidOperationException from Dequeue()");
            throw new InvalidOperationException("There are no items in the queue.");
        }

        var item = queue.First;
        queue.RemoveFirst();
		_logger.LogWarning("<<< Exiting Dequeue()");
        return item.Value;
    }

    /// <summary>
    ///     Returns a boolean indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty()
    {
        _logger.LogError(">>> Entering IsEmpty()");
        _logger.LogWarning("<<< Exiting IsEmpty()");
        return !queue.Any();
    }

    /// <summary>
    ///     Returns a boolean indicating whether the queue is full.
    /// </summary>
    public bool IsFull()
    {
        _logger.LogError(">>> Entering IsFull()");
        _logger.LogWarning("<<< Exiting IsFull()");
        return false;
    }

    /// <summary>
    ///     Returns the first item in the queue and keeps it in the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Peek()
    {
		_logger.LogError(">>> Entering Peek()");
        if (queue.First is null)
        {
			_logger.LogError("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("There are no items in the queue.");
        }

		_logger.LogWarning("<<< Exiting Peek()");
        return queue.First.Value;
    }

    /// <summary>
    ///     Adds an item at the last position in the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is full.</exception>
    public void Enqueue(T item)
    {
		_logger.LogError(">>> Entering Enqueue(item={Item})", item);
        queue.AddLast(item);
		_logger.LogWarning("<<< Exiting Enqueue(item={Item})", item);
    }
}
