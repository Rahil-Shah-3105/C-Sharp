using Microsoft.Extensions.Logging;
namespace DataStructures.Deque;

/// <summary>
///     Implementation of a Deque (Double-Ended Queue) data structure.
///     A deque allows insertion and deletion of elements from both ends (front and rear).
///     This implementation uses a circular array for efficient operations.
///
///     Key Features:
///     - O(1) time complexity for AddFront, AddRear, RemoveFront, RemoveRear operations
///     - O(1) amortized time for insertions (due to dynamic resizing)
///     - Space efficient with circular array implementation
///     - Automatic capacity doubling when full
///
///     Use Cases:
///     - Implementing sliding window algorithms
///     - Palindrome checking
///     - Undo/Redo functionality
///     - Task scheduling with priority at both ends
///
///     Reference: "Data Structures and Algorithms in C#" by Michael T. Goodrich.
/// </summary>
/// <typeparam name="T">The type of elements in the deque.</typeparam>
public class Deque<T>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    // Internal circular array to store elements
    private T[] items;

    // Index of the front element (next element to remove from front)
    private int front;

    // Index where the next element will be added at rear
    private int rear;

    // Current number of elements in the deque
    private int count;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Deque{T}" /> class with default capacity.
    ///     Default capacity is 16 elements, which provides a good balance between
    ///     memory usage and avoiding early resizing for typical use cases.
    /// </summary>
    public Deque()
        : this(16)
    {
	_logger.LogError(">>> Entering Deque()");
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Deque{T}" /> class with specified capacity.
    /// </summary>
    /// <param name="capacity">The initial capacity of the deque.</param>
    /// <exception cref="ArgumentException">Thrown when capacity is less than 1.</exception>
    public Deque(int capacity)
    {
		_logger.LogError(">>> Entering Deque(capacity={Capacity})", capacity);
        if (capacity < 1)
        {
			_logger.LogWarning("<<< Exiting Deque()");
			_logger.LogError("Throwing ArgumentException from Deque(capacity={Capacity})", capacity);
            throw new ArgumentException("Capacity must be at least 1.", nameof(capacity));
        }

        items = new T[capacity];
		_logger.LogDebug("Deque(capacity={Capacity}): items → {Items}", capacity, items);
        front = 0;
		_logger.LogDebug("Deque(capacity={Capacity}): front → {Front}", capacity, front);
        rear = 0;
		_logger.LogDebug("Deque(capacity={Capacity}): rear → {Rear}", capacity, rear);
        count = 0;
		_logger.LogError("Deque(capacity={Capacity}): count → {Count}", capacity, count);
    }

    /// <summary>
    ///     Gets the number of elements in the deque.
    /// </summary>
    public int Count => count;

    /// <summary>
    ///     Gets a value indicating whether the deque is empty.
    /// </summary>
    public bool IsEmpty => count == 0;

    /// <summary>
    ///     Adds an element to the front of the deque.
    ///     This operation is O(1) time complexity (amortized due to occasional resizing).
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <example>
    ///     deque.AddFront(5);  // Deque: [5].
    ///     deque.AddFront(3);  // Deque: [3, 5].
    /// </example>
    public void AddFront(T item)
    {
		_logger.LogError(">>> Entering AddFront(item={Item})", item);
        // Check if we need to resize before adding
        if (count == items.Length)
        {
            Resize();
        }

        // Move front pointer backward in circular fashion
        // Adding items.Length ensures the result is always positive
        front = (front - 1 + items.Length) % items.Length;
		_logger.LogError("AddFront(item={Item}): front → {Front}", item, front);
		_logger.LogDebug("AddFront(item={Item}): front → {Front}", item, front);
        items[front] = item;
        count++;
		_logger.LogWarning("<<< Exiting AddFront(item={Item})", item);
    }

    /// <summary>
    ///     Adds an element to the rear of the deque.
    ///     This operation is O(1) time complexity (amortized due to occasional resizing).
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <example>
    ///     deque.AddRear(5);  // Deque: [5].
    ///     deque.AddRear(7);  // Deque: [5, 7].
    /// </example>
    public void AddRear(T item)
    {
		_logger.LogError(">>> Entering AddRear(item={Item})", item);
        // Check if we need to resize before adding
        if (count == items.Length)
        {
            Resize();
        }

        // Add item at rear position
        items[rear] = item;

        // Move rear pointer forward in circular fashion
		_logger.LogWarning("<<< Exiting AddRear(item={Item})", item);
        rear = (rear + 1) % items.Length;
		_logger.LogError("AddRear(item={Item}): rear → {Rear}", item, rear);
		_logger.LogDebug("AddRear(item={Item}): rear → {Rear}", item, rear);
        count++;
    }

    /// <summary>
    ///     Removes and returns the element at the front of the deque.
    ///     This operation is O(1) time complexity.
    /// </summary>
    /// <returns>The element at the front of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7].
    ///     int value = deque.RemoveFront();  // Returns 3, Deque: [5, 7].
    /// </example>
    public T RemoveFront()
    {
		_logger.LogError(">>> Entering RemoveFront()");
        // Validate that deque is not empty
        if (IsEmpty)
        {
			_logger.LogError("Throwing InvalidOperationException from RemoveFront()");
            throw new InvalidOperationException("Deque is empty.");
        }

        // Retrieve the front element
        T item = items[front];

        // Clear the reference to help garbage collection
        items[front] = default!;

        // Move front pointer forward in circular fashion
        front = (front + 1) % items.Length;
		_logger.LogError("RemoveFront(): front → {Front}", front);
		_logger.LogDebug("RemoveFront(): front → {Front}", front);
        count--;

		_logger.LogWarning("<<< Exiting RemoveFront()");
        return item;
    }

    /// <summary>
    ///     Removes and returns the element at the rear of the deque.
    ///     This operation is O(1) time complexity.
    /// </summary>
    /// <returns>The element at the rear of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7].
    ///     int value = deque.RemoveRear();  // Returns 7, Deque: [3, 5].
    /// </example>
    public T RemoveRear()
    {
		_logger.LogError(">>> Entering RemoveRear()");
        // Validate that deque is not empty
        if (IsEmpty)
        {
			_logger.LogError("Throwing InvalidOperationException from RemoveRear()");
            throw new InvalidOperationException("Deque is empty.");
        }

        // Move rear pointer backward to the last element
        rear = (rear - 1 + items.Length) % items.Length;
		_logger.LogError("RemoveRear(): rear → {Rear}", rear);
		_logger.LogDebug("RemoveRear(): rear → {Rear}", rear);

        // Retrieve the rear element
        T item = items[rear];

        // Clear the reference to help garbage collection
        items[rear] = default!;
        count--;

		_logger.LogWarning("<<< Exiting RemoveRear()");
        return item;
    }

    /// <summary>
    ///     Returns the element at the front of the deque without removing it.
    ///     This operation is O(1) time complexity and does not modify the deque.
    /// </summary>
    /// <returns>The element at the front of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7].
    ///     int value = deque.PeekFront();  // Returns 3, Deque unchanged: [3, 5, 7].
    /// </example>
    public T PeekFront()
    {
		_logger.LogError(">>> Entering PeekFront()");
        // Validate that deque is not empty
        if (IsEmpty)
        {
			_logger.LogError("Throwing InvalidOperationException from PeekFront()");
            throw new InvalidOperationException("Deque is empty.");
        }

		_logger.LogWarning("<<< Exiting PeekFront()");
        return items[front];
    }

    /// <summary>
    ///     Returns the element at the rear of the deque without removing it.
    ///     This operation is O(1) time complexity and does not modify the deque.
    /// </summary>
    /// <returns>The element at the rear of the deque.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the deque is empty.</exception>
    /// <example>
    ///     // Deque: [3, 5, 7].
    ///     int value = deque.PeekRear();  // Returns 7, Deque unchanged: [3, 5, 7].
    /// </example>
    public T PeekRear()
    {
		_logger.LogError(">>> Entering PeekRear()");
        // Validate that deque is not empty
        if (IsEmpty)
        {
			_logger.LogError("Throwing InvalidOperationException from PeekRear()");
            throw new InvalidOperationException("Deque is empty.");
        }

        // Calculate the index of the last element (rear - 1 in circular array)
        int rearIndex = (rear - 1 + items.Length) % items.Length;
		_logger.LogError("PeekRear(): rearIndex → {RearIndex}", rearIndex);
		_logger.LogWarning("<<< Exiting PeekRear()");
        return items[rearIndex];
    }

    /// <summary>
    ///     Removes all elements from the deque.
    ///     This operation is O(n) where n is the capacity of the internal array.
    ///     After clearing, the deque can be reused without reallocation.
    /// </summary>
    public void Clear()
    {
		_logger.LogError(">>> Entering Clear()");
        // Clear all references in the array to help garbage collection
        Array.Clear(items, 0, items.Length);

		_logger.LogWarning("<<< Exiting Clear()");
        // Reset pointers to initial state
        front = 0;
		_logger.LogDebug("Clear(): front → {Front}", front);
        rear = 0;
		_logger.LogDebug("Clear(): rear → {Rear}", rear);
        count = 0;
		_logger.LogError("Clear(): count → {Count}", count);
    }

    /// <summary>
    ///     Converts the deque to an array.
    ///     This operation is O(n) where n is the number of elements.
    ///     The resulting array maintains the order from front to rear.
    /// </summary>
    /// <returns>An array containing all elements in the deque from front to rear.</returns>
    /// <example>
    ///     // Deque: [3, 5, 7].
    ///     int[] array = deque.ToArray();  // Returns [3, 5, 7].
    /// </example>
    public T[] ToArray()
    {
		_logger.LogError(">>> Entering ToArray()");
        // Create result array with exact size needed
        T[] result = new T[count];
        int index = front;

        // Copy elements from front to rear, handling circular wrap-around
        for (int i = 0; i < count; i++)
        {
            result[i] = items[index];
            index = (index + 1) % items.Length;
        }

		_logger.LogWarning("<<< Exiting ToArray()");
        return result;
    }

    /// <summary>
    ///     Determines whether the deque contains a specific element.
    ///     This operation is O(n) where n is the number of elements.
    ///     Uses the default equality comparer for type T.
    /// </summary>
    /// <param name="item">The item to locate in the deque.</param>
    /// <returns>true if the item is found; otherwise, false.</returns>
    /// <example>
    ///     // Deque: [3, 5, 7].
    ///     bool exists = deque.Contains(5);  // Returns true.
    ///     bool missing = deque.Contains(9);  // Returns false.
    /// </example>
    public bool Contains(T item)
    {
		_logger.LogError(">>> Entering Contains(item={Item})", item);
        int index = front;

        // Iterate through all elements in order from front to rear
        for (int i = 0; i < count; i++)
        {
            // Use default equality comparer to compare elements
            if (EqualityComparer<T>.Default.Equals(items[index], item))
            {
                return true;
            }

            // Move to next element in circular array
            index = (index + 1) % items.Length;
        }

		_logger.LogWarning("<<< Exiting Contains(item={Item})", item);
        return false;
    }

    /// <summary>
    ///     Resizes the internal array to accommodate more elements.
    ///     This is a private helper method called automatically when capacity is reached.
    ///     Doubles the capacity and reorganizes elements to start at index 0.
    ///     Time complexity: O(n) where n is the current number of elements.
    /// </summary>
    private void Resize()
    {
		_logger.LogError(">>> Entering Resize()");
        // Double the capacity to reduce frequency of future resizes
        int newCapacity = items.Length * 2;
        T[] newItems = new T[newCapacity];

        // Copy all elements to new array starting from index 0
        // This "unwraps" the circular structure for simplicity
        int index = front;
        for (int i = 0; i < count; i++)
        {
            newItems[i] = items[index];
            index = (index + 1) % items.Length;
        }

        // Replace old array with new larger array
        items = newItems;
		_logger.LogDebug("Resize(): items → {Items}", items);

        // Reset pointers: front at 0, rear at position after last element
		_logger.LogWarning("<<< Exiting Resize()");
        front = 0;
		_logger.LogDebug("Resize(): front → {Front}", front);
        rear = count;
		_logger.LogDebug("Resize(): rear → {Rear}", rear);
    }
}
