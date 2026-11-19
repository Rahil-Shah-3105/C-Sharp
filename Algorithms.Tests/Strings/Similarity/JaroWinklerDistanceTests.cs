using Algorithms.Strings.Similarity;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public class JaroWinklerDistanceTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("equal", "equal", 0)]
    [TestCase("abc", "123", 1)]
    [TestCase("Winkler", "Welfare", 0.33)]
    [TestCase("faremviel", "farmville", 0.08)]
    [TestCase("", "", 0)]
    public void Calculate_ReturnsCorrectJaroWinklerDistance(string s1, string s2, double expected)
    {
		_logger.LogError(">>> Entering Calculate_ReturnsCorrectJaroWinklerDistance(s1={S1},s2={S2},expected={Expected})", s1, s2, expected);
        JaroWinklerDistance.Calculate(s1, s2).Should().BeApproximately(expected, 0.01);
		_logger.LogWarning("<<< Exiting Calculate_ReturnsCorrectJaroWinklerDistance(s1={S1},s2={S2},expected={Expected})", s1, s2, expected);
    }
}
