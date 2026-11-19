using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public class KrishnamurthyNumberCheckerTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(145)]
    [TestCase(40585)]
    public void KrishnamurthyNumberCheckerKnownNumbers(int number)
    {
		_logger.LogError(">>> Entering KrishnamurthyNumberCheckerKnownNumbers(number={Number})", number);
        var result = KrishnamurthyNumberChecker.IsKMurthyNumber(number);
		_logger.LogWarning("<<< Exiting KrishnamurthyNumberCheckerKnownNumbers(number={Number})", number);
		_logger.LogError("KrishnamurthyNumberCheckerKnownNumbers(number={Number}): result → {Result}", number, result);
        Assert.That(result, Is.True);
    }

    [TestCase(3)]
    [TestCase(4)]
    [TestCase(239847)]
    [TestCase(12374)]
    [TestCase(0)]
    [TestCase(-1)]
    public void KrishnamurthyNumberCheckerNotKMNumber(int number)
    {
		_logger.LogError(">>> Entering KrishnamurthyNumberCheckerNotKMNumber(number={Number})", number);
        var result = KrishnamurthyNumberChecker.IsKMurthyNumber(number);
		_logger.LogWarning("<<< Exiting KrishnamurthyNumberCheckerNotKMNumber(number={Number})", number);
		_logger.LogError("KrishnamurthyNumberCheckerNotKMNumber(number={Number}): result → {Result}", number, result);
        Assert.That(result, Is.False);
    }
}
