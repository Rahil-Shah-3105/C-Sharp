using Microsoft.Extensions.Logging;
﻿using Algorithms.Strings.PatternMatching;

namespace Algorithms.Tests.Strings.PatternMatching;

[TestFixture]
public class BitapTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void FindExactPattern_EmptyTextReturnsError()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_EmptyTextReturnsError()");
        Assert.That(Bitap.FindExactPattern("", "abc"), Is.EqualTo(-1));
		_logger.LogDebug("<<< Exiting FindExactPattern_EmptyTextReturnsError()");
    }

    [Test]
    public void FindExactPattern_EmptyPatternReturnsZero()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_EmptyPatternReturnsZero()");
        Assert.That(Bitap.FindExactPattern("abc", ""), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindExactPattern_EmptyPatternReturnsZero()");
    }

    [Test]
    public void FindExactPattern_PatternFoundAtBeginning()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_PatternFoundAtBeginning()");
        Assert.That(Bitap.FindExactPattern("hello world", "hello"), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindExactPattern_PatternFoundAtBeginning()");
    }

    [Test]
    public void FindExactPattern_PatternFoundInTheMiddle()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_PatternFoundInTheMiddle()");
        Assert.That(Bitap.FindExactPattern("abcabc", "cab"), Is.EqualTo(2));
		_logger.LogDebug("<<< Exiting FindExactPattern_PatternFoundInTheMiddle()");
    }

    [Test]
    public void FindExactPattern_PatternFoundAtEnd()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_PatternFoundAtEnd()");
        Assert.That(Bitap.FindExactPattern("the end", "end"), Is.EqualTo(4));
		_logger.LogDebug("<<< Exiting FindExactPattern_PatternFoundAtEnd()");
    }

    [Test]
    public void FindExactPattern_PatternNotFound()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_PatternNotFound()");
        Assert.That(Bitap.FindExactPattern("abcdefg", "xyz"), Is.EqualTo(-1));
		_logger.LogDebug("<<< Exiting FindExactPattern_PatternNotFound()");
    }

    [Test]
    public void FindExactPattern_PatternLongerThanText()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_PatternLongerThanText()");
        Assert.That(Bitap.FindExactPattern("short", "longerpattern"), Is.EqualTo(-1));
		_logger.LogDebug("<<< Exiting FindExactPattern_PatternLongerThanText()");
    }

    [Test]
    public void FindExactPattern_OverlappingPatterns()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_OverlappingPatterns()");
        Assert.That(Bitap.FindExactPattern("ababab", "abab"), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindExactPattern_OverlappingPatterns()");
    }

    [Test]
    public void FindExactPattern_PatternTooLongThrowsException()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_PatternTooLongThrowsException()");
        var longPattern = new string('a', 32);
        Assert.Throws<ArgumentException>(() => Bitap.FindExactPattern("some text", longPattern));
		_logger.LogDebug("FindExactPattern_PatternTooLongThrowsException(): longPattern → {LongPattern}", longPattern);
		_logger.LogDebug("<<< Exiting FindExactPattern_PatternTooLongThrowsException()");
    }

    [Test]
    public void FindExactPattern_SpecialCharactersInPattern()
    {
		_logger.LogDebug(">>> Entering FindExactPattern_SpecialCharactersInPattern()");
        Assert.That(Bitap.FindExactPattern("hello, world!", ", wo"), Is.EqualTo(5));
		_logger.LogDebug("<<< Exiting FindExactPattern_SpecialCharactersInPattern()");
    }

    [Test]
    public void FindFuzzyPattern_EmptyTextReturnsZero()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_EmptyTextReturnsZero()");
        Assert.That(Bitap.FindFuzzyPattern("", "abc", 1), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_EmptyTextReturnsZero()");
    }

    [Test]
    public void FindFuzzyPattern_EmptyPatternReturnsZero()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_EmptyPatternReturnsZero()");
        Assert.That(Bitap.FindFuzzyPattern("def", "", 1), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_EmptyPatternReturnsZero()");
    }

    [Test]
    public void FindFuzzyPattern_ExactMatchFound()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_ExactMatchFound()");
        Assert.That(Bitap.FindFuzzyPattern("hello world", "hello", 0), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_ExactMatchFound()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchWithOneMismatch()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_FuzzyMatchWithOneMismatch()");
        Assert.That(Bitap.FindFuzzyPattern("hello world", "hellp", 1), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_FuzzyMatchWithOneMismatch()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchWithMultipleMismatches()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_FuzzyMatchWithMultipleMismatches()");
        Assert.That(Bitap.FindFuzzyPattern("abcde", "xbcdz", 2), Is.EqualTo(0));
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_FuzzyMatchWithMultipleMismatches()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchAtEnd()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_FuzzyMatchAtEnd()");
        Assert.That(Bitap.FindFuzzyPattern("abcdefg", "efx", 1), Is.EqualTo(4));
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_FuzzyMatchAtEnd()");
    }

    [Test]
    public void FindFuzzyPattern_FuzzyMatchNotFound()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_FuzzyMatchNotFound()");
        Assert.That(Bitap.FindFuzzyPattern("abcdefg", "xyz", 2), Is.EqualTo(-1));
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_FuzzyMatchNotFound()");
    }

    [Test]
    public void FindFuzzyPattern_PatternTooLongReturnsNegativeOne()
    {
		_logger.LogDebug(">>> Entering FindFuzzyPattern_PatternTooLongReturnsNegativeOne()");
        var longPattern = new string('a', 32);
        Assert.That(Bitap.FindFuzzyPattern("some text", longPattern, 1), Is.EqualTo(-1));
		_logger.LogDebug("FindFuzzyPattern_PatternTooLongReturnsNegativeOne(): longPattern → {LongPattern}", longPattern);
		_logger.LogDebug("<<< Exiting FindFuzzyPattern_PatternTooLongReturnsNegativeOne()");
    }
}
