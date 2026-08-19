using DataStructures.UnrolledList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.UnrolledList;

public class UnrolledLinkedListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void Insert_LinkArrayToLinkedList_ReturnArrayHaveSameItems()
    {
		_logger.LogDebug(">>> Entering Insert_LinkArrayToLinkedList_ReturnArrayHaveSameItems()");
        var linkedList = new UnrolledLinkedList(6);
        var contest = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
		_logger.LogDebug("Insert_LinkArrayToLinkedList_ReturnArrayHaveSameItems(): linkedList → {LinkedList}", linkedList);
        foreach (var number in contest)
        {
            linkedList.Insert(number);
        }

        var result = linkedList.GetRolledItems();
		_logger.LogDebug("Insert_LinkArrayToLinkedList_ReturnArrayHaveSameItems(): result → {Result}", result);

        result.Should().BeEquivalentTo(contest);
		_logger.LogDebug("<<< Exiting Insert_LinkArrayToLinkedList_ReturnArrayHaveSameItems()");
    }
}
