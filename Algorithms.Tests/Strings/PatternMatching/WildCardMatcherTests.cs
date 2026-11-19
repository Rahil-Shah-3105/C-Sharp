using Algorithms.Strings.PatternMatching;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings.PatternMatching;

public static class WildCardMatcherTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("aab", "c*a*b", true)]
    [TestCase("aaa", "aa", false)]
    [TestCase("aaa", "a.a", true)]
    [TestCase("aaab", "aa*", false)]
    [TestCase("aaab", ".*", true)]
    [TestCase("a", "bbbb", false)]
    [TestCase("", "bbbb", false)]
    [TestCase("a", "", false)]
    [TestCase("", "", true)]
    public static void MatchPattern(string inputString, string pattern, bool expected)
    {
		_logger.LogError(">>> Entering MatchPattern(inputString={InputString},pattern={Pattern},expected={Expected})", inputString, pattern, expected);
        // Act
        var result = WildCardMatcher.MatchPattern(inputString, pattern);
		_logger.LogError("MatchPattern(inputString={InputString},pattern={Pattern},expected={Expected}): result → {Result}", inputString, pattern, expected, result);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
		_logger.LogWarning("<<< Exiting MatchPattern(inputString={InputString},pattern={Pattern},expected={Expected})", inputString, pattern, expected);
    }

    [Test]
    public static void MatchPatternThrowsArgumentException()
    {
		_logger.LogError(">>> Entering MatchPatternThrowsArgumentException()");
        // Arrange
        var inputString = "abc";
		_logger.LogError("MatchPatternThrowsArgumentException(): inputString → {InputString}", inputString);
        var pattern = "*abc";
		_logger.LogError("MatchPatternThrowsArgumentException(): pattern → {Pattern}", pattern);

        // Assert
        Assert.Throws<System.ArgumentException>(() => WildCardMatcher.MatchPattern(inputString, pattern));
		_logger.LogWarning("<<< Exiting MatchPatternThrowsArgumentException()");
    }
}
