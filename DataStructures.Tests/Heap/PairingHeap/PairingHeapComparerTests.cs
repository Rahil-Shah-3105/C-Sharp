using DataStructures.Heap.PairingHeap;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.Heap.PairingHeap;

internal class PairingHeapComparerTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Compare_CheckAscending_ReturnNegative()
    {
		_logger.LogError(">>> Entering Compare_CheckAscending_ReturnNegative()");
        var minHeap = new PairingNodeComparer<int>(Sorting.Ascending, Comparer<int>.Default);
        var node1 = new PairingHeapNode<int>(10);
		_logger.LogError("Compare_CheckAscending_ReturnNegative(): node1 → {Node1}", node1);
        var node2 = new PairingHeapNode<int>(20);
		_logger.LogError("Compare_CheckAscending_ReturnNegative(): node2 → {Node2}", node2);
		_logger.LogError("Compare_CheckAscending_ReturnNegative(): minHeap → {MinHeap}", minHeap);

        var items = minHeap.Compare(node1.Value, node2.Value);
		_logger.LogError("Compare_CheckAscending_ReturnNegative(): items → {Items}", items);

        items.Should().Be(-1);
		_logger.LogWarning("<<< Exiting Compare_CheckAscending_ReturnNegative()");
    }

    [Test]
    public void Compare_CheckAscending_ReturnPositive()
    {
		_logger.LogError(">>> Entering Compare_CheckAscending_ReturnPositive()");
        var minHeap = new PairingNodeComparer<int>(Sorting.Descending, Comparer<int>.Default);
        var node1 = new PairingHeapNode<int>(10);
		_logger.LogError("Compare_CheckAscending_ReturnPositive(): node1 → {Node1}", node1);
        var node2 = new PairingHeapNode<int>(20);
		_logger.LogError("Compare_CheckAscending_ReturnPositive(): node2 → {Node2}", node2);
		_logger.LogError("Compare_CheckAscending_ReturnPositive(): minHeap → {MinHeap}", minHeap);

        var items = minHeap.Compare(node1.Value, node2.Value);
		_logger.LogError("Compare_CheckAscending_ReturnPositive(): items → {Items}", items);

        items.Should().Be(1);
		_logger.LogWarning("<<< Exiting Compare_CheckAscending_ReturnPositive()");
    }
}
