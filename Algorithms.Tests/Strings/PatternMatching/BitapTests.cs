using Microsoft.Extensions.Logging;
using Algorithms.Strings.PatternMatching;

namespace Algorithms.Tests.Strings.PatternMatching;

[TestFixture]
public class BitapTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void FindExactPattern_EmptyTextReturnsError()
    {
		_logger.LogError(">>> Entering FindExactPattern_EmptyTextReturnsError()");
        Assert.That(Bitap.FindExactPattern("", "abc"), Is.EqualTo(-1));
		_logger.LogWarning("<<< Exiting FindExactPattern_EmptyTextReturnsError()");
    }

    [Test]
    public void FindExactPattern_EmptyPatternReturnsZero()
    {
		_logger.LogError(">>> Entering FindExactPattern_EmptyPatternReturnsZero()");
        Assert.That(Bitap.FindExactPattern("abc", ""), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindExactPattern_EmptyPatternReturnsZero()");
    }

    [Test]
    public void FindExactPattern_PatternFoundAtBeginning()
    {
		_logger.LogError(">>> Entering FindExactPattern_PatternFoundAtBeginning()");
        Assert.That(Bitap.FindExactPattern("hello world", "hello"), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindExactPattern_PatternFoundAtBeginning()");
    }

    [Test]
    public void FindExactPattern_PatternFoundInTheMiddle()
    {
		_logger.LogError(">>> Entering FindExactPattern_PatternFoundInTheMiddle()");
        Assert.That(Bitap.FindExactPattern("abcabc", "cab"), Is.EqualTo(2));
		_logger.LogWarning("<<< Exiting FindExactPattern_PatternFoundInTheMiddle()");
    }

    [Test]
    public void FindExactPattern_PatternFoundAtEnd()
    {
		_logger.LogError(">>> Entering FindExactPattern_PatternFoundAtEnd()");
        Assert.That(Bitap.FindExactPattern("the end", "end"), Is.EqualTo(4));
		_logger.LogWarning("<<< Exiting FindExactPattern_PatternFoundAtEnd()");
    }

    [Test]
    public void FindExactPattern_PatternNotFound()
    {
		_logger.LogError(">>> Entering FindExactPattern_PatternNotFound()");
        Assert.That(Bitap.FindExactPattern("abcdefg", "xyz"), Is.EqualTo(-1));
		_logger.LogWarning("<<< Exiting FindExactPattern_PatternNotFound()");
    }

    [Test]
    public void FindExactPattern_PatternLongerThanText()
    {
		_logger.LogError(">>> Entering FindExactPattern_PatternLongerThanText()");
        Assert.That(Bitap.FindExactPattern("short", "longerpattern"), Is.EqualTo(-1));
		_logger.LogWarning("<<< Exiting FindExactPattern_PatternLongerThanText()");
    }

    [Test]
    public void FindExactPattern_OverlappingPatterns()
    {
		_logger.LogError(">>> Entering FindExactPattern_OverlappingPatterns()");
        Assert.That(Bitap.FindExactPattern("ababab", "abab"), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindExactPattern_OverlappingPatterns()");
    }

    [Test]
    public void FindExactPattern_PatternTooLongThrowsException()
    {
		_logger.LogError(">>> Entering FindExactPattern_PatternTooLongThrowsException()");
        var longPattern = new string('a', 32);
		_logger.LogWarning("<<< Exiting FindExactPattern_PatternTooLongThrowsException()");
		_logger.LogError("FindExactPattern_PatternTooLongThrowsException(): longPattern → {LongPattern}", longPattern);
        Assert.Throws<ArgumentException>(() => Bitap.FindExactPattern("some text", longPattern));
    }

    [Test]
    public void FindExactPattern_SpecialCharactersInPattern()
    {
		_logger.LogError(">>> Entering FindExactPattern_SpecialCharactersInPattern()");
        Assert.That(Bitap.FindExactPattern("hello, world!", ", wo"), Is.EqualTo(5));
		_logger.LogWarning("<<< Exiting FindExactPattern_SpecialCharactersInPattern()");
    }

    [Test]
    public void FindFuzzyPattern_EmptyTextReturnsZero()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_EmptyTextReturnsZero()");
        Assert.That(Bitap.FindFuzzyPattern("", "abc", 1), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_EmptyTextReturnsZero()");
    }

    [Test]
    public void FindFuzzyPattern_EmptyPatternReturnsZero()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_EmptyPatternReturnsZero()");
        Assert.That(Bitap.FindFuzzyPattern("def", "", 1), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_EmptyPatternReturnsZero()");
    }

    [Test]
    public void FindFuzzyPattern_ExactMatchFound()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_ExactMatchFound()");
        Assert.That(Bitap.FindFuzzyPattern("hello world", "hello", 0), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_ExactMatchFound()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchWithOneMismatch()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_FuzzyMatchWithOneMismatch()");
        Assert.That(Bitap.FindFuzzyPattern("hello world", "hellp", 1), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_FuzzyMatchWithOneMismatch()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchWithMultipleMismatches()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_FuzzyMatchWithMultipleMismatches()");
        Assert.That(Bitap.FindFuzzyPattern("abcde", "xbcdz", 2), Is.EqualTo(0));
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_FuzzyMatchWithMultipleMismatches()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchAtEnd()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_FuzzyMatchAtEnd()");
        Assert.That(Bitap.FindFuzzyPattern("abcdefg", "efx", 1), Is.EqualTo(4));
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_FuzzyMatchAtEnd()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchNotFound()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_FuzzyMatchNotFound()");
        Assert.That(Bitap.FindFuzzyPattern("abcdefg", "xyz", 2), Is.EqualTo(-1));
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_FuzzyMatchNotFound()");
    }

    [Test]
    public void FindFuzzyPattern_PatternTooLongReturnsNegativeOne()
    {
		_logger.LogError(">>> Entering FindFuzzyPattern_PatternTooLongReturnsNegativeOne()");
        var longPattern = new string('a', 32);
		_logger.LogWarning("<<< Exiting FindFuzzyPattern_PatternTooLongReturnsNegativeOne()");
		_logger.LogError("FindFuzzyPattern_PatternTooLongReturnsNegativeOne(): longPattern → {LongPattern}", longPattern);
        Assert.That(Bitap.FindFuzzyPattern("some text", longPattern, 1), Is.EqualTo(-1));
    }
}
