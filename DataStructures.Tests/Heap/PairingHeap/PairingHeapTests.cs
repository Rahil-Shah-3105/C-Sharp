using System.Collections;
using DataStructures.Heap.PairingHeap;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Heap.PairingHeap;

internal class PairingHeapTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void BuildMinHeap_CheckEnumerator_NotThrowOnEnumerate()
    {
		_logger.LogError(">>> Entering BuildMinHeap_CheckEnumerator_NotThrowOnEnumerate()");
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(1);
		_logger.LogError("BuildMinHeap_CheckEnumerator_NotThrowOnEnumerate(): minHeap → {MinHeap}", minHeap);

        var items = minHeap.ToList();
		_logger.LogError("BuildMinHeap_CheckEnumerator_NotThrowOnEnumerate(): items → {Items}", items);

        items.Should().HaveCount(1);
		_logger.LogWarning("<<< Exiting BuildMinHeap_CheckEnumerator_NotThrowOnEnumerate()");
    }

    [Test]
    public void BuildMinHeap_CheckEnumerable_NotThrowOnEnumerate()
    {
		_logger.LogError(">>> Entering BuildMinHeap_CheckEnumerable_NotThrowOnEnumerate()");
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(1);
		_logger.LogError("BuildMinHeap_CheckEnumerable_NotThrowOnEnumerate(): minHeap → {MinHeap}", minHeap);

        foreach (var node in (IEnumerable)minHeap)
        {
            node.Should().NotBe(null);
        }
		_logger.LogWarning("<<< Exiting BuildMinHeap_CheckEnumerable_NotThrowOnEnumerate()");
    }

    [Test]
    public void BuildMinHeap_UpdateNonExistingNode_ThrowException()
    {
		_logger.LogError(">>> Entering BuildMinHeap_UpdateNonExistingNode_ThrowException()");
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(1);
		_logger.LogError("BuildMinHeap_UpdateNonExistingNode_ThrowException(): minHeap → {MinHeap}", minHeap);
        minHeap.Extract();

        Action act = () => minHeap.UpdateKey(1, 10);
		_logger.LogError("BuildMinHeap_UpdateNonExistingNode_ThrowException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>();
		_logger.LogWarning("<<< Exiting BuildMinHeap_UpdateNonExistingNode_ThrowException()");
    }

    [Test]
    public void BuildMinHeap_UpdateBadNode_ThrowException()
    {
		_logger.LogError(">>> Entering BuildMinHeap_UpdateBadNode_ThrowException()");
        var minHeap = new PairingHeap<int>();
        minHeap.Insert(10);
		_logger.LogError("BuildMinHeap_UpdateBadNode_ThrowException(): minHeap → {MinHeap}", minHeap);

        Action act = () => minHeap.UpdateKey(10, 11);
		_logger.LogError("BuildMinHeap_UpdateBadNode_ThrowException(): act → {Act}", act);

        act.Should().Throw<ArgumentException>();
		_logger.LogWarning("<<< Exiting BuildMinHeap_UpdateBadNode_ThrowException()");
    }

    [Test]
    public void BuildMinHeap_CreateHeap_HeapIsCheked()
    {
		_logger.LogError(">>> Entering BuildMinHeap_CreateHeap_HeapIsCheked()");
        var nodeCount = 1000 * 10;
        var minHeap = new PairingHeap<int>();
		_logger.LogError("BuildMinHeap_CreateHeap_HeapIsCheked(): minHeap → {MinHeap}", minHeap);
        for (var i = 0; i <= nodeCount; i++)
        {
            minHeap.Insert(i);
        }

        for (var i = 0; i <= nodeCount; i++)
        {
            minHeap.UpdateKey(i, i - 1);
        }

        var min = 0;
        for (var i = 0; i <= nodeCount; i++)
        {
            min = minHeap.Extract();
			_logger.LogError("BuildMinHeap_CreateHeap_HeapIsCheked(): min → {Min}", min);
            Assert.That(min, Is.EqualTo(i - 1));
        }

        Assert.That(minHeap.Count, Is.EqualTo(minHeap.Count));

        var rnd = new Random();
		_logger.LogError("BuildMinHeap_CreateHeap_HeapIsCheked(): rnd → {Rnd}", rnd);
        var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(_ => rnd.Next()).ToList();
		_logger.LogError("BuildMinHeap_CreateHeap_HeapIsCheked(): testSeries → {TestSeries}", testSeries);

        foreach (var item in testSeries)
        {
            minHeap.Insert(item);
        }

        for (var i = 0; i < testSeries.Count; i++)
        {
            var decremented = testSeries[i] - rnd.Next(0, 1000);
			_logger.LogError("BuildMinHeap_CreateHeap_HeapIsCheked(): decremented → {Decremented}", decremented);
            minHeap.UpdateKey(testSeries[i], decremented);
            testSeries[i] = decremented;
        }

        testSeries.Sort();

        for (var i = 0; i < nodeCount - 2; i++)
        {
            min = minHeap.Extract();
            Assert.That(testSeries[i], Is.EqualTo(min));
        }

        Assert.That(minHeap.Count, Is.EqualTo(minHeap.Count));
		_logger.LogWarning("<<< Exiting BuildMinHeap_CreateHeap_HeapIsCheked()");
    }

    [Test]
    public void BuildMaxHeap_CreateHeap_HeapIsCheked()
    {
		_logger.LogError(">>> Entering BuildMaxHeap_CreateHeap_HeapIsCheked()");
        var nodeCount = 1000 * 10;
        var maxHeap = new PairingHeap<int>(Sorting.Descending);
		_logger.LogError("BuildMaxHeap_CreateHeap_HeapIsCheked(): maxHeap → {MaxHeap}", maxHeap);
        for (var i = 0; i <= nodeCount; i++)
        {
            maxHeap.Insert(i);
        }

        for (var i = 0; i <= nodeCount; i++)
        {
            maxHeap.UpdateKey(i, i + 1);
        }

        Assert.That(maxHeap.Count, Is.EqualTo(maxHeap.Count));

        var max = 0;
        for (var i = nodeCount; i >= 0; i--)
        {
            max = maxHeap.Extract();
			_logger.LogError("BuildMaxHeap_CreateHeap_HeapIsCheked(): max → {Max}", max);
            Assert.That(max, Is.EqualTo(i + 1));
        }

        var rnd = new Random();
		_logger.LogError("BuildMaxHeap_CreateHeap_HeapIsCheked(): rnd → {Rnd}", rnd);
        var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(_ => rnd.Next()).ToList();
		_logger.LogError("BuildMaxHeap_CreateHeap_HeapIsCheked(): testSeries → {TestSeries}", testSeries);

        foreach (var item in testSeries)
        {
            maxHeap.Insert(item);
        }

        for (var i = 0; i < testSeries.Count; i++)
        {
            var incremented = testSeries[i] + rnd.Next(0, 1000);
			_logger.LogError("BuildMaxHeap_CreateHeap_HeapIsCheked(): incremented → {Incremented}", incremented);
            maxHeap.UpdateKey(testSeries[i], incremented);
            testSeries[i] = incremented;
        }

        testSeries = testSeries.OrderByDescending(x => x).ToList();
        for (var i = 0; i < nodeCount - 2; i++)
        {
            max = maxHeap.Extract();
            Assert.That(testSeries[i], Is.EqualTo(max));
        }

        Assert.That(maxHeap.Count, Is.EqualTo(maxHeap.Count));
		_logger.LogWarning("<<< Exiting BuildMaxHeap_CreateHeap_HeapIsCheked()");
    }
}
