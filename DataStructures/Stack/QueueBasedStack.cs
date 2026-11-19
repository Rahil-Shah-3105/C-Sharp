using Microsoft.Extensions.Logging;
namespace DataStructures.Stack;

public class QueueBasedStack<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Queue<T> queue;

    public QueueBasedStack()
    {
        _logger.LogError(">>> Entering QueueBasedStack()");
		_logger.LogWarning("<<< Exiting QueueBasedStack()");
        return queue = new Queue<T>();
    }

    /// <summary>
    ///     Clears the stack.
    /// </summary>
    public void Clear()
    {
        _logger.LogError(">>> Entering Clear()");
        _logger.LogWarning("<<< Exiting Clear()");
        queue.Clear();
    }

    public bool IsEmpty()
    {
        _logger.LogError(">>> Entering IsEmpty()");
        _logger.LogWarning("<<< Exiting IsEmpty()");
        return queue.Count == 0;
    }

    /// <summary>
    ///     Adds an item on top of the stack.
    /// </summary>
    /// <param name="item">Item to be added on top of stack.</param>
    public void Push(T item)
    {
        _logger.LogError(">>> Entering Push(item={Item})", item);
        _logger.LogWarning("<<< Exiting Push(item={Item})", item);
        queue.Enqueue(item);
    }

    /// <summary>
    ///     Removes an item from  top of the stack and returns it.
    ///  </summary>
    /// <returns>item on top of stack.</returns>
    /// <exception cref="InvalidOperationException">Throw if stack is empty.</exception>
    public T Pop()
    {
		_logger.LogError(">>> Entering Pop()");
        if (IsEmpty())
        {
			_logger.LogError("Throwing InvalidOperationException from Pop()");
            throw new InvalidOperationException("The stack contains no items.");
        }

        for (int i = 0; i < queue.Count - 1; i++)
        {
            queue.Enqueue(queue.Dequeue());
        }

		_logger.LogWarning("<<< Exiting Pop()");
        return queue.Dequeue();
    }

    /// <summary>
    ///     return an item from the top of the stack without removing it.
    /// </summary>
    /// <returns>item on top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Throw if stack is empty.</exception>
    public T Peek()
    {
		_logger.LogError(">>> Entering Peek()");
        if (IsEmpty())
        {
			_logger.LogError("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("The stack contains no items.");
        }

        for (int i = 0; i < queue.Count - 1; i++)
        {
            queue.Enqueue(queue.Dequeue());
        }

        var item = queue.Peek();
		_logger.LogError("Peek(): item → {Item}", item);
        queue.Enqueue(queue.Dequeue());
		_logger.LogWarning("<<< Exiting Peek()");
        return item;
    }

    /// <summary>
    ///     returns the count of items on the stack.
    /// </summary>
    /// <returns>number of items on the stack.</returns>
    public int Length()
    {
        _logger.LogError(">>> Entering Length()");
        _logger.LogWarning("<<< Exiting Length()");
        return queue.Count;
    }
}
