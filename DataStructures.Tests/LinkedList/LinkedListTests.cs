using DataStructures.LinkedList.SinglyLinkedList;
using Microsoft.Extensions.Logging;

namespace DataStructures.Tests.LinkedList;

public static class LinkedListTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void LengthWorksCorrectly([Random(0, 1000, 100)] int quantity)
    {
		_logger.LogError(">>> Entering LengthWorksCorrectly(quantity={Quantity})", quantity);
        // Arrange
        var a = new SinglyLinkedList<int>();
		_logger.LogError("LengthWorksCorrectly(quantity={Quantity}): a → {A}", quantity, a);

        // Act
        var r = TestContext.CurrentContext.Random;
        for (var i = 0; i < quantity; i++)
        {
            _ = a.AddFirst(r.Next());
        }

        // Assert
        Assert.That(quantity, Is.EqualTo(a.Length()));
		_logger.LogWarning("<<< Exiting LengthWorksCorrectly(quantity={Quantity})", quantity);
    }

    [Test]
    public static void LengthOnEmptyListIsZero()
    {
		_logger.LogError(">>> Entering LengthOnEmptyListIsZero()");
        // Arrange
        var a = new SinglyLinkedList<int>();
		_logger.LogError("LengthOnEmptyListIsZero(): a → {A}", a);

        // Act

        // Assert
        Assert.That(0, Is.EqualTo(a.Length()));
		_logger.LogWarning("<<< Exiting LengthOnEmptyListIsZero()");
    }

    [Test]
    public static void GetItemsFromLinkedList()
    {
		_logger.LogError(">>> Entering GetItemsFromLinkedList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogError("GetItemsFromLinkedList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var items = testObj.GetListData();
		_logger.LogError("GetItemsFromLinkedList(): items → {Items}", items);

        // Assert
        Assert.That(5, Is.EqualTo(items.Count()));
        Assert.That("O", Is.EqualTo(testObj.GetElementByIndex(4)));
		_logger.LogWarning("<<< Exiting GetItemsFromLinkedList()");
    }

    [Test]
    public static void GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()
    {
		_logger.LogError(">>> Entering GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()");
        // Arrange
        var list = new SinglyLinkedList<int>();
		_logger.LogError("GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown(): list → {List}", list);

        // Act
        _ = list.AddFirst(1);
        _ = list.AddFirst(2);
        _ = list.AddFirst(3);

        // Assert
		_logger.LogWarning("<<< Exiting GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()");
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(-1));
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(3));
    }


    [Test]
    public static void RemoveItemsFromList()
    {
		_logger.LogError(">>> Entering RemoveItemsFromList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogError("RemoveItemsFromList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("X");
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("I");
        _ = testObj.AddLast("O");

        // Act
        var xRemoveSucess = testObj.DeleteElement("X");
		_logger.LogError("RemoveItemsFromList(): xRemoveSucess → {XRemoveSucess}", xRemoveSucess);
        var oRemoveSucess = testObj.DeleteElement("O");
		_logger.LogError("RemoveItemsFromList(): oRemoveSucess → {ORemoveSucess}", oRemoveSucess);
        var eRemoveSucess = testObj.DeleteElement("E");
		_logger.LogError("RemoveItemsFromList(): eRemoveSucess → {ERemoveSucess}", eRemoveSucess);
        var lRemoveSucess = testObj.DeleteElement("L");
		_logger.LogError("RemoveItemsFromList(): lRemoveSucess → {LRemoveSucess}", lRemoveSucess);
        var l2RemoveSucess = testObj.DeleteElement("L");
		_logger.LogError("RemoveItemsFromList(): l2RemoveSucess → {L2RemoveSucess}", l2RemoveSucess);
        var l3RemoveSucess = testObj.DeleteElement("L");
		_logger.LogError("RemoveItemsFromList(): l3RemoveSucess → {L3RemoveSucess}", l3RemoveSucess);
        var nonExistantRemoveSucess = testObj.DeleteElement("F");
		_logger.LogError("RemoveItemsFromList(): nonExistantRemoveSucess → {NonExistantRemoveSucess}", nonExistantRemoveSucess);

        var resultString = testObj.GetElementByIndex(0) + testObj.GetElementByIndex(1);
		_logger.LogError("RemoveItemsFromList(): resultString → {ResultString}", resultString);

        // Assert
        Assert.That("HI", Is.EqualTo(resultString));
        Assert.That(xRemoveSucess, Is.True);
        Assert.That(oRemoveSucess, Is.True);
        Assert.That(eRemoveSucess, Is.True);
        Assert.That(lRemoveSucess, Is.True);
        Assert.That(l2RemoveSucess, Is.True);
        Assert.That(l3RemoveSucess, Is.False);
        Assert.That(nonExistantRemoveSucess, Is.False);
		_logger.LogWarning("<<< Exiting RemoveItemsFromList()");
    }

    [Test]
    public static void DeleteFirstFromList()
    {
		_logger.LogError(">>> Entering DeleteFirstFromList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogError("DeleteFirstFromList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var deleteSuccess = testObj.DeleteFirst();
		_logger.LogError("DeleteFirstFromList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.True);
        Assert.That(4, Is.EqualTo(testObj.Length()));
        Assert.That("E", Is.EqualTo(testObj.GetElementByIndex(0)));
		_logger.LogWarning("<<< Exiting DeleteFirstFromList()");
    }

    [Test]
    public static void DeleteFirstFromEmptyList()
    {
		_logger.LogError(">>> Entering DeleteFirstFromEmptyList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogError("DeleteFirstFromEmptyList(): testObj → {TestObj}", testObj);

        // Act
        var deleteSuccess = testObj.DeleteFirst();
		_logger.LogError("DeleteFirstFromEmptyList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.False);
		_logger.LogWarning("<<< Exiting DeleteFirstFromEmptyList()");
    }

    [Test]
    public static void DeleteLastFromList()
    {
		_logger.LogError(">>> Entering DeleteLastFromList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogError("DeleteLastFromList(): testObj → {TestObj}", testObj);
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var deleteSuccess = testObj.DeleteLast();
		_logger.LogError("DeleteLastFromList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.True);
        Assert.That(4, Is.EqualTo(testObj.Length()));
        Assert.That("L", Is.EqualTo(testObj.GetElementByIndex(testObj.Length() - 1)));
		_logger.LogWarning("<<< Exiting DeleteLastFromList()");
    }

    [Test]
    public static void DeleteLastFromEmptyList()
    {
		_logger.LogError(">>> Entering DeleteLastFromEmptyList()");
        // Arrange
        var testObj = new SinglyLinkedList<string>();
		_logger.LogError("DeleteLastFromEmptyList(): testObj → {TestObj}", testObj);

        // Act
        var deleteSuccess = testObj.DeleteLast();
		_logger.LogError("DeleteLastFromEmptyList(): deleteSuccess → {DeleteSuccess}", deleteSuccess);

        // Assert
        Assert.That(deleteSuccess, Is.False);
		_logger.LogWarning("<<< Exiting DeleteLastFromEmptyList()");
    }

}
