using Algorithms.Problems.DynamicProgramming;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.DynamicProgramming;

public class LevenshteinDistanceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("kitten", "sitting", 3)]
    [TestCase("bob", "bond", 2)]
    [TestCase("algorithm", "logarithm", 3)]
    [TestCase("star", "", 4)]
    [TestCase("", "star", 4)]
    [TestCase("abcde", "12345", 5)]
    public void Calculate_ReturnsCorrectLevenshteinDistance(string source, string destination, int expectedDistance)
    {
		_logger.LogDebug(">>> Entering Calculate_ReturnsCorrectLevenshteinDistance(source={Source},destination={Destination},expectedDistance={ExpectedDistance})", source, destination, expectedDistance);
        var result = LevenshteinDistance.Calculate(source, destination);
		_logger.LogDebug("<<< Exiting Calculate_ReturnsCorrectLevenshteinDistance(source={Source},destination={Destination},expectedDistance={ExpectedDistance})", source, destination, expectedDistance);
		_logger.LogDebug("Calculate_ReturnsCorrectLevenshteinDistance(source={Source},destination={Destination},expectedDistance={ExpectedDistance}): result → {Result}", source, destination, expectedDistance, result);
        Assert.That(result, Is.EqualTo(expectedDistance));
    }
}
