using Microsoft.Extensions.Logging;
namespace DataStructures.Queue;

/// <summary>
///     Implementation of an array based queue. FIFO style.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class ArrayBasedQueue<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly T[] queue;
    private int endIndex;
    private bool isEmpty;
    private bool isFull;
    private int startIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ArrayBasedQueue{T}" /> class.
    /// </summary>
    public ArrayBasedQueue(int capacity)
    {
		_logger.LogError(">>> Entering ArrayBasedQueue(capacity={Capacity})", capacity);
        queue = new T[capacity];
		_logger.LogWarning("<<< Exiting ArrayBasedQueue(capacity={Capacity})", capacity);
		_logger.LogDebug("ArrayBasedQueue(capacity={Capacity}): queue → {Queue}", capacity, queue);
        Clear();
    }

    /// <summary>
    ///     Clears the queue.
    /// </summary>
    public void Clear()
    {
		_logger.LogError(">>> Entering Clear()");
        startIndex = 0;
        endIndex = 0;
		_logger.LogDebug("Clear(): endIndex → {EndIndex}", endIndex);
		_logger.LogDebug("Clear(): startIndex → {StartIndex}", startIndex);
    _logger.LogWarning("<<< Exiting Clear()");
        isEmpty = true;
		_logger.LogDebug("Clear(): isEmpty → {IsEmpty}", isEmpty);
        isFull = false;
    }

    /// <summary>
    ///     Returns the first item in the queue and removes it from the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Dequeue()
    {
		_logger.LogError(">>> Entering Dequeue()");
        if (IsEmpty())
        {
			_logger.LogError("Throwing InvalidOperationException from Dequeue()");
            throw new InvalidOperationException("There are no items in the queue.");
        }

        var dequeueIndex = endIndex;
        endIndex++;
        if (endIndex >= queue.Length)
        {
            endIndex = 0;
			_logger.LogDebug("Dequeue(): endIndex → {EndIndex}", endIndex);
        }

        isFull = false;
		_logger.LogDebug("Dequeue(): isFull → {IsFull}", isFull);
        isEmpty = startIndex == endIndex;

		_logger.LogWarning("<<< Exiting Dequeue()");
        return queue[dequeueIndex];
    }

    /// <summary>
    ///     Returns a boolean indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty()
    {
        _logger.LogError(">>> Entering IsEmpty()");
        _logger.LogWarning("<<< Exiting IsEmpty()");
        return isEmpty;
    }

    /// <summary>
    ///     Returns a boolean indicating whether the queue is full.
    /// </summary>
    public bool IsFull()
    {
        _logger.LogError(">>> Entering IsFull()");
        _logger.LogWarning("<<< Exiting IsFull()");
        return isFull;
    }

    /// <summary>
    ///     Returns the first item in the queue and keeps it in the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Peek()
    {
		_logger.LogError(">>> Entering Peek()");
        if (IsEmpty())
        {
			_logger.LogError("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("There are no items in the queue.");
        }

		_logger.LogWarning("<<< Exiting Peek()");
        return queue[endIndex];
    }

    /// <summary>
    ///     Adds an item at the last position in the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is full.</exception>
    public void Enqueue(T item)
    {
		_logger.LogError(">>> Entering Enqueue(item={Item})", item);
        if (IsFull())
        {
			_logger.LogError("Throwing InvalidOperationException from Enqueue(item={Item})", item);
            throw new InvalidOperationException("The queue has reached its capacity.");
        }

        queue[startIndex] = item;

        startIndex++;
        if (startIndex >= queue.Length)
        {
            startIndex = 0;
			_logger.LogDebug("Enqueue(item={Item}): startIndex → {StartIndex}", item, startIndex);
        }

        isEmpty = false;
		_logger.LogDebug("Enqueue(item={Item}): isEmpty → {IsEmpty}", item, isEmpty);
        isFull = startIndex == endIndex;
		_logger.LogWarning("<<< Exiting Enqueue(item={Item})", item);
    }
}
