using DataStructures.Heap.FibonacciHeap;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Heap.FibonacciHeaps;

internal class TestFHeap : FibonacciHeap<int>
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public void RawCut(FHeapNode<int> x, FHeapNode<int> y)
    {
		_logger.LogDebug(">>> Entering RawCut(x={X},y={Y})", x, y);
        Cut(x, y);
		_logger.LogDebug("<<< Exiting RawCut(x={X},y={Y})", x, y);
    }

    public void RawCascadingCut(FHeapNode<int> y)
    {
		_logger.LogDebug(">>> Entering RawCascadingCut(y={Y})", y);
        CascadingCut(y);
		_logger.LogDebug("<<< Exiting RawCascadingCut(y={Y})", y);
    }

    public void RawConsolidate()
    {
		_logger.LogDebug(">>> Entering RawConsolidate()");
        Consolidate();
		_logger.LogDebug("<<< Exiting RawConsolidate()");
    }
}

internal static class FibonacciHeapTests
{
    private static FibonacciHeap<int> BuildTestHeap()
    {
		_logger.LogDebug(">>> Entering BuildTestHeap()");
        var heap = new FibonacciHeap<int>();
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
    public static void Push_AddElements_BuildCorrectHeap()
    {
		_logger.LogDebug(">>> Entering Push_AddElements_BuildCorrectHeap()");
        var heap = BuildTestHeap();

		_logger.LogDebug("Push_AddElements_BuildCorrectHeap(): heap → {Heap}", heap);
        Assert.That(heap.Peek(), Is.EqualTo(1));
        Assert.That(heap.Count, Is.EqualTo(10));
		_logger.LogDebug("<<< Exiting Push_AddElements_BuildCorrectHeap()");
    }

    public static void Pop_RemoveElements_HeapStillValid()
    {
		_logger.LogDebug(">>> Entering Pop_RemoveElements_HeapStillValid()");
        var heap = BuildTestHeap();

		_logger.LogDebug("Pop_RemoveElements_HeapStillValid(): heap → {Heap}", heap);
        Assert.That(heap.Peek(), Is.EqualTo(1));
        Assert.That(heap.Count, Is.EqualTo(10));

        Assert.That(heap.Pop(), Is.EqualTo(1));
        Assert.That(heap.Count, Is.EqualTo(9));

        Assert.That(heap.Pop(), Is.EqualTo(2));
        Assert.That(heap.Count, Is.EqualTo(8));
		_logger.LogDebug("<<< Exiting Pop_RemoveElements_HeapStillValid()");
    }

    [Test]
    public static void Pop_EmptyHeap_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering Pop_EmptyHeap_ThrowsCorrectException()");
        var heap = new FibonacciHeap<int>();

		_logger.LogDebug("Pop_EmptyHeap_ThrowsCorrectException(): heap → {Heap}", heap);
        Assert.Throws<InvalidOperationException>(() => heap.Pop());
		_logger.LogDebug("<<< Exiting Pop_EmptyHeap_ThrowsCorrectException()");
    }

    [Test]
    public static void Pop_NonEmptyHeap_ReturnsInSortedOrder()
    {
		_logger.LogDebug(">>> Entering Pop_NonEmptyHeap_ReturnsInSortedOrder()");
        var heap = new FibonacciHeap<int>();

		_logger.LogDebug("Pop_NonEmptyHeap_ReturnsInSortedOrder(): heap → {Heap}", heap);
        var rand = new Random();
		_logger.LogDebug("Pop_NonEmptyHeap_ReturnsInSortedOrder(): rand → {Rand}", rand);
        var heapSize = 100;

        for (var i = 0; i < heapSize; i++)
        {
            heap.Push(rand.Next(1000));
        }

        var element = heap.Pop();
		_logger.LogDebug("Pop_NonEmptyHeap_ReturnsInSortedOrder(): element → {Element}", element);

        for (var i = 0; i < heapSize - 1; i++)
        {
            var newElement = heap.Pop();
			_logger.LogDebug("Pop_NonEmptyHeap_ReturnsInSortedOrder(): newElement → {NewElement}", newElement);
            Assert.That(element, Is.LessThanOrEqualTo(newElement));
            element = newElement;
        }

        Assert.That(heap.Count, Is.Zero);
		_logger.LogDebug("<<< Exiting Pop_NonEmptyHeap_ReturnsInSortedOrder()");
    }

