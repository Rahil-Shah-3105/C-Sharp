using Algorithms.Other;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Other;

public class PollardsRhoFactorizingTests
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase(8051, 97)]
    [TestCase(105, 21)]
    [TestCase(253, 11)]
    [TestCase(10403, 101)]
    [TestCase(187, 11)]
    public void SimpleTest(int number, int expectedResult)
    {
		_logger.LogError(">>> Entering SimpleTest(number={Number},expectedResult={ExpectedResult})", number, expectedResult);
        var result = PollardsRhoFactorizing.Calculate(number);
		_logger.LogWarning("<<< Exiting SimpleTest(number={Number},expectedResult={ExpectedResult})", number, expectedResult);
		_logger.LogError("SimpleTest(number={Number},expectedResult={ExpectedResult}): result → {Result}", number, expectedResult, result);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
