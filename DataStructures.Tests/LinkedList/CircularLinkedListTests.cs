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
		_logger.LogError(">>> Entering TestInsertAtBeginning()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
		_logger.LogError("TestInsertAtBeginning(): cll → {Cll}", cll);
        cll.InsertAtBeginning(20);
        cll.InsertAtBeginning(30);

        Assert.That("30 20 10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestInsertAtBeginning()");
    }

    [Test]
    public static void TestInsertAtEnd()
    {
		_logger.LogError(">>> Entering TestInsertAtEnd()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestInsertAtEnd(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        Assert.That("10 20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestInsertAtEnd()");
    }

    [Test]
    public static void TestInsertAfter()
    {
		_logger.LogError(">>> Entering TestInsertAfter()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestInsertAfter(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.InsertAfter(20, 25);

        Assert.That("10 20 25 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestInsertAfter()");
    }

    [Test]
    public static void TestInsertAtBeginningInEmptyList()
    {
		_logger.LogError(">>> Entering TestInsertAtBeginningInEmptyList()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
		_logger.LogError("TestInsertAtBeginningInEmptyList(): cll → {Cll}", cll);

        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestInsertAtBeginningInEmptyList()");
    }

    [Test]
    public static void TestInsertAtEndInEmptyList()
    {
		_logger.LogError(">>> Entering TestInsertAtEndInEmptyList()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestInsertAtEndInEmptyList(): cll → {Cll}", cll);

        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestInsertAtEndInEmptyList()");
    }

    [Test]
    public static void TestInsertAfterInEmptyList()
    {
		_logger.LogError(">>> Entering TestInsertAfterInEmptyList()");
        var cll = new CircularLinkedList<int>();
        var ex = Assert.Throws<InvalidOperationException>(() => cll.InsertAfter(10, 20));
		_logger.LogError("TestInsertAfterInEmptyList(): ex → {Ex}", ex);
		_logger.LogError("TestInsertAfterInEmptyList(): cll → {Cll}", cll);

        Assert.That(ex!.Message, Is.EqualTo("List is empty."));
		_logger.LogWarning("<<< Exiting TestInsertAfterInEmptyList()");
    }

    [Test]
    public static void TestInsertAfterSpecificNode()
    {
		_logger.LogError(">>> Entering TestInsertAfterSpecificNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestInsertAfterSpecificNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.InsertAfter(20, 25); // Insert after node with value 20

        Assert.That("10 20 25 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestInsertAfterSpecificNode()");
    }

    [Test]
    public static void TestInsertAfterOnNonExistingValue()
    {
		_logger.LogError(">>> Entering TestInsertAfterOnNonExistingValue()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestInsertAfterOnNonExistingValue(): cll → {Cll}", cll);
        cll.InsertAfter(99, 25); // 99 does not exist

        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestInsertAfterOnNonExistingValue()");
    }

    [Test]
    public static void TestDeleteNode()
    {
		_logger.LogError(">>> Entering TestDeleteNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestDeleteNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(20);

        Assert.That("10 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestDeleteNode()");
    }

    [Test]
    public static void TestDeleteOnlyNode()
    {
		_logger.LogError(">>> Entering TestDeleteOnlyNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
		_logger.LogError("TestDeleteOnlyNode(): cll → {Cll}", cll);
        cll.DeleteNode(10);

        Assert.That(cll.IsEmpty(), Is.EqualTo(true));
		_logger.LogWarning("<<< Exiting TestDeleteOnlyNode()");
    }

    [Test]
    public static void TestDeleteHeadNode()
    {
		_logger.LogError(">>> Entering TestDeleteHeadNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestDeleteHeadNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(10);

        Assert.That("20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestDeleteHeadNode()");
    }

    [Test]
    public static void TestDeleteTailNode()
    {
		_logger.LogError(">>> Entering TestDeleteTailNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestDeleteTailNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(30);

        Assert.That("10 20", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestDeleteTailNode()");
    }

    [Test]
    public static void TestDeleteFromEmptyList()
    {
		_logger.LogError(">>> Entering TestDeleteFromEmptyList()");
        var cll = new CircularLinkedList<int>();
        var ex = Assert.Throws<InvalidOperationException>(() => cll.DeleteNode(10));
		_logger.LogError("TestDeleteFromEmptyList(): ex → {Ex}", ex);
		_logger.LogError("TestDeleteFromEmptyList(): cll → {Cll}", cll);

        Assert.That(ex!.Message, Is.EqualTo("List is empty."));
		_logger.LogWarning("<<< Exiting TestDeleteFromEmptyList()");
    }

    [Test]
    public static void TestDeleteNonExistentNode()
    {
		_logger.LogError(">>> Entering TestDeleteNonExistentNode()");
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
		_logger.LogError("TestDeleteNonExistentNode(): cll → {Cll}", cll);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(40); // Attempting to delete a node that doesn't exist

        Assert.That("10 20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
		_logger.LogWarning("<<< Exiting TestDeleteNonExistentNode()");
    }

    private static string GetDisplayOutput<T>(CircularLinkedList<T> list)
    {
		_logger.LogError(">>> Entering GetDisplayOutput(list={List})", list);
        var head = list.GetHead();
		_logger.LogError("GetDisplayOutput(list={List}): head → {Head}", list, head);
        if (head == null)
        {
            return string.Empty;
        }

        var current = head;
		_logger.LogError("GetDisplayOutput(list={List}): current → {Current}", list, current);
        var result = new System.Text.StringBuilder();
		_logger.LogError("GetDisplayOutput(list={List}): result → {Result}", list, result);

        do
        {
            result.Append(current!.Data + " ");
            current = current.Next;
        }
        while (current != head);

		_logger.LogWarning("<<< Exiting GetDisplayOutput(list={List})", list);
        return result.ToString().Trim();
    }
}
