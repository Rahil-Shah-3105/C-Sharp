using Algorithms.Strings.PatternMatching;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public class ZblockSubstringSearchTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("abc", "abcdef", 1)]
    [TestCase("xxx", "abxxxcdexxxf", 2)]
    [TestCase("aa", "waapaaxcdaalaabb", 4)]
    [TestCase("ABC", "ABAAABCDBBABCDDEBCABC", 3)]
    [TestCase("xxx", "abcdefghij", 0)]
    [TestCase("aab", "caabxaaaz", 1)]
    [TestCase("abc", "xababaxbabcdabx", 1)]
    [TestCase("GEEK", "GEEKS FOR GEEKS", 2)]
    [TestCase("ground", "Hello, playground!", 1)]
    public void Test(string pattern, string text, int expectedOccurences)
    {
		_logger.LogDebug(">>> Entering Test(pattern={Pattern},text={Text},expectedOccurences={ExpectedOccurences})", pattern, text, expectedOccurences);
        var occurencesFound = ZblockSubstringSearch.FindSubstring(pattern, text);
        Assert.That(occurencesFound, Is.EqualTo(expectedOccurences));
		_logger.LogDebug("Test(pattern={Pattern},text={Text},expectedOccurences={ExpectedOccurences}): occurencesFound → {OccurencesFound}", pattern, text, expectedOccurences, occurencesFound);
		_logger.LogDebug("<<< Exiting Test(pattern={Pattern},text={Text},expectedOccurences={ExpectedOccurences})", pattern, text, expectedOccurences);
    }
}
