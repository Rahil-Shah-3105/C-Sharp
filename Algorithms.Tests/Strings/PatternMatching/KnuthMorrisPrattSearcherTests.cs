using Algorithms.Strings.PatternMatching;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public static class KnuthMorrisPrattSearcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void FindIndexes_ItemsPresent_PassExpected()
    {
		_logger.LogDebug(">>> Entering FindIndexes_ItemsPresent_PassExpected()");
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
		_logger.LogDebug("FindIndexes_ItemsPresent_PassExpected(): searcher → {Searcher}", searcher);
        var str = "ABABAcdeABA";
		_logger.LogDebug("FindIndexes_ItemsPresent_PassExpected(): str → {Str}", str);
        var pat = "ABA";
		_logger.LogDebug("FindIndexes_ItemsPresent_PassExpected(): pat → {Pat}", pat);

        // Act
        var expectedItem = new[] { 0, 2, 8 };
        var actualItem = searcher.FindIndexes(str, pat);
		_logger.LogDebug("FindIndexes_ItemsPresent_PassExpected(): actualItem → {ActualItem}", actualItem);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
		_logger.LogDebug("<<< Exiting FindIndexes_ItemsPresent_PassExpected()");
    }

    [Test]
    public static void FindIndexes_ItemsMissing_NoIndexesReturned()
    {
		_logger.LogDebug(">>> Entering FindIndexes_ItemsMissing_NoIndexesReturned()");
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
		_logger.LogDebug("FindIndexes_ItemsMissing_NoIndexesReturned(): searcher → {Searcher}", searcher);
        var str = "ABABA";
		_logger.LogDebug("FindIndexes_ItemsMissing_NoIndexesReturned(): str → {Str}", str);
        var pat = "ABB";
		_logger.LogDebug("FindIndexes_ItemsMissing_NoIndexesReturned(): pat → {Pat}", pat);

        // Act & Assert
        var indexes = searcher.FindIndexes(str, pat);
		_logger.LogDebug("FindIndexes_ItemsMissing_NoIndexesReturned(): indexes → {Indexes}", indexes);

        // Assert
        Assert.That(indexes, Is.Empty);
		_logger.LogDebug("<<< Exiting FindIndexes_ItemsMissing_NoIndexesReturned()");
    }

    [Test]
    public static void LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected()
    {
		_logger.LogDebug(">>> Entering LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected()");
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected(): searcher → {Searcher}", searcher);
        var s = "ABA";
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected(): s → {S}", s);

        // Act
        var expectedItem = new[] { 0, 0, 1 };
        var actualItem = searcher.FindLongestPrefixSuffixValues(s);
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected(): actualItem → {ActualItem}", actualItem);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
		_logger.LogDebug("<<< Exiting LongestPrefixSuffixArray_PrefixSuffixOfLength1_PassExpected()");
    }

    [Test]
    public static void LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected()
    {
		_logger.LogDebug(">>> Entering LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected()");
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected(): searcher → {Searcher}", searcher);
        var s = "AABAACAABAA";
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected(): s → {S}", s);

        // Act
        var expectedItem = new[] { 0, 1, 0, 1, 2, 0, 1, 2, 3, 4, 5 };
        var actualItem = searcher.FindLongestPrefixSuffixValues(s);
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected(): actualItem → {ActualItem}", actualItem);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
		_logger.LogDebug("<<< Exiting LongestPrefixSuffixArray_PrefixSuffixOfLength5_PassExpected()");
    }

    [Test]
    public static void LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected()
    {
		_logger.LogDebug(">>> Entering LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected()");
        // Arrange
        var searcher = new KnuthMorrisPrattSearcher();
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected(): searcher → {Searcher}", searcher);
        var s = "AB";
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected(): s → {S}", s);

        // Act
        var expectedItem = new[] { 0, 0 };
        var actualItem = searcher.FindLongestPrefixSuffixValues(s);
		_logger.LogDebug("LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected(): actualItem → {ActualItem}", actualItem);

        // Assert
        Assert.That(actualItem, Is.EqualTo(expectedItem));
		_logger.LogDebug("<<< Exiting LongestPrefixSuffixArray_PrefixSuffixOfLength0_PassExpected()");
    }
}
