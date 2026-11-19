using Algorithms.Search;
using Utilities.Exceptions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public static class LinearSearcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void Find_ItemPresent_ItemCorrect([Random(0, 1_000_000, 100)] int n)
    {
		_logger.LogError(">>> Entering Find_ItemPresent_ItemCorrect(n={N})", n);
        // Arrange
        var searcher = new LinearSearcher<int>();
		_logger.LogError("Find_ItemPresent_ItemCorrect(n={N}): searcher → {Searcher}", n, searcher);
        var random = Randomizer.CreateRandomizer();
		_logger.LogError("Find_ItemPresent_ItemCorrect(n={N}): random → {Random}", n, random);
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();
		_logger.LogError("Find_ItemPresent_ItemCorrect(n={N}): arrayToSearch → {ArrayToSearch}", n, arrayToSearch);

        // Act
        var expectedItem = Array.Find(arrayToSearch, x => x == arrayToSearch[n / 2]);
		_logger.LogError("Find_ItemPresent_ItemCorrect(n={N}): expectedItem → {ExpectedItem}", n, expectedItem);
        var actualItem = searcher.Find(arrayToSearch, x => x == arrayToSearch[n / 2]);
		_logger.LogError("Find_ItemPresent_ItemCorrect(n={N}): actualItem → {ActualItem}", n, actualItem);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
		_logger.LogWarning("<<< Exiting Find_ItemPresent_ItemCorrect(n={N})", n);
    }

    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect([Random(0, 1_000_000, 100)] int n)
    {
		_logger.LogError(">>> Entering FindIndex_ItemPresent_IndexCorrect(n={N})", n);
        // Arrange
        var searcher = new LinearSearcher<int>();
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): searcher → {Searcher}", n, searcher);
        var random = Randomizer.CreateRandomizer();
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): random → {Random}", n, random);
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): arrayToSearch → {ArrayToSearch}", n, arrayToSearch);

        // Act
        var expectedIndex = Array.FindIndex(arrayToSearch, x => x == arrayToSearch[n / 2]);
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): expectedIndex → {ExpectedIndex}", n, expectedIndex);
        var actualIndex = searcher.FindIndex(arrayToSearch, x => x == arrayToSearch[n / 2]);
		_logger.LogError("FindIndex_ItemPresent_IndexCorrect(n={N}): actualIndex → {ActualIndex}", n, actualIndex);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		_logger.LogWarning("<<< Exiting FindIndex_ItemPresent_IndexCorrect(n={N})", n);
    }

    [Test]
    public static void Find_ItemMissing_ItemNotFoundExceptionThrown([Random(0, 1_000_000, 100)] int n)
    {
		_logger.LogError(">>> Entering Find_ItemMissing_ItemNotFoundExceptionThrown(n={N})", n);
        // Arrange
        var searcher = new LinearSearcher<int>();
		_logger.LogError("Find_ItemMissing_ItemNotFoundExceptionThrown(n={N}): searcher → {Searcher}", n, searcher);
        var random = Randomizer.CreateRandomizer();
		_logger.LogError("Find_ItemMissing_ItemNotFoundExceptionThrown(n={N}): random → {Random}", n, random);
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();
		_logger.LogError("Find_ItemMissing_ItemNotFoundExceptionThrown(n={N}): arrayToSearch → {ArrayToSearch}", n, arrayToSearch);

        // Act
        // Assert
		_logger.LogWarning("<<< Exiting Find_ItemMissing_ItemNotFoundExceptionThrown(n={N})", n);
        _ = Assert.Throws<ItemNotFoundException>(() => searcher.Find(arrayToSearch, _ => false));
    }

    [Test]
    public static void FindIndex_ItemMissing_MinusOneReturned([Random(0, 1_000_000, 100)] int n)
    {
		_logger.LogError(">>> Entering FindIndex_ItemMissing_MinusOneReturned(n={N})", n);
        // Arrange
        var searcher = new LinearSearcher<int>();
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): searcher → {Searcher}", n, searcher);
        var random = Randomizer.CreateRandomizer();
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): random → {Random}", n, random);
        var arrayToSearch = Enumerable.Range(0, n).Select(_ => random.Next(0, 1000)).ToArray();
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): arrayToSearch → {ArrayToSearch}", n, arrayToSearch);

        // Act
        var actualIndex = searcher.FindIndex(arrayToSearch, _ => false);
		_logger.LogError("FindIndex_ItemMissing_MinusOneReturned(n={N}): actualIndex → {ActualIndex}", n, actualIndex);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(-1));
		_logger.LogWarning("<<< Exiting FindIndex_ItemMissing_MinusOneReturned(n={N})", n);
    }
}
