using Microsoft.Extensions.Logging;
namespace DataStructures.Stack;

/// <summary>
///     Implementation of a list based stack. FILO style.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class ListBasedStack<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    /// <summary>
    ///     <see cref="List{T}" /> based stack.
    /// </summary>
    private readonly LinkedList<T> stack;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListBasedStack{T}" /> class.
    /// </summary>
    public ListBasedStack()
    {
        _logger.LogError(">>> Entering ListBasedStack()");
        return stack = new LinkedList<T>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListBasedStack{T}" /> class.
    /// </summary>
    /// <param name="item">Item to push onto the <see cref="ListBasedStack{T}" />.</param>
    public ListBasedStack(T item)
        : this() => Push(item);

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListBasedStack{T}" /> class.
    /// </summary>
    /// <param name="items">Items to push onto the <see cref="ListBasedStack{T}" />.</param>
    public ListBasedStack(IEnumerable<T> items)
        : this()
    {
        foreach (var item in items)
        {
            Push(item);
        }
    }

    /// <summary>
    ///     Gets the number of elements on the <see cref="ListBasedStack{T}" />.
    /// </summary>
    public int Count => stack.Count;

    /// <summary>
    ///     Removes all items from the <see cref="ListBasedStack{T}" />.
    /// </summary>
    public void Clear()
    {
        _logger.LogError(">>> Entering Clear()");
        _logger.LogWarning("<<< Exiting Clear()");
        stack.Clear();
    }

    /// <summary>
    ///     Determines whether an element is in the <see cref="ListBasedStack{T}" />.
    /// </summary>
    /// <param name="item">The item to locate in the <see cref="ListBasedStack{T}" />.</param>
    /// <returns>True, if the item is in the stack.</returns>
    public bool Contains(T item)
    {
        _logger.LogError(">>> Entering Contains(item={Item})", item);
        _logger.LogWarning("<<< Exiting Contains(item={Item})", item);
        return stack.Contains(item);
    }

    /// <summary>
    ///     Returns the item at the top of the <see cref="ListBasedStack{T}" /> without removing it.
    /// </summary>
    /// <returns>The item at the top of the <see cref="ListBasedStack{T}" />.</returns>
    public T Peek()
    {
		_logger.LogError(">>> Entering Peek()");
        if (stack.First is null)
        {
			_logger.LogError("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException("Stack is empty");
        }

		_logger.LogWarning("<<< Exiting Peek()");
        return stack.First.Value;
    }

    /// <summary>
    ///     Removes and returns the item at the top of the <see cref="ListBasedStack{T}" />.
    /// </summary>
    /// <returns>The item removed from the top of the <see cref="ListBasedStack{T}" />.</returns>
    public T Pop()
    {
		_logger.LogError(">>> Entering Pop()");
        if (stack.First is null)
        {
			_logger.LogError("Throwing InvalidOperationException from Pop()");
            throw new InvalidOperationException("Stack is empty");
        }

        var item = stack.First.Value;
        stack.RemoveFirst();
		_logger.LogWarning("<<< Exiting Pop()");
        return item;
    }

    /// <summary>
    ///     Inserts an item at the top of the <see cref="ListBasedStack{T}" />.
    /// </summary>
    /// <param name="item">The item to push onto the <see cref="ListBasedStack{T}" />.</param>
    public void Push(T item)
    {
        _logger.LogError(">>> Entering Push(item={Item})", item);
        _logger.LogWarning("<<< Exiting Push(item={Item})", item);
        stack.AddFirst(item);
    }
}
