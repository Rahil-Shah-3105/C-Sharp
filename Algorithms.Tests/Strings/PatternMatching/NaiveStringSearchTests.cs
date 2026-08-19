using Algorithms.Strings.PatternMatching;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public static class NaiveStringSearchTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public static void ThreeMatchesFound_PassExpected()
    {
		_logger.LogDebug(">>> Entering ThreeMatchesFound_PassExpected()");
        // Arrange
        var pattern = "ABB";
		_logger.LogDebug("ThreeMatchesFound_PassExpected(): pattern → {Pattern}", pattern);
        var content = "ABBBAAABBAABBBBAB";
		_logger.LogDebug("ThreeMatchesFound_PassExpected(): content → {Content}", content);

        // Act
        var expectedOccurrences = new[] { 0, 6, 10 };
        var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
		_logger.LogDebug("ThreeMatchesFound_PassExpected(): actualOccurrences → {ActualOccurrences}", actualOccurrences);
        var sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);
		_logger.LogDebug("ThreeMatchesFound_PassExpected(): sequencesAreEqual → {SequencesAreEqual}", sequencesAreEqual);

        // Assert
        Assert.That(sequencesAreEqual, Is.True);
		_logger.LogDebug("<<< Exiting ThreeMatchesFound_PassExpected()");
    }

    [Test]
    public static void OneMatchFound_PassExpected()
    {
		_logger.LogDebug(">>> Entering OneMatchFound_PassExpected()");
        // Arrange
        var pattern = "BAAB";
		_logger.LogDebug("OneMatchFound_PassExpected(): pattern → {Pattern}", pattern);
        var content = "ABBBAAABBAABBBBAB";
		_logger.LogDebug("OneMatchFound_PassExpected(): content → {Content}", content);

        // Act
        var expectedOccurrences = new[] { 8 };
        var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
		_logger.LogDebug("OneMatchFound_PassExpected(): actualOccurrences → {ActualOccurrences}", actualOccurrences);
        var sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);
		_logger.LogDebug("OneMatchFound_PassExpected(): sequencesAreEqual → {SequencesAreEqual}", sequencesAreEqual);

        // Assert
        Assert.That(sequencesAreEqual, Is.True);
		_logger.LogDebug("<<< Exiting OneMatchFound_PassExpected()");
    }

    [Test]
    public static void NoMatchFound_PassExpected()
    {
		_logger.LogDebug(">>> Entering NoMatchFound_PassExpected()");
        // Arrange
        var pattern = "XYZ";
		_logger.LogDebug("NoMatchFound_PassExpected(): pattern → {Pattern}", pattern);
        var content = "ABBBAAABBAABBBBAB";
		_logger.LogDebug("NoMatchFound_PassExpected(): content → {Content}", content);

        // Act
        var expectedOccurrences = new int[0];
        var actualOccurrences = NaiveStringSearch.NaiveSearch(content, pattern);
		_logger.LogDebug("NoMatchFound_PassExpected(): actualOccurrences → {ActualOccurrences}", actualOccurrences);
        var sequencesAreEqual = expectedOccurrences.SequenceEqual(actualOccurrences);
		_logger.LogDebug("NoMatchFound_PassExpected(): sequencesAreEqual → {SequencesAreEqual}", sequencesAreEqual);

        // Assert
        Assert.That(sequencesAreEqual, Is.True);
		_logger.LogDebug("<<< Exiting NoMatchFound_PassExpected()");
    }
}
