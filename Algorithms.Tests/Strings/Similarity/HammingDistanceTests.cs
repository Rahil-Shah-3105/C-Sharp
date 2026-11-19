using Algorithms.Strings.Similarity;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public class HammingDistanceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("equal", "equal", 0)]
    [TestCase("dog", "dig", 1)]
    [TestCase("12345", "abcde", 5)]
    public void Calculate_ReturnsCorrectHammingDistance(string s1, string s2, int expectedDistance)
    {
		_logger.LogError(">>> Entering Calculate_ReturnsCorrectHammingDistance(s1={S1},s2={S2},expectedDistance={ExpectedDistance})", s1, s2, expectedDistance);
        var result = HammingDistance.Calculate(s1, s2);
		_logger.LogWarning("<<< Exiting Calculate_ReturnsCorrectHammingDistance(s1={S1},s2={S2},expectedDistance={ExpectedDistance})", s1, s2, expectedDistance);
		_logger.LogError("Calculate_ReturnsCorrectHammingDistance(s1={S1},s2={S2},expectedDistance={ExpectedDistance}): result → {Result}", s1, s2, expectedDistance, result);
        Assert.That(result, Is.EqualTo(expectedDistance));
    }

    [Test]
    public void Calculate_ThrowsArgumentExceptionWhenStringLengthsDiffer()
    {
		_logger.LogError(">>> Entering Calculate_ThrowsArgumentExceptionWhenStringLengthsDiffer()");
        Assert.Throws<ArgumentException>(() => HammingDistance.Calculate("123", "12345"));
		_logger.LogWarning("<<< Exiting Calculate_ThrowsArgumentExceptionWhenStringLengthsDiffer()");
    }
}
