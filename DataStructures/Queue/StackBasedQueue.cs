using Microsoft.Extensions.Logging;
namespace DataStructures.Queue;

/// <summary>
///     Implementation of a stack based queue. FIFO style.
/// </summary>
/// <remarks>
///     Enqueue is O(1) and Dequeue is amortized O(1).
/// </remarks>
/// <typeparam name="T">Generic Type.</typeparam>
public class StackBasedQueue<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Stack<T> input;
    private readonly Stack<T> output;

    /// <summary>
    ///     Initializes a new instance of the <see cref="StackBasedQueue{T}" /> class.
    /// </summary>
    public StackBasedQueue()
    {
		_logger.LogError(">>> Entering StackBasedQueue()");
        input = new Stack<T>();
		_logger.LogError("StackBasedQueue(): input → {Input}", input);
		_logger.LogDebug("StackBasedQueue(): input → {Input}", input);
        output = new Stack<T>();
		_logger.LogError("StackBasedQueue(): output → {Output}", output);
		_logger.LogWarning("<<< Exiting StackBasedQueue()");
    }

    /// <summary>
    ///     Clears the queue.
    /// </summary>
    public void Clear()
    {
		_logger.LogError(">>> Entering Clear()");
        input.Clear();
        output.Clear();
		_logger.LogWarning("<<< Exiting Clear()");
    }

    /// <summary>
    ///     Returns the first item in the queue and removes it from the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Dequeue()
    {
		_logger.LogError(">>> Entering Dequeue()");
        if (input.Count == 0 && output.Count == 0)
        {
			_logger.LogError("Throwing InvalidOperationException from Dequeue()");
            throw new InvalidOperationException("The queue contains no items.");
        }

        if (output.Count == 0)
        {
            while (input.Count > 0)
            {
                var item = input.Pop();
				_logger.LogError("Dequeue(): item → {Item}", item);
                output.Push(item);
            }
        }

		_logger.LogWarning("<<< Exiting Dequeue()");
        return output.Pop();
    }

    /// <summary>
    ///     Returns a boolean indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty()
    {
        _logger.LogError(">>> Entering IsEmpty()");
        _logger.LogWarning("<<< Exiting IsEmpty()");
        return input.Count == 0 && output.Count == 0;
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
        if (input.Count == 0 && output.Count == 0)
        {
			_logger.LogError("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("The queue contains no items.");
        }

        if (output.Count == 0)
        {
            while (input.Count > 0)
            {
                var item = input.Pop();
				_logger.LogError("Peek(): item → {Item}", item);
                output.Push(item);
            }
        }

		_logger.LogWarning("<<< Exiting Peek()");
        return output.Peek();
    }

    /// <summary>
    ///     Adds an item at the last position in the queue.
    /// </summary>
    public void Enqueue(T item)
    {
        _logger.LogError(">>> Entering Enqueue(item={Item})", item);
        _logger.LogWarning("<<< Exiting Enqueue(item={Item})", item);
        input.Push(item);
    }
}
