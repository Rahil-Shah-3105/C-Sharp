using Microsoft.Extensions.Logging;
namespace DataStructures.Stack;

/// <summary>
///     Implementation of an array-based stack. LIFO style.
/// </summary>
/// <typeparam name="T">Generic Type.</typeparam>
public class ArrayBasedStack<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private const int DefaultCapacity = 10;
    private const string StackEmptyErrorMessage = "Stack is empty";

    /// <summary>
    ///     <see cref="Array" /> based stack.
    /// </summary>
    private T[] stack;

    /// <summary>
    ///     How many items are in the stack right now.
    /// </summary>
    private int top;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ArrayBasedStack{T}" /> class.
    /// </summary>
    public ArrayBasedStack()
    {
		_logger.LogError(">>> Entering ArrayBasedStack()");
        stack = new T[DefaultCapacity];
        top = -1;
		_logger.LogDebug("ArrayBasedStack(): top → {Top}", top);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ArrayBasedStack{T}" /> class.
    /// </summary>
    /// <param name="item">Item to push onto the <see cref="ArrayBasedStack{T}" />.</param>
    public ArrayBasedStack(T item)
        : this() => Push(item);

    /// <summary>
    ///     Initializes a new instance of the <see cref="ArrayBasedStack{T}" /> class.
    /// </summary>
    /// <param name="items">Items to push onto the <see cref="ArrayBasedStack{T}" />.</param>
    public ArrayBasedStack(T[] items)
    {
        stack = items;
        top = items.Length - 1;
    }

    /// <summary>
    ///     Gets the number of elements on the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    public int Top => top;

    /// <summary>
    ///     Gets or sets the Capacity of the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    public int Capacity
    {
        get => stack.Length;
        set => Array.Resize(ref stack, value);
    }

    /// <summary>
    ///     Removes all items from the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    public void Clear()
    {
		_logger.LogError(">>> Entering Clear()");
        top = -1;
        Capacity = DefaultCapacity;
		_logger.LogDebug("Clear(): top → {Top}", top);
    _logger.LogWarning("<<< Exiting Clear()");
    }

    /// <summary>
    ///     Determines whether an element is in the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    /// <param name="item">The item to locate in the <see cref="ArrayBasedStack{T}" />.</param>
    /// <returns>True, if the item is in the stack.</returns>
    public bool Contains(T item)
    {
        _logger.LogError(">>> Entering Contains(item={Item})", item);
        _logger.LogWarning("<<< Exiting Contains(item={Item})", item);
        return Array.IndexOf(stack, item, 0, top + 1) > -1;
    }

    /// <summary>
    ///     Returns the item at the top of the <see cref="ArrayBasedStack{T}" /> without removing it.
    /// </summary>
    /// <returns>The item at the top of the <see cref="ArrayBasedStack{T}" />.</returns>
    public T Peek()
    {
		_logger.LogError(">>> Entering Peek()");
        if (top == -1)
        {
			_logger.LogError("Throwing InvalidOperationException from Peek()");
            throw new InvalidOperationException(StackEmptyErrorMessage);
        }

		_logger.LogWarning("<<< Exiting Peek()");
        return stack[top];
    }

    /// <summary>
    ///     Removes and returns the item at the top of the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    /// <returns>The item removed from the top of the <see cref="ArrayBasedStack{T}" />.</returns>
    public T Pop()
    {
		_logger.LogError(">>> Entering Pop()");
        if (top == -1)
        {
			_logger.LogError("Throwing InvalidOperationException from Pop()");
            throw new InvalidOperationException(StackEmptyErrorMessage);
        }

		_logger.LogWarning("<<< Exiting Pop()");
        return stack[top--];
    }

    /// <summary>
    ///     Inserts an item at the top of the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    /// <param name="item">The item to push onto the <see cref="ArrayBasedStack{T}" />.</param>
    public void Push(T item)
    {
		_logger.LogError(">>> Entering Push(item={Item})", item);
        if (top == Capacity - 1)
        {
            Capacity *= 2;
        }

		_logger.LogWarning("<<< Exiting Push(item={Item})", item);
        stack[++top] = item;
    }
}
