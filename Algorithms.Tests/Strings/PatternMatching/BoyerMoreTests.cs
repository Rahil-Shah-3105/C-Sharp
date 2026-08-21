using Algorithms.Strings.PatternMatching;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Strings;

public class BoyerMooreTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("HelloImATestcaseAndIWillPass", "Testcase", 8)]
    [TestCase("HelloImATestcaseAndImCaseSensitive", "TestCase", -1)]
    [TestCase("Hello Im a testcase and I work with whitespaces", "testcase", 11)]
    [TestCase("Hello Im a testcase and I work with numbers like 1 2 3 4", "testcase", 11)]
    public void FindFirstOccurrence_IndexCheck(string t, string p, int expectedIndex)
    {
		_logger.LogDebug(">>> Entering FindFirstOccurrence_IndexCheck(t={T},p={P},expectedIndex={ExpectedIndex})", t, p, expectedIndex);
        var resultIndex = BoyerMoore.FindFirstOccurrence(t, p);
		_logger.LogDebug("<<< Exiting FindFirstOccurrence_IndexCheck(t={T},p={P},expectedIndex={ExpectedIndex})", t, p, expectedIndex);
		_logger.LogDebug("FindFirstOccurrence_IndexCheck(t={T},p={P},expectedIndex={ExpectedIndex}): resultIndex → {ResultIndex}", t, p, expectedIndex, resultIndex);
        Assert.That(expectedIndex, Is.EqualTo(resultIndex));
    }
}
