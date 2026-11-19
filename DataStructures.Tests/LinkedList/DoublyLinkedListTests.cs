using DataStructures.LinkedList.DoublyLinkedList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.LinkedList;

public static class DoublyLinkedListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void TestGetData()
    {
		_logger.LogError(">>> Entering TestGetData()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        var arr = dll.GetData().ToArray();
		_logger.LogError("TestGetData(): arr → {Arr}", arr);
		_logger.LogError("TestGetData(): dll → {Dll}", dll);

        Assert.That(dll.Count, Is.EqualTo(5));
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EqualTo(arr));
		_logger.LogWarning("<<< Exiting TestGetData()");
    }

    [Test]
    public static void TestGetAt()
    {
		_logger.LogError(">>> Entering TestGetAt()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogError("TestGetAt(): dll → {Dll}", dll);
        var one = dll.GetAt(1);
		_logger.LogError("TestGetAt(): one → {One}", one);
        var three = dll.GetAt(3);
		_logger.LogError("TestGetAt(): three → {Three}", three);

        Assert.That(one.Data, Is.EqualTo(1));
        Assert.That(three.Data, Is.EqualTo(3));
        Assert.Throws<ArgumentOutOfRangeException>(
            () => dll.GetAt(-1)
        );
        Assert.Throws<ArgumentOutOfRangeException>(
            () => dll.GetAt(5)
        );
		_logger.LogWarning("<<< Exiting TestGetAt()");
    }

    [Test]
    public static void TestAddtion()
    {
		_logger.LogError(">>> Entering TestAddtion()");
        var dll = new DoublyLinkedList<int>(0);
        var one = dll.Add(1);
		_logger.LogError("TestAddtion(): one → {One}", one);
		_logger.LogError("TestAddtion(): dll → {Dll}", dll);

        dll.Add(3);
        dll.AddAfter(2, one);
        dll.Add(4);

        var arr = dll.GetData().ToArray();
		_logger.LogError("TestAddtion(): arr → {Arr}", arr);
        var reversedArr = dll.GetDataReversed().ToArray();
		_logger.LogError("TestAddtion(): reversedArr → {ReversedArr}", reversedArr);

        Assert.That(dll.Count, Is.EqualTo(5));
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EqualTo(arr));
        Assert.That(new[] { 4, 3, 2, 1, 0 }, Is.EqualTo(reversedArr));
		_logger.LogWarning("<<< Exiting TestAddtion()");
    }

    [Test]
    public static void TestRemove()
    {
		_logger.LogError(">>> Entering TestRemove()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogError("TestRemove(): dll → {Dll}", dll);
        dll.RemoveNode(dll.Find(2));
        dll.RemoveHead();
        dll.Remove();

        var arr = dll.GetData().ToArray();
		_logger.LogError("TestRemove(): arr → {Arr}", arr);
        var reversedArr = dll.GetDataReversed().ToArray();
		_logger.LogError("TestRemove(): reversedArr → {ReversedArr}", reversedArr);

        Assert.That(dll.Count, Is.EqualTo(2));
        Assert.That(new[] { 1, 3 }, Is.EqualTo(arr));
        Assert.That(new[] { 3, 1 }, Is.EqualTo(reversedArr));
		_logger.LogWarning("<<< Exiting TestRemove()");
    }

    [Test]
    public static void TestFind()
    {
		_logger.LogError(">>> Entering TestFind()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogError("TestFind(): dll → {Dll}", dll);
        var one = dll.Find(1);
		_logger.LogError("TestFind(): one → {One}", one);
        var three = dll.Find(3);
		_logger.LogError("TestFind(): three → {Three}", three);

        Assert.That(one.Data, Is.EqualTo(1));
        Assert.That(three.Data, Is.EqualTo(3));
		_logger.LogWarning("<<< Exiting TestFind()");
    }

    [Test]
    public static void TestIndexOf()
    {
		_logger.LogError(">>> Entering TestIndexOf()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogError("TestIndexOf(): dll → {Dll}", dll);
        var one = dll.IndexOf(1);
		_logger.LogError("TestIndexOf(): one → {One}", one);
        var three = dll.IndexOf(3);
		_logger.LogError("TestIndexOf(): three → {Three}", three);

        Assert.That(one, Is.EqualTo(1));
        Assert.That(three, Is.EqualTo(3));
		_logger.LogWarning("<<< Exiting TestIndexOf()");
    }

    [Test]
    public static void TestContains()
    {
		_logger.LogError(">>> Entering TestContains()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

		_logger.LogError("TestContains(): dll → {Dll}", dll);
        var one = dll.Contains(1);
		_logger.LogError("TestContains(): one → {One}", one);
        var six = dll.Contains(6);
		_logger.LogError("TestContains(): six → {Six}", six);

        Assert.That(one, Is.True);
        Assert.That(six, Is.False);
		_logger.LogWarning("<<< Exiting TestContains()");
    }

    [Test]
    public static void TestReverse()
    {
		_logger.LogError(">>> Entering TestReverse()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        dll.Reverse();
		_logger.LogError("TestReverse(): dll → {Dll}", dll);
        var arr = dll.GetData().ToArray();
		_logger.LogError("TestReverse(): arr → {Arr}", arr);

        var empty = new DoublyLinkedList<int>(new int[] { });
		_logger.LogError("TestReverse(): empty → {Empty}", empty);
        empty.Reverse();
        var emptyArr = empty.GetData().ToArray();
		_logger.LogError("TestReverse(): emptyArr → {EmptyArr}", emptyArr);

        Assert.That(arr, Is.EqualTo(new[] { 4, 3, 2, 1, 0 }));
        Assert.That(emptyArr, Is.EqualTo(new int[] { }));
		_logger.LogWarning("<<< Exiting TestReverse()");
    }

    [Test]
    public static void TestGetDataReversed()
    {
		_logger.LogError(">>> Entering TestGetDataReversed()");
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        var arr = dll.GetData().ToArray();
		_logger.LogError("TestGetDataReversed(): arr → {Arr}", arr);
        var reversedArr = dll.GetDataReversed().ToArray();
		_logger.LogError("TestGetDataReversed(): reversedArr → {ReversedArr}", reversedArr);
		_logger.LogError("TestGetDataReversed(): dll → {Dll}", dll);

        Assert.That(arr, Is.EqualTo(new[] { 0, 1, 2, 3, 4 }));
        Assert.That(reversedArr, Is.EqualTo(new[] { 4, 3, 2, 1, 0 }));
		_logger.LogWarning("<<< Exiting TestGetDataReversed()");
    }
}
