using DataStructures.Heap;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Heap;

internal static class BinaryHeapTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static BinaryHeap<int> BuildTestHeap()
    {
		_logger.LogDebug(">>> Entering BuildTestHeap()");
        var heap = new BinaryHeap<int>();
        var elems = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		_logger.LogDebug("BuildTestHeap(): heap → {Heap}", heap);
        foreach (var i in elems)
        {
            heap.Push(i);
        }

		_logger.LogDebug("<<< Exiting BuildTestHeap()");
        return heap;
    }

    [Test]
    public static void Constructor_UseCustomComparer_BuildCorrectHeap()
    {
		_logger.LogDebug(">>> Entering Constructor_UseCustomComparer_BuildCorrectHeap()");
        var revHeap = new BinaryHeap<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (var i in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
        {
		_logger.LogDebug("Constructor_UseCustomComparer_BuildCorrectHeap(): revHeap → {RevHeap}", revHeap);
            revHeap.Push(i);
        }

        Assert.That(revHeap.Count, Is.EqualTo(10));
        Assert.That(revHeap.Peek(), Is.EqualTo(1));
        Assert.That(revHeap.Pop(), Is.EqualTo(1));
        Assert.That(revHeap.Peek(), Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting Constructor_UseCustomComparer_BuildCorrectHeap()");
    }

    [Test]
    public static void Push_AddElements_BuildCorrectHeap()
    {
		_logger.LogDebug(">>> Entering Push_AddElements_BuildCorrectHeap()");
        var heap = BuildTestHeap();

		_logger.LogDebug("Push_AddElements_BuildCorrectHeap(): heap → {Heap}", heap);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(10));
		_logger.LogDebug("<<< Exiting Push_AddElements_BuildCorrectHeap()");
    }

    public static void Pop_RemoveElements_HeapStillValid()
    {
		_logger.LogDebug(">>> Entering Pop_RemoveElements_HeapStillValid()");
        var heap = BuildTestHeap();

		_logger.LogDebug("Pop_RemoveElements_HeapStillValid(): heap → {Heap}", heap);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(10));

        Assert.That(heap.Pop(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(9));
        Assert.That(heap.Contains(10), Is.False);

        Assert.That(heap.Pop(), Is.EqualTo(9));
        Assert.That(heap.Count, Is.EqualTo(8));
        Assert.That(heap.Contains(9), Is.False);
		_logger.LogDebug("<<< Exiting Pop_RemoveElements_HeapStillValid()");
    }

    [Test]
    public static void Pop_EmptyHeap_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering Pop_EmptyHeap_ThrowsCorrectException()");
        var heap = new BinaryHeap<int>();

		_logger.LogDebug("Pop_EmptyHeap_ThrowsCorrectException(): heap → {Heap}", heap);
        Assert.Throws<InvalidOperationException>(() => heap.Pop());
		_logger.LogDebug("<<< Exiting Pop_EmptyHeap_ThrowsCorrectException()");
    }

    [Test]
    public static void Peek_NonEmptyHeap_ReturnsCorrectAnswer()
    {
		_logger.LogDebug(">>> Entering Peek_NonEmptyHeap_ReturnsCorrectAnswer()");
        var heap = BuildTestHeap();

		_logger.LogDebug("Peek_NonEmptyHeap_ReturnsCorrectAnswer(): heap → {Heap}", heap);
        Assert.That(heap.Peek(), Is.EqualTo(10));
		_logger.LogDebug("<<< Exiting Peek_NonEmptyHeap_ReturnsCorrectAnswer()");
    }

    [Test]
    public static void Peek_EmptyHeap_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering Peek_EmptyHeap_ThrowsCorrectException()");
        var heap = new BinaryHeap<int>();

		_logger.LogDebug("Peek_EmptyHeap_ThrowsCorrectException(): heap → {Heap}", heap);
        Assert.Throws<InvalidOperationException>(() => heap.Peek());
		_logger.LogDebug("<<< Exiting Peek_EmptyHeap_ThrowsCorrectException()");
    }

    [Test]
    public static void PushPop_EmptyHeap_ReturnsCorrectAnswer()
    {
		_logger.LogDebug(">>> Entering PushPop_EmptyHeap_ReturnsCorrectAnswer()");
        var heap = new BinaryHeap<int>();

		_logger.LogDebug("PushPop_EmptyHeap_ReturnsCorrectAnswer(): heap → {Heap}", heap);
        Assert.That(heap.PushPop(10), Is.EqualTo(10));
		_logger.LogDebug("<<< Exiting PushPop_EmptyHeap_ReturnsCorrectAnswer()");
    }

    [Test]
    public static void PushPop_NonEmptyHeap_ReturnsCorrectAnswer()
    {
		_logger.LogDebug(">>> Entering PushPop_NonEmptyHeap_ReturnsCorrectAnswer()");
        var heap = BuildTestHeap();

		_logger.LogDebug("PushPop_NonEmptyHeap_ReturnsCorrectAnswer(): heap → {Heap}", heap);
        Assert.That(heap.PushPop(20), Is.EqualTo(20));
        Assert.That(heap.PushPop(-10), Is.EqualTo(10));
		_logger.LogDebug("<<< Exiting PushPop_NonEmptyHeap_ReturnsCorrectAnswer()");
    }

    [Test]
    public static void Contains_NonEmptyHeap_ReturnsCorrectAnswer()
    {
		_logger.LogDebug(">>> Entering Contains_NonEmptyHeap_ReturnsCorrectAnswer()");
        var heap = BuildTestHeap();

		_logger.LogDebug("Contains_NonEmptyHeap_ReturnsCorrectAnswer(): heap → {Heap}", heap);
        Assert.That(heap.Contains(1), Is.True);
        Assert.That(heap.Contains(5), Is.True);
        Assert.That(heap.Contains(10), Is.True);
        Assert.That(heap.Contains(11), Is.False);
		_logger.LogDebug("<<< Exiting Contains_NonEmptyHeap_ReturnsCorrectAnswer()");
    }

    [Test]
    public static void Contains_EmptyHeap_ReturnsCorrectAnswer()
    {
		_logger.LogDebug(">>> Entering Contains_EmptyHeap_ReturnsCorrectAnswer()");
        var heap = new BinaryHeap<int>();

		_logger.LogDebug("Contains_EmptyHeap_ReturnsCorrectAnswer(): heap → {Heap}", heap);
        Assert.That(heap.Contains(1), Is.False);
        Assert.That(heap.Contains(5), Is.False);
        Assert.That(heap.Contains(10), Is.False);
        Assert.That(heap.Contains(11), Is.False);
		_logger.LogDebug("<<< Exiting Contains_EmptyHeap_ReturnsCorrectAnswer()");
    }

    [Test]
    public static void Remove_NonEmptyHeap_HeapStillValid()
    {
		_logger.LogDebug(">>> Entering Remove_NonEmptyHeap_HeapStillValid()");
        var heap = BuildTestHeap();

		_logger.LogDebug("Remove_NonEmptyHeap_HeapStillValid(): heap → {Heap}", heap);
        heap.Remove(2);
        Assert.That(heap.Contains(2), Is.False);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(9));

        heap.Remove(8);
        Assert.That(heap.Contains(8), Is.False);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(8));

        heap.Remove(5);
        Assert.That(heap.Contains(5), Is.False);
        Assert.That(heap.Peek(), Is.EqualTo(10));
        Assert.That(heap.Count, Is.EqualTo(7));

        Assert.Throws<ArgumentException>(() => heap.Remove(11));
		_logger.LogDebug("<<< Exiting Remove_NonEmptyHeap_HeapStillValid()");
    }

    [Test]
    public static void Remove_EmptyHeap_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering Remove_EmptyHeap_ThrowsCorrectException()");
        var heap = new BinaryHeap<int>();

		_logger.LogDebug("Remove_EmptyHeap_ThrowsCorrectException(): heap → {Heap}", heap);
        Assert.Throws<ArgumentException>(() => heap.Remove(1));
		_logger.LogDebug("<<< Exiting Remove_EmptyHeap_ThrowsCorrectException()");
    }
}
