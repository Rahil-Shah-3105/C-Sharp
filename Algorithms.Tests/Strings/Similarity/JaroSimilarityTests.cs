using Algorithms.Strings.Similarity;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public class JaroSimilarityTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("equal", "equal", 1)]
    [TestCase("abc", "123", 0)]
    [TestCase("FAREMVIEL", "FARMVILLE", 0.88d)]
    [TestCase("CRATE", "TRACE", 0.73d)]
    [TestCase("CRATE11111", "CRTAE11111", 0.96d)]
    [TestCase("a", "a", 1)]
    [TestCase("", "", 1)]
    public void Calculate_ReturnsCorrectJaroSimilarity(string s1, string s2, double expected)
    {
		_logger.LogDebug(">>> Entering Calculate_ReturnsCorrectJaroSimilarity(s1={S1},s2={S2},expected={Expected})", s1, s2, expected);
        JaroSimilarity.Calculate(s1, s2).Should().BeApproximately(expected, 0.01);
		_logger.LogDebug("<<< Exiting Calculate_ReturnsCorrectJaroSimilarity(s1={S1},s2={S2},expected={Expected})", s1, s2, expected);
    }
}
