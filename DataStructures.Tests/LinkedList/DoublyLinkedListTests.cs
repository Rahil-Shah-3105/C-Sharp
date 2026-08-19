using DataStructures.LinkedList.DoublyLinkedList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.LinkedList;

public static class DoublyLinkedListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestGetData()
    {
		_logger.LogDebug(">>> Entering TestGetData()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        var arr = dll.GetData().ToArray();
		_logger.LogDebug("TestGetData(): arr → {Arr}", arr);
		_logger.LogDebug("TestGetData(): dll → {Dll}", dll);

        Assert.That(dll.Count, Is.EqualTo(5));
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EqualTo(arr));
		_logger.LogDebug("<<< Exiting TestGetData()");
    }

    [Test]
    public static void TestGetAt()
    {
		_logger.LogDebug(">>> Entering TestGetAt()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogDebug("TestGetAt(): dll → {Dll}", dll);
        var one = dll.GetAt(1);
		_logger.LogDebug("TestGetAt(): one → {One}", one);
        var three = dll.GetAt(3);
		_logger.LogDebug("TestGetAt(): three → {Three}", three);

        Assert.That(one.Data, Is.EqualTo(1));
        Assert.That(three.Data, Is.EqualTo(3));
        Assert.Throws<ArgumentOutOfRangeException>(
            () => dll.GetAt(-1)
        );
        Assert.Throws<ArgumentOutOfRangeException>(
            () => dll.GetAt(5)
        );
		_logger.LogDebug("<<< Exiting TestGetAt()");
    }

    [Test]
    public static void TestAddtion()
    {
		_logger.LogDebug(">>> Entering TestAddtion()");
        var dll = new DoublyLinkedList<int>(0);
        var one = dll.Add(1);
		_logger.LogDebug("TestAddtion(): one → {One}", one);
		_logger.LogDebug("TestAddtion(): dll → {Dll}", dll);

        dll.Add(3);
        dll.AddAfter(2, one);
        dll.Add(4);

        var arr = dll.GetData().ToArray();
		_logger.LogDebug("TestAddtion(): arr → {Arr}", arr);
        var reversedArr = dll.GetDataReversed().ToArray();
		_logger.LogDebug("TestAddtion(): reversedArr → {ReversedArr}", reversedArr);

        Assert.That(dll.Count, Is.EqualTo(5));
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EqualTo(arr));
        Assert.That(new[] { 4, 3, 2, 1, 0 }, Is.EqualTo(reversedArr));
		_logger.LogDebug("<<< Exiting TestAddtion()");
    }

    [Test]
    public static void TestRemove()
    {
		_logger.LogDebug(">>> Entering TestRemove()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogDebug("TestRemove(): dll → {Dll}", dll);
        dll.RemoveNode(dll.Find(2));
        dll.RemoveHead();
        dll.Remove();

        var arr = dll.GetData().ToArray();
		_logger.LogDebug("TestRemove(): arr → {Arr}", arr);
        var reversedArr = dll.GetDataReversed().ToArray();
		_logger.LogDebug("TestRemove(): reversedArr → {ReversedArr}", reversedArr);

        Assert.That(dll.Count, Is.EqualTo(2));
        Assert.That(new[] { 1, 3 }, Is.EqualTo(arr));
        Assert.That(new[] { 3, 1 }, Is.EqualTo(reversedArr));
		_logger.LogDebug("<<< Exiting TestRemove()");
    }

    [Test]
    public static void TestFind()
    {
		_logger.LogDebug(">>> Entering TestFind()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogDebug("TestFind(): dll → {Dll}", dll);
        var one = dll.Find(1);
		_logger.LogDebug("TestFind(): one → {One}", one);
        var three = dll.Find(3);
		_logger.LogDebug("TestFind(): three → {Three}", three);

        Assert.That(one.Data, Is.EqualTo(1));
        Assert.That(three.Data, Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting TestFind()");
    }

    [Test]
    public static void TestIndexOf()
    {
		_logger.LogDebug(">>> Entering TestIndexOf()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogDebug("TestIndexOf(): dll → {Dll}", dll);
        var one = dll.IndexOf(1);
		_logger.LogDebug("TestIndexOf(): one → {One}", one);
        var three = dll.IndexOf(3);
		_logger.LogDebug("TestIndexOf(): three → {Three}", three);

        Assert.That(one, Is.EqualTo(1));
        Assert.That(three, Is.EqualTo(3));
		_logger.LogDebug("<<< Exiting TestIndexOf()");
    }

    [Test]
    public static void TestContains()
    {
		_logger.LogDebug(">>> Entering TestContains()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogDebug("TestContains(): dll → {Dll}", dll);
        var one = dll.Contains(1);
		_logger.LogDebug("TestContains(): one → {One}", one);
        var six = dll.Contains(6);
		_logger.LogDebug("TestContains(): six → {Six}", six);

        Assert.That(one, Is.True);
        Assert.That(six, Is.False);
		_logger.LogDebug("<<< Exiting TestContains()");
    }

    [Test]
    public static void TestReverse()
    {
		_logger.LogDebug(">>> Entering TestReverse()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        dll.Reverse();
		_logger.LogDebug("TestReverse(): dll → {Dll}", dll);
        var arr = dll.GetData().ToArray();
		_logger.LogDebug("TestReverse(): arr → {Arr}", arr);

        var empty = new DoublyLinkedList<int>(new int[] { });
		_logger.LogDebug("TestReverse(): empty → {Empty}", empty);
        empty.Reverse();
        var emptyArr = empty.GetData().ToArray();
		_logger.LogDebug("TestReverse(): emptyArr → {EmptyArr}", emptyArr);

        Assert.That(arr, Is.EqualTo(new[] { 4, 3, 2, 1, 0 }));
        Assert.That(emptyArr, Is.EqualTo(new int[] { }));
		_logger.LogDebug("<<< Exiting TestReverse()");
    }

    [Test]
    public static void TestGetDataReversed()
    {
		_logger.LogDebug(">>> Entering TestGetDataReversed()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        var arr = dll.GetData().ToArray();
		_logger.LogDebug("TestGetDataReversed(): arr → {Arr}", arr);
        var reversedArr = dll.GetDataReversed().ToArray();
		_logger.LogDebug("TestGetDataReversed(): reversedArr → {ReversedArr}", reversedArr);
		_logger.LogDebug("TestGetDataReversed(): dll → {Dll}", dll);

        Assert.That(arr, Is.EqualTo(new[] { 0, 1, 2, 3, 4 }));
        Assert.That(reversedArr, Is.EqualTo(new[] { 4, 3, 2, 1, 0 }));
		_logger.LogDebug("<<< Exiting TestGetDataReversed()");
    }
}
