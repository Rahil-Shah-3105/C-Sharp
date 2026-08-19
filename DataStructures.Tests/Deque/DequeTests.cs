using DataStructures.Deque;
using NUnit.Framework;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Deque;

/// <summary>
///     Comprehensive test suite for the Deque (Double-Ended Queue) data structure.
///     Tests cover:
private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
///     - Constructor validation and initialization
///     - Basic operations (AddFront, AddRear, RemoveFront, RemoveRear)
///     - Peek operations (non-destructive reads)
///     - Edge cases (empty deque, single element, capacity overflow)
///     - Type flexibility (int, string, tuples)
///     - Circular array behavior and automatic resizing
///     - Mixed operations maintaining correct order
/// </summary>
public static class DequeTests
{
    [Test]
    public static void Constructor_WithDefaultCapacity_CreatesEmptyDeque()
    {
		_logger.LogDebug(">>> Entering Constructor_WithDefaultCapacity_CreatesEmptyDeque()");
        // Arrange & Act: Create deque with default capacity (16)
        var deque = new Deque<int>();
		_logger.LogDebug("Constructor_WithDefaultCapacity_CreatesEmptyDeque(): deque → {Deque}", deque);

        // Assert: Should be empty initially
        Assert.That(deque.Count, Is.EqualTo(0));
        Assert.That(deque.IsEmpty, Is.True);
		_logger.LogDebug("<<< Exiting Constructor_WithDefaultCapacity_CreatesEmptyDeque()");
    }

    [Test]
    public static void Constructor_WithSpecifiedCapacity_CreatesEmptyDeque()
    {
		_logger.LogDebug(">>> Entering Constructor_WithSpecifiedCapacity_CreatesEmptyDeque()");
        // Arrange & Act
        var deque = new Deque<int>(10);
		_logger.LogDebug("Constructor_WithSpecifiedCapacity_CreatesEmptyDeque(): deque → {Deque}", deque);

        // Assert
        Assert.That(deque.Count, Is.EqualTo(0));
        Assert.That(deque.IsEmpty, Is.True);
		_logger.LogDebug("<<< Exiting Constructor_WithSpecifiedCapacity_CreatesEmptyDeque()");
    }

    [Test]
    public static void Constructor_WithInvalidCapacity_ThrowsArgumentException()
    {
		_logger.LogDebug(">>> Entering Constructor_WithInvalidCapacity_ThrowsArgumentException()");
        // Arrange, Act & Assert: Capacity must be at least 1
        // Zero capacity should throw
        Assert.Throws<ArgumentException>(() => new Deque<int>(0));
        // Negative capacity should throw
        Assert.Throws<ArgumentException>(() => new Deque<int>(-1));
		_logger.LogDebug("<<< Exiting Constructor_WithInvalidCapacity_ThrowsArgumentException()");
    }

