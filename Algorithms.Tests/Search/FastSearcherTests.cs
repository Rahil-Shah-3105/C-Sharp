using Algorithms.Search;
using Utilities.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public static class FastSearcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect()
    {
		_logger.LogError(">>> Entering FindIndex_ItemPresent_IndexCorrect()");
        // Arrange
        var searcher = new FastSearcher();
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(): searcher → {Searcher}", searcher);
        var arr = Helper.GetSortedArray(1000);
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(): arr → {Arr}", arr);
        var present = Helper.GetItemIn(arr);
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(): present → {Present}", present);

        // Act
        var index = searcher.FindIndex(arr, present);

        // Assert
        arr[index].Should().Be(present);
		_logger.LogWarning("<<< Exiting FindIndex_ItemPresent_IndexCorrect()");
    }

    [TestCase(new[] { 1, 2 }, 1)]
    [TestCase(new[] { 1, 2 }, 2)]
    [TestCase(new[] { 1, 2, 3, 3, 3 }, 2)]
    public static void FindIndex_ItemPresentInSpecificCase_IndexCorrect(int[] arr, int present)
    {
		_logger.LogError(">>> Entering FindIndex_ItemPresentInSpecificCase_IndexCorrect(arr={Arr},present={Present})", arr, present);
        // Arrange
        var searcher = new FastSearcher();
		_logger.LogError("FindIndex_ItemPresentInSpecificCase_IndexCorrect(arr={Arr},present={Present}): searcher → {Searcher}", arr, present, searcher);

        // Act
        var index = searcher.FindIndex(arr, present);

        // Assert
        arr[index].Should().Be(present);
		_logger.LogWarning("<<< Exiting FindIndex_ItemPresentInSpecificCase_IndexCorrect(arr={Arr},present={Present})", arr, present);
    }

    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 3)] // Item in left segment (< indexBinary)
    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5)] // Item at binary index
    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 8)] // Item in right segment (> indexBinary)
    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1)] // Item at start
    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10)] // Item at end
    [TestCase(new[] { 1, 10, 20, 30, 40, 50, 60, 70, 80, 90 }, 20)] // Non-uniform distribution, interpolation < binary
    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 100 }, 100)] // Non-uniform distribution, interpolation > binary
    [TestCase(new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 }, 50)] // Uniform spacing
    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, 7)] // Larger array
    [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }, 15)] // Larger array, right side
    public static void FindIndex_ItemPresentDeterministic_IndexCorrect(int[] arr, int present)
    {
		_logger.LogError(">>> Entering FindIndex_ItemPresentDeterministic_IndexCorrect(arr={Arr},present={Present})", arr, present);
        // Arrange
        var searcher = new FastSearcher();
		_logger.LogError("FindIndex_ItemPresentDeterministic_IndexCorrect(arr={Arr},present={Present}): searcher → {Searcher}", arr, present, searcher);

        // Act
        var index = searcher.FindIndex(arr, present);

        // Assert
        arr[index].Should().Be(present);
		_logger.LogWarning("<<< Exiting FindIndex_ItemPresentDeterministic_IndexCorrect(arr={Arr},present={Present})", arr, present);
    }

    [Test]
    public static void FindIndex_ItemPresentInArrayOfDuplicates_IndexCorrect()
    {
		_logger.LogError(">>> Entering FindIndex_ItemPresentInArrayOfDuplicates_IndexCorrect()");
        // Arrange
        var searcher = new FastSearcher();
		_logger.LogError("FindIndex_ItemPresentInArrayOfDuplicates_IndexCorrect(): searcher → {Searcher}", searcher);
        var arr = CreateArrayOfDuplicates(1000, 0); // Helper for large duplicate arrays
		_logger.LogError("FindIndex_ItemPresentInArrayOfDuplicates_IndexCorrect(): arr → {Arr}", arr);
        var present = 0;

        // Act
        var index = searcher.FindIndex(arr, present);

        // Assert
        arr[index].Should().Be(0);
		_logger.LogWarning("<<< Exiting FindIndex_ItemPresentInArrayOfDuplicates_IndexCorrect()");
    }

    [TestCase(new int[0], 2)] // Empty array
    [TestCase(new[] { 1, 2, 3 }, 4)] // Item missing in array
    public static void FindIndex_ItemMissing_ItemNotFoundExceptionThrown(int[] arr, int missing)
    {
		_logger.LogError(">>> Entering FindIndex_ItemMissing_ItemNotFoundExceptionThrown(arr={Arr},missing={Missing})", arr, missing);
        // Arrange
        var searcher = new FastSearcher();
		_logger.LogError("FindIndex_ItemMissing_ItemNotFoundExceptionThrown(arr={Arr},missing={Missing}): searcher → {Searcher}", arr, missing, searcher);

        // Act
        Action act = () => searcher.FindIndex(arr, missing);
		_logger.LogError("FindIndex_ItemMissing_ItemNotFoundExceptionThrown(arr={Arr},missing={Missing}): act → {Act}", arr, missing, act);

        // Assert
        act.Should().Throw<ItemNotFoundException>();
		_logger.LogWarning("<<< Exiting FindIndex_ItemMissing_ItemNotFoundExceptionThrown(arr={Arr},missing={Missing})", arr, missing);
    }

    [Test]
    public static void FindIndex_ItemMissingInArrayOfDuplicates_ItemNotFoundExceptionThrown()
    {
		_logger.LogError(">>> Entering FindIndex_ItemMissingInArrayOfDuplicates_ItemNotFoundExceptionThrown()");
        // Arrange
        var searcher = new FastSearcher();
		_logger.LogError("FindIndex_ItemMissingInArrayOfDuplicates_ItemNotFoundExceptionThrown(): searcher → {Searcher}", searcher);
        var arr = CreateArrayOfDuplicates(1000, 0); // Helper for large duplicate arrays
		_logger.LogError("FindIndex_ItemMissingInArrayOfDuplicates_ItemNotFoundExceptionThrown(): arr → {Arr}", arr);
        var missing = 1;

        // Act
        Action act = () => searcher.FindIndex(arr, missing);
		_logger.LogError("FindIndex_ItemMissingInArrayOfDuplicates_ItemNotFoundExceptionThrown(): act → {Act}", act);

        // Assert
        act.Should().Throw<ItemNotFoundException>();
		_logger.LogWarning("<<< Exiting FindIndex_ItemMissingInArrayOfDuplicates_ItemNotFoundExceptionThrown()");
    }

    [Test]
    public static void FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown()
    {
		_logger.LogError(">>> Entering FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown()");
        // Arrange
        var searcher = new FastSearcher();
		_logger.LogError("FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown(): searcher → {Searcher}", searcher);
        var arr = Helper.GetSortedArray(1000);
		_logger.LogError("FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown(): arr → {Arr}", arr);
        var smaller = Helper.GetItemSmallerThanAllIn(arr);
		_logger.LogError("FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown(): smaller → {Smaller}", smaller);
        var bigger = Helper.GetItemBiggerThanAllIn(arr);
		_logger.LogError("FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown(): bigger → {Bigger}", bigger);

        // Act & Assert
        Action act1 = () => searcher.FindIndex(arr, smaller);
		_logger.LogError("FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown(): act1 → {Act1}", act1);
        Action act2 = () => searcher.FindIndex(arr, bigger);
		_logger.LogError("FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown(): act2 → {Act2}", act2);

        act1.Should().Throw<ItemNotFoundException>();
        act2.Should().Throw<ItemNotFoundException>();
		_logger.LogWarning("<<< Exiting FindIndex_ItemOutOfRange_ItemNotFoundExceptionThrown()");
    }

    private static int[] CreateArrayOfDuplicates(int length, int value)
    {
		_logger.LogError(">>> Entering CreateArrayOfDuplicates(length={Length},value={Value})", length, value);
        var arr = new int[length];
        Array.Fill(arr, value);
		_logger.LogWarning("<<< Exiting CreateArrayOfDuplicates(length={Length},value={Value})", length, value);
        return arr;
    }
}
