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
		_logger.LogDebug(">>> Entering KrishnamurthyNumberCheckerKnownNumbers(number={Number})", number);
        var result = KrishnamurthyNumberChecker.IsKMurthyNumber(number);
        Assert.That(result, Is.True);
		_logger.LogDebug("KrishnamurthyNumberCheckerKnownNumbers(number={Number}): result → {Result}", number, result);
		_logger.LogDebug("<<< Exiting KrishnamurthyNumberCheckerKnownNumbers(number={Number})", number);
    }

    [TestCase(3)]
    [TestCase(4)]
    [TestCase(239847)]
    [TestCase(12374)]
    [TestCase(0)]
    [TestCase(-1)]
    public void KrishnamurthyNumberCheckerNotKMNumber(int number)
    {
		_logger.LogDebug(">>> Entering KrishnamurthyNumberCheckerNotKMNumber(number={Number})", number);
        var result = KrishnamurthyNumberChecker.IsKMurthyNumber(number);
        Assert.That(result, Is.False);
		_logger.LogDebug("KrishnamurthyNumberCheckerNotKMNumber(number={Number}): result → {Result}", number, result);
		_logger.LogDebug("<<< Exiting KrishnamurthyNumberCheckerNotKMNumber(number={Number})", number);
    }
}
