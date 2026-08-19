using DataStructures.LinkedList.CircularLinkedList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.LinkedList;

[TestFixture]
public static class CircularLinkedListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestInsertAtBeginning()
    {
		_logger.LogDebug(">>> Entering TestInsertAtBeginning()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
		_logger.LogDebug("TestInsertAtBeginning(): cll → {Cll}", cll);
        cll.InsertAtBeginning(20);
        cll.InsertAtBeginning(30);

        Assert.That("30 20 10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestInsertAtBeginning()");
    }

    [Test]
    public static void TestInsertAtEnd()
    {
		_logger.LogDebug(">>> Entering TestInsertAtEnd()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestInsertAtEnd(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        Assert.That("10 20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestInsertAtEnd()");
    }

    [Test]
    public static void TestInsertAfter()
    {
		_logger.LogDebug(">>> Entering TestInsertAfter()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestInsertAfter(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.InsertAfter(20, 25);

        Assert.That("10 20 25 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestInsertAfter()");
    }

    [Test]
    public static void TestInsertAtBeginningInEmptyList()
    {
		_logger.LogDebug(">>> Entering TestInsertAtBeginningInEmptyList()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
		_logger.LogDebug("TestInsertAtBeginningInEmptyList(): cll → {Cll}", cll);

        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestInsertAtBeginningInEmptyList()");
    }

    [Test]
    public static void TestInsertAtEndInEmptyList()
    {
		_logger.LogDebug(">>> Entering TestInsertAtEndInEmptyList()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestInsertAtEndInEmptyList(): cll → {Cll}", cll);

        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestInsertAtEndInEmptyList()");
    }

    [Test]
    public static void TestInsertAfterInEmptyList()
    {
		_logger.LogDebug(">>> Entering TestInsertAfterInEmptyList()");
        var cll = new CircularLinkedList<int>();
        var ex = Assert.Throws<InvalidOperationException>(() => cll.InsertAfter(10, 20));
		_logger.LogDebug("TestInsertAfterInEmptyList(): ex → {Ex}", ex);
		_logger.LogDebug("TestInsertAfterInEmptyList(): cll → {Cll}", cll);

        Assert.That(ex!.Message, Is.EqualTo("List is empty."));
		_logger.LogDebug("<<< Exiting TestInsertAfterInEmptyList()");
    }

    [Test]
    public static void TestInsertAfterSpecificNode()
    {
		_logger.LogDebug(">>> Entering TestInsertAfterSpecificNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestInsertAfterSpecificNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.InsertAfter(20, 25); // Insert after node with value 20

        Assert.That("10 20 25 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestInsertAfterSpecificNode()");
    }

    [Test]
    public static void TestInsertAfterOnNonExistingValue()
    {
		_logger.LogDebug(">>> Entering TestInsertAfterOnNonExistingValue()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestInsertAfterOnNonExistingValue(): cll → {Cll}", cll);
        cll.InsertAfter(99, 25); // 99 does not exist

        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestInsertAfterOnNonExistingValue()");
    }

    [Test]
    public static void TestDeleteNode()
    {
		_logger.LogDebug(">>> Entering TestDeleteNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestDeleteNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(20);

        Assert.That("10 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestDeleteNode()");
    }

    [Test]
    public static void TestDeleteOnlyNode()
    {
		_logger.LogDebug(">>> Entering TestDeleteOnlyNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
		_logger.LogDebug("TestDeleteOnlyNode(): cll → {Cll}", cll);
        cll.DeleteNode(10);

        Assert.That(cll.IsEmpty(), Is.EqualTo(true));
		_logger.LogDebug("<<< Exiting TestDeleteOnlyNode()");
    }

    [Test]
    public static void TestDeleteHeadNode()
    {
		_logger.LogDebug(">>> Entering TestDeleteHeadNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestDeleteHeadNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(10);

        Assert.That("20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestDeleteHeadNode()");
    }

    [Test]
    public static void TestDeleteTailNode()
    {
		_logger.LogDebug(">>> Entering TestDeleteTailNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestDeleteTailNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(30);

        Assert.That("10 20", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestDeleteTailNode()");
    }

    [Test]
    public static void TestDeleteFromEmptyList()
    {
		_logger.LogDebug(">>> Entering TestDeleteFromEmptyList()");
        var cll = new CircularLinkedList<int>();
        var ex = Assert.Throws<InvalidOperationException>(() => cll.DeleteNode(10));
		_logger.LogDebug("TestDeleteFromEmptyList(): ex → {Ex}", ex);
		_logger.LogDebug("TestDeleteFromEmptyList(): cll → {Cll}", cll);

        Assert.That(ex!.Message, Is.EqualTo("List is empty."));
		_logger.LogDebug("<<< Exiting TestDeleteFromEmptyList()");
    }

    [Test]
    public static void TestDeleteNonExistentNode()
    {
		_logger.LogDebug(">>> Entering TestDeleteNonExistentNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogDebug("TestDeleteNonExistentNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(40); // Attempting to delete a node that doesn't exist

        Assert.That("10 20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogDebug("<<< Exiting TestDeleteNonExistentNode()");
    }

    private static string GetDisplayOutput<T>(CircularLinkedList<T> list)
    {
		_logger.LogDebug(">>> Entering GetDisplayOutput(list={List})", list);
        var head = list.GetHead();
        if (head == null)
		_logger.LogDebug("GetDisplayOutput(list={List}): head → {Head}", list, head);
        {
            return string.Empty;
        }

        var current = head;
		_logger.LogDebug("GetDisplayOutput(list={List}): current → {Current}", list, current);
        var result = new System.Text.StringBuilder();
		_logger.LogDebug("GetDisplayOutput(list={List}): result → {Result}", list, result);

        do
        {
            result.Append(current!.Data + " ");
            current = current.Next;
        }
        while (current != head);

		_logger.LogDebug("<<< Exiting GetDisplayOutput(list={List})", list);
        return result.ToString().Trim();
    }
}