    [Test]
    public static void Peek_EmptyHeap_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering Peek_EmptyHeap_ThrowsCorrectException()");
        var heap = new FibonacciHeap<int>();

		_logger.LogDebug("Peek_EmptyHeap_ThrowsCorrectException(): heap → {Heap}", heap);
        Assert.Throws<InvalidOperationException>(() => heap.Peek());
		_logger.LogDebug("<<< Exiting Peek_EmptyHeap_ThrowsCorrectException()");
    }

    [Test]
    public static void DecreaseKey_NonEmptyHeap_ReturnsCorrectAnswer()
    {
		_logger.LogDebug(">>> Entering DecreaseKey_NonEmptyHeap_ReturnsCorrectAnswer()");
        var heap = BuildTestHeap();

		_logger.LogDebug("DecreaseKey_NonEmptyHeap_ReturnsCorrectAnswer(): heap → {Heap}", heap);
        var node = heap.Push(11);
		_logger.LogDebug("DecreaseKey_NonEmptyHeap_ReturnsCorrectAnswer(): node → {Node}", node);
        heap.DecreaseKey(node, -1);

        Assert.That(heap.Pop(), Is.EqualTo(-1));
        Assert.That(heap.Pop(), Is.EqualTo(1));

        node = heap.Push(5);
        heap.DecreaseKey(node, 1);
        Assert.That(heap.Pop(), Is.EqualTo(1));

        Assert.That(heap.Pop(), Is.EqualTo(2));
        Assert.That(heap.Pop(), Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting DecreaseKey_NonEmptyHeap_ReturnsCorrectAnswer()");
    }

    [Test]
    public static void Union_NonEmptyHeap_ReturnsSortedOrder()
    {
		_logger.LogDebug(">>> Entering Union_NonEmptyHeap_ReturnsSortedOrder()");
        var oddHeap = new FibonacciHeap<int>();

		_logger.LogDebug("Union_NonEmptyHeap_ReturnsSortedOrder(): oddHeap → {OddHeap}", oddHeap);
        for (var i = 1; i < 10; i += 2)
        {
            oddHeap.Push(i);
        }

        var evenHeap = new FibonacciHeap<int>();
		_logger.LogDebug("Union_NonEmptyHeap_ReturnsSortedOrder(): evenHeap → {EvenHeap}", evenHeap);

        for (var i = 0; i < 10; i += 2)
        {
            evenHeap.Push(i);
        }

        oddHeap.Union(evenHeap);

        for (var i = 0; i < 10; i++)
        {
            Assert.That(oddHeap.Pop(), Is.EqualTo(i));
        }

        Assert.That(oddHeap.Count, Is.Zero);
        Assert.That(evenHeap.Count, Is.Zero);
		_logger.LogDebug("<<< Exiting Union_NonEmptyHeap_ReturnsSortedOrder()");
    }

    [Test]
    public static void Union_EmptyHeap_BecomesOtherHeap()
    {
		_logger.LogDebug(">>> Entering Union_EmptyHeap_BecomesOtherHeap()");
        var thisHeap = new FibonacciHeap<int>();
        var otherHeap = BuildTestHeap();
		_logger.LogDebug("Union_EmptyHeap_BecomesOtherHeap(): otherHeap → {OtherHeap}", otherHeap);
		_logger.LogDebug("Union_EmptyHeap_BecomesOtherHeap(): thisHeap → {ThisHeap}", thisHeap);

        var minNode = otherHeap.Peek();
		_logger.LogDebug("Union_EmptyHeap_BecomesOtherHeap(): minNode → {MinNode}", minNode);
        var otherCount = otherHeap.Count;

        Assert.That(thisHeap.Count, Is.Zero);

        thisHeap.Union(otherHeap);

        Assert.That(otherHeap.Count, Is.Zero);
        Assert.That(minNode, Is.EqualTo(thisHeap.Peek()));
        Assert.Throws<InvalidOperationException>(() => otherHeap.Peek());

        Assert.That(thisHeap.Count, Is.EqualTo(otherCount));
		_logger.LogDebug("<<< Exiting Union_EmptyHeap_BecomesOtherHeap()");
    }

    [Test]
    public static void Union_FullHeapWithEmptyHeap_Unchanged()
    {
		_logger.LogDebug(">>> Entering Union_FullHeapWithEmptyHeap_Unchanged()");
        var thisHeap = BuildTestHeap();
        var otherHeap = new FibonacciHeap<int>();
		_logger.LogDebug("Union_FullHeapWithEmptyHeap_Unchanged(): otherHeap → {OtherHeap}", otherHeap);
		_logger.LogDebug("Union_FullHeapWithEmptyHeap_Unchanged(): thisHeap → {ThisHeap}", thisHeap);

        var previousCount = thisHeap.Count;
        var previousMin = thisHeap.Peek();
		_logger.LogDebug("Union_FullHeapWithEmptyHeap_Unchanged(): previousMin → {PreviousMin}", previousMin);

        thisHeap.Union(otherHeap);

        Assert.That(previousCount, Is.EqualTo(thisHeap.Count));
        Assert.That(previousMin, Is.EqualTo(thisHeap.Peek()));
		_logger.LogDebug("<<< Exiting Union_FullHeapWithEmptyHeap_Unchanged()");
    }

    [Test]
    public static void DecreaseKey_EmptyHeap_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering DecreaseKey_EmptyHeap_ThrowsCorrectException()");
        var heap = new FibonacciHeap<int>();
        var item = new FHeapNode<int>(1);
		_logger.LogDebug("DecreaseKey_EmptyHeap_ThrowsCorrectException(): item → {Item}", item);
		_logger.LogDebug("DecreaseKey_EmptyHeap_ThrowsCorrectException(): heap → {Heap}", heap);

        Assert.Throws<ArgumentException>(() => heap.DecreaseKey(item, 0));
		_logger.LogDebug("<<< Exiting DecreaseKey_EmptyHeap_ThrowsCorrectException()");
    }

    [Test]
    public static void DecreaseKey_TryIncreaseKey_ThrowsCorrectException()
    {
		_logger.LogDebug(">>> Entering DecreaseKey_TryIncreaseKey_ThrowsCorrectException()");
        var heap = new FibonacciHeap<int>();
        var item = heap.Push(1);
		_logger.LogDebug("DecreaseKey_TryIncreaseKey_ThrowsCorrectException(): item → {Item}", item);
		_logger.LogDebug("DecreaseKey_TryIncreaseKey_ThrowsCorrectException(): heap → {Heap}", heap);

        Assert.Throws<InvalidOperationException>(() => heap.DecreaseKey(item, 2));
		_logger.LogDebug("<<< Exiting DecreaseKey_TryIncreaseKey_ThrowsCorrectException()");
    }

    [Test]
    public static void DecreaseKey_NonEmptyHeap_PreservesHeapStructure()
    {
		_logger.LogDebug(">>> Entering DecreaseKey_NonEmptyHeap_PreservesHeapStructure()");
        var heap = new FibonacciHeap<int>();

		_logger.LogDebug("DecreaseKey_NonEmptyHeap_PreservesHeapStructure(): heap → {Heap}", heap);
        for (var i = 11; i < 20; i++)
        {
            heap.Push(i);
        }

        var item = heap.Push(10);
		_logger.LogDebug("DecreaseKey_NonEmptyHeap_PreservesHeapStructure(): item → {Item}", item);

        for (var i = 0; i < 10; i++)
        {
            heap.Push(i);
        }

        var bigItem = heap.Push(20);
		_logger.LogDebug("DecreaseKey_NonEmptyHeap_PreservesHeapStructure(): bigItem → {BigItem}", bigItem);

        heap.DecreaseKey(item, -1);
        Assert.That(-1, Is.EqualTo(heap.Pop()));

        var currentVal = -1;
		_logger.LogDebug("DecreaseKey_NonEmptyHeap_PreservesHeapStructure(): currentVal → {CurrentVal}", currentVal);
        for (var i = 0; i < 10; i++)
        {
            var newVal = heap.Pop();
			_logger.LogDebug("DecreaseKey_NonEmptyHeap_PreservesHeapStructure(): newVal → {NewVal}", newVal);
            Assert.That(currentVal < newVal, Is.True);

            currentVal = newVal;
        }

        heap.DecreaseKey(bigItem, -1);
        Assert.That(-1, Is.EqualTo(heap.Pop()));

        currentVal = -1;
        for (var i = 0; i < 9; i++)
        {
            var newVal = heap.Pop();
            Assert.That(currentVal < newVal, Is.True);

            currentVal = newVal;
        }
		_logger.LogDebug("<<< Exiting DecreaseKey_NonEmptyHeap_PreservesHeapStructure()");
    }

    [Test]
    public static void Cut_EmptyHeap_ThrowsCorrectExcpetion()
    {
		_logger.LogDebug(">>> Entering Cut_EmptyHeap_ThrowsCorrectExcpetion()");
        var heap = new TestFHeap();
        var item1 = new FHeapNode<int>(1);
		_logger.LogDebug("Cut_EmptyHeap_ThrowsCorrectExcpetion(): item1 → {Item1}", item1);
        var item2 = new FHeapNode<int>(2);
		_logger.LogDebug("Cut_EmptyHeap_ThrowsCorrectExcpetion(): item2 → {Item2}", item2);
		_logger.LogDebug("Cut_EmptyHeap_ThrowsCorrectExcpetion(): heap → {Heap}", heap);

        Assert.Throws<InvalidOperationException>(() => heap.RawCut(item1, item2));
		_logger.LogDebug("<<< Exiting Cut_EmptyHeap_ThrowsCorrectExcpetion()");
    }

    [Test]
    public static void Cut_FilledHeap_AlteredItem()
    {
		_logger.LogDebug(">>> Entering Cut_FilledHeap_AlteredItem()");
        var heap = new TestFHeap();
        var item1 = heap.Push(1);
		_logger.LogDebug("Cut_FilledHeap_AlteredItem(): item1 → {Item1}", item1);
        var item2 = heap.Push(2);
		_logger.LogDebug("Cut_FilledHeap_AlteredItem(): item2 → {Item2}", item2);
		_logger.LogDebug("Cut_FilledHeap_AlteredItem(): heap → {Heap}", heap);
		_logger.LogDebug("Cut_FilledHeap_AlteredItem(): item2.Degree → {Arg0}", item2.Degree);

        item2.Degree = -1;

        Assert.Throws<InvalidOperationException>(() => heap.RawCut(item1, item2));
		_logger.LogDebug("<<< Exiting Cut_FilledHeap_AlteredItem()");
    }

    [Test]
    public static void Consolidate_EmptyHeap_DoesNothing()
    {
		_logger.LogDebug(">>> Entering Consolidate_EmptyHeap_DoesNothing()");
        var heap = new TestFHeap();
        heap.RawConsolidate();
		_logger.LogDebug("Consolidate_EmptyHeap_DoesNothing(): heap → {Heap}", heap);

        Assert.Throws<InvalidOperationException>(() => heap.Peek());
		_logger.LogDebug("<<< Exiting Consolidate_EmptyHeap_DoesNothing()");
    }
}
