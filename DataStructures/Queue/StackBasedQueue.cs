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
		_logger.LogDebug(">>> Entering StackBasedQueue()");
        input = new Stack<T>();
		_logger.LogDebug("StackBasedQueue(): input → {Input}", input);
        output = new Stack<T>();
    }

    /// <summary>
    ///     Clears the queue.
    /// </summary>
    public void Clear()
    {
		_logger.LogDebug(">>> Entering Clear()");
        input.Clear();
        output.Clear();
		_logger.LogDebug("<<< Exiting Clear()");
    }

    /// <summary>
    ///     Returns the first item in the queue and removes it from the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Dequeue()
    {
		_logger.LogDebug(">>> Entering Dequeue()");
        if (input.Count == 0 && output.Count == 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from Dequeue()");
            throw new InvalidOperationException("The queue contains no items.");
        }

        if (output.Count == 0)
        {
            while (input.Count > 0)
            {
                var item = input.Pop();
				_logger.LogDebug("Dequeue(): item → {Item}", item);
                output.Push(item);
            }
        }

		_logger.LogDebug("<<< Exiting Dequeue()");
        return output.Pop();
    }

    /// <summary>
    ///     Returns a boolean indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty() => input.Count == 0 && output.Count == 0;

    /// <summary>
    ///     Returns a boolean indicating whether the queue is full.
    /// </summary>
    public bool IsFull() => false;

    /// <summary>
    ///     Returns the first item in the queue and keeps it in the queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the queue is empty.</exception>
    public T Peek()
    {
		_logger.LogDebug(">>> Entering Peek()");
        if (input.Count == 0 && output.Count == 0)
        {
			_logger.LogWarning("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("The queue contains no items.");
        }

        if (output.Count == 0)
        {
            while (input.Count > 0)
            {
                var item = input.Pop();
				_logger.LogDebug("Peek(): item → {Item}", item);
                output.Push(item);
            }
        }

		_logger.LogDebug("<<< Exiting Peek()");
        return output.Peek();
    }

    /// <summary>
    ///     Adds an item at the last position in the queue.
    /// </summary>
    public void Enqueue(T item) => input.Push(item);
}
