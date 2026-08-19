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
		_logger.LogDebug(">>> Entering ArrayBasedStack()");
        stack = new T[DefaultCapacity];
        top = -1;
		_logger.LogDebug("ArrayBasedStack(): stack → {Stack}", stack);
		_logger.LogDebug("ArrayBasedStack(items={Items}): stack → {Stack}", items, stack);
		_logger.LogDebug("ArrayBasedStack(): top → {Top}", top);
		_logger.LogDebug("<<< Exiting ArrayBasedStack()");
		_logger.LogDebug("ArrayBasedStack(items={Items}): top → {Top}", items, top);
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
		_logger.LogDebug(">>> Entering Push(item={Item})", item);
		_logger.LogDebug("<<< Exiting Push(item={Item})", item);
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
		_logger.LogDebug(">>> Entering Clear()");
        top = -1;
        Capacity = DefaultCapacity;
		_logger.LogDebug("Clear(): top → {Top}", top);
		_logger.LogDebug("Clear(): Capacity → {Capacity}", Capacity);
		_logger.LogDebug("<<< Exiting Clear()");
    }

    /// <summary>
    ///     Determines whether an element is in the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    /// <param name="item">The item to locate in the <see cref="ArrayBasedStack{T}" />.</param>
    /// <returns>True, if the item is in the stack.</returns>
    public bool Contains(T item) => Array.IndexOf(stack, item, 0, top + 1) > -1;

    /// <summary>
    ///     Returns the item at the top of the <see cref="ArrayBasedStack{T}" /> without removing it.
    /// </summary>
    /// <returns>The item at the top of the <see cref="ArrayBasedStack{T}" />.</returns>
    public T Peek()
    {
		_logger.LogDebug(">>> Entering Peek()");
        if (top == -1)
        {
			_logger.LogWarning("Throwing InvalidOperationException from Peek()");
		_logger.LogDebug("<<< Exiting Peek()");
            throw new InvalidOperationException(StackEmptyErrorMessage);
        }

        return stack[top];
    }

    /// <summary>
    ///     Removes and returns the item at the top of the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    /// <returns>The item removed from the top of the <see cref="ArrayBasedStack{T}" />.</returns>
    public T Pop()
    {
		_logger.LogDebug(">>> Entering Pop()");
        if (top == -1)
        {
			_logger.LogWarning("Throwing InvalidOperationException from Pop()");
		_logger.LogDebug("<<< Exiting Pop()");
            throw new InvalidOperationException(StackEmptyErrorMessage);
        }

        return stack[top--];
    }

    /// <summary>
    ///     Inserts an item at the top of the <see cref="ArrayBasedStack{T}" />.
    /// </summary>
    /// <param name="item">The item to push onto the <see cref="ArrayBasedStack{T}" />.</param>
    public void Push(T item)
    {
        if (top == Capacity - 1)
        {
            Capacity *= 2;
        }

        stack[++top] = item;
    }
}
