using Algorithms.Search;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public static class FibonacciSearcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 10)] int n)
    {
		_logger.LogError(">>> Entering FindIndex_ItemPresent_IndexCorrect(n={N})", n);
        // Arranges
        var searcher = new FibonacciSearcher<int>();
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): searcher → {Searcher}", n, searcher);
        var arrayToSearch = Helper.GetSortedArray(n);
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): arrayToSearch → {ArrayToSearch}", n, arrayToSearch);
        var present = Helper.GetItemIn(arrayToSearch);
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): present → {Present}", n, present);

        // Act
        var actualIndex = searcher.FindIndex(arrayToSearch, present);
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): actualIndex → {ActualIndex}", n, actualIndex);

        // Assert
        arrayToSearch[actualIndex].Should().Be(present);
		_logger.LogWarning("<<< Exiting FindIndex_ItemPresent_IndexCorrect(n={N})", n);
    }

    [Test]
    public static void FindIndex_ItemMissing_MinusOneReturned([Random(1, 1000, 10)] int n)
    {
		_logger.LogError(">>> Entering FindIndex_ItemMissing_MinusOneReturned(n={N})", n);
        // Arranges
        var searcher = new FibonacciSearcher<int>();
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): searcher → {Searcher}", n, searcher);
        var arrayToSearch = Helper.GetSortedArray(n);
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): arrayToSearch → {ArrayToSearch}", n, arrayToSearch);
        var present = Helper.GetItemNotIn(arrayToSearch);
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): present → {Present}", n, present);
        var expectedIndex = -1;

        // Act
        var actualIndex = searcher.FindIndex(arrayToSearch, present);
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): actualIndex → {ActualIndex}", n, actualIndex);

        // Assert
        actualIndex.Should().Be(expectedIndex);
		_logger.LogWarning("<<< Exiting FindIndex_ItemMissing_MinusOneReturned(n={N})", n);
    }

    [Test]
    public static void FindIndex_ArrayEmpty_MinusOneReturned([Random(1, 1000, 10)] int missingItem)
    {
		_logger.LogError(">>> Entering FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem})", missingItem);
        // Arrange
        var searcher = new FibonacciSearcher<int>();
		_logger.LogError("FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem}): searcher → {Searcher}", missingItem, searcher);
        var sortedArray = Array.Empty<int>();
		_logger.LogError("FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem}): sortedArray → {SortedArray}", missingItem, sortedArray);
        var expectedIndex = -1;

        // Act
        var actualIndex = searcher.FindIndex(sortedArray, missingItem);
		_logger.LogError("FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem}): actualIndex → {ActualIndex}", missingItem, actualIndex);

        // Assert
        actualIndex.Should().Be(expectedIndex);
		_logger.LogWarning("<<< Exiting FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem})", missingItem);
    }

    [TestCase(null, "a")]
    [TestCase(new[] { "a", "b", "c" }, null)]
    [TestCase(null, null)]
    public static void FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(string[] sortedArray, string searchItem)
    {
		_logger.LogError(">>> Entering FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(sortedArray={SortedArray},searchItem={SearchItem})", sortedArray, searchItem);
        // Arranges
        var searcher = new FibonacciSearcher<string>();
		_logger.LogError("FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(sortedArray={SortedArray},searchItem={SearchItem}): searcher → {Searcher}", sortedArray, searchItem, searcher);

        // Act
        Action action = () => searcher.FindIndex(sortedArray, searchItem);
		_logger.LogError("FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(sortedArray={SortedArray},searchItem={SearchItem}): action → {Action}", sortedArray, searchItem, action);

        // Assert
        action.Should().Throw<ArgumentNullException>();
		_logger.LogWarning("<<< Exiting FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(sortedArray={SortedArray},searchItem={SearchItem})", sortedArray, searchItem);
    }
}