    [Test]
    public static void AddFront_AddsElementToFront()
    {
		_logger.LogDebug(">>> Entering AddFront_AddsElementToFront()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("AddFront_AddsElementToFront(): deque → {Deque}", deque);

        // Act: Add elements to front (each becomes new front)
        deque.AddFront(1);  // Deque: [1]
        deque.AddFront(2);  // Deque: [2, 1]
        deque.AddFront(3);  // Deque: [3, 2, 1]

        // Assert: Most recently added element should be at front
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekFront(), Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting AddFront_AddsElementToFront()");
    }

    [Test]
    public static void AddRear_AddsElementToRear()
    {
		_logger.LogDebug(">>> Entering AddRear_AddsElementToRear()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("AddRear_AddsElementToRear(): deque → {Deque}", deque);

        // Act
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Assert
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekRear(), Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting AddRear_AddsElementToRear()");
    }

    [Test]
    public static void RemoveFront_RemovesAndReturnsElementFromFront()
    {
		_logger.LogDebug(">>> Entering RemoveFront_RemovesAndReturnsElementFromFront()");
        // Arrange: Build deque [1, 2, 3]
        var deque = new Deque<int>();
		_logger.LogDebug("RemoveFront_RemovesAndReturnsElementFromFront(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act: Remove front element
        int result = deque.RemoveFront();
		_logger.LogDebug("RemoveFront_RemovesAndReturnsElementFromFront(): result → {Result}", result);

        // Assert: Should return 1 and deque becomes [2, 3]
        Assert.That(result, Is.EqualTo(1));
        Assert.That(deque.Count, Is.EqualTo(2));
        Assert.That(deque.PeekFront(), Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting RemoveFront_RemovesAndReturnsElementFromFront()");
    }

    [Test]
    public static void RemoveRear_RemovesAndReturnsElementFromRear()
    {
		_logger.LogDebug(">>> Entering RemoveRear_RemovesAndReturnsElementFromRear()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("RemoveRear_RemovesAndReturnsElementFromRear(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        int result = deque.RemoveRear();
		_logger.LogDebug("RemoveRear_RemovesAndReturnsElementFromRear(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.EqualTo(3));
        Assert.That(deque.Count, Is.EqualTo(2));
        Assert.That(deque.PeekRear(), Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting RemoveRear_RemovesAndReturnsElementFromRear()");
    }

    [Test]
    public static void RemoveFront_OnEmptyDeque_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering RemoveFront_OnEmptyDeque_ThrowsInvalidOperationException()");
        // Arrange: Create empty deque
        var deque = new Deque<int>();
		_logger.LogDebug("RemoveFront_OnEmptyDeque_ThrowsInvalidOperationException(): deque → {Deque}", deque);

        // Act & Assert: Cannot remove from empty deque
        Assert.Throws<InvalidOperationException>(() => deque.RemoveFront());
		_logger.LogDebug("<<< Exiting RemoveFront_OnEmptyDeque_ThrowsInvalidOperationException()");
    }

    [Test]
    public static void RemoveRear_OnEmptyDeque_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering RemoveRear_OnEmptyDeque_ThrowsInvalidOperationException()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("RemoveRear_OnEmptyDeque_ThrowsInvalidOperationException(): deque → {Deque}", deque);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.RemoveRear());
		_logger.LogDebug("<<< Exiting RemoveRear_OnEmptyDeque_ThrowsInvalidOperationException()");
    }

    [Test]
    public static void PeekFront_ReturnsElementWithoutRemoving()
    {
		_logger.LogDebug(">>> Entering PeekFront_ReturnsElementWithoutRemoving()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("PeekFront_ReturnsElementWithoutRemoving(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);

        // Act
        int result = deque.PeekFront();
		_logger.LogDebug("PeekFront_ReturnsElementWithoutRemoving(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        Assert.That(deque.Count, Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting PeekFront_ReturnsElementWithoutRemoving()");
    }

    [Test]
    public static void PeekRear_ReturnsElementWithoutRemoving()
    {
		_logger.LogDebug(">>> Entering PeekRear_ReturnsElementWithoutRemoving()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("PeekRear_ReturnsElementWithoutRemoving(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);

        // Act
        int result = deque.PeekRear();
		_logger.LogDebug("PeekRear_ReturnsElementWithoutRemoving(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.EqualTo(2));
        Assert.That(deque.Count, Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting PeekRear_ReturnsElementWithoutRemoving()");
    }

    [Test]
    public static void PeekFront_OnEmptyDeque_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering PeekFront_OnEmptyDeque_ThrowsInvalidOperationException()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("PeekFront_OnEmptyDeque_ThrowsInvalidOperationException(): deque → {Deque}", deque);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.PeekFront());
		_logger.LogDebug("<<< Exiting PeekFront_OnEmptyDeque_ThrowsInvalidOperationException()");
    }

    [Test]
    public static void PeekRear_OnEmptyDeque_ThrowsInvalidOperationException()
    {
		_logger.LogDebug(">>> Entering PeekRear_OnEmptyDeque_ThrowsInvalidOperationException()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("PeekRear_OnEmptyDeque_ThrowsInvalidOperationException(): deque → {Deque}", deque);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => deque.PeekRear());
		_logger.LogDebug("<<< Exiting PeekRear_OnEmptyDeque_ThrowsInvalidOperationException()");
    }

    [Test]
    public static void Clear_RemovesAllElements()
    {
		_logger.LogDebug(">>> Entering Clear_RemovesAllElements()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("Clear_RemovesAllElements(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        deque.Clear();

        // Assert
        Assert.That(deque.Count, Is.EqualTo(0));
        Assert.That(deque.IsEmpty, Is.True);
		_logger.LogDebug("<<< Exiting Clear_RemovesAllElements()");
    }

    [Test]
    public static void ToArray_ReturnsElementsInCorrectOrder()
    {
		_logger.LogDebug(">>> Entering ToArray_ReturnsElementsInCorrectOrder()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("ToArray_ReturnsElementsInCorrectOrder(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        int[] result = deque.ToArray();

        // Assert
        Assert.That(result, Is.EqualTo(new[] { 1, 2, 3 }));
		_logger.LogDebug("<<< Exiting ToArray_ReturnsElementsInCorrectOrder()");
    }

    [Test]
    public static void ToArray_WithMixedOperations_ReturnsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering ToArray_WithMixedOperations_ReturnsCorrectOrder()");
        // Arrange: Build deque using both front and rear operations
        var deque = new Deque<int>();
		_logger.LogDebug("ToArray_WithMixedOperations_ReturnsCorrectOrder(): deque → {Deque}", deque);
        deque.AddFront(2);  // Deque: [2]
        deque.AddFront(1);  // Deque: [1, 2]
        deque.AddRear(3);   // Deque: [1, 2, 3]
        deque.AddRear(4);   // Deque: [1, 2, 3, 4]

        // Act
        int[] result = deque.ToArray();

        // Assert: Array should maintain front-to-rear order
        Assert.That(result, Is.EqualTo(new[] { 1, 2, 3, 4 }));
		_logger.LogDebug("<<< Exiting ToArray_WithMixedOperations_ReturnsCorrectOrder()");
    }

    [Test]
    public static void Contains_WithExistingElement_ReturnsTrue()
    {
		_logger.LogDebug(">>> Entering Contains_WithExistingElement_ReturnsTrue()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("Contains_WithExistingElement_ReturnsTrue(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        bool result = deque.Contains(2);
		_logger.LogDebug("Contains_WithExistingElement_ReturnsTrue(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.True);
		_logger.LogDebug("<<< Exiting Contains_WithExistingElement_ReturnsTrue()");
    }

    [Test]
    public static void Contains_WithNonExistingElement_ReturnsFalse()
    {
		_logger.LogDebug(">>> Entering Contains_WithNonExistingElement_ReturnsFalse()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("Contains_WithNonExistingElement_ReturnsFalse(): deque → {Deque}", deque);
        deque.AddRear(1);
        deque.AddRear(2);
        deque.AddRear(3);

        // Act
        bool result = deque.Contains(5);
		_logger.LogDebug("Contains_WithNonExistingElement_ReturnsFalse(): result → {Result}", result);

        // Assert
        Assert.That(result, Is.False);
		_logger.LogDebug("<<< Exiting Contains_WithNonExistingElement_ReturnsFalse()");
    }

    [Test]
    public static void Deque_WithStringType_WorksCorrectly()
    {
		_logger.LogDebug(">>> Entering Deque_WithStringType_WorksCorrectly()");
        // Arrange
        var deque = new Deque<string>();
		_logger.LogDebug("Deque_WithStringType_WorksCorrectly(): deque → {Deque}", deque);

        // Act
        deque.AddRear("Hello");
        deque.AddFront("World");
        deque.AddRear("!");

        // Assert
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekFront(), Is.EqualTo("World"));
        Assert.That(deque.PeekRear(), Is.EqualTo("!"));
		_logger.LogDebug("<<< Exiting Deque_WithStringType_WorksCorrectly()");
    }

    [Test]
    public static void Deque_AutomaticallyResizes_WhenCapacityExceeded()
    {
		_logger.LogDebug(">>> Entering Deque_AutomaticallyResizes_WhenCapacityExceeded()");
        // Arrange: Create deque with small capacity of 2
        var deque = new Deque<int>(2);
		_logger.LogDebug("Deque_AutomaticallyResizes_WhenCapacityExceeded(): deque → {Deque}", deque);

        // Act: Add more elements than initial capacity
        deque.AddRear(1);  // Capacity: 2, Count: 1
        deque.AddRear(2);  // Capacity: 2, Count: 2 (full)
        deque.AddRear(3);  // Should trigger resize to capacity 4, Count: 3
        deque.AddRear(4);  // Capacity: 4, Count: 4

        // Assert: All elements should be present in correct order
        Assert.That(deque.Count, Is.EqualTo(4));
        Assert.That(deque.ToArray(), Is.EqualTo(new[] { 1, 2, 3, 4 }));
		_logger.LogDebug("<<< Exiting Deque_AutomaticallyResizes_WhenCapacityExceeded()");
    }

    [Test]
    public static void Deque_MixedOperations_MaintainsCorrectOrder()
    {
		_logger.LogDebug(">>> Entering Deque_MixedOperations_MaintainsCorrectOrder()");
        // Arrange
        var deque = new Deque<int>();
		_logger.LogDebug("Deque_MixedOperations_MaintainsCorrectOrder(): deque → {Deque}", deque);

        // Act: Perform complex sequence of operations
        deque.AddRear(3);      // Deque: [3]
        deque.AddFront(2);     // Deque: [2, 3]
        deque.AddFront(1);     // Deque: [1, 2, 3]
        deque.AddRear(4);      // Deque: [1, 2, 3, 4]
        deque.RemoveFront();   // Deque: [2, 3, 4] (removed 1)
        deque.RemoveRear();    // Deque: [2, 3] (removed 4)
        deque.AddRear(5);      // Deque: [2, 3, 5]

        // Assert: Final order should be correct after all operations
        Assert.That(deque.ToArray(), Is.EqualTo(new[] { 2, 3, 5 }));
		_logger.LogDebug("<<< Exiting Deque_MixedOperations_MaintainsCorrectOrder()");
    }

    [Test]
    public static void Deque_WithComplexType_WorksCorrectly()
    {
		_logger.LogDebug(">>> Entering Deque_WithComplexType_WorksCorrectly()");
        // Arrange
        var deque = new Deque<(int, string)>();
		_logger.LogDebug("Deque_WithComplexType_WorksCorrectly(): deque → {Deque}", deque);

        // Act
        deque.AddRear((1, "One"));
        deque.AddRear((2, "Two"));
        deque.AddFront((0, "Zero"));

        // Assert
        Assert.That(deque.Count, Is.EqualTo(3));
        Assert.That(deque.PeekFront(), Is.EqualTo((0, "Zero")));
        Assert.That(deque.PeekRear(), Is.EqualTo((2, "Two")));
		_logger.LogDebug("<<< Exiting Deque_WithComplexType_WorksCorrectly()");
    }

    [Test]
    public static void Deque_AfterMultipleResizes_MaintainsIntegrity()
    {
		_logger.LogDebug(">>> Entering Deque_AfterMultipleResizes_MaintainsIntegrity()");
        // Arrange: Start with very small capacity
        var deque = new Deque<int>(2);
		_logger.LogDebug("Deque_AfterMultipleResizes_MaintainsIntegrity(): deque → {Deque}", deque);

        // Act: Add many elements to trigger multiple resizes
        // Capacity progression: 2 -> 4 -> 8 -> 16 -> 32 -> 64 -> 128
        for (int i = 0; i < 100; i++)
        {
            deque.AddRear(i);
        }

        // Assert: All elements should be intact after multiple resizes
        Assert.That(deque.Count, Is.EqualTo(100));
        Assert.That(deque.PeekFront(), Is.EqualTo(0));
        Assert.That(deque.PeekRear(), Is.EqualTo(99));
        Assert.That(deque.ToArray(), Is.EqualTo(Enumerable.Range(0, 100).ToArray()));
		_logger.LogDebug("<<< Exiting Deque_AfterMultipleResizes_MaintainsIntegrity()");
    }

    [Test]
    public static void Deque_CircularBehavior_WorksCorrectly()
    {
		_logger.LogDebug(">>> Entering Deque_CircularBehavior_WorksCorrectly()");
        // Arrange: Create deque with capacity 4
        var deque = new Deque<int>(4);
		_logger.LogDebug("Deque_CircularBehavior_WorksCorrectly(): deque → {Deque}", deque);

        // Act: Test circular wrap-around behavior
        deque.AddRear(1);      // Internal: [1, _, _, _], front=0, rear=1
        deque.AddRear(2);      // Internal: [1, 2, _, _], front=0, rear=2
        deque.RemoveFront();   // Internal: [_, 2, _, _], front=1, rear=2
        deque.RemoveFront();   // Internal: [_, _, _, _], front=2, rear=2
        deque.AddRear(3);      // Internal: [_, _, 3, _], front=2, rear=3
        deque.AddRear(4);      // Internal: [_, _, 3, 4], front=2, rear=0 (wrapped)
        deque.AddRear(5);      // Internal: [5, _, 3, 4], front=2, rear=1 (wrapped)

        // Assert: Elements should be in correct logical order despite circular storage
        Assert.That(deque.ToArray(), Is.EqualTo(new[] { 3, 4, 5 }));
		_logger.LogDebug("<<< Exiting Deque_CircularBehavior_WorksCorrectly()");
    }
}
