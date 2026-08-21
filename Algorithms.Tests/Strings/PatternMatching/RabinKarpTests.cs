using Algorithms.Strings.PatternMatching;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public class RabinKarpTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("HelloImATestcaseAndIWillPass", "Testcase", new[] { 8 })]
    [TestCase("HelloImATestcaseAndImCaseSensitive", "TestCase", new int[] { })]
    [TestCase("Hello Im a testcase and you can use whitespaces", "testcase", new[] { 11 })]
    [TestCase("Hello Im a testcase and you can use numbers like 1, 2, 3, etcetera", "etcetera", new[] { 58 })]
    [TestCase("HelloImATestcaseAndIHaveTwoOccurrencesOfTestcase", "Testcase", new[] { 8, 40 })]
    public void FindAllOccurrences_IndexCheck(string t, string p, int[] expectedIndices)
    {
		_logger.LogDebug(">>> Entering FindAllOccurrences_IndexCheck(t={T},p={P},expectedIndices={ExpectedIndices})", t, p, expectedIndices);
        List<int> result = RabinKarp.FindAllOccurrences(t, p);
		_logger.LogDebug("<<< Exiting FindAllOccurrences_IndexCheck(t={T},p={P},expectedIndices={ExpectedIndices})", t, p, expectedIndices);
		_logger.LogDebug("FindAllOccurrences_IndexCheck(t={T},p={P},expectedIndices={ExpectedIndices}): result → {Result}", t, p, expectedIndices, result);
        Assert.That(result, Is.EqualTo(new List<int>(expectedIndices)));
    }
}
