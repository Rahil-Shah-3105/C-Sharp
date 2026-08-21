using Algorithms.Other;
using NUnit.Framework;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public class BoyerMooreMajorityVoteTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [Test]
    public void FindMajority_SimpleMajority_ReturnsCorrectElement()
    {
		_logger.LogDebug(">>> Entering FindMajority_SimpleMajority_ReturnsCorrectElement()");
        var nums = new[] { 3, 3, 4, 2, 3, 3, 3 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_SimpleMajority_ReturnsCorrectElement(): result → {Result}", result);
        result.Should().Be(3);
		_logger.LogDebug("<<< Exiting FindMajority_SimpleMajority_ReturnsCorrectElement()");
    }

    [Test]
    public void FindMajority_AllSameElements_ReturnsThatElement()
    {
		_logger.LogDebug(">>> Entering FindMajority_AllSameElements_ReturnsThatElement()");
        var nums = new[] { 5, 5, 5, 5 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_AllSameElements_ReturnsThatElement(): result → {Result}", result);
        result.Should().Be(5);
		_logger.LogDebug("<<< Exiting FindMajority_AllSameElements_ReturnsThatElement()");
    }

    [Test]
    public void FindMajority_NoMajority_ReturnsNull()
    {
		_logger.LogDebug(">>> Entering FindMajority_NoMajority_ReturnsNull()");
        var nums = new[] { 1, 2, 3, 4 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_NoMajority_ReturnsNull(): result → {Result}", result);
        result.Should().BeNull();
		_logger.LogDebug("<<< Exiting FindMajority_NoMajority_ReturnsNull()");
    }

    [Test]
    public void FindMajority_EmptyArray_ReturnsNull()
    {
		_logger.LogDebug(">>> Entering FindMajority_EmptyArray_ReturnsNull()");
        var nums = Array.Empty<int>();
		_logger.LogDebug("<<< Exiting FindMajority_EmptyArray_ReturnsNull()");
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_EmptyArray_ReturnsNull(): result → {Result}", result);
		_logger.LogDebug("FindMajority_EmptyArray_ReturnsNull(): nums → {Nums}", nums);
        result.Should().BeNull();
    }

    [Test]
    public void FindMajority_NullArray_ReturnsNull()
    {
		_logger.LogDebug(">>> Entering FindMajority_NullArray_ReturnsNull()");
        int[]? nums = null;
        var result = BoyerMooreMajorityVote.FindMajority(nums!);
		_logger.LogDebug("FindMajority_NullArray_ReturnsNull(): result → {Result}", result);
        result.Should().BeNull();
		_logger.LogDebug("<<< Exiting FindMajority_NullArray_ReturnsNull()");
    }

    [Test]
    public void FindMajority_SingleElement_ReturnsThatElement()
    {
		_logger.LogDebug(">>> Entering FindMajority_SingleElement_ReturnsThatElement()");
        var nums = new[] { 7 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_SingleElement_ReturnsThatElement(): result → {Result}", result);
        result.Should().Be(7);
		_logger.LogDebug("<<< Exiting FindMajority_SingleElement_ReturnsThatElement()");
    }

    [Test]
    public void FindMajority_MajorityAtEnd_ReturnsCorrectElement()
    {
		_logger.LogDebug(">>> Entering FindMajority_MajorityAtEnd_ReturnsCorrectElement()");
        var nums = new[] { 1, 2, 2, 2, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_MajorityAtEnd_ReturnsCorrectElement(): result → {Result}", result);
        result.Should().Be(2);
		_logger.LogDebug("<<< Exiting FindMajority_MajorityAtEnd_ReturnsCorrectElement()");
    }

    [Test]
    public void FindMajority_MajorityAtStart_ReturnsCorrectElement()
    {
		_logger.LogDebug(">>> Entering FindMajority_MajorityAtStart_ReturnsCorrectElement()");
        var nums = new[] { 8, 8, 8, 8, 1, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_MajorityAtStart_ReturnsCorrectElement(): result → {Result}", result);
        result.Should().Be(8);
		_logger.LogDebug("<<< Exiting FindMajority_MajorityAtStart_ReturnsCorrectElement()");
    }

    [Test]
    public void FindMajority_NegativeNumbers_ReturnsCorrectElement()
    {
		_logger.LogDebug(">>> Entering FindMajority_NegativeNumbers_ReturnsCorrectElement()");
        var nums = new[] { -1, -1, -1, 2, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_NegativeNumbers_ReturnsCorrectElement(): result → {Result}", result);
        result.Should().Be(-1);
		_logger.LogDebug("<<< Exiting FindMajority_NegativeNumbers_ReturnsCorrectElement()");
    }

    [Test]
    public void FindMajority_ExactlyHalf_ReturnsNull()
    {
		_logger.LogDebug(">>> Entering FindMajority_ExactlyHalf_ReturnsNull()");
        var nums = new[] { 1, 1, 2, 2 };
        var result = BoyerMooreMajorityVote.FindMajority(nums);
		_logger.LogDebug("FindMajority_ExactlyHalf_ReturnsNull(): result → {Result}", result);
        result.Should().BeNull();
		_logger.LogDebug("<<< Exiting FindMajority_ExactlyHalf_ReturnsNull()");
    }
}
