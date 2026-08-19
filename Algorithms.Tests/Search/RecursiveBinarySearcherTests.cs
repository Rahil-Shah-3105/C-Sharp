using Algorithms.Search;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public static class RecursiveBinarySearcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void FindIndex_ItemPresent_IndexCorrect([Random(1, 1000, 100)] int n)
    {
		_logger.LogDebug(">>> Entering FindIndex_ItemPresent_IndexCorrect(n={N})", n);
        // Arrange
        var subject = new RecursiveBinarySearcher<int>();
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): subject → {Subject}", n, subject);
        var randomizer = Randomizer.CreateRandomizer();
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): randomizer → {Randomizer}", n, randomizer);
        var selectedIndex = randomizer.Next(0, n);
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): selectedIndex → {SelectedIndex}", n, selectedIndex);
        var collection = Enumerable.Range(0, n).Select(_ => randomizer.Next(0, 1000)).OrderBy(x => x).ToList();
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): collection → {Collection}", n, collection);

        // Act
        var actualIndex = subject.FindIndex(collection, collection[selectedIndex]);
		_logger.LogDebug("FindIndex_ItemPresent_IndexCorrect(n={N}): actualIndex → {ActualIndex}", n, actualIndex);

        // Assert
        Assert.That(collection[actualIndex], Is.EqualTo(collection[selectedIndex]));
		_logger.LogDebug("<<< Exiting FindIndex_ItemPresent_IndexCorrect(n={N})", n);
    }

    [Test]
    public static void FindIndex_ItemMissing_MinusOneReturned(
        [Random(0, 1000, 10)] int n,
        [Random(-100, 1100, 10)] int missingItem)
    {
		_logger.LogDebug(">>> Entering FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem})", n, missingItem);
        // Arrange
        var subject = new RecursiveBinarySearcher<int>();
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): subject → {Subject}", n, missingItem, subject);
        var random = Randomizer.CreateRandomizer();
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): random → {Random}", n, missingItem, random);
        var collection = Enumerable.Range(0, n)
            .Select(_ => random.Next(0, 1000))
            .Where(x => x != missingItem)
            .OrderBy(x => x).ToList();

        // Act
        var actualIndex = subject.FindIndex(collection, missingItem);
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
        var subject = new RecursiveBinarySearcher<int>();
		_logger.LogDebug("FindIndex_ArrayEmpty_MinusOneReturned(itemToSearch={ItemToSearch}): subject → {Subject}", itemToSearch, subject);
        var collection = new int[0];

        // Act
        var actualIndex = subject.FindIndex(collection, itemToSearch);
		_logger.LogDebug("FindIndex_ArrayEmpty_MinusOneReturned(itemToSearch={ItemToSearch}): actualIndex → {ActualIndex}", itemToSearch, actualIndex);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(-1));
		_logger.LogDebug("<<< Exiting FindIndex_ArrayEmpty_MinusOneReturned(itemToSearch={ItemToSearch})", itemToSearch);
    }

    [Test]
    public static void FindIndex_NullCollection_Throws()
    {
		_logger.LogDebug(">>> Entering FindIndex_NullCollection_Throws()");
        // Arrange
        var subject = new RecursiveBinarySearcher<int>();
		_logger.LogDebug("FindIndex_NullCollection_Throws(): subject → {Subject}", subject);
        var collection = (IList<int>?)null;
		_logger.LogDebug("FindIndex_NullCollection_Throws(): collection → {Collection}", collection);

        // Act
        Action act = () => subject.FindIndex(collection, 42);
		_logger.LogDebug("FindIndex_NullCollection_Throws(): act → {Act}", act);

        // Assert
        act.Should().Throw<ArgumentNullException>();
		_logger.LogDebug("<<< Exiting FindIndex_NullCollection_Throws()");
    }
}
