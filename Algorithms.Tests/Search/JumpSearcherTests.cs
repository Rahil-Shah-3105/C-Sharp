using Algorithms.Search;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Search;

public class JumpSearcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void FindIndex_ItemPresent_ItemCorrect([Random(1, 1000, 100)] int n)
    {
		_logger.LogDebug(">>> Entering FindIndex_ItemPresent_ItemCorrect(n={N})", n);
        // Arrange
        var searcher = new JumpSearcher<int>();
		var __logStart = System.Diagnostics.Stopwatch.StartNew();
        var sortedArray = Enumerable.Range(0, n).Select(_ => TestContext.CurrentContext.Random.Next(1_000_000)).OrderBy(x => x).ToArray();
        var expectedIndex = TestContext.CurrentContext.Random.Next(sortedArray.Length);
		_logger.LogDebug("FindIndex_ItemPresent_ItemCorrect(n={N}): searcher → {Searcher}", n, searcher);
		_logger.LogDebug("FindIndex_ItemPresent_ItemCorrect(n={N}): sortedArray → {SortedArray}", n, sortedArray);
		_logger.LogDebug("FindIndex_ItemPresent_ItemCorrect(n={N}): expectedIndex → {ExpectedIndex}", n, expectedIndex);
		_logger.LogInformation("FindIndex_ItemPresent_ItemCorrect(n)={N}: find query executed in {Arg1} ms", n, __logStart.ElapsedMilliseconds);

        // Act
        var actualIndex = searcher.FindIndex(sortedArray, sortedArray[expectedIndex]);
		_logger.LogDebug("FindIndex_ItemPresent_ItemCorrect(n={N}): actualIndex → {ActualIndex}", n, actualIndex);

        // Assert
        sortedArray[actualIndex].Should().Be(sortedArray[expectedIndex]);
		_logger.LogDebug("<<< Exiting FindIndex_ItemPresent_ItemCorrect(n={N})", n);
    }

    [Test]
    public void FindIndex_ItemMissing_MinusOneReturned([Random(1, 1000, 10)] int n, [Random(-100, 1100, 10)] int missingItem)
    {
		_logger.LogDebug(">>> Entering FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem})", n, missingItem);
        // Arrange
        var searcher = new JumpSearcher<int>();
		var __logStart = System.Diagnostics.Stopwatch.StartNew();
        var sortedArray = Enumerable.Range(0, n).Select(_ => TestContext.CurrentContext.Random.Next(1_000_000)).Where(x => x != missingItem).OrderBy(x => x).ToArray();
        var expectedIndex = -1;
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): searcher → {Searcher}", n, missingItem, searcher);
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): sortedArray → {SortedArray}", n, missingItem, sortedArray);
		_logger.LogInformation("FindIndex_ItemMissing_MinusOneReturned(n,missingItem)={N},{MissingItem}: find query executed in {Arg2} ms", n, missingItem, __logStart.ElapsedMilliseconds);

        // Act
        var actualIndex = searcher.FindIndex(sortedArray, missingItem);
		_logger.LogDebug("FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem}): actualIndex → {ActualIndex}", n, missingItem, actualIndex);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		_logger.LogDebug("<<< Exiting FindIndex_ItemMissing_MinusOneReturned(n={N},missingItem={MissingItem})", n, missingItem);
    }

    [Test]
    public void FindIndex_ArrayEmpty_MinusOneReturned([Random(-100, 1100, 10)] int missingItem)
    {
		_logger.LogDebug(">>> Entering FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem})", missingItem);
        // Arrange
        var searcher = new JumpSearcher<int>();
		_logger.LogDebug("FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem}): searcher → {Searcher}", missingItem, searcher);
        var sortedArray = Array.Empty<int>();
		_logger.LogDebug("FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem}): sortedArray → {SortedArray}", missingItem, sortedArray);
        var expectedIndex = -1;

        // Act
        var actualIndex = searcher.FindIndex(sortedArray, missingItem);
		_logger.LogDebug("FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem}): actualIndex → {ActualIndex}", missingItem, actualIndex);

        // Assert
        Assert.That(actualIndex, Is.EqualTo(expectedIndex));
		_logger.LogDebug("<<< Exiting FindIndex_ArrayEmpty_MinusOneReturned(missingItem={MissingItem})", missingItem);
    }

    [TestCase(null, "abc")]
    [TestCase(new[] { "abc", "def", "ghi" }, null)]
    [TestCase(null, null)]
    public void FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(string[] sortedArray, string searchItem)
    {
		_logger.LogDebug(">>> Entering FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(sortedArray={SortedArray},searchItem={SearchItem})", sortedArray, searchItem);
        // Arrange
        var searcher = new JumpSearcher<string>();
		_logger.LogDebug("FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(sortedArray={SortedArray},searchItem={SearchItem}): searcher → {Searcher}", sortedArray, searchItem, searcher);

        // Act, Assert
        _ = Assert.Throws<ArgumentNullException>(() => searcher.FindIndex(sortedArray, searchItem));
		_logger.LogDebug("<<< Exiting FindIndex_ArrayNull_ItemNull_ArgumentNullExceptionThrown(sortedArray={SortedArray},searchItem={SearchItem})", sortedArray, searchItem);
    }
}
