using Algorithms.Numeric;
using Microsoft.Extensions.Logging;

namespace Algorithms.Tests.Numeric;

public static class MillerRabinPrimalityTest
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    [TestCase("7", ExpectedResult = true)]  // true
    [TestCase("47", ExpectedResult = true)] // true
    [TestCase("247894109041876714378152933343208766493", ExpectedResult = true)] // true
    [TestCase("247894109041876714378152933343208766493", 1, ExpectedResult = true)] // true
    [TestCase("315757551269487563269454472438030700351", ExpectedResult = true)] // true
    [TestCase("2476099", 12445, ExpectedResult = false)] // false 19^5
    // false 247894109041876714378152933343208766493*315757551269487563269454472438030700351
    [TestCase("78274436845194327170519855212507883195883737501141260366253362532531612139043", ExpectedResult = false)]
    [Retry(3)]
    public static bool MillerRabinPrimalityWork(string testcase, int? seed = null)
    {
		_logger.LogDebug(">>> Entering MillerRabinPrimalityWork(testcase={Testcase},seed={Seed})", testcase, seed);
        // Arrange
        BigInteger number = BigInteger.Parse(testcase);
		_logger.LogDebug("MillerRabinPrimalityWork(testcase={Testcase},seed={Seed}): number → {Number}", testcase, seed, number);

        // Recommended number of checks' rounds = Log2(number) as BigInteger has no Log2 function we need to convert Log10
        BigInteger rounds = (BigInteger)(BigInteger.Log10(number) / BigInteger.Log10(2));
		_logger.LogDebug("MillerRabinPrimalityWork(testcase={Testcase},seed={Seed}): rounds → {Rounds}", testcase, seed, rounds);

        // Act
        var result = MillerRabinPrimalityChecker.IsProbablyPrimeNumber(number, rounds, seed);
		_logger.LogDebug("MillerRabinPrimalityWork(testcase={Testcase},seed={Seed}): result → {Result}", testcase, seed, result);

        // Assert
		_logger.LogDebug("<<< Exiting MillerRabinPrimalityWork(testcase={Testcase},seed={Seed})", testcase, seed);
        return result;
    }

    [TestCase("-2")]
    [TestCase("0")]
    [TestCase("3")]
    // By the algorithm definition the number which is checked should be more than 3
    public static void MillerRabinPrimalityShouldThrowEx(string testcase)
    {
		_logger.LogDebug(">>> Entering MillerRabinPrimalityShouldThrowEx(testcase={Testcase})", testcase);
        // Arrange
        BigInteger number = BigInteger.Parse(testcase);
		_logger.LogDebug("MillerRabinPrimalityShouldThrowEx(testcase={Testcase}): number → {Number}", testcase, number);
        BigInteger rounds = 1;
        // Assert
        Assert.Throws<ArgumentException>(() => MillerRabinPrimalityChecker.IsProbablyPrimeNumber(number, rounds));
		_logger.LogDebug("<<< Exiting MillerRabinPrimalityShouldThrowEx(testcase={Testcase})", testcase);
    }
}
