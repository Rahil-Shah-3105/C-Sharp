using Microsoft.Extensions.Logging;
namespace DataStructures.Stack;

public class QueueBasedStack<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private readonly Queue<T> queue;

    public QueueBasedStack() => queue = new Queue<T>();

    /// <summary>
    ///     Clears the stack.
    /// </summary>
    public void Clear() => queue.Clear();

    public bool IsEmpty() => queue.Count == 0;

    /// <summary>
    ///     Adds an item on top of the stack.
    /// </summary>
    /// <param name="item">Item to be added on top of stack.</param>
    public void Push(T item) => queue.Enqueue(item);

    /// <summary>
    ///     Removes an item from  top of the stack and returns it.
    ///  </summary>
    /// <returns>item on top of stack.</returns>
    /// <exception cref="InvalidOperationException">Throw if stack is empty.</exception>
    public T Pop()
    {
		_logger.LogDebug(">>> Entering Pop()");
        if (IsEmpty())
        {
			_logger.LogWarning("Throwing InvalidOperationException from Pop()");
            throw new InvalidOperationException("The stack contains no items.");
        }

        for (int i = 0; i < queue.Count - 1; i++)
        {
            queue.Enqueue(queue.Dequeue());
        }

		_logger.LogDebug("<<< Exiting Pop()");
        return queue.Dequeue();
    }

    /// <summary>
    ///     return an item from the top of the stack without removing it.
    /// </summary>
    /// <returns>item on top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Throw if stack is empty.</exception>
    public T Peek()
    {
		_logger.LogDebug(">>> Entering Peek()");
        if (IsEmpty())
        {
			_logger.LogWarning("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("The stack contains no items.");
        }

        for (int i = 0; i < queue.Count - 1; i++)
        {
            queue.Enqueue(queue.Dequeue());
        }

        var item = queue.Peek();
		_logger.LogDebug("Peek(): item → {Item}", item);
        queue.Enqueue(queue.Dequeue());
		_logger.LogDebug("<<< Exiting Peek()");
        return item;
    }

    /// <summary>
    ///     returns the count of items on the stack.
    /// </summary>
    /// <returns>number of items on the stack.</returns>
    public int Length() => queue.Count;
}
