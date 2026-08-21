using DataStructures.LinkedList.SinglyLinkedList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.LinkedList;

public static class LinkedListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void LengthWorksCorrectly([Random(0, 1000, 100)] int quantity)
    {
		_logger.LogDebug(">>> Entering LengthWorksCorrectly(quantity={Quantity})", quantity);
        // Arrange
        var a = new SinglyLinkedList<int>();
		_logger.LogDebug("LengthWorksCorrectly(quantity={Quantity}): a → {A}", quantity, a);

        // Act
        var r = TestContext.CurrentContext.Random;
        for (var i = 0; i < quantity; i++)
        {
            _ = a.AddFirst(r.Next());
			_logger.LogDebug("LengthWorksCorrectly(quantity={Quantity}): _ → {_}", quantity, _);
        }

        // Assert
        Assert.That(quantity, Is.EqualTo(a.Length()));
		_logger.LogDebug("<<< Exiting LengthWorksCorrectly(quantity={Quantity})", quantity);
    }

    [Test]
    public static void LengthOnEmptyListIsZero()
    {
		_logger.LogDebug(">>> Entering LengthOnEmptyListIsZero()");
        // Arrange
        var a = new SinglyLinkedList<int>();
		_logger.LogDebug("LengthOnEmptyListIsZero(): a → {A}", a);

        // Act

        // Assert
        Assert.That(0, Is.EqualTo(a.Length()));
		_logger.LogDebug("<<< Exiting LengthOnEmptyListIsZero()");
    }

    [Test]
    public static void GetItemsFromLinkedList()
    {
		_logger.LogDebug(">>> Entering GetItemsFromLinkedList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogDebug("GetItemsFromLinkedList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("H");
		_logger.LogDebug("GetItemsFromLinkedList(): _ → {_}", _);
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var items = testObj.GetListData();
		_logger.LogDebug("GetItemsFromLinkedList(): items → {Items}", items);

        // Assert
        Assert.That(5, Is.EqualTo(items.Count()));
        Assert.That("O", Is.EqualTo(testObj.GetElementByIndex(4)));
		_logger.LogDebug("<<< Exiting GetItemsFromLinkedList()");
    }

    [Test]
    public static void GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()
    {
		_logger.LogDebug(">>> Entering GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()");
        // Arrange
        var list = new SinglyLinkedList<int>();
		_logger.LogDebug("GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown(): list → {List}", list);

        // Act
        _ = list.AddFirst(1);
		_logger.LogDebug("GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown(): _ → {_}", _);
        _ = list.AddFirst(2);
        _ = list.AddFirst(3);

        // Assert
		_logger.LogDebug("<<< Exiting GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()");
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(-1));
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(3));
    }


    [Test]
    public static void RemoveItemsFromList()
    {
		_logger.LogDebug(">>> Entering RemoveItemsFromList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogDebug("RemoveItemsFromList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("X");
		_logger.LogDebug("RemoveItemsFromList(): _ → {_}", _);
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("I");
        _ = testObj.AddLast("O");

        // Act
        var xRemoveSucess = testObj.DeleteElement("X");
		_logger.LogDebug("RemoveItemsFromList(): xRemoveSucess → {XRemoveSucess}", xRemoveSucess);
        var oRemoveSucess = testObj.DeleteElement("O");
		_logger.LogDebug("RemoveItemsFromList(): oRemoveSucess → {ORemoveSucess}", oRemoveSucess);
        var eRemoveSucess = testObj.DeleteElement("E");
		_logger.LogDebug("RemoveItemsFromList(): eRemoveSucess → {ERemoveSucess}", eRemoveSucess);
        var lRemoveSucess = testObj.DeleteElement("L");
		_logger.LogDebug("RemoveItemsFromList(): lRemoveSucess → {LRemoveSucess}", lRemoveSucess);
        var l2RemoveSucess = testObj.DeleteElement("L");
		_logger.LogDebug("RemoveItemsFromList(): l2RemoveSucess → {L2RemoveSucess}", l2RemoveSucess);
        var l3RemoveSucess = testObj.DeleteElement("L");
		_logger.LogDebug("RemoveItemsFromList(): l3RemoveSucess → {L3RemoveSucess}", l3RemoveSucess);
        var nonExistantRemoveSucess = testObj.DeleteElement("F");
		_logger.LogDebug("RemoveItemsFromList(): nonExistantRemoveSucess → {NonExistantRemoveSucess}", nonExistantRemoveSucess);

        var resultString = testObj.GetElementByIndex(0) + testObj.GetElementByIndex(1);
		_logger.LogDebug("RemoveItemsFromList(): resultString → {ResultString}", resultString);

        // Assert
        Assert.That("HI", Is.EqualTo(resultString));
        Assert.That(xRemoveSucess, Is.True);
        Assert.That(oRemoveSucess, Is.True);
        Assert.That(eRemoveSucess, Is.True);
        Assert.That(lRemoveSucess, Is.True);
        Assert.That(l2RemoveSucess, Is.True);
        Assert.That(l3RemoveSucess, Is.False);
        Assert.That(nonExistantRemoveSucess, Is.False);
		_logger.LogDebug("<<< Exiting RemoveItemsFromList()");
    }

    [Test]
    public static void DeleteFirstFromList()
    {
		_logger.LogDebug(">>> Entering DeleteFirstFromList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogDebug("DeleteFirstFromList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("H");
		_logger.LogDebug("DeleteFirstFromList(): _ → {_}", _);
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var deleteSuccess = testObj.DeleteFirst();
		_logger.LogDebug("DeleteFirstFromList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.True);
        Assert.That(4, Is.EqualTo(testObj.Length()));
        Assert.That("E", Is.EqualTo(testObj.GetElementByIndex(0)));
		_logger.LogDebug("<<< Exiting DeleteFirstFromList()");
    }

    [Test]
    public static void DeleteFirstFromEmptyList()
    {
		_logger.LogDebug(">>> Entering DeleteFirstFromEmptyList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogDebug("DeleteFirstFromEmptyList(): testObj → {TestObj}", testObj);

        // Act
        var deleteSuccess = testObj.DeleteFirst();
		_logger.LogDebug("DeleteFirstFromEmptyList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.False);
		_logger.LogDebug("<<< Exiting DeleteFirstFromEmptyList()");
    }

    [Test]
    public static void DeleteLastFromList()
    {
		_logger.LogDebug(">>> Entering DeleteLastFromList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogDebug("DeleteLastFromList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("H");
		_logger.LogDebug("DeleteLastFromList(): _ → {_}", _);
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var deleteSuccess = testObj.DeleteLast();
		_logger.LogDebug("DeleteLastFromList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.True);
        Assert.That(4, Is.EqualTo(testObj.Length()));
        Assert.That("L", Is.EqualTo(testObj.GetElementByIndex(testObj.Length() - 1)));
		_logger.LogDebug("<<< Exiting DeleteLastFromList()");
    }

    [Test]
    public static void DeleteLastFromEmptyList()
    {
		_logger.LogDebug(">>> Entering DeleteLastFromEmptyList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogDebug("DeleteLastFromEmptyList(): testObj → {TestObj}", testObj);

        // Act
        var deleteSuccess = testObj.DeleteLast();
		_logger.LogDebug("DeleteLastFromEmptyList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.False);
		_logger.LogDebug("<<< Exiting DeleteLastFromEmptyList()");
    }

}
