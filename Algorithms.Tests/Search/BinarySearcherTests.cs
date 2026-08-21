using Algorithms.Search;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public static class BinarySearcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
    {
		_logger.LogDebug(">>> Entering FindIndex_ItemPresent_IndexCorrect(n={N})", n);
        // Arrange
        var searcher = new BinarySearcher<int>();
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): searcher → {Searcher}", n, searcher);
        var random = Randomizer.CreateRandomizer();
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): random → {Random}", n, random);
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).OrderBy(x => x).ToArray();
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): arrayToSearch → {ArrayToSearch}", n, arrayToSearch);
        var selectedIndex = random.Next(0, n);
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): selectedIndex → {SelectedIndex}", n, selectedIndex);

        // Act
        var actualIndex = searcher.FindIndex(arrayToSearch, arrayToSearch[selectedIndex]);
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): actualIndex → {ActualIndex}", n, actualIndex);

        // Assert
        Assert.That(arrayToSearch[actualIndex], Is.EqualTo(arrayToSearch[selectedIndex]));
		_logger.LogDebug("<<< Exiting FindIndex_ItemPresent_IndexCorrect(n={N})", n);
    }

    [Test]
    public static void FindIndex_ItemMissing_MinusOneReturned(
        [Random(0, 1000, 10)] int n,
        [Random(-100, 1100, 10)] int missingItem)
    {
		_logger.LogDebug(">>> Entering FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem})", n, missingItem);
        // Arrange
        var searcher = new BinarySearcher<int>();
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): searcher → {Searcher}", n, missingItem, searcher);
        var random = Randomizer.CreateRandomizer();
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): random → {Random}", n, missingItem, random);
        var arrayToSearch = Enumerable.Range(0, n)
            .Select(_ => random.Next(0, 1000))
            .Where(x => x != missingItem)
            .OrderBy(x => x).ToArray();

        // Act
        var actualIndex = searcher.FindIndex(arrayToSearch, missingItem);
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): actualIndex → {ActualIndex}", n, missingItem, actualIndex);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(-1));
		_logger.LogDebug("<<< Exiting FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem})", n, missingItem);
    }

    [Test]
    public static void FindIndex_ArrayEmpty_MinusOneReturned([Random(100)] int itemToSearch)
    {
		_logger.LogDebug(">>> Entering FindIndex_ArrayEmpty_MinusOneReturned(itemToSearch={ItemToSearch})", itemToSearch);
        // Arrange
        var searcher = new BinarySearcher<int>();
		_logger.LogDebug("FindIndex_ArrayEmpty_MinusOneReturned(itemToSearch={ItemToSearch}): searcher → {Searcher}", itemToSearch, searcher);
        var arrayToSearch = new int[0];

        // Act
        var actualIndex = searcher.FindIndex(arrayToSearch, itemToSearch);
		_logger.LogDebug("FindIndex_ArrayEmpty_MinusOneReturned(itemToSearch={ItemToSearch}): actualIndex → {ActualIndex}", itemToSearch, actualIndex);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(-1));
		_logger.LogDebug("<<< Exiting FindIndex_ArrayEmpty_MinusOneReturned(itemToSearch={ItemToSearch})", itemToSearch);
    }
}
